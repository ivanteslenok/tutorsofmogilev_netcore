using System.Collections.Generic;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.DistrictModule;

namespace TutorsOfMogilev_NetCore.Controllers.API
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    public class DistrictsController : Controller
    {
        private readonly DistrictRepository _districtRepository;

        public DistrictsController(DistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        [HttpGet]
        public async Task<List<DistrictDTO>> Get()
        {
            return await _districtRepository.GetList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _districtRepository.GetItem(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] District item)
        {
            var result = await _districtRepository.InsertItem(item);

            if (result == null)
                return BadRequest("Объект не был создан");

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] District item)
        {
            item.Id = id;
            var result = await _districtRepository.UpdateItem(item);

            if (result == null)
                return BadRequest("Объект не был обновлен");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _districtRepository.DeleteItem(id);

            if (!result)
                return BadRequest("Объект не был удален");

            return Ok();
        }
    }
}