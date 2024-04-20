using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Proyecto1.Models
{
    public class Employees
    {
        [Key]
        [JsonPropertyName("employee_id")]
        public int EMPLOYEE_ID { get; set; }

        [JsonPropertyName("first_name")]
        [Required]
        public string FIRST_NAME { get; set; }

        [JsonPropertyName("last_name")]
        [Required]
        public string LAST_NAME { get; set; }

        [JsonPropertyName("email")]
        [Required]
        public string EMAIL { get; set; }

        [JsonPropertyName("phone")]
        [Required]
        public string PHONE { get; set; }

        [JsonPropertyName("hire_date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HIRE_DATE { get; set; }

        [JsonPropertyName("job_title")]
        [Required]
        public string JOB_TITLE { get; set; }
    }
}
