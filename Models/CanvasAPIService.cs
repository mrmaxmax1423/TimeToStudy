﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace TimeToStudy.Models
{
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
        var url = $"{_baseUrl}/api/v1/courses?enrollment_state=active";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        String JsonResults = await response.Content.ReadAsStringAsync();
        //Console.WriteLine(JsonResults);


            return(JsonResults);
    }

    // Other API Methods Here 
    }
}