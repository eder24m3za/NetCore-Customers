using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Proyecto1.Models;

namespace Proyecto1.Models
{
    [Table("product_categories")]
    public class CategoriaProducto
    {
        [Key]
        [JsonPropertyName("category_id")]
        public int category_id { get; set; }


        [JsonPropertyName("category_name")]
        public string category_name { get; set; }

        // Propiedad de navegación para la relación con los productos
        public List<Productos>? products { get; }
    }
}
