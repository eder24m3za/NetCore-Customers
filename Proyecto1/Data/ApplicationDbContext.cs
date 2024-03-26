using Microsoft.EntityFrameworkCore;
using WebIbero.Models;

namespace Proyecto1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Product_Categories> Product_categories { get; set; }
        public DbSet<Products> Products { get; set; }

        [DbFunction(Schema = "dbo")]
        public static int fn_PorductCategory_count(int pCategoryId)
        {
            throw new Exception();
        }
    }
}
