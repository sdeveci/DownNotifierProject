using DownNotifier.API.Services;
using DownNotifier.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DownNotifier.WebApp.Controllers
{
    public class MonitoringController : Controller
    {
        private readonly DownNotifierAPIService _apiService;

        public MonitoringController(DownNotifierAPIService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> HealthCheck()
        {   
          await _apiService.Api.HealthCheck();
          return View();
        }
    }
}
