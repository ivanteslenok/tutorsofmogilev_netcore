using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modules.DistrictModule;
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
        private readonly TutorRepository _tutorRepository;
        private readonly DistrictRepository _districtRepository;
        private readonly SpecializationRepository _specializationRepository;
        private readonly SubjectRepository _subjectRepository;
        private readonly PhoneRepository _phoneRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly ImageService _imageService;

        public ResumeController(
            TutorRepository tutorRepository,
            DistrictRepository districtRepository,
            SpecializationRepository specializationRepository,
            SubjectRepository subjectRepository,
            PhoneRepository phoneRepository,
            IMapper mapper,
            IHostingEnvironment appEnvironment,
            ImageService imageService
            )
        {
            _districtRepository = districtRepository;
            _specializationRepository = specializationRepository;
            _subjectRepository = subjectRepository;
            _phoneRepository = phoneRepository;
            _mapper = mapper;
            _tutorRepository = tutorRepository;
            _appEnvironment = appEnvironment;
            _imageService = imageService;
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.Districts = new SelectList(await _districtRepository.GetList(), "Id", "Name");
            ViewBag.Specializations = new MultiSelectList(await _specializationRepository.GetList(), "Id", "Name");
            ViewBag.Subjects = new MultiSelectList(await _subjectRepository.GetList(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ResumeVM resume)
        {
            if (ModelState.IsValid)
            {
                var tutorDTO = _mapper.Map<TutorDTO>(resume);
                tutorDTO.District = await _districtRepository.GetItem(resume.DistrictId);

                var newTutor = await _tutorRepository.InsertItem(tutorDTO);
                await _tutorRepository.UpdateTutorSpecializations(newTutor.Id, resume.SpecializationsIds, new long[] { });
                await _tutorRepository.UpdateTutorSubjects(newTutor.Id, resume.SubjectsIds, new long[] { });
                await _phoneRepository.InsertItem(new Phone
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
                var tutor = await _tutorRepository.GetItem(tutorId);
                var oldPhoto = tutor.PhotoPath;

                if (!string.IsNullOrWhiteSpace(oldPhoto))
                    _imageService.DeleteOldPhoto(oldPhoto);

                var savedPhotoName = await _imageService.SavePhoto(photo);

                await _tutorRepository.SetPhotoPath(tutorId, savedPhotoName);
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