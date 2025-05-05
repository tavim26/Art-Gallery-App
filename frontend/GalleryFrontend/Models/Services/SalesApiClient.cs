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
    }
}