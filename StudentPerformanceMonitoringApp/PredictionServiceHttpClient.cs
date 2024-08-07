using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using StudentPerformanceTracker.Models;

public class PredictionServiceHttpClient
{
    private readonly HttpClient _httpClient;

    public PredictionServiceHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

  public async Task<Prediction> GetPredictionAsync(string subject, DateTime futureDate)
{
    string formattedDate = futureDate.ToString("yyyy-MM-dd");
     Console.WriteLine($"date: {formattedDate}");
    try
    {
        var responseMessage = await _httpClient.GetAsync($"api/predictions?subject={subject}&futureDate={formattedDate}");

        if (responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadFromJsonAsync<Prediction>();
            if (response != null)
            {
                Console.WriteLine($"Subject: {response.Subject}");
                Console.WriteLine($"Grade: {response.PredictedGrade}");
                Console.WriteLine($"Progress Percentage: {response.GradeProgress}");
                return response;
            }
            else
            {
                Console.WriteLine("Received null response");
            }
        }
        else
        {
            Console.WriteLine($"Error: {responseMessage.StatusCode} - {responseMessage.ReasonPhrase}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }

    return null;
}

}