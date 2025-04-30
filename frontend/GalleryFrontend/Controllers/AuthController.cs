using Microsoft.AspNetCore.Mvc;
using GalleryFrontend.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace GalleryFrontend.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7000/");
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string passwordHash)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwordHash))
            {
                ViewBag.Error = "Email and password are required.";
                return View();
            }

            var response = await _httpClient.PostAsync($"auth/login?email={Uri.EscapeDataString(email)}&passwordHash={Uri.EscapeDataString(passwordHash)}", null);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();
            var auth = JsonSerializer.Deserialize<AuthModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var userResponse = await _httpClient.GetAsync($"users/{auth.UserId}");
            if (!userResponse.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to retrieve user information.";
                return View();
            }

            var userJson = await userResponse.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserModel>(userJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Auth/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string passwordHash, string role, string phone)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwordHash) || string.IsNullOrEmpty(role))
            {
                ViewBag.Error = "Name, email, password, and role are required.";
                return View();
            }

            var user = new UserModel
            {
                Name = name,
                Role = role,
                Phone = phone ?? ""
            };

            var userResponse = await _httpClient.PostAsync("users", new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));
            if (!userResponse.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to create user.";
                return View();
            }

            var userJson = await userResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(userJson))
            {
                ViewBag.Error = "Failed to retrieve created user data.";
                return View();
            }

            var createdUser = JsonSerializer.Deserialize<UserModel>(userJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

           
            if (createdUser.Id <= 0)
            {
                ViewBag.Error = "Invalid user ID received from server.";
                return View();
            }

            var auth = new AuthModel
            {
                UserId = createdUser.Id,
                Email = email,
                PasswordHash = passwordHash
            };

            var authResponse = await _httpClient.PostAsync("auth/signup", new StringContent(JsonSerializer.Serialize(auth), Encoding.UTF8, "application/json"));
            if (!authResponse.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to create authentication. Please try again.";
                return View();
            }

            return RedirectToAction("Login");
        }

        // GET: /Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}