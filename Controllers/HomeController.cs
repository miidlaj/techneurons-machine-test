using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Models.ViewModels;

namespace Todo.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Home Page
        public IActionResult Index(int pageIndex = 1, int pageSize = 5, string sortOrder = "")
        {
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var totalCount = _context.Employees.Count();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            pageIndex = Math.Max(1, Math.Min(totalPages, pageIndex));

            IQueryable<Employee> employees = _context.Employees;

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.Name);
                    break;
                default:
                    employees = employees.OrderBy(e => e.Name);
                    break;
            }

            var pagedEmployees = employees
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new EmployeeViewModel
            {
                EmployeeList = pagedEmployees,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(viewModel);
        }

        // For Updating 
        [HttpGet]
        public JsonResult PopulateForm(int id)
        {
            var todo = _context.Employees.Find(id);
            return Json(todo);
        }

        // Creating 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Employee employee)
        {

            if (ModelState.IsValid)
            {
                employee.CreatedAt = DateTime.UtcNow;

                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            var defaultPageNumber = 1;
            var defaultPageSize = 5;

            var defaultEmployeeList = _context.Employees
                .OrderBy(e => e.Id)
                .Skip((defaultPageNumber - 1) * defaultPageSize)
                .Take(defaultPageSize)
                .ToList();

            var defaultEmployeeViewModel = new EmployeeViewModel
            {
                EmployeeList = defaultEmployeeList
            };

            return View(defaultEmployeeViewModel);
        }

        // Deleting
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var todo = _context.Employees.Find(id);
            if (todo != null)
            {
                _context.Employees.Remove(todo);
                _context.SaveChanges();
            }
            return Json(new { });
        }

        // Updating
        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
