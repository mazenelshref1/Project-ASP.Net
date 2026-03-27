using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;

namespace StudentSystem.Controllers
{
    public class StudentController : Controller
    {
        AppDbContext db = new AppDbContext();

        public IActionResult ShowAll()
        {
            var students = db.Students.ToList();
            return View(students);
        }

        public IActionResult ShowDetails(int id)
        {
            var student = db.Students
                .Include(s => s.Department)
                .FirstOrDefault(s => s.Id == id);

            return View(student);
        }
    }
}
