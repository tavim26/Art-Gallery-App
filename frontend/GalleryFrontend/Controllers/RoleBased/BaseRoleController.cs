using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers.RoleBased
{
    public abstract class BaseRoleController : Controller
    {
        public virtual IActionResult Index()
        {
            var model = PrepareModel(); 
            return View(model);
        }

        protected abstract object PrepareModel(); 
    }
}