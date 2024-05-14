using DownNotifier.API.Entities;
using DownNotifier.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
namespace DownNotifier.WebApp.Controllers
{
    public class TargetAppController : Controller
    {   
        private readonly DownNotifierAPIService _apiService;
        private readonly UserSessionService _userSessionService;
        private readonly int _userId;

        public TargetAppController(DownNotifierAPIService apiService, UserSessionService userSessionService)
        {
            _apiService = apiService;
            _userSessionService = userSessionService;
            _userId = int.Parse(_userSessionService.GetUserId());
        }


        public async Task<IActionResult> Index()
        {
            var targetAppList = await _apiService.Api.GetAll();
            return View(targetAppList.ToList().Where(p=>p.AplicationUserId== _userId));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TargetApp pReq)
        {
            if (pReq == null)
            {
                ModelState.AddModelError(string.Empty, "required field");
                return View("~/Views/Shared/Error.cshtml");
            }
            pReq.AplicationUserId = _userId;
            await _apiService.Api.Insert(pReq);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var targetAppList = await _apiService.Api.GetAll();
            var targetApp = targetAppList.Where(p => p.Id == id && p.AplicationUserId== _userId).FirstOrDefault();
            if (targetApp == null)
            {                
                ModelState.AddModelError(string.Empty, "Not found!");
                return View("~/Views/Shared/Error.cshtml");
            }

            return View(targetApp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TargetApp pReq)
        {
            if (id != pReq.Id)
            {
                ModelState.AddModelError(string.Empty, "required field");
                return View("~/Views/Shared/Error.cshtml");
            }
            pReq.AplicationUserId = _userId;
            await _apiService.Api.Update(id, pReq);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _apiService.Api.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
   
}
