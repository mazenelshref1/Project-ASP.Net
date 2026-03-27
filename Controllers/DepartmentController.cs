using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;
using StudentSystem.ViewModels;
namespace StudentSystem.Controllers
{
    public class DepartmentController : Controller
    {
        AppDbContext db = new AppDbContext();
        public IActionResult Showall()
        { 
            var departments = db.Departments.ToList();
            return View(departments);
        }
        public IActionResult ShowDetails(int id)
        { 
            var department = db.Departments.Include(d => d.Students).FirstOrDefault(d => d.Id == id);
            return View(department);
        }
        [HttpGet]
        public IActionResult Add()
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Add(Department dept)
        {
            if (dept.Name != null && dept.MgrName != null)
            { 
                db.Departments.Add(dept);
                db.SaveChanges();
                return RedirectToAction("ShowAll");
            }
            return View(dept);
        }
        public IActionResult DetailsVM(int id)
        {
            var dept = db.Departments
                .Include(d => d.Students)
                .FirstOrDefault(d => d.Id == id);

            if (dept == null)
                return NotFound();

            DepartmentWithStudentsVM vm = new DepartmentWithStudentsVM();

            vm.DeptName = dept.Name;

            vm.StudentNames = dept.Students
                .Where(s => s.Age > 21)
                .Select(s => s.Name)
                .ToList();

            vm.State = dept.Students.Count > 50 ? "Main" : "Branch";

            return View(vm);
        }
    }
}
