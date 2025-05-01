using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
