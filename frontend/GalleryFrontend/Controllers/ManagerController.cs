using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers
{
    public class ManagerController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
