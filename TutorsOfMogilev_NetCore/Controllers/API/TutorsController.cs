using Core.Models;
using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.TutorModule;
using Modules.TutorModule.Filters;
using System.Linq;
using System.Threading.Tasks;
using TutorsOfMogilev_NetCore.Models;
using TutorsOfMogilev_NetCore.Services;

namespace TutorsOfMogilev_NetCore.Controllers.API
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    public class TutorsController : Controller
    {
        private readonly TutorRepository _tutorRepository;
        private readonly ImageService _imageService;

        public TutorsController(TutorRepository tutorRepository, ImageService imageService)
        {
            _tutorRepository = tutorRepository;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<ListModel<TutorDTO>> Get([FromQuery] TutorListFilter filter)
        {
            return await _tutorRepository.GetList(filter);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _tutorRepository.GetItem(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TutorDTO item)
        {
            if (!ModelState.IsValid)
                throw new System.Exception(
                    ModelState.Values
                        .Select(x =>
                            x.Errors
                            .Select(y => y.ErrorMessage)
                                .Aggregate((a, b) => $"{a} \n {b}")
                            )
                        .Aggregate((a, b) => $"{a} \n {b}"));

            var result = await _tutorRepository.InsertItem(item);

            if (result == null)
                return BadRequest("Объект не был создан");

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TutorDTO item)
        {
            item.Id = id;
            var result = await _tutorRepository.UpdateItem(item);

            if (result == null)
                return BadRequest("Объект не был обновлен");

            return Ok(result);
        }

        [HttpPut("{id}/subjects")]
        public async Task<IActionResult> UpdateTutorSubjects(int id, [FromBody] UpdateManytoManyModel data)
        {
            var result = await _tutorRepository.UpdateTutorSubjects(id, data.added, data.deleted);

            if (!result)
                return BadRequest("Объект не был обновлен");

            return Ok(result);
        }

        [HttpPut("{id}/specializations")]
        public async Task<IActionResult> UpdateTutorSpecializations(int id, [FromBody] UpdateManytoManyModel data)
        {
            var result = await _tutorRepository.UpdateTutorSpecializations(id, data.added, data.deleted);

            if (!result)
                return BadRequest("Объект не был обновлен");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tutor = await _tutorRepository.GetItem(id);

            if (tutor != null && !string.IsNullOrWhiteSpace(tutor.PhotoPath))
            {
                _imageService.DeleteOldPhoto(tutor.PhotoPath);
            }

            var result = await _tutorRepository.DeleteItem(id);

            if (!result)
                return BadRequest("Объект не был удален");

            return Ok();
        }
    }
}