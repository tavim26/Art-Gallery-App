using Microsoft.AspNetCore.Mvc;
using GalleryFrontend.Models;
using System.Net.Http;
using System.Text.Json;

namespace GalleryFrontend.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ArtistsController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7000/");
        }

        public async Task<IActionResult> VisitorIndex(string name = null)
        {
            string url = string.IsNullOrWhiteSpace(name)
                ? "artists/"
                : $"artists/searchByName?name={Uri.EscapeDataString(name)}";

            var res = await _httpClient.GetAsync(url);
            if (!res.IsSuccessStatusCode) return View(new List<ArtistModel>());

            var json = await res.Content.ReadAsStringAsync();
            var artists = JsonSerializer.Deserialize<List<ArtistModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<ArtistModel>();

            return View("IndexVisitor", artists);
        }

        public async Task<IActionResult> ViewPhoto(int id)
        {
            var res = await _httpClient.GetAsync($"artists/photo/{id}");
            if (!res.IsSuccessStatusCode) return NotFound();

            var photoUrl = (await res.Content.ReadAsStringAsync()).Trim('"');
            return View(model: photoUrl);
        }





        public async Task<IActionResult> EmployeeIndex(string name = null)
        {
            string url = string.IsNullOrWhiteSpace(name)
                ? "artists/"
                : $"artists/searchByName?name={Uri.EscapeDataString(name)}";

            var res = await _httpClient.GetAsync(url);
            if (!res.IsSuccessStatusCode) return View("IndexEmployee", new List<ArtistModel>());

            var json = await res.Content.ReadAsStringAsync();
            var artists = JsonSerializer.Deserialize<List<ArtistModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<ArtistModel>();

            return View("IndexEmployee", artists);
        }




        [HttpGet]
        public IActionResult Add()
        {
            return View(new ArtistModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArtistModel model, IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                ViewBag.Error = "Photo is required.";
                return View(model);
            }

            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(model.Name), "Name");
            content.Add(new StringContent(model.BirthDate?.ToString("yyyy-MM-dd") ?? ""), "BirthDate");
            content.Add(new StringContent(model.Birthplace ?? ""), "Birthplace");
            content.Add(new StringContent(model.Nationality ?? ""), "Nationality");

            var stream = photo.OpenReadStream();
            content.Add(new StreamContent(stream), "Photo", photo.FileName);

            var response = await _httpClient.PostAsync("artists", content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Artist creation failed.";
                return View(model);
            }

            return RedirectToAction("EmployeeIndex");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"artists/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var artist = JsonSerializer.Deserialize<ArtistModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(artist);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArtistModel model, IFormFile? photo)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(model.Id.ToString()), "Id");
            content.Add(new StringContent(model.Name), "Name");
            content.Add(new StringContent(model.BirthDate?.ToString("yyyy-MM-dd") ?? ""), "BirthDate");
            content.Add(new StringContent(model.Birthplace ?? ""), "Birthplace");
            content.Add(new StringContent(model.Nationality ?? ""), "Nationality");

            if (photo != null && photo.Length > 0)
            {
                var stream = photo.OpenReadStream();
                content.Add(new StreamContent(stream), "Photo", photo.FileName);
            }

            var response = await _httpClient.PutAsync("artists", content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Update failed.";
                return View(model);
            }

            return RedirectToAction("EmployeeIndex");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"artists/{id}");
            if (!response.IsSuccessStatusCode)
                TempData["Error"] = "Delete failed.";

            return RedirectToAction("EmployeeIndex");
        }


    }
}