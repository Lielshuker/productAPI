using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.controllers;
using WebApplication5.models;


namespace WebApplication5.controllers
{
    public class ProductController
    {
        public static async Task<IResult> GetAllProducts(ProductDB db)
        {
            var products = await db.Products.Include(p => p.ProductDetails).ToArrayAsync();
            return TypedResults.Ok(products);
        }

        public static async Task<IResult> GetProductByID(int id, ProductDB db)
        {
            var productByID = CheckByID(id, db);
            return productByID is Product product
                ? TypedResults.Ok(productByID)
                : TypedResults.NotFound();

        }

        public static Product? CheckByID(int id, ProductDB db)
        {
            return  db.Products.Where(p => p.ProductId == id).
                    Include(d => d.ProductDetails).FirstOrDefault();
        }
            

        public static async Task<IResult> CreateProduct(ProductDB dbProduct, Product product)
        {
            try
            {
                if (product.ProductId <= 0 || product.ProductName == null | product.ProductDetails.ProductPrice == null)
                {
                    return TypedResults.BadRequest();
                }
                var productById = CheckByID(product.ProductId, dbProduct);
                if (productById == null)
                {
                    dbProduct.ProductsDetails.Add(product.ProductDetails);
                    dbProduct.Products.Add(product);
                    await dbProduct.SaveChangesAsync();
                    return TypedResults.Ok(product);
                }
                else
                {
                    return TypedResults.Conflict();
                }

            }
            catch (Exception)
            {
                return TypedResults.StatusCode(500);
            }
        }
    }
}
