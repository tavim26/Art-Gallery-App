using Microsoft.AspNetCore.Mvc;
using UserService.Services.Notifications;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("email")]
        public ActionResult SendEmail([FromQuery] string email, [FromQuery] string message)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(message))
                return BadRequest("Email and message are required.");

            bool result = _notificationService.NotifyByEmail(email, message);
            return result ? Ok("Email sent.") : StatusCode(500, "Failed to send email.");
        }

        [HttpPost("sms")]
        public ActionResult SendSMS([FromQuery] string phone, [FromQuery] string message)
        {
            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(message))
                return BadRequest("Phone and message are required.");

            bool result = _notificationService.NotifyBySms(phone, message);
            return result ? Ok("SMS sent.") : StatusCode(500, "Failed to send SMS.");
        }


    }
}
