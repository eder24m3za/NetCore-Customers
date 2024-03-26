using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebIbero.Models
{
    public class Products
    {
        [Key]
        public int PRODUCT_ID { get; set; }
        public string? PRODUCT_NAME { get; set; }
        public string? DESCRIPTION { get; set; }
        public decimal STANDARD_COST { get; set; }
        public decimal LIST_PRICE { get; set; }
        [ForeignKey("Product_Categories")]
        public int CATEGORY_ID { get; set; }

        //public Product_Categories? Product_Categories { get; set; }

    }
}
