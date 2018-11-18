using System.Threading.Tasks;
using Core.Models;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Modules.TutorModule;
using Modules.TutorModule.Filters;

namespace TutorsOfMogilev_NetCore.Controllers.API
{
    #if !DEBUG
    [Authorize(Roles = "Admin")]
    #endif
    [Route("api/[controller]")]
    public class TutorsController : Controller
    {
        private readonly TutorRepository _tutorRepository;

        public TutorsController(TutorRepository tutorRepository)
        {
            _tutorRepository = tutorRepository;
        }
        
        [HttpGet]
        public async Task<ListModel<TutorDTO>> Get([FromQuery] TutorListFilter filter)
        {
            return await _tutorRepository.GetList(filter);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _tutorRepository.GetItem(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Tutor item)
        {
            var result = await _tutorRepository.InsertItem(item);

            if (result == null)
                return BadRequest("Объект не был создан");

            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Tutor item)
        {
            item.Id = id;
            var result = await _tutorRepository.UpdateItem(item);

            if (result == null)
                return BadRequest("Объект не был обновлен");

            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tutorRepository.DeleteItem(id);

            if (!result)
                return BadRequest("Объект не был удален");

            return Ok();
        }
    }
}