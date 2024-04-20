using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Data;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Controller/index
        public ActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        // GET: Controller/details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Employees
                .FirstOrDefaultAsync(m => m.EMPLOYEE_ID == id);

            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
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

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        // GET:  Controllers/delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.EMPLOYEE_ID == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }




        // POST: Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employees employees)
        {
            if (ModelState.IsValid)
            {
                await _context.Employees.AddAsync(employees);
                await _context.SaveChangesAsync();

               
                return RedirectToAction(nameof(Index));
            }

            
            return View(employees);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employees employee)
        {
            if (id != employee.EMPLOYEE_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employee.EMPLOYEE_ID))
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
            return View(employee);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var reg = _context.Employees.Find(id);
            if (reg == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _context.Employees.Remove(reg);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }



        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.EMPLOYEE_ID == id);
        }
    }
}
