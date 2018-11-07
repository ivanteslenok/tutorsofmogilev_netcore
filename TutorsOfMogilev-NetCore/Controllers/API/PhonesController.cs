using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Modules.PhoneModule;

namespace TutorsOfMogilev_NetCore.Controllers.API
{
    #if !DEBUG
    [Authorize(Roles = "Admin")]
    #endif
    [Route("api/[controller]")]
    public class PhonesController : Controller
    {
        private readonly PhoneRepository _phoneRepository;

        public PhonesController(PhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }
        
        [HttpGet]
        public async Task<List<Phone>> Get()
        {
            return await _phoneRepository.GetList();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _phoneRepository.GetItem(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Phone item)
        {
            var result = await _phoneRepository.InsertItem(item);

            if (result == null)
                return BadRequest("Объект не был создан");

            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Phone item)
        {
            item.Id = id;
            var result = await _phoneRepository.UpdateItem(item);

            if (result == null)
                return BadRequest("Объект не был обновлен");

            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _phoneRepository.DeleteItem(id);

            if (!result)
                return BadRequest("Объект не был удален");

            return Ok();
        }
    }
}