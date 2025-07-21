using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BlobAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddSingleton<BlobStorageService>();

// builder.Configuration.AddAzureKeyVault(new Uri(builder.Configuration["AzureBlob:KeyVaultUrl"]!), new DefaultAzureCredential());

Console.WriteLine(builder.Configuration["Test"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
