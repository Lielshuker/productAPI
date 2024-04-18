using Microsoft.EntityFrameworkCore;
using WebApplication5;
using WebApplication5.controllers;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDB>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
var app = builder.Build();
app.MapProductEndpoints();
app.Run();


public static class ProductEndpointsExt
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var productApi = app.MapGroup("/products");
        productApi.MapGet("/", ProductController.GetAllProducts);
        productApi.MapGet("/{id}", ProductController.GetProductByID);
        productApi.MapPost("/", ProductController.CreateProduct);
    }
}







