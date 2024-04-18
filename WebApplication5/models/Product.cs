using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication5.models
{
    public class Product
    {
        [Key]
        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public virtual ProductDetails? ProductDetails { get; set; }

    }
}
