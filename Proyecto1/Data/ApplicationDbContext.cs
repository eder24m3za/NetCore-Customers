using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Proyecto1.Models;

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
        public DbSet<Regions> Regions { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Locations> Locations { get; set; }
                }

    }
}
