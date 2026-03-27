using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<StuCrsRes> StuCrsRes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=WebDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StuCrsRes>()
            .HasKey(x => new { x.StudentId, x.CourseId });

        modelBuilder.Entity<StuCrsRes>()
            .HasOne(x => x.Student)
            .WithMany(s => s.StuCrsRes)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<StuCrsRes>()
            .HasOne(x => x.Course)
            .WithMany(c => c.StuCrsRes)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
