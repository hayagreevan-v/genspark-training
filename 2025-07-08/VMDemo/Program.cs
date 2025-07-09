using Microsoft.EntityFrameworkCore;
using VMDemo.Contexts;
using VMDemo.Repositories;
using VMDemo.Services;
using AppContext = VMDemo.Contexts.AppContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddDbContext<AppContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddTransient<UserRepo>();
builder.Services.AddTransient<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

Console.WriteLine($"Connection: {builder.Configuration.GetConnectionString("Default")}");



app.Run();
