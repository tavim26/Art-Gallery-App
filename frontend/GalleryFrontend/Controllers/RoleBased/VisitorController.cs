using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers.RoleBased
{
    public class VisitorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
