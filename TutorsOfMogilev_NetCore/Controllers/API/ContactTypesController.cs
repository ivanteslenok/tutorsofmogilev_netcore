using System.Collections.Generic;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.ContactTypeModule;

namespace TutorsOfMogilev_NetCore.Controllers.API
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/contact-types")]
    public class ContactTypesController : Controller
    {
        private readonly ContactTypeRepository _contactTypeRepository;

        public ContactTypesController(ContactTypeRepository contactTypeRepository)
        {
            _contactTypeRepository = contactTypeRepository;
        }

        [HttpGet]
        public async Task<List<ContactTypeDTO>> Get()
        {
            return await _contactTypeRepository.GetList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _contactTypeRepository.GetItem(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactType item)
        {
            var result = await _contactTypeRepository.InsertItem(item);

            if (result == null)
                return BadRequest("Объект не был создан");

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContactType item)
        {
            item.Id = id;
            var result = await _contactTypeRepository.UpdateItem(item);

            if (result == null)
                return BadRequest("Объект не был обновлен");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _contactTypeRepository.DeleteItem(id);

            if (!result)
                return BadRequest("Объект не был удален");

            return Ok();
        }
    }
}