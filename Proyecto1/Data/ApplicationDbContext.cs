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
        //public DbSet<Products> Products { get; set; }

        public DbSet<Regions> Regions { get; set; }

        public DbSet<Countries> Countries { get; set; }

        public DbSet<Contacts> Contacts { get; set; }

        public DbSet<Productos> Productos { get; set; }

        public DbSet<CategoriaProducto> CategoriasProductos { get;set; }

        public DbSet<Inventario> Inventario { get; set; }

        public DbSet<Almacen> Almacen { get; set; }

        [DbFunction(Schema = "dbo")]
        public static int fn_PorductCategory_count(int pCategoryId)
        {
            throw new Exception();
        }

    }
}
