using System.Text.Json;
using Shared;

namespace Blazor.Data.Http;

public class GradeHttpClient:IGradeService
{
    private readonly HttpClient client;

    public GradeHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    
    public async Task<StatisticsOverviewDTO> GetCourseStatisticsAsync(string courseName)
    {
        HttpResponseMessage response = await client.GetAsync("http://localhost:5265/Grade/GetCourseStatisticsAsync?courseCode="+courseName);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        StatisticsOverviewDTO stats = JsonSerializer.Deserialize<StatisticsOverviewDTO>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
        return stats;
    }
}