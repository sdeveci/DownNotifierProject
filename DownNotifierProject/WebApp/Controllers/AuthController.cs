using DownNotifier.API.Entities;
using DownNotifier.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DownNotifier.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly DownNotifierAPIService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserSessionService _userSessionService;

        public AuthController(DownNotifierAPIService apiService, IHttpContextAccessor httpContextAccessor, UserSessionService userSessionService)
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
            _userSessionService = userSessionService;
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
                    _userSessionService.SetJwtToken(response.Token);
                    
                    var username = _userSessionService.GetUsername();
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.Message);
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "username or password not null.");
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        public async Task<IActionResult> Logout()
        {
            _httpContextAccessor.HttpContext?.Session.Remove("JwtToken");
            _httpContextAccessor.HttpContext?.Session.Remove("Username");
            _httpContextAccessor.HttpContext?.Session.Remove("UserId");
            return RedirectToAction("Index", "Auth");
        }


    }
}
