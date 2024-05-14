using DownNotifier.API.Entities;
using DownNotifier.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
namespace DownNotifier.WebApp.Controllers
{
    public class TargetAppController : Controller
    {   
        private readonly DownNotifierAPIService _apiService;
        public TargetAppController(DownNotifierAPIService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var targetAppList = await _apiService.Api.GetAll();
            return View(targetAppList);
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
                return BadRequest();
            }

            await _apiService.Api.Insert(pReq);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var targetAppList = await _apiService.Api.GetAll();
            var targetApp = targetAppList.Where(p => p.Id == id).FirstOrDefault();
            if (targetApp == null)
            {
                return NotFound();
            }

            return View(targetApp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TargetApp pReq)
        {
            if (id != pReq.Id)
            {
                return BadRequest();
            }

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
