using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers.Core
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}