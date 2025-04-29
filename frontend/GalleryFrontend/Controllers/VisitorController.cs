using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers
{
    public class VisitorController : Controller
    {
        public IActionResult Index()
        {
            return View("VisitorIndex");
        }
    }
}
