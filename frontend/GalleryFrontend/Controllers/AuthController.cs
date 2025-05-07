using Microsoft.AspNetCore.Mvc;
using GalleryFrontend.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using GalleryFrontend.Models.Services;

namespace GalleryFrontend.Controllers
{
    public class AuthController : Controller
    {


        private readonly AuthApiClient _auth;

        public AuthController(AuthApiClient auth)
        {
            _auth = auth;
        }




        [HttpGet]
        public IActionResult Login()
        {
            return View(); 
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(); 
        }



        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _auth.LoginAsync(email, password);
            if (user == null)
            {
                ViewBag.Error = "Invalid credentials.";
                return View();
            }

            // salvare in sesiune
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserRole", user.Role);

            
            Console.WriteLine($"[AUTH SUCCESS] User '{user.Name}' (ID={user.Id}, Role={user.Role}) logged in and saved to session.");

            return user.Role switch
            {
                "Employee" => RedirectToAction("Index", "Employee"),
                "Manager" => RedirectToAction("Index", "Manager"),
                "Admin" => RedirectToAction("Index", "Admin"),
                _ => RedirectToAction("Index", "Home")
            };

        }




        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string password, string role, string phone)
        {
            var user = new UserModel
            {
                Name = name,
                Email = email,
                Password = password,
                Role = role,
                Phone = phone
            };

            bool success = await _auth.RegisterAsync(user);
            if (!success)
            {
                ViewBag.Error = "User registration failed.";
                return View();
            }

            return RedirectToAction("Login");
        }






        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }





    }
}
