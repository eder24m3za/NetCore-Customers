using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Proyecto1.Models
{
    [Table("inventories")]
    public class Inventario
    {
        [Key]
        [JsonPropertyName("product_id")]
        [Column("product_id")]
        [ForeignKey("product")]
        public int product_id { get; set; }

        [JsonPropertyName("quantity")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Column("quantity")]
        public int quantity { get; set; }

        [JsonPropertyName("warehouse_id")]
        [Column("warehouse_id")]
        [ForeignKey("warehouse")]
        public int warehouse_id { get; set; }

        public Productos? product { get; set; }

        public Almacen? warehouse { get; set; }

    }
}
