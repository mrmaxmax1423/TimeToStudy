using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class CanvasApiService
{
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;

    //sets up link, currently uses Constant token and base URL in appsettings.json
    public CanvasApiService(IConfiguration configuration)
    {
        _baseUrl = configuration["CanvasApiBaseUrl"];
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration["CanvasApiToken"]);
    }

    public async Task<string> GetCoursesAsync()
    {
        var url = $"{_baseUrl}/api/v1/courses";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    // Other API Methods Here
}