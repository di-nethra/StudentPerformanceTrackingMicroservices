using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using StudentPerformanceTracker.Models;

public class StudySessionServiceHttpClient
{
    private readonly HttpClient _httpClient;

    public StudySessionServiceHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<StudySession>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<StudySession>>("api/studysessions");
    }

    public async Task<StudySession> GetByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<StudySession>($"api/studysessions/{id}");
    }

    public async Task CreateAsync(StudySession studySession)
    {
        await _httpClient.PostAsJsonAsync("api/studysessions", studySession);
    }

    public async Task UpdateAsync(string id, StudySession studySession)
    {
        await _httpClient.PutAsJsonAsync($"api/studysessions/{id}", studySession);
    }

    public async Task DeleteAsync(string id)
    {
        await _httpClient.DeleteAsync($"api/studysessions/{id}");
    }
}
