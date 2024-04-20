using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Data;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class WarehousesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarehousesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WarehousesController/index
        public ActionResult Index()
        {
            var warehousesWithLocation = _context.Warehouses.Include(c => c.Locations).ToList();
            return View(warehousesWithLocation);
        }

        // GET: WarehousesController/edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouses = await _context.Warehouses.FindAsync(id);
            if (warehouses == null)
            {
                return NotFound();
            }

            // Obtener la lista de clientes
            ViewData["location_id"] = new SelectList(_context.Locations, "lOCATION_ID", "lOCATION_NAME");

            return View(warehouses);
        }


        // GET: WarehousesController/Create
        public ActionResult Create()
        {
            ViewData["location_id"] = new SelectList(_context.Locations, "lOCATION_ID", "lOCATION_NAME");
            return View();
        }

        // GET:  WarehousesController/delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            Console.WriteLine("Mal");
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouses
                .Include(c => c.Locations)
                .FirstOrDefaultAsync(m => m.WAREHOUSE_ID == id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }


        // POST: WarehousesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Warehouses warehouse, int location_id, string id)
        {
            if (ModelState.IsValid)
            {
                // Asignar el location_id al Warehouses
                warehouse.LOCATION_ID = location_id;

                // Agregar el Warehouses al contexto y guardarlo
                await _context.Warehouses.AddAsync(warehouse);
                    await _context.SaveChangesAsync();

                    // Mensaje de éxito y redireccionamiento a la acción Index
                    TempData["mensaje"] = "El almacen se guardó correctamente";
                    return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, regresar a la vista Create con el modelo warehouse
            return View(warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WAREHOUSE_ID,WAREHOUSE_NAME,LOCATION_ID")] Warehouses Warehouses)
        {
            if (id != Warehouses.WAREHOUSE_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Warehouses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehousesExists(Warehouses.WAREHOUSE_ID))
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
            return View(Warehouses);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var reg = _context.Warehouses.Find(id);
            if (reg == null)
            {
                return Json(new { success = false, message = "Algo salió mal... inténtalo de nuevo." });
            }
            else
            {
                _context.Warehouses.Remove(reg);
                _context.SaveChanges();
                return Json(new { success = true, message = "Almacen eliminado exitosamente." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDatos()
        {
            var todos = await _context.Warehouses.Include(c => c.Locations).ToListAsync();
            return Json(new { data = todos });
        }

        private bool WarehousesExists(int id)
        {
            return _context.Warehouses.Any(e => e.WAREHOUSE_ID == id);
        }
    }
}
