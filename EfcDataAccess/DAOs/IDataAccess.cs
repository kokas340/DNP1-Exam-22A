using Shared;

namespace EfcDataAccess.DAOs;

public interface IDataAccess
{
    Task<Student> CreateStudentAsync(CreateStudentDTO student);
    Task<ICollection<Student>> GetAllStudentsAsync();
    Task AddGradeToStudent(CreateGradeDTO grade, int studentId);
    Task<StatisticsOverviewDTO> GetCourseStatisticsAsync(string courseeCode);
}