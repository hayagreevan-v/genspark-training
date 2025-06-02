using BankingChatbot.Contexts;
using BankingChatbot.EnvReader;
using BankingChatbot.Repositories;
using BankingChatbot.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    opts.JsonSerializerOptions.WriteIndented = true;
                }
                );

EnvReader.Read();
builder.Services.AddTransient<GenAIContext, GenAIContext>();
builder.Services.AddTransient<ChatRepository, ChatRepository>();
builder.Services.AddTransient<UserRepository, UserRepository>();
builder.Services.AddTransient<ChatService, ChatService>();
builder.Services.AddTransient<UserService, UserService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();


app.Run();

