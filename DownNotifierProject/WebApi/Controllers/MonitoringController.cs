﻿using DownNotifier.API.Entities;
using DownNotifier.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownNotifier.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MonitoringController : ControllerBase
    {
        private readonly IMonitoringService _monitoringService;
        public MonitoringController(IMonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }

        [HttpPost("healthCheck")]
        public async Task<IActionResult> HealthCheck(TargetApp pReq)
        {
            if (pReq == null)
            {
                return BadRequest();
            }
            await _monitoringService.MonitorTargetApplications(pReq);
            return Ok("Monitor Target Applications successfully.");
        }

    }
}
