using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Data;
using Proyecto1.Models;
using System.Text.Json;

namespace Proyecto1.Controllers
{
    public class InventarioController : Controller
    {
        private readonly ApplicationDbContext _context;


        public InventarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get Return View()
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inventario.Include(p => p.warehouse).Include(p => p.product).ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Productos = await _context.Productos.ToListAsync();
            ViewBag.Almacenes = await _context.Almacen.ToListAsync();
            return View();
        }
        public async Task<IActionResult> Edit(int? id, int? id2)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers =  await _context.Inventario.FirstOrDefaultAsync(i => i.product_id == id2 && id == i.warehouse_id );
            if (customers == null)
            {
                return NotFound();
            }
            ViewBag.Productos = await _context.Productos.ToListAsync();
            ViewBag.Almacenes = await _context.Almacen.ToListAsync(); ;
            ViewBag.prod = customers.product_id;
            ViewBag.alm = customers.warehouse_id;
            return View(customers);
        }
        public async Task<IActionResult> Details(int? id, int? id2)
        {
            if (id == null && id2 == null)
            {
                return NotFound();
            }

            var customers = await _context.Inventario.Include(p => p.warehouse).Include(p => p.product)
                .FirstOrDefaultAsync(m => m.warehouse_id == id && m.product_id == id2);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }




        // POST create return View
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Inventario>> Create(Inventario customers)
        {
            var inventory = await _context.Inventario.FirstOrDefaultAsync(i => i.product_id == customers.product_id);

            if (inventory != null)
            {
                ModelState.AddModelError(string.Empty, "El registro ya existe en la base de datos.");
                ViewBag.Productos = await _context.Productos.ToListAsync();
                ViewBag.Almacenes = await _context.Almacen.ToListAsync();
                return View(customers); // Devolver la vista con el error
            }
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
            ViewBag.Productos = await _context.Productos.ToListAsync();
            ViewBag.Almacenes = await _context.Almacen.ToListAsync();
            return View(customers);
        }


        // POST Update return View

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Inventario>> Edit(int id, int id2 ,Inventario customers)
        {
            if (id != customers.warehouse_id && id2 == customers.product_id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(customers);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CategoryExists(customers.warehouse_id, customers.product_id))
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
            }catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error de registro");
                ViewBag.Productos = await _context.Productos.ToListAsync();
                ViewBag.Almacenes = await _context.Almacen.ToListAsync();
                return View(customers);
            }
            ViewBag.Productos = await _context.Productos.ToListAsync();
            ViewBag.Almacenes = await _context.Almacen.ToListAsync();
            return View(customers);
        }

        // POST delete Categoria return View
        public async Task<IActionResult> Delete(int? id, int? id2)
        {
            if (id == null && id2 == null)
            {
                return NotFound();
            }

            var customers = await _context.Inventario.Include(p => p.warehouse).Include(p => p.product)
                .FirstOrDefaultAsync(m => m.warehouse_id == id && m.product_id == id2);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int id2)
        {
            var producto = await _context.Inventario
            .Where(p => p.product_id == id2 && p.warehouse_id == id)
            .SingleOrDefaultAsync();

                if (producto != null)
                {
                    _context.Inventario.Remove(producto); 
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
        }
        private bool CategoryExists(int id, int id2)
        {
            return _context.Inventario.Any(e => e.warehouse_id == id && e.product_id == id2);
        }
    }
}
