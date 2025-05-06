using System.Text.Json;

namespace GalleryFrontend.Models.Services
{
    public class UsersApiClient
    {
        private readonly HttpClient _client;

        public UsersApiClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:7000/");
        }

        public async Task<UserModel?> GetUserAsync(int id)
        {
            var res = await _client.GetAsync($"users/{id}");
            if (!res.IsSuccessStatusCode) return null;

            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }


}
