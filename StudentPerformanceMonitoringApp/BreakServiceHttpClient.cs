using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using StudentPerformanceTracker.Models;

public class BreakServiceHttpClient
{
    private readonly HttpClient _httpClient;

    public BreakServiceHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Break>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Break>>("api/breaks");
    }

    public async Task<Break> GetByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<Break>($"api/breaks/{id}");
    }

    public async Task AddAsync(Break studentBreak)
    {
        var response = await _httpClient.PostAsJsonAsync("api/breaks", studentBreak);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(string id, Break studentBreak)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/breaks/{id}", studentBreak);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"api/breaks/{id}");
        response.EnsureSuccessStatusCode();
    }
}
