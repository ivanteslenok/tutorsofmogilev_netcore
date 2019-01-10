using System.Collections.Generic;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.SubjectModule;

namespace TutorsOfMogilev_NetCore.Controllers.API
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    public class SubjectsController : Controller
    {
        private readonly SubjectRepository _subjectRepository;

        public SubjectsController(SubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        public async Task<List<SubjectDTO>> Get()
        {
            return await _subjectRepository.GetList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _subjectRepository.GetItem(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Subject item)
        {
            var result = await _subjectRepository.InsertItem(item);

            if (result == null)
                return BadRequest("Объект не был создан");

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Subject item)
        {
            item.Id = id;
            var result = await _subjectRepository.UpdateItem(item);

            if (result == null)
                return BadRequest("Объект не был обновлен");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _subjectRepository.DeleteItem(id);

            if (!result)
                return BadRequest("Объект не был удален");

            return Ok();
        }
    }
}