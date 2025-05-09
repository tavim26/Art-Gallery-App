using GalleryFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using GalleryFrontend.ApiClients;
using GalleryFrontend.Models.ViewModels;

namespace GalleryFrontend.Controllers.Feature
{
    public class ArtworksController : Controller
    {
        private readonly ArtworksApiClient _api;
        private readonly ArtistApiClient _artistsApi;

        public ArtworksController(ArtworksApiClient api, ArtistApiClient artistsApi)
        {
            _api = api;
            _artistsApi = artistsApi;
        }

        public async Task<IActionResult> VisitorIndex(string title = null, string type = null, int? artistId = null)
        {
            var artworks = await _api.GetArtworksAsync(title, type, artistId);
            var artists = await _artistsApi.GetArtistsAsync();

            foreach (var artwork in artworks)
                artwork.ArtistName = artists.FirstOrDefault(a => a.Id == artwork.ArtistId)?.Name ?? "Unknown";

            var model = new ArtworkFilterModel
            {
                Artworks = artworks,
                Artists = artists
            };

            return View("Visitor/Index", model);
        }

        public async Task<IActionResult> EmployeeIndex(string title = null, string type = null, int? artistId = null)
        {
            var artworks = await _api.GetArtworksAsync(title, type, artistId);
            var artists = await _artistsApi.GetArtistsAsync();

            foreach (var artwork in artworks)
                artwork.ArtistName = artists.FirstOrDefault(a => a.Id == artwork.ArtistId)?.Name ?? "Unknown";

            var model = new ArtworkFilterModel
            {
                Artworks = artworks,
                Artists = artists
            };

            return View("Employee/Index", model);
        }


        [HttpGet]
        public async Task<IActionResult> ViewImages(int artworkId)
        {
            var images = await _api.GetArtworkImagesAsync(artworkId);
            if (images == null || images.Count == 0)
            {
                ViewBag.Error = "No images found for this artwork.";
                return View("Common/ViewImages",new List<string>());
            }

            return View("Common/ViewImages",images);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Artists = await _artistsApi.GetArtistsAsync();
            return View("Employee/Add", new ArtworkModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArtworkModel model)
        {
            var success = await _api.AddArtworkAsync(model);
            if (!success)
            {
                ViewBag.Error = "Creation failed.";
                ViewBag.Artists = await _artistsApi.GetArtistsAsync();
                return View("Employee/Add", model);
            }

            return RedirectToAction("EmployeeIndex");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetArtworkAsync(id);
            if (model == null) return NotFound();

            ViewBag.Artists = await _artistsApi.GetArtistsAsync();
            return View("Employee/Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArtworkModel model)
        {
            var success = await _api.UpdateArtworkAsync(model);
            if (!success)
            {
                ViewBag.Error = "Update failed.";
                ViewBag.Artists = await _artistsApi.GetArtistsAsync();
                return View("Employee/Edit", model);
            }

            return RedirectToAction("EmployeeIndex");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _api.DeleteArtworkAsync(id);
            if (!success)
            {
                TempData["Error"] = "Deletion failed.";
            }

            return RedirectToAction("EmployeeIndex");
        }

        public async Task<IActionResult> ExportCsv()
        {
            var bytes = await _api.ExportCsvAsync();
            return File(bytes, "text/csv", "artworks.csv");
        }

        public async Task<IActionResult> ExportJson()
        {
            var json = await _api.ExportJsonAsync();
            return File(Encoding.UTF8.GetBytes(json), "application/json", "artworks.json");
        }


        public async Task<IActionResult> ExportXml()
        {
            var bytes = await _api.ExportXmlAsync();
            return File(bytes, "application/xml", "artworks.xml");
        }




        [HttpGet]
        public IActionResult AddImage(int artworkId)
        {
            return View("Employee/AddImage", new ArtworkImageModel { ArtworkId = artworkId });
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(ArtworkImageModel model)
        {
            var success = await _api.AddArtworkImageAsync(model);
            if (!success)
            {
                ViewBag.Error = "Failed to add image.";
                return View("Employee/AddImage",model);
            }

            return RedirectToAction("EmployeeIndex", new { artworkId = model.ArtworkId });
        }



        public async Task<IActionResult> Stats()
        {
            var byType = await _api.GetStatsByTypeAsync();
            var byArtist = await _api.GetStatsByArtistAsync();
            var artists = await _artistsApi.GetArtistsAsync();

            foreach (var item in byArtist)
            {
                if (int.TryParse(item.Label.Replace("Artist ", ""), out int artistId))
                {
                    var artist = artists.FirstOrDefault(a => a.Id == artistId);
                    if (artist != null)
                        item.Label = artist.Name;
                }
            }

            return View("Manager/Stats",(byType, byArtist));
        }





    }
}
