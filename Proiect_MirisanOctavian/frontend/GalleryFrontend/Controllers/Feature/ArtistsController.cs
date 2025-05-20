using GalleryFrontend.Controllers.ApiClients;
using GalleryFrontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers.Feature
{
    public class ArtistsController : Controller
    {
        private readonly ArtistApiClient _api;

        public ArtistsController(ArtistApiClient api)
        {
            _api = api;
        }

        public async Task<IActionResult> VisitorIndex(string? name = null)
        {
            var artists = await _api.GetArtistsAsync(name);
            return View("Visitor/Index", artists);
        }

        public async Task<IActionResult> EmployeeIndex(string? name = null)
        {
            var artists = await _api.GetArtistsAsync(name);
            return View("Employee/Index", artists);
        }

        public async Task<IActionResult> ViewPhoto(int id)
        {
            var photoBase64 = await _api.GetPhotoBase64Async(id);
            if (photoBase64 == null)
                return NotFound();

            return View("Common/ViewPhoto",model: photoBase64);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var artist = await _api.GetArtistAsync(id);
            if (artist == null) return NotFound();

            return View("Employee/Edit",artist);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArtistModel model, IFormFile? PhotoFile)
        {
            if (PhotoFile != null && PhotoFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await PhotoFile.CopyToAsync(ms);
                model.Photo = Convert.ToBase64String(ms.ToArray());
            }

            var success = await _api.UpdateArtistAsync(model);
            if (!success)
            {
                ViewBag.Error = "Update failed.";
                return View("Employee/Edit",model);
            }

            return RedirectToAction("EmployeeIndex");
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View("Employee/Add",new ArtistModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArtistModel model, IFormFile PhotoFile)
        {
            if (PhotoFile != null && PhotoFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await PhotoFile.CopyToAsync(ms);
                model.Photo = Convert.ToBase64String(ms.ToArray());
            }

            var success = await _api.AddArtistAsync(model);
            if (!success)
            {
                ViewBag.Error = "Creation failed.";
                return View("Employee/Add", model);
            }

            return RedirectToAction("EmployeeIndex");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _api.DeleteArtistAsync(id);
            if (!success)
                TempData["Error"] = "Delete failed.";

            return RedirectToAction("EmployeeIndex");
        }
    }
}
