using Shared;

namespace Blazor.Data;

public interface IGradeService
{
    Task<StatisticsOverviewDTO> GetCourseStatisticsAsync(string courseName);
}