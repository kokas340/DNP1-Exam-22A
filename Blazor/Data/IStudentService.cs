using Shared;

namespace Blazor.Data;

public interface IStudentService
{
    Task CreateAsync(Student student);
    Task<ICollection<Student>> GetAllStudentsAsync();
    Task<ICollection<Student>> AddGradeToStudentAsync(GradeInCourse grade, int studentId);
  
}