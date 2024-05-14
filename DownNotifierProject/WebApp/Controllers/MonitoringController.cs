using DownNotifier.API.Entities;
using DownNotifier.API.Enums;
using DownNotifier.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DownNotifier.WebApp.Controllers
{
    public class MonitoringController : Controller
    {
        private readonly DownNotifierAPIService _apiService;
        private readonly UserSessionService _userSessionService;
        private readonly int _userId;
        public MonitoringController(DownNotifierAPIService apiService ,UserSessionService userSessionService)
        {
            _apiService = apiService;
            _userSessionService = userSessionService;
            _userId = int.Parse(_userSessionService.GetUserId());
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> HealthCheck(TargetApp pReq)
        {
                pReq.AplicationUserId = _userId;
                pReq.NotificationType = NotificationType.Email;
                await _apiService.Api.HealthCheck(pReq);
          return View();
        }
    }
}
