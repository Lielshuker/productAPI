using Microsoft.EntityFrameworkCore;
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
    var products = await db.Products.Include(h => h.ProductDetails).ToArrayAsync();
    return TypedResults.Ok(products);
}

static async Task<IResult> GetProductByID(int id, ProductDB db)
{
    var productByID = await db.Products.Where(h => h.ProductId == id)
        .Include(i => i.ProductDetails).FirstOrDefaultAsync();
    return productByID is Product product
        ? TypedResults.Ok(productByID) 
        : TypedResults.NotFound();
    
}

static async Task<IResult> CreateProduct(ProductDB db,ProductDB dbDetails, Product product)
{
    try
    {
        if (product.ProductId == null || product.ProductName == null | product.ProductDetails.ProductId == null)
        {
            return TypedResults.BadRequest();
        } 
        db.ProductsDetails.Add(product.ProductDetails);
        db.Products.Add(product);
        await db.SaveChangesAsync();
        return TypedResults.Ok(product);
        
    } catch (Exception ex)
    {
        return TypedResults.NotFound(ex);
    }
}

app.Run();








