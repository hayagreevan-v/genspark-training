using ChienVHShopOnline.Contexts;
using ChienVHShopOnline.Models;
using ChienVHShopOnline.Repositories;
using ChienVHShopOnline.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    }
);

builder.Services.AddDbContext<ChienVHShopDBEntities>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
    }
);

builder.Services.AddTransient<CategoryRepo>();
builder.Services.AddTransient<ColorRepo>();
builder.Services.AddTransient<ContactURepo>();
builder.Services.AddTransient<ModelRepo>();
builder.Services.AddTransient<NewsRepo>();
builder.Services.AddTransient<OrderDetailRepo>();
builder.Services.AddTransient<OrderRepo>();
builder.Services.AddTransient<ProductRepo>();
builder.Services.AddTransient<UserRepo>();

builder.Services.AddTransient<CategoryService>();
builder.Services.AddTransient<ColorService>();
builder.Services.AddTransient<ContactUsService>();
builder.Services.AddTransient<NewsService>();
builder.Services.AddTransient<OrderService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<ShoppingCartService>();

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

