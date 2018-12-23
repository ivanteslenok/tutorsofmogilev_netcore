using System.Collections.Generic;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Modules.SpecializationModule;

namespace TutorsOfMogilev_NetCore.Controllers.API
{
    #if !DEBUG
    [Authorize(Roles = "Admin")]
    #endif
    [Route("api/[controller]")]
    public class SpecializationsController : Controller
    {
        private readonly SpecializationRepository _specializationRepository;

        public SpecializationsController(SpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }
        
        [HttpGet]
        public async Task<List<SpecializationDTO>> Get()
        {
            return await _specializationRepository.GetList();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _specializationRepository.GetItem(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Specialization item)
        {
            var result = await _specializationRepository.InsertItem(item);

            if (result == null)
                return BadRequest("Объект не был создан");

            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Specialization item)
        {
            item.Id = id;
            var result = await _specializationRepository.UpdateItem(item);

            if (result == null)
                return BadRequest("Объект не был обновлен");

            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _specializationRepository.DeleteItem(id);

            if (!result)
                return BadRequest("Объект не был удален");

            return Ok();
        }
    }
}