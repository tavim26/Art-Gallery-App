using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers.RoleBased
{
    public class AdminController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
