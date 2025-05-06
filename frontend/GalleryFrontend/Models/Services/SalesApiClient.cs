using System.Net.Http;
using System.Text;
using System.Text.Json;
using GalleryFrontend.Models;

namespace GalleryFrontend.Models.Services
{
    public class SalesApiClient
    {
        private readonly HttpClient _client;

        public SalesApiClient(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:7000/");
        }

        public async Task<bool> SellArtworkAsync(SaleModel sale)
        {
            var json = JsonSerializer.Serialize(sale);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("sales", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<SaleModel>> GetSalesAsync()
        {
            var res = await _client.GetAsync("sales");
            if (!res.IsSuccessStatusCode) return new List<SaleModel>();

            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<SaleModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<SaleModel>();
        }

        public async Task<double> GetTotalSalesAsync()
        {
            var res = await _client.GetAsync("sales/totalSalesAmount");
            if (!res.IsSuccessStatusCode) return 0.0;

            var json = await res.Content.ReadAsStringAsync();
            return double.TryParse(json, out var val) ? val : 0.0;
        }
    }
}