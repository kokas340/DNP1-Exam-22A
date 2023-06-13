using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Shared;

namespace Blazor.Data.Http;

public class StudentHttpClient:IStudentService
{
    
    private readonly HttpClient client;

    public StudentHttpClient(HttpClient client)
    {
        this.client = client;
    }
    public async Task CreateAsync(Student student)
    {
        
        string taskAsJson = JsonSerializer.Serialize(student);
     
        StringContent content = new(taskAsJson, Encoding.UTF8, "application/json");
      
        HttpResponseMessage response = await client.PostAsync("http://localhost:5265/Student/CreateStudentAsync", content);
        string responseContent = await response.Content.ReadAsStringAsync();
     
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        
    }

    public async Task<ICollection<Student>> GetAllStudentsAsync()
    {
        HttpResponseMessage response = await client.GetAsync("http://localhost:5265/Student/GetAllStudentsAsync");
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        ICollection<Student> students = JsonSerializer.Deserialize<ICollection<Student>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
        return students;
    }

    public async Task<ICollection<Student>> AddGradeToStudentAsync(GradeInCourse grade, int studentId)
    {
        string taskAsJson = JsonSerializer.Serialize(grade);
     
        StringContent content = new(taskAsJson, Encoding.UTF8, "application/json");
      
        HttpResponseMessage response = await client.PostAsync("http://localhost:5265/Student/AddGradeToStudentAsync?studentId="+studentId, content);
        string responseContent = await response.Content.ReadAsStringAsync();
     
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        // If successful, return collection of studen
        ICollection<Student> students = JsonSerializer.Deserialize<ICollection<Student>>(responseContent);
        return students;
    }
}