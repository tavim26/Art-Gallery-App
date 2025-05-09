using GalleryFrontend.Models;
using System.Text;
using System.Text.Json;

namespace GalleryFrontend.ApiClients
{
    public class ArtistApiClient
    {
        private readonly HttpClient _client;

        public ArtistApiClient(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:7000/"); // API Gateway
        }

        public async Task<List<ArtistModel>> GetArtistsAsync(string? name = null)
        {
            var url = string.IsNullOrWhiteSpace(name)
                ? "artists"
                : $"artists/searchByName?name={Uri.EscapeDataString(name)}";

            var res = await _client.GetAsync(url);
            if (!res.IsSuccessStatusCode)
                return new List<ArtistModel>();

            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ArtistModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<ArtistModel>();
        }

        public async Task<ArtistModel?> GetArtistAsync(int id)
        {
            var res = await _client.GetAsync($"artists/{id}");
            if (!res.IsSuccessStatusCode) return null;

            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ArtistModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<string?> GetPhotoBase64Async(int id)
        {
            var res = await _client.GetAsync($"artists/photo/{id}");
            if (!res.IsSuccessStatusCode) return null;

            return (await res.Content.ReadAsStringAsync()).Trim('"');
        }

        public async Task<bool> AddArtistAsync(ArtistModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _client.PostAsync("artists", content);
            return res.IsSuccessStatusCode;
        }


        public async Task<bool> UpdateArtistAsync(ArtistModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _client.PutAsync("artists", content);
            return res.IsSuccessStatusCode;
        }


        public async Task<bool> DeleteArtistAsync(int id)
        {
            var res = await _client.DeleteAsync($"artists/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
