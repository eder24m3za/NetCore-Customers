using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Proyecto1.Models;

namespace Proyecto1.Models
{
    [Table("products")]
    public class Productos
    {
        [Key]
        [JsonPropertyName("product_id")]
        public int product_id { get; set; }

        [JsonPropertyName("product_name")]
        public string product_name { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("standard_cost")]
        public decimal standard_cost { get; set; }

        [JsonPropertyName("list_price")]
        public decimal list_price { get; set; }

        [JsonPropertyName("category_id")]
        [ForeignKey("product_categories")]
        public int category_id { get; set; } // Foreign Key

        // Propiedad de navegación para la relación con la categoría
        public CategoriaProducto? product_categories { get; set; }

        //public Inventario? Inventarios { get; set; }
    }
}
