// GalleryFrontend/Services/ArtworksApiClient.cs
using GalleryFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace GalleryFrontend.ApiClients
{
    public class ArtworksApiClient
    {
        private readonly HttpClient _client;

        public ArtworksApiClient(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:7000/");
        }

        public async Task<List<ArtworkModel>> GetArtworksAsync(string? title = null, string? type = null, int? artistId = null)
        {
            string url = "artworks";

            if (!string.IsNullOrWhiteSpace(title))
                url = $"artworks/searchByTitle?title={Uri.EscapeDataString(title)}";
            else if (!string.IsNullOrWhiteSpace(type))
                url = $"artworks/filterByType?type={Uri.EscapeDataString(type)}";
            else if (artistId.HasValue)
                url = $"artworks/filterByArtistId?artistId={artistId}";

            var res = await _client.GetAsync(url);
            if (!res.IsSuccessStatusCode) return new();

            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ArtworkModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
        }

        public async Task<ArtworkModel?> GetArtworkAsync(int id)
        {
            var res = await _client.GetAsync($"artworks/{id}");
            if (!res.IsSuccessStatusCode) return null;

            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ArtworkModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<string>> GetArtworkImagesAsync(int artworkId)
        {
            var res = await _client.GetAsync($"artworks/images/{artworkId}");
            if (!res.IsSuccessStatusCode) return new();

            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<string>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
        }

        public async Task<bool> AddArtworkAsync(ArtworkModel artwork)
        {

            var json = JsonSerializer.Serialize(artwork);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _client.PostAsync("artworks", content);
            Console.WriteLine(json);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateArtworkAsync(ArtworkModel artwork)
        {
            var json = JsonSerializer.Serialize(artwork);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _client.PutAsync("artworks", content);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteArtworkAsync(int id)
        {
            var res = await _client.DeleteAsync($"artworks/{id}");
            return res.IsSuccessStatusCode;
        }

        public async Task<byte[]> ExportCsvAsync()
        {
            var res = await _client.GetAsync("artworks/export/csv");
            return await res.Content.ReadAsByteArrayAsync();
        }

        public async Task<string> ExportJsonAsync()
        {
            var res = await _client.GetAsync("artworks/export/json");
            return await res.Content.ReadAsStringAsync();
        }


        public async Task<byte[]> ExportXmlAsync()
        {
            var res = await _client.GetAsync("artworks/export/xml");
            return await res.Content.ReadAsByteArrayAsync();
        }




        public async Task<bool> AddArtworkImageAsync(ArtworkImageModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _client.PostAsync("artworkimages", content);
            return res.IsSuccessStatusCode;
        }


        public async Task<List<ArtworkStatsModel>> GetStatsByTypeAsync()
        {
            var res = await _client.GetAsync("artworks/stats/byType");
            if (!res.IsSuccessStatusCode) return new();
            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ArtworkStatsModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
        }

        public async Task<List<ArtworkStatsModel>> GetStatsByArtistAsync()
        {
            var res = await _client.GetAsync("artworks/stats/byArtist");
            if (!res.IsSuccessStatusCode) return new();
            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ArtworkStatsModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
        }


    }
}
