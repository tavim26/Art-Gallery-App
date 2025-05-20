
namespace GalleryFrontend.Controllers.RoleBased
{
    public class ManagerController : BaseRoleController
    {
        protected override object PrepareModel()
        {
            return new { Role = "Manager", Message = "Welcome, Manager!" };
        }
    }
}