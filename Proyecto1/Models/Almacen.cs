using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Proyecto1.Models
{
    [Table("warehouses")]
    public class Almacen
    {
            [Key]
            [JsonPropertyName("warehouse_id")]
            public int warehouse_id { get; set; }


            [JsonPropertyName("warehouse_name")]
            public string warehouse_name { get; set; }

        //public List<Inventario>? inventori { get; }
    }
}
