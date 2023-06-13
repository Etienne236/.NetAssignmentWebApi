using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

public class MyClient
{
    private readonly HttpClient _httpClient;

    public MyClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:44317/");
    }

    public async Task<string> GetDataFromApi()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Product");
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            return data;
        }

        // Handle error cases
        return null;
    }

    public async Task<bool> PostDataToApi(Product model)
    {
        var content = new StringContent(model.ToString(), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("api/Product", content);
        return response.IsSuccessStatusCode;
    }
}
