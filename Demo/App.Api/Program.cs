using System.Text;
using App.Api.common;
using App.Api.exception;
using App.Api.Job;
using App.Api.repository;
using App.Api.repository.impl;
using App.Api.Service;
using App.Api.Service.impl;
using App.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag; // Add this
using NSwag.Generation.Processors.Security; // Add this

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Add OpenApiDocument with JWT Bearer config
builder.Services.AddOpenApiDocument(config =>
{
    config.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Type into the textbox: Bearer {your JWT token}."
    });

    config.OperationProcessors.Add(
        new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

// Configure the HTTP request pipeline.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Bind JwtSettings from configuration
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

// Add JwtSettings to DI container
builder.Services.Configure<JwtSettings>(jwtSettingsSection);
builder.Services.AddSingleton(jwtSettings);

var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });


builder.Services.AddControllers();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register AuthService
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Add Swagger services with JWT support
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHostedService<ProductCleanupBackgroundService>();

var app = builder.Build();

// Enable Swagger middleware
app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();
app.MapOpenApi();
app.UseOpenApi();
app.UseSwaggerUi();
app.UseAuthentication();
app.UseAuthorization();

app.Run();