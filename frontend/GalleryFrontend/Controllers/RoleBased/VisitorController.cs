using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers.RoleBased
{
    public class VisitorController : BaseRoleController
    {
        protected override object PrepareModel()
        {
            return new { Role = "Visitor", Message = "Welcome, Visitor!" };
        }
    }
}