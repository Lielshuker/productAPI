using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WebApplication5;
using WebApplication5.models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDB>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
var app = builder.Build();

var productApi = app.MapGroup("/products");

productApi.MapGet("/", GetAllProducts);
productApi.MapGet("/{id}", GetProductByID);
productApi.MapPost("/", CreateProduct);


static async Task<IResult> GetAllProducts(ProductDB db, ProductDB dbPetails)
{
    var products = await db.Products.Include(product=>product.ProductId).ToArrayAsync();
    //var products_details = await dbPetails.ProductsDetails.ToArrayAsync();
    return TypedResults.Ok();
}

/*    return await db.Products.FindAsync(id.) 
        is Product product
            ? TypedResults.Ok(product)
            : TypedResults.NotFound();*/

static async Task<IResult> GetProductByID(int id, ProductDB db)
{
    var productByID =  db.Products.Where(h => h.ProductId == id)
        .Include(i => i.ProductDetails).FirstOrDefault();
    return TypedResults.Ok(productByID);
    
}


static async Task<IResult> CreateProduct(ProductDB db, Product product)
{

    var newProductDetails = new ProductDetails
    {
        ProductPrice = 2
    };
    db.ProductsDetails.Add(newProductDetails);
    db.SaveChanges();




    //p.ProductDetails.Add(p);

    //var newProduct = db.ProductsDetails.Add(new Product());
    //var newProductDetails = db.ProductsDetails.Add(new ProductDetails()).Entity;
    // await db.SaveChangesAsync();

    //return TypedResults.Created($"/todoitems/{newProduct.ProductId}", newProduct);
    return TypedResults.Ok();

}

app.Run();








