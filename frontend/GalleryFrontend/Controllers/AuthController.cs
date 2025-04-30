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
                Role = role,
                Phone = phone
            };

            // creează user
            var userJson = JsonSerializer.Serialize(user);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            var userResponse = await _httpClient.PostAsync("users", userContent);

            if (!userResponse.IsSuccessStatusCode)
            {
                ViewBag.Error = "User creation failed.";
                return View();
            }

            var userBody = await userResponse.Content.ReadAsStringAsync();
            var createdUser = JsonSerializer.Deserialize<UserModel>(userBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


            Console.WriteLine($"userBody = {userBody}");
            Console.WriteLine($"createdUser.Id = {createdUser?.Id}");

            if (createdUser == null)
            {
                ViewBag.Error = "Failed to parse user.";
                return View();
            }

            var auth = new AuthModel
            {
                UserId = createdUser.Id,
                Email = email,
                PasswordHash = passwordHash
            };

            var authJson = JsonSerializer.Serialize(auth);
            var authContent = new StringContent(authJson, Encoding.UTF8, "application/json");
            var authResponse = await _httpClient.PostAsync("auth/signup", authContent);

            if (!authResponse.IsSuccessStatusCode)
            {
                ViewBag.Error = "Auth creation failed.";
                return View();
            }

            return RedirectToAction("Login");
        }





        [HttpGet]
        public IActionResult Login() => View();




        [HttpPost]
        public async Task<IActionResult> Login(string email, string passwordHash)
        {
            var res = await _httpClient.GetAsync($"auth/login?email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(passwordHash)}");
            if (!res.IsSuccessStatusCode)
            {
                ViewBag.Error = "Invalid credentials.";
                return View();
            }

            var json = await res.Content.ReadAsStringAsync();
            var auth = JsonSerializer.Deserialize<AuthModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (auth == null)
            {
                ViewBag.Error = "Invalid login response.";
                return View();
            }

            // obține user detalii
            var userRes = await _httpClient.GetAsync($"users/{auth.UserId}");
            if (!userRes.IsSuccessStatusCode)
            {
                ViewBag.Error = "User not found.";
                return View();
            }

            var userJson = await userRes.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserModel>(userJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (user == null)
            {
                ViewBag.Error = "Invalid user data.";
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
