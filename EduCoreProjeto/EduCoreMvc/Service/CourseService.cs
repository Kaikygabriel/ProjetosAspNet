using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using EduCoreMvc.Models;
using EduCoreMvc.Models.Course;

namespace EduCoreMvc.Service;

public class CourseService
{
    private readonly IHttpClientFactory _client;
    private readonly JsonSerializerOptions _options;

    public CourseService(IHttpClientFactory client)
    {
        _client = client;
        _options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
    }

    public async Task<IEnumerable<CourseModel>> GetCourses(string token)
    {
        var client = _client.CreateClient("ApiEduCore");
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        List<CourseModel>? courses = new(); 
        using (var result = await client.GetAsync("Courses"))
        {
            if (!result.IsSuccessStatusCode)
                return null;
            var content = await result.Content.ReadAsStreamAsync();
            courses = JsonSerializer.Deserialize<List<CourseModel>>
                (content, _options);
        }

        return courses;
    }
    public async Task<bool> Create(CourseCreate model,string token)
    {
        var client = _client.CreateClient("ApiEduCore");
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        
        var userJson = JsonSerializer.Serialize(model);
        var content = new StringContent(userJson, Encoding.UTF8, "application/json");

        using var result = await client.PostAsync("Courses", content);

        if (!result.IsSuccessStatusCode)
            return false;

        return true;
    }
}