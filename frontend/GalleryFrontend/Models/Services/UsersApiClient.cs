using System.Text;
using System.Text.Json;
using GalleryFrontend.Models;

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

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var res = await _client.GetAsync("users");
            if (!res.IsSuccessStatusCode) return new();

            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<UserModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
        }

        public async Task<List<UserModel>> FilterUsersByRoleAsync(string role)
        {
            var res = await _client.GetAsync($"users/filterByRole?role={Uri.EscapeDataString(role)}");
            if (!res.IsSuccessStatusCode) return new();

            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<UserModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
        }

        public async Task<UserModel?> GetUserAsync(int id)
        {
            var res = await _client.GetAsync($"users/{id}");
            if (!res.IsSuccessStatusCode) return null;

            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> AddUserAsync(UserModel user)
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _client.PostAsync("users", content);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserAsync(UserModel user)
        {
            var dto = new
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Phone = user.Phone,
                Password = "" // trimite parolă goală => backend o va păstra pe cea veche
            };

            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _client.PutAsync("users", content);
            return res.IsSuccessStatusCode;
        }



        public async Task<bool> DeleteUserAsync(int id)
        {
            var res = await _client.DeleteAsync($"users/{id}");
            return res.IsSuccessStatusCode;
        }

        public async Task<byte[]> ExportCsvAsync()
        {
            var res = await _client.GetAsync("users/export/csv");
            return await res.Content.ReadAsByteArrayAsync();
        }
    }
}
