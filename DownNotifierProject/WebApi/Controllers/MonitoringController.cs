using DownNotifier.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DownNotifier.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonitoringController : ControllerBase
    {
        private readonly IMonitoringService _monitoringService;
        public MonitoringController(IMonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }

        [HttpPost("healthCheck")]
        public async Task<IActionResult> HealthCheck()
        {
            await _monitoringService.MonitorTargetApplications();
            return Ok("Monitor Target Applications successfully.");
        }

    }
}
