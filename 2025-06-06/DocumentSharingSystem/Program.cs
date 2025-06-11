using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DocumentSharingSystem.Contexts;
using DocumentSharingSystem.Interfaces;
using DocumentSharingSystem.Misc;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Repositories;
using DocumentSharingSystem.Services;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DocumentSharingSystem.Authorizations;
using DocumentSharingSystem.Models.DTOs;
using System.Threading.RateLimiting;

Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File("./log.txt", outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {UserEmail}{NewLine}{Exception}")
                .WriteTo.Console(outputTemplate : "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {UserEmail}{NewLine}{Exception}")
                .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(
    options =>
    {
        options.ListenLocalhost(7120, listenOptions =>
        {
            listenOptions.UseHttps(); // Enable HTTPS 
        });
    });
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Document Sharing System App", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Enter Token ",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers(options =>
                    {
                        options.Filters.Add<CustomExceptionFilter>();
                    })
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                        options.JsonSerializerOptions.WriteIndented = true;
                    });

builder.Services.AddDbContext<DocumentSharingSystemContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

#region Rate Limiter
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        {
        var path = httpContext.Request.Path.ToString();

        if (path.StartsWith("/notification"))
        {
            return RateLimitPartition.GetNoLimiter("signalr");
        }

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 1000,
                QueueLimit = 0,
                Window = TimeSpan.FromMinutes(60)
            });
    });

});
#endregion

#region Misc
builder.Services.AddScoped<ExceptionFilterAttribute, CustomExceptionFilter>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<PaginationContextFns>();
#endregion

#region Repositories
builder.Services.AddTransient<IRepo<Guid, User>, UserRepo>();
builder.Services.AddTransient<IRepo<Guid, Document>, DocumentRepo>();
builder.Services.AddTransient<IRepo<Guid, RefreshToken>, RefreshTokenRepo>();
#endregion

#region Services
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<DocumentService>();
builder.Services.AddTransient<TokenService>();
builder.Services.AddTransient<AuthenticationService>();
builder.Services.AddTransient<CustomResponseGeneration>();
builder.Services.AddTransient<RefreshTokenService>();
#endregion

#region Middleware & Authorization
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddleware>();
builder.Services.AddTransient<IAuthorizationHandler, SpecificOwnerAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, SpecificUserAuthorizationHandler>();
#endregion


#region Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Keys:JwtTokenKey"]!))
                        };
                    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SpecifiedUserOrAdmin", policy => policy.Requirements.Add(new SpecificUserAuthorizationRequirement()));
    options.AddPolicy("SpecifiedOwnerOrAdmin", policy => policy.Requirements.Add(new SpecificOwnerAuthorizationRequirement()));
});
#endregion 

#region  Mappers
builder.Services.AddAutoMapper(typeof(User));
builder.Services.AddAutoMapper(typeof(UserAddRequestDTO));
builder.Services.AddAutoMapper(typeof(UserAddServiceDTO));
builder.Services.AddAutoMapper(typeof(UserResponseDTO));
builder.Services.AddAutoMapper(typeof(UserUpdateRequestDTO));
builder.Services.AddAutoMapper(typeof(Document));
builder.Services.AddAutoMapper(typeof(DocumentReponseDTO));
#endregion

builder.Services.AddSignalR(options =>
{
    options.KeepAliveInterval = TimeSpan.FromSeconds(12);
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(15);
});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500","http://localhost:5175","https://localhost:7120")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        // Console.WriteLine("USer : " +httpContext.User.FindFirstValue(ClaimTypes.Email));
        diagnosticContext.Set("UserEmail", httpContext.User.FindFirstValue(ClaimTypes.Email) ?? "UnIdentifiedUser");
    };
});

app.MapHub<NotificationHub>("/notification");
app.MapControllers();

app.Run();
