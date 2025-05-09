using GalleryFrontend.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace GalleryFrontend.ApiClients
{
    public class AuthApiClient
    {
        private readonly HttpClient _http;

        public AuthApiClient(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
            _http.BaseAddress = new Uri("http://localhost:7000/");
        }

        public async Task<UserModel?> LoginAsync(string email, string password)
        {
            var payload = new { Email = email, Password = password };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await _http.PostAsync("users/login", content);
            if (!res.IsSuccessStatusCode) return null;

            var jsonRes = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserModel>(jsonRes, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> RegisterAsync(UserModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await _http.PostAsync("users", content);
            var responseText = await res.Content.ReadAsStringAsync();

            Console.WriteLine($"[RegisterAsync] Status: {res.StatusCode}, Response: {responseText}");
            return res.IsSuccessStatusCode;
        }

    }
}