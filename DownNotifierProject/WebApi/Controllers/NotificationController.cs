using DownNotifier.API.Entities;
using DownNotifier.API.Enums;
using DownNotifier.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownNotifier.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("sendEmailNotification")]
        public async Task<IActionResult> HealthCheck(TargetApp pReq)
        {
            await _notificationService.SendNotification(pReq);
            return Ok("Email notification sent successfully.");
        }
    }
}
