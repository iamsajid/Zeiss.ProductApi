using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductApi.Common.Behaviours;
using ProductApi.Common.Interfaces;
using ProductApi.Common.Middleware;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Redis connection and ID generator
var redis = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
builder.Services.AddSingleton<IProductIdGenerator, RedisProductIdGenerator>();

// Add services to the container.
builder.Services.AddScoped<ProductRepository>();
// Register RedisCachedProductRepository as the decorator for IProductRepository
builder.Services.AddScoped<IProductRepository>(sp =>
    new RedisCachedProductRepository(
        sp.GetRequiredService<ProductRepository>(),
        sp.GetRequiredService<IConnectionMultiplexer>(),
        sp.GetRequiredService<IConfiguration>(),
        sp.GetRequiredService<ILogger<RedisCachedProductRepository>>()
    )
);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.SwaggerDoc("ProductApi", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Zeiss APIs", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/ProductApi/swagger.json", "Product API v1");
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
