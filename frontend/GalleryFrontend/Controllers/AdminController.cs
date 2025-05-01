using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers
{
    public class AdminController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
