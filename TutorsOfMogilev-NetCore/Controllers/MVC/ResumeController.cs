using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Modules.ContactTypeModule;
using Modules.DistrictModule;
using Modules.SpecializationModule;
using Modules.SubjectModule;
using TutorsOfMogilev_NetCore.Models;

namespace TutorsOfMogilev_NetCore.Controllers.MVC
{
    public class ResumeController : Controller
    {
        private readonly DistrictRepository _districtRepository;
        private readonly SpecializationRepository _specializationRepository;
        private readonly SubjectRepository _subjectRepository;
        private readonly ContactTypeRepository _contactTypeRepository;

        public ResumeController(
            DistrictRepository districtRepository,
            SpecializationRepository specializationRepository,
            SubjectRepository subjectRepository,
            ContactTypeRepository contactTypeRepository
            )
        {
            _districtRepository = districtRepository;
            _specializationRepository = specializationRepository;
            _subjectRepository = subjectRepository;
            _contactTypeRepository = contactTypeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ResumePageVM
            {
                Menu = new MenuModel { Resume = { IsActive = true } },
                Resume = new ResumeVM
                {
                    Districts = await _districtRepository.GetList(),
                    Specializations = await _specializationRepository.GetList(),
                    Subjects = await _subjectRepository.GetList(),
                    ContactTypes = await _contactTypeRepository.GetList()
                }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddResume(ResumeVM resume)
        {
            //todo
            if (ModelState.IsValid)
            {
                var newTutor = new Tutor();
            }

            return RedirectToAction("LoadPhoto");
        }
        
        //todo
        [HttpPost]
        public bool LoadPhoto()
        {
            return false;
        }
    }
}