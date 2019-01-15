using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modules.CityModule;
using Modules.PhoneModule;
using Modules.SpecializationModule;
using Modules.SubjectModule;
using Modules.TutorModule;
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
        public async Task<IActionResult> Add(ResumeVM resume)
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
                    Tutor = _mapper.Map<Tutor>(newTutor),
                    Number = resume.Phone,
                    Operator = resume.PhoneOperator
                });

                TempData["addedTutorId"] = newTutor.Id;

                return RedirectToAction("LoadPhoto");
            }

            return View(resume);
        }

        public IActionResult LoadPhoto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadPhoto(IFormFile photo)
        {
            if (photo != null
                && _imageService.IsImage(photo)
                && TempData["addedTutorId"] != null)
            {
                var tutorId = Convert.ToInt64(TempData["addedTutorId"]);
                var tutor = await _tutorRepo.GetItem(tutorId);
                var oldPhoto = tutor.PhotoPath;

                if (!string.IsNullOrWhiteSpace(oldPhoto))
                    _imageService.DeleteOldPhoto(oldPhoto);

                var savedPhotoName = await _imageService.SavePhoto(photo);

                await _tutorRepo.SetPhotoPath(tutorId, savedPhotoName);
                TempData["addedTutorId"] = null;

                return RedirectToAction("SuccessPhotoLoad");
            }

            return RedirectToAction("FailPhotoLoad");
        }

        public IActionResult SuccessPhotoLoad()
        {
            return View();
        }

        public IActionResult FailPhotoLoad()
        {
            return View();
        }
    }
}