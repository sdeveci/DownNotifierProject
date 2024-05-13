using DownNotifier.API.Entities;
using DownNotifier.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownNotifier.API.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TargetAppController : ControllerBase
    {
        private readonly IRepository<TargetApp> _repository;

        public TargetAppController(IRepository<TargetApp> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TargetApp>>> Get()
        {
            var targetAppList = await _repository.GetAll();
            return Ok(targetAppList);
        }

        [HttpPost]
        public async Task<ActionResult<TargetApp>> Insert(TargetApp pReq)
        {
            if (pReq == null)
            {
                return BadRequest();
            }

            await _repository.Add(pReq);
            return CreatedAtAction(nameof(Get), new { id = pReq.Id }, pReq);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TargetApp pReq)
        {
            if (id != pReq.Id)
            {
                return BadRequest();
            }

            await _repository.Update(pReq);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var targetAppToDelete = await _repository.GetById(id);
            if (targetAppToDelete == null)
            {
                return NotFound();
            }

            await _repository.Delete(targetAppToDelete.Id);
            return NoContent();
        }
    }
}
