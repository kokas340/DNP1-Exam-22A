using Microsoft.EntityFrameworkCore;
using Shared;

namespace EfcDataAccess;



public class StudentContext: DbContext
{
    public DbSet<Student> Student { get; set; }
    public DbSet<GradeInCourse> GradeInCourse { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Student.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasKey(student => student.Student_Id);
        modelBuilder.Entity<GradeInCourse>().HasKey(grade => grade.GradeInCourse_Id);
    }
}