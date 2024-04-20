using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Drawing;

namespace Proyecto1.Models
{
    public class Orders
    {
        [Key]
        [JsonPropertyName("order_id")]
        public int ORDER_ID { get; set; }

        [JsonPropertyName("status")]
        [Required]
        public string STATUS { get; set; }

        [JsonPropertyName("order_date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ORDER_DATE { get; set; }


        [ForeignKey("Customers")]
        [JsonPropertyName("customer_id")]
        [Required]
        public int CUSTOMER_ID { get; set; }

        public Customers? Customers { get; set; }

        [ForeignKey("Employees")]
        [JsonPropertyName("salesman_id")]
        public int? SALESMAN_ID { get; set; }

        public Employees? Employees { get; set; }
    }
}
