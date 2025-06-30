using App.Api.exception;
using App.Api.Job;
using App.Api.Mapper;
using App.Api.repository;
using App.Api.repository.impl;
using App.Api.Service;
using App.Api.Service.impl;
using App.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApiDocument();

// Configure the HTTP request pipeline.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
// Add Swagger services
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHostedService<ProductCleanupBackgroundService>();

var app = builder.Build();

// Enable Swagger middleware
app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();
app.MapOpenApi();
app.UseOpenApi();
app.UseSwaggerUi();

app.Run();