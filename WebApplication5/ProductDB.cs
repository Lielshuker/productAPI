using Microsoft.EntityFrameworkCore;
using WebApplication5.models;

namespace WebApplication5
{
    public class ProductDB : DbContext
    {
        public ProductDB(DbContextOptions<ProductDB> options)
        : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductDetails> ProductsDetails => Set<ProductDetails>();

    }
}
