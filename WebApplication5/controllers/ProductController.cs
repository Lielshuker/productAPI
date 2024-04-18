using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.controllers;
using WebApplication5.models;


namespace WebApplication5.controllers
{
    public class ProductController
    {
        public static async Task<IResult> GetAllProducts(ProductDB db, ProductDB dbPetails)
        {
            var products = await db.Products.Include(h => h.ProductDetails).ToArrayAsync();
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
            return  db.Products.Where(h => h.ProductId == id).
                    Include(i => i.ProductDetails).FirstOrDefault();
        }
            

        public static async Task<IResult> CreateProduct(ProductDB db, ProductDB dbDetails, Product product)
        {
            try
            {
                if (product.ProductId == 0 || product.ProductName == null | product.ProductDetails.ProductPrice == null)
                {
                    return TypedResults.BadRequest();
                }
                var productById = CheckByID(product.ProductId, db);
                if (productById == null)
                {
                    db.ProductsDetails.Add(product.ProductDetails);
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                    return TypedResults.Ok(product);
                }
                else
                {
                    return TypedResults.BadRequest();
                }

            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex);
            }
        }
    }
}
