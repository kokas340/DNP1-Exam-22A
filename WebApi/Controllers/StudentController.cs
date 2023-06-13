using EfcDataAccess.DAOs;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace WebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    
    private readonly IDataAccess dataAccess;

    public StudentController(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }
    
    [HttpPost, Route("CreateStudentAsync")]
    public async Task<ActionResult> CreateStudentAsync([FromBody] CreateStudentDTO student)
    {
        try
        {
            await dataAccess.CreateStudentAsync(student);
            return Created($"/users/{student.Name}", student);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
       
    }
    
    [HttpGet, Route("GetAllStudentsAsync")]
    public async Task<ActionResult<ICollection<Student>>> GetAllStudentsAsync()
    {
        try
        {
            ICollection<Student> listStudents = await dataAccess.GetAllStudentsAsync();
            return Ok(listStudents);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost, Route("AddGradeToStudentAsync")]
    public async Task<ActionResult> AddGradeToStudentAsync(int studentId, [FromBody] CreateGradeDTO gradeInCourse)
    {
        try
        {
            
            await dataAccess.AddGradeToStudent(gradeInCourse,studentId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
       
    }
}