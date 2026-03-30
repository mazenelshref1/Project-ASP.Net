using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;
using StudentSystem.ViewModels;

namespace StudentSystem.Controllers
{
    public class StudentController : Controller
    {
        AppDbContext db = new AppDbContext();
        AppDbContext context = new AppDbContext(); 
        public IActionResult ShowAll()
        {
            var students = context.Students.ToList();
            return View(students);
        }

        public IActionResult ShowDetails(int id)
        {
            var students = context.Students
                .Include(s => s.Department)
                .FirstOrDefault(s => s.Id == id);

            return View(students);
        }
        public IActionResult GetAll()
        {
            var students = context.Students.Include(s => s.Department).ToList();
            return View(students);
        }
        public IActionResult GetById(int id)
        {
            var students = context.Students.Include(s => s.Department).FirstOrDefault(s => s.Id == id);
            return View(students);
        }
        public IActionResult Add()
        {
            StudentWithDeptListVM vm = new StudentWithDeptListVM();
            vm.Departments = context.Departments.ToList();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Add(StudentWithDeptListVM vm)
        {
            Student student = new Student();
            student.Name = vm.Name;
            student.Age = vm.Age;
            student.DepartmentId = vm.DepartmentId;
            context.Students.Add(student);
            context.SaveChanges();
            return RedirectToAction("GetAll");
        }
        public IActionResult Edit(int id)
        { 
            Student? student = context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            StudentWithDeptListVM vm = new StudentWithDeptListVM();
            vm.Id = student.Id;
            vm.Name = student.Name;
            vm.Age = student.Age;
            vm.DepartmentId = student.DepartmentId;
            vm.Departments = context.Departments.ToList();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(StudentWithDeptListVM vm)
        { 
            Student? student = context.Students.FirstOrDefault(s => s.Id == vm.Id);
            if (student == null)
            {
                return NotFound();
            }
            student.Name = vm.Name;
            student.Age = vm.Age;
            student.DepartmentId = vm.DepartmentId;
            context.SaveChanges();
            return RedirectToAction("GetAll");
        }
        public ActionResult Delete(int id)
        { 
            Student? student = context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            context.Students.Remove(student);
            context.SaveChanges();
            return RedirectToAction("GetAll");
        }
    }
}
