using System.Collections.Generic;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Modules.ContactModule;

namespace TutorsOfMogilev_NetCore.Controllers.API
{
    #if !DEBUG
    [Authorize(Roles = "Admin")]
    #endif
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactRepository _contactRepository;

        public ContactsController(ContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<List<ContactDTO>> Get()
        {
            return await _contactRepository.GetList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _contactRepository.GetItem(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact item)
        {
            var result = await _contactRepository.InsertItem(item);

            if (result == null)
                return BadRequest("Объект не был создан");

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Contact item)
        {
            item.Id = id;
            var result = await _contactRepository.UpdateItem(item);

            if (result == null)
                return BadRequest("Объект не был обновлен");

            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _contactRepository.DeleteItem(id);

            if (!result)
                return BadRequest("Объект не был удален");

            return Ok();
        }
    }
}