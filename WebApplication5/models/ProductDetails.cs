using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.models
{
    public class ProductDetails
    {
        [Key]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public int? ProductPrice { get; set; }
        public ICollection<Product> Product { get; set; }

    }
}
