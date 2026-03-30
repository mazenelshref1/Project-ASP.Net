using StudentSystem.Models;

namespace StudentSystem.ViewModels
{
    public class StudentWithDeptListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }
        public List<Department> Departments { get; set; }
    }
}
