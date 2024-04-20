using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Data;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class LocationsController: Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Controller/index
        public ActionResult Index()
        {
            var data = _context.Locations.ToList();

            return View(data);
        }


        // GET: Controller/details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Include(c => c.Countries)
                .FirstOrDefaultAsync(m => m.LOCATION_ID == id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Controller/Create
        public ActionResult Create()
        {
            ViewData["country_id"] = new SelectList(_context.Countries, "COUNTRY_ID", "COUNTRY_NAME");
            return View();
        }

        // GET: Controller/edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            ViewData["country_id"] = new SelectList(_context.Countries, "COUNTRY_ID", "COUNTRY_NAME");

            return View(location);
        }

        // GET:  Controllers/delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Include(c => c.Countries)
                .FirstOrDefaultAsync(m => m.LOCATION_ID == id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }




        // POST: Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Locations location, string country_id)
        {
            if (ModelState.IsValid)
            {

                location.COUNTRY_ID = country_id;
                

               
                await _context.Locations.AddAsync(location);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            
            return View(location);
        }

        //POST: Controller/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Locations location)
        {
            if (id != location.LOCATION_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationsExists(location.LOCATION_ID))
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
            return View(location);
        }


        //POST: Controller/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var reg = _context.Locations.Find(id);
            if (reg == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _context.Locations.Remove(reg);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        private bool LocationsExists(int id)
        {
            return _context.Locations.Any(e => e.LOCATION_ID == id);
        }
    }
}
