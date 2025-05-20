
namespace GalleryFrontend.Controllers.RoleBased
{
    public class EmployeeController : BaseRoleController
    {
        protected override object PrepareModel()
        {
            return new { Role = "Employee", Message = "Welcome, Employee!" };
        }
    }
}