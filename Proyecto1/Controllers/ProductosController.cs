using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Proyecto1.Data;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.Include(p => p.product_categories).ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categorias = await  _context.CategoriasProductos.ToListAsync();
            return View();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Productos.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            ViewBag.Categorias =  _context.CategoriasProductos.ToList();
            ViewBag.cat = customers.category_id;
            return View(customers);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Productos.Include(p => p.product_categories)
                .FirstOrDefaultAsync(m => m.category_id == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }




        // POST create return View
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("product_id,product_name,description,standard_cost,list_price,category_id,product_categories")] Productos customers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(customers);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("list_price", "Error al guardar el precio de la lista.");
                    Console.WriteLine(ex.Message); // Registrar el mensaje de error para diagnóstico
                }
            }
            ViewBag.Categorias = await _context.CategoriasProductos.ToListAsync();
            return View(customers);
        }


        // POST Update return View

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("product_id,product_name,description,standard_cost,list_price,category_id,product_categories")] Productos customers)
        {
            if (id != customers.product_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(customers.product_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = await _context.CategoriasProductos.ToListAsync();
            return View(customers);
        }

        // POST delete Categoria return View
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Productos.Include(p => p.product_categories)
                .FirstOrDefaultAsync(m => m.product_id == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customers = await _context.Productos.FindAsync(id);
            if (customers != null)
            {
                _context.Productos.Remove(customers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CategoryExists(int id)
        {
            return _context.Productos.Any(e => e.product_id == id);
        }
    }
}
