using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Proyecto1.Models
{
    public class Product_Categories
    {
        [Key]
        [JsonPropertyName("category_id")]
        public int CATEGORY_ID { get; set; }

        [JsonPropertyName("category_name")]
        [Required]
        public string CATEGORY_NAME { get; set; }
    }
}
