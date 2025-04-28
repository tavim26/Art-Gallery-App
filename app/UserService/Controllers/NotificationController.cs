using Microsoft.AspNetCore.Mvc;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private NotificationService _notificationService;

        public NotificationController()
        {
            _notificationService = new NotificationService();
        }

        [HttpPost("email")]
        public ActionResult SendEmail([FromQuery] string email, [FromQuery] string message)
        {
            bool result = _notificationService.SendEmail(email, message);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPost("sms")]
        public ActionResult SendSMS([FromQuery] string phone, [FromQuery] string message)
        {
            bool result = _notificationService.SendSMS(phone, message);
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
