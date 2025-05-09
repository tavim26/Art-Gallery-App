using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers.RoleBased
{
    public class ManagerController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
