
namespace GalleryFrontend.Controllers.RoleBased
{
    public class AdminController : BaseRoleController
    {
        protected override object PrepareModel()
        {
            return new { Role = "Admin", Message = "Welcome, Admin!" };
        }
    }
}