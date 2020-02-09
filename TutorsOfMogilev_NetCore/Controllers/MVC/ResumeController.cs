using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modules.CityModule;
using Modules.PhoneModule;
using Modules.SpecializationModule;
using Modules.SubjectModule;
using Modules.TutorModule;
using System.Threading.Tasks;
using TutorsOfMogilev_NetCore.Models;
using TutorsOfMogilev_NetCore.Services;

namespace TutorsOfMogilev_NetCore.Controllers.MVC
{
    public class ResumeController : Controller
    {
        private readonly TutorRepository _tutorRepo;
        private readonly CityRepository _cityRepo;
        private readonly SpecializationRepository _specializationRepo;
        private readonly SubjectRepository _subjectRepo;
        private readonly PhoneRepository _phoneRepo;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;

        public ResumeController(
            TutorRepository tutorRepository,
            CityRepository cityRepository,
            SpecializationRepository specializationRepository,
            SubjectRepository subjectRepository,
            PhoneRepository phoneRepository,
            IMapper mapper,
            ImageService imageService
            )
        {
            _tutorRepo = tutorRepository;
            _cityRepo = cityRepository;
            _specializationRepo = specializationRepository;
            _subjectRepo = subjectRepository;
            _phoneRepo = phoneRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.Cities = new SelectList(await _cityRepo.GetList(), "Id", "Name");
            ViewBag.Specializations = new MultiSelectList(await _specializationRepo.GetList(), "Id", "Name");
            ViewBag.Subjects = new MultiSelectList(await _subjectRepo.GetList(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] ResumeVM resume)
        {
            if (ModelState.IsValid)
            {
                var tutorDTO = _mapper.Map<TutorDTO>(resume);
                tutorDTO.City = await _cityRepo.GetItem(resume.CityId);

                var newTutor = await _tutorRepo.InsertItem(tutorDTO);
                await _tutorRepo.UpdateTutorSpecializations(newTutor.Id, resume.SpecializationsIds, new long[] { });
                await _tutorRepo.UpdateTutorSubjects(newTutor.Id, resume.SubjectsIds, new long[] { });
                await _phoneRepo.InsertItem(new Phone
                {
                    TutorId = newTutor.Id,
                    Number = resume.Phone,
                    Operator = resume.PhoneOperator
                });

                var IsPhotoLoadSuccess = await LoadPhoto(resume.Photo, newTutor.Id);

                if (!IsPhotoLoadSuccess)
                    ModelState.AddModelError("", "Ошибка при загрузке фото");
                else
                    return RedirectToAction("Success");
            }

            ViewBag.Cities = new SelectList(await _cityRepo.GetList(), "Id", "Name");
            ViewBag.Specializations = new MultiSelectList(await _specializationRepo.GetList(), "Id", "Name");
            ViewBag.Subjects = new MultiSelectList(await _subjectRepo.GetList(), "Id", "Name");

            return View(resume);
        }

        public IActionResult Success()
        {
            return View();
        }

        private async Task<bool> LoadPhoto(IFormFile photo, long tutorId)
        {
            if (photo != null && _imageService.IsImage(photo))
            {
                var tutor = await _tutorRepo.GetItem(tutorId);
                var oldPhoto = tutor.PhotoPath;

                if (!string.IsNullOrWhiteSpace(oldPhoto))
                {
                    _imageService.DeleteOldPhoto(oldPhoto);
                }

                var savedPhotoName = _imageService.SavePhoto(photo.OpenReadStream());

                await _tutorRepo.SetPhotoPath(tutorId, savedPhotoName);

                return true;
            }

            return false;
        }
    }
}