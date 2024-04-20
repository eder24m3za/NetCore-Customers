using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Proyecto1.Models
{
    public class Countries
    {
        [Key]
        [JsonPropertyName("country_id")]
        public string COUNTRY_ID { get; set; }

        [JsonPropertyName("country_name")]
        public string? COUNTRY_NAME { get; set; }

        [ForeignKey("Regions")]
        [JsonPropertyName("region_id")]
        public int? REGION_ID { get; set; }
        public Regions? Regions { get; set; }
    }
}
