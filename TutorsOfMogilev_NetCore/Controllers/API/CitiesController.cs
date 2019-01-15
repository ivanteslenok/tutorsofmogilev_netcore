using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Modules.CityModule;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TutorsOfMogilev_NetCore.Controllers.API
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private readonly CityRepository _CityRepository;

        public CitiesController(CityRepository CityRepository)
        {
            _CityRepository = CityRepository;
        }

        [HttpGet]
        public async Task<List<CityDTO>> Get()
        {
            return await _CityRepository.GetList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _CityRepository.GetItem(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] City item)
        {
            var result = await _CityRepository.InsertItem(item);

            if (result == null)
                return BadRequest("Объект не был создан");

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] City item)
        {
            item.Id = id;
            var result = await _CityRepository.UpdateItem(item);

            if (result == null)
                return BadRequest("Объект не был обновлен");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _CityRepository.DeleteItem(id);

            if (!result)
                return BadRequest("Объект не был удален");

            return Ok();
        }
    }
}
