using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Data;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class CategoriaProductosController : Controller
    {
        private readonly ApplicationDbContext _context;


        public CategoriaProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get Return View()
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriasProductos.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.CategoriasProductos
                .FirstOrDefaultAsync(m => m.category_id == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.CategoriasProductos.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            return View(customers);
        }


        // POST create return View
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("category_id,category_name")] CategoriaProducto customers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customers);
        }


        // POST Update return View

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("category_id,category_name")] CategoriaProducto customers)
        {
            if (id != customers.category_id)
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
                    if (!CategoryExists(customers.category_id))
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
            return View(customers);
        }

        // POST delete Categoria return View
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.CategoriasProductos
                .FirstOrDefaultAsync(m => m.category_id == id);
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
            var customers = await _context.CategoriasProductos.FindAsync(id);
            if (customers != null)
            {
                _context.CategoriasProductos.Remove(customers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CategoryExists(int id)
        {
            return _context.CategoriasProductos.Any(e => e.category_id == id);
        }
    }
}
