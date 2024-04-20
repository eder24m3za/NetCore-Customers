using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Data;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Controller/index
        public ActionResult Index()
        {
            var data = _context.Orders.ToList();

            return View(data);
        }

        // GET: Controller/details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(c => c.Customers).Include(e => e.Employees)
                .FirstOrDefaultAsync(m => m.ORDER_ID == id);

            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Controller/Create
        public ActionResult Create()
        {
            ViewData["customer_id"] = new SelectList(_context.Customers, "CUSTOMER_ID", "NAME");
            ViewData["employee_id"] = new SelectList(_context.Employees, "EMPLOYEE_ID", "FIRST_NAME");
            return View();
        }

        // GET: Controller/edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewData["customer_id"] = new SelectList(_context.Customers, "CUSTOMER_ID", "NAME");
            ViewData["employee_id"] = new SelectList(_context.Employees, "EMPLOYEE_ID", "FIRST_NAME");

            return View(order);
        }


        // GET:  Controllers/delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(c => c.Customers).Include(e => e.Employees)
                .FirstOrDefaultAsync(m => m.ORDER_ID == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Orders order, int employee_id, int customer_id)
        {
            if (ModelState.IsValid)
            {
                // Asignar el customer_id al contacto
                order.CUSTOMER_ID = customer_id;
                order.SALESMAN_ID = employee_id;

                // Agregar el contacto al contexto y guardarlo
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                // Mensaje de éxito y redireccionamiento a la acción Index
                TempData["mensaje"] = "El contacto se guardó correctamente";
                return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, regresar a la vista Create con el modelo contact
            return View(order);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Orders order)
        {
            if (id != order.ORDER_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(order.ORDER_ID))
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
            return View(order);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var reg = _context.Orders.Find(id);
            if (reg == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _context.Orders.Remove(reg);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.ORDER_ID == id);
        }
    }
}
