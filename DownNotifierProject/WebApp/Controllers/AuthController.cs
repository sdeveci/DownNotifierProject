using DownNotifier.API.Entities;
using DownNotifier.WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DownNotifier.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly DownNotifierAPIService _apiService;
        public AuthController(DownNotifierAPIService apiService)
        {
            _apiService = apiService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser pReq)
        {
            var response = await _apiService.Api.Login(pReq); 
            if (!string.IsNullOrEmpty(response.Token))
            {
                HttpContext.Session.SetString("JwtToken", response.Token);
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
        }
        

    }
}
