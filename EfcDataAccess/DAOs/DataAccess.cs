using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared;

namespace EfcDataAccess.DAOs;

public class DataAccess : IDataAccess
{
    private readonly StudentContext context;

    public DataAccess(StudentContext context)
    {
        this.context = context;
    }

    public async Task<Student> CreateStudentAsync(CreateStudentDTO student)
    {
        Student toCreate = new Student()
        {
            Name = student.Name,
            Programme = student.Programme
        };

        EntityEntry<Student> newUser = await context.Student.AddAsync(toCreate);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<ICollection<Student>> GetAllStudentsAsync()
    {
        ICollection<Student> students = await context.Student.ToListAsync();

        return students;
    }

    public async Task AddGradeToStudent(CreateGradeDTO grade, int studentId)
    {
        GradeInCourse existingGrade = await context.GradeInCourse
            .FirstOrDefaultAsync(g => g.Student_Id == studentId && g.CourseCode == grade.CourseCode);
        if (existingGrade != null)
        {
            existingGrade.Grade = grade.Grade; // Update the grade
        }
        else
        {
            GradeInCourse toCreate = new GradeInCourse()
            {
                Grade = grade.Grade,
                CourseCode = grade.CourseCode,
                Student_Id = studentId
            };
            await context.GradeInCourse.AddAsync(toCreate);
        }

        await context.SaveChangesAsync();
    }

    public async Task<StatisticsOverviewDTO> GetCourseStatisticsAsync(string courseName)
    {
        var course = await context.GradeInCourse.FirstOrDefaultAsync(c => c.CourseCode == courseName);
        if (course == null)
        {
            throw new ArgumentException("Coose not found");
        }

        var statistics = new StatisticsOverviewDTO();
        statistics.CourseCode = courseName;
        // Total number of students in a course
        statistics.TotalNumberOfStudents = await context.GradeInCourse
            .Where(g => g.CourseCode == course.CourseCode)
            .Select(g => g.Student_Id)
            .Distinct()
            .CountAsync();

        // Total number of students with a passing grade (2 or above) in a course
        statistics.TotalNumberOfPassedStudents = await context.GradeInCourse
            .Where(g => g.CourseCode == course.CourseCode && g.Grade >= 2)
            .Select(g => g.Student_Id)
            .Distinct()
            .CountAsync();

        // The average grade of all students in a course
        statistics.AvgGradeOverall = (float?)await context.GradeInCourse
            .Where(g => g.CourseCode == course.CourseCode)
            .AverageAsync(g => g.Grade);

        // The average grade of students in a course who have a passing grade (2 or above)
        statistics.AvgGradeOfPassed = (float?)await context.GradeInCourse
            .Where(g => g.CourseCode == course.CourseCode && g.Grade >= 2)
            .AverageAsync(g => g.Grade);

        // The median grade of a course
        var grades = await context.GradeInCourse
            .Where(g => g.CourseCode == course.CourseCode)
            .Select(g => g.Grade)
            .ToListAsync();

        grades.Sort();
        int count = grades.Count;
        int middleIndex = count / 2;
        statistics.MedianGrade = count % 2 == 0 ? grades[middleIndex - 1] : grades[middleIndex];


        return statistics;
    }

}