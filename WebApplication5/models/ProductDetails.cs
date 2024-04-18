using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication5.models
{
    public class ProductDetails
    {
        [Key]
        [JsonIgnore]
        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }
        public int? ProductPrice { get; set; }

    }
}
