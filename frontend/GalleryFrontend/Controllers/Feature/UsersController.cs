using GalleryFrontend.ApiClients;
using GalleryFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GalleryFrontend.Controllers.Feature
{
    public class UsersController : Controller
    {
        private readonly UsersApiClient _usersApi;

        public UsersController(UsersApiClient usersApi)
        {
            _usersApi = usersApi;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? role)
        {
            List<UserModel> users = string.IsNullOrWhiteSpace(role)
                ? await _usersApi.GetAllUsersAsync()
                : await _usersApi.FilterUsersByRoleAsync(role);

            users = users.Where(u => u.Role != "Admin").ToList();

            ViewBag.SelectedRole = role;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _usersApi.GetUserAsync(id);
            if (user == null) return NotFound();


            return View(user);
        }




        [HttpPost]
        public async Task<IActionResult> Edit(UserModel model)
        {
            
            if (!ModelState.IsValid)
            {
               
                return View(model);
            }

            bool success = await _usersApi.UpdateUserAsync(model);

            if (!success)
            {
                ViewBag.Error = "Update failed.";
                return View(model);
            }

            return RedirectToAction("Index");
        }






        [HttpGet]
        public IActionResult Add()
        {
            return View(new UserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool success = await _usersApi.AddUserAsync(model);
            if (!success)
            {
                ViewBag.Error = "Insert failed.";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _usersApi.DeleteUserAsync(id);
            if (!success)
                TempData["Error"] = "Delete failed.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ExportCsv()
        {
            var file = await _usersApi.ExportCsvAsync();
            return File(file, "text/csv", "users.csv");
        }
    }
}
