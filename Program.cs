using Microsoft.EntityFrameworkCore;
using ShopifyAPI.Interfaces;
using ShopifyAPI.Models;
using ShopifyAPI.Services;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IWebhookAuthenticationService, WebhookAuthenticationService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ClothingStoreContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers()
        .AddNewtonsoftJson(options => {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            // Add any additional Newtonsoft.Json settings here
        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


