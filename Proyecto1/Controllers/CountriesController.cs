using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Data;
using Proyecto1.Models;
using System.Diagnostics.Metrics;

namespace Proyecto1.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Controller/index
        public ActionResult Index()
        {
            var data = _context.Countries.ToList();

            return View(data);
        }

        // GET: Controller/details
        public async Task<IActionResult> Details(string? id)
        {
            if (id == "")
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(c => c.Regions)
                .FirstOrDefaultAsync(m => m.COUNTRY_ID == id);

            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Controller/Create
        public ActionResult Create()
        {
            ViewData["region_id"] = new SelectList(_context.Regions, "REGION_ID", "REGION_NAME");
            return View();
        }

        // GET: Controller/edit
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == "")
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            ViewData["region_id"] = new SelectList(_context.Regions, "REGION_ID", "REGION_NAME");
            return View(country);
        }


        // GET:  Controllers/delete
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == "")
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(c => c.Regions)
                .FirstOrDefaultAsync(m => m.COUNTRY_ID == id);

            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Countries country, int region_id)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el ID del país ya existe
                if (_context.Countries.Any(c => c.COUNTRY_ID == country.COUNTRY_ID))
                {
                    ModelState.AddModelError("COUNTRY_ID", "El ID del país ya está en uso.");
                    return View(country);
                }

                // Asignar el region_id al país
                country.REGION_ID = region_id;

                // Agregar el país al contexto y guardarlo
                await _context.Countries.AddAsync(country);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Countries country)
        {
            if (id != country.COUNTRY_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.COUNTRY_ID))
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
            return View(country);
        }

        [HttpPost]
        public ActionResult Deletes(string id)
        {
            Console.WriteLine("uwu");
            Console.WriteLine(id);
            string idString = id.ToString();
            Console.WriteLine(idString);
            var reg = _context.Countries.Find(idString);
            if (reg == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _context.Countries.Remove(reg);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        private bool CountryExists(string id)
        {
            return _context.Countries.Any(e => e.COUNTRY_ID == id);
        }
    }
}
