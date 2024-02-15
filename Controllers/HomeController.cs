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

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 5)
        {
            var totalCount = _context.Employees.Count();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            pageIndex = Math.Max(1, Math.Min(totalPages, pageIndex));

            var employees = _context.Employees
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new EmployeeViewModel
            {
                EmployeeList = employees,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(viewModel);
        }

        [HttpGet]
        public JsonResult PopulateForm(int id)
        {
            var todo = _context.Employees.Find(id);
            return Json(todo);
        }


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
            return RedirectToAction(nameof(Index));

            var taskListViewModel = new EmployeeViewModel
            {
                EmployeeList = _context.Employees.ToList()
            };
            return View(taskListViewModel);



        }

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

        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
