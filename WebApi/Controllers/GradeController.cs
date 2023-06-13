using EfcDataAccess.DAOs;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace WebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class GradeController : ControllerBase
{
    
    private readonly IDataAccess dataAccess;

    public GradeController(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }
    
    [HttpGet, Route("GetCourseStatisticsAsync")]
    public async Task<ActionResult<StatisticsOverviewDTO>> GetAllStudentsAsync(string courseCode)
    {
        try
        {
            StatisticsOverviewDTO stats = await dataAccess.GetCourseStatisticsAsync(courseCode);
            return Ok(stats);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    

}