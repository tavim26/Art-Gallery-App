using Microsoft.AspNetCore.Mvc;
using GalleryFrontend.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace GalleryFrontend.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7000/");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string passwordHash, string role, string phone)
        {
            var user = new UserModel
            {
                Name = name,
                Email = email,
                PasswordHash = passwordHash,
                Role = role,
                Phone = phone
            };

            var json = JsonSerializer.Serialize(user, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            Console.WriteLine("Sending user JSON: " + json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("users", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "User registration failed.";
                return View();
            }

            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string passwordHash)
        {
            var res = await _httpClient.GetAsync($"users/login?email={Uri.EscapeDataString(email)}&passwordHash={Uri.EscapeDataString(passwordHash)}");

            if (!res.IsSuccessStatusCode)
            {
                ViewBag.Error = "Invalid credentials.";
                return View();
            }

            var json = await res.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (user == null)
            {
                ViewBag.Error = "Invalid response.";
                return View();
            }

            return user.Role switch
            {
                "Employee" => RedirectToAction("Index", "Employee"),
                "Manager" => RedirectToAction("Index", "Manager"),
                "Admin" => RedirectToAction("Index", "Admin"),
                _ => RedirectToAction("Index", "Home")
            };
        }
    }
}
