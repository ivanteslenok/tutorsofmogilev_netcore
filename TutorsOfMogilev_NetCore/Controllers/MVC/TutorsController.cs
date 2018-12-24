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

        [Route("Tutors/{Subject}/Page{PageNumber}", Order = 1)]
        [Route("Tutors/Page{PageNumber}", Order = 2)]
        [Route("Tutors/{Subject}", Order = 3)]
        [Route("", Order = 4)]
        [Route("Tutors", Order = 5)]
        public async Task<IActionResult> List(TutorListFilter filter)
        {
            filter.PageSize = 5;
            filter.IsVisible = true;
            filter.SortDirection = "desc";
            filter.SortBy = "rating";
            filter.Subject = RouteData.Values["Subject"]?.ToString();

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