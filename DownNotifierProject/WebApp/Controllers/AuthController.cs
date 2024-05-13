using DownNotifier.API.Entities;
using DownNotifier.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace DownNotifier.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly DownNotifierAPIService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(DownNotifierAPIService apiService, IHttpContextAccessor httpContextAccessor)
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser pReq)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiService.Api.Login(pReq);
                if (!string.IsNullOrEmpty(response.Token))
                {
                    HttpContext.Session.SetString("JwtToken", response.Token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "username or password not null.");
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            _httpContextAccessor.HttpContext?.Session.Remove("JwtToken");
            return RedirectToAction("Index", "Auth");
        }


    }
}
