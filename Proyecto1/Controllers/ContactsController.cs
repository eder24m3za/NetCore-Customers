using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Data;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CountriesController
        public ActionResult Index()
        {
            var contactsWithCustomers = _context.Contacts.Include(c => c.Customers).ToList();
            return View(contactsWithCustomers);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            // Obtener la lista de clientes
            ViewData["customer_id"] = new SelectList(_context.Customers, "CUSTOMER_ID", "NAME");

            return View(contact);
        }



        public ActionResult Index2()
        {
            var datos = _context.Contacts.Include(c => c.Customers).Where(c => c.Customers != null).ToList();
            return View(datos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .Include(c => c.Customers)
                .FirstOrDefaultAsync(m => m.CONTACT_ID == id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }


        // GET: CountriesController/Create
        public ActionResult Create()
        {
            ViewData["customer_id"] = new SelectList(_context.Customers, "CUSTOMER_ID", "NAME");
            return View();
        }

        // POST: CountriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contacts contact, int customer_id, string id)
        {
            if (ModelState.IsValid)
            {
                    // Asignar el customer_id al contacto
                    contact.CUSTOMER_ID = customer_id;

                    // Agregar el contacto al contexto y guardarlo
                    await _context.Contacts.AddAsync(contact);
                    await _context.SaveChangesAsync();

                    // Mensaje de éxito y redireccionamiento a la acción Index
                    TempData["mensaje"] = "El contacto se guardó correctamente";
                    return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, regresar a la vista Create con el modelo contact
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CONTACT_ID,FIRST_NAME,LAST_NAME,EMAIL,PHONE,CUSTOMER_ID")] Contacts contacts)
        {
            if (id != contacts.CONTACT_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactsExists(contacts.CONTACT_ID))
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
            return View(contacts);
        }



        [HttpDelete]
        public ActionResult Delete(string id)
        {
            var reg = _context.Contacts.Find(id);
            if (reg == null)
            {
                return Json(new { success = false, message = "Algo salió mal... inténtalo de nuevo." });
            }
            else
            {

                _context.Contacts.Remove(reg);
                _context.SaveChanges();
                return Json(new { success = true, message = "Pais eliminada exitosamente." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDatos()
        {
            var todos = await _context.Contacts.Include(c => c.Customers).ToListAsync();
            return Json(new { data = todos });
        }

        private bool ContactsExists(int id)
        {
            return _context.Contacts.Any(e => e.CONTACT_ID == id);
        }
    }
}
