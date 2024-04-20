using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Data;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class ProductsCategories : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsCategories(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var data = _context.Product_categories.ToList();

            return View(data);
        }

        // GET: Controller/details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategories = await _context.Product_categories
                .FirstOrDefaultAsync(m => m.CATEGORY_ID == id);

            if (productCategories == null)
            {
                return NotFound();
            }

            return View(productCategories);
        }

        // GET: Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Controller/edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategories = await _context.Product_categories.FindAsync(id);
            if (productCategories == null)
            {
                return NotFound();
            }

            return View(productCategories);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategories = await _context.Product_categories
                .FirstOrDefaultAsync(m => m.CATEGORY_ID == id);

            if (productCategories == null)
            {
                return NotFound();
            }

            return View(productCategories);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product_Categories productsCategories)
        {
            if (ModelState.IsValid)
            {

                await _context.Product_categories.AddAsync(productsCategories);
                await _context.SaveChangesAsync();

                
                TempData["mensaje"] = "El contacto se guardó correctamente";
                return RedirectToAction(nameof(Index));
            }

            
            return View(productsCategories);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product_Categories productCategories)
        {
            if (id != productCategories.CATEGORY_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!productsCategoriesExists(productCategories.CATEGORY_ID))
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
            return View(productCategories);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var reg = _context.Product_categories.Find(id);
            if (reg == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _context.Product_categories.Remove(reg);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        private bool productsCategoriesExists(int id)
        {
            return _context.Product_categories.Any(e => e.CATEGORY_ID == id);
        }
    }
}
