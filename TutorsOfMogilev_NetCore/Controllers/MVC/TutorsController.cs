using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TutorsOfMogilev_NetCore.Services;
using Modules.TutorModule.Filters;
using System;

namespace TutorsOfMogilev_NetCore.Controllers.MVC
{
    public class TutorsController : Controller
    {
        private readonly TutorService _tutorService;

        public TutorsController(TutorService tutorService)
        {
            _tutorService = tutorService;
        }

        [Route("tutors/{City}/{Subject}/page{PageNumber}", Order = 1)]
        [Route("tutors/{City}/page{PageNumber}", Order = 2)]
        [Route("tutors/{City}/{Subject}", Order = 3)]
        [Route("tutors/{City}", Order = 4)]
        [Route("tutors", Order = 5)]
        [Route("", Order = 6)]
        public async Task<IActionResult> List(TutorListFilter filter)
        {
            filter.PageSize = 10;
            filter.IsVisible = true;
            filter.SortDirection = "desc";
            filter.SortBy = "rating";

            var model = await _tutorService.GetListVM(filter);

            return View(model);
        }

        [Route("Tutors/Item/{key}")]
        public async Task<IActionResult> Item(string key)
        {
            var tutorId = Convert.ToInt64(key.Split('-')[0]);
            var model = await _tutorService.GetItemVM(tutorId);

            return View(model);
        }
    }
}