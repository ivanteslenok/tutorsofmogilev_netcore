using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modules.TutorModule.Filters;
using System;
using System.Threading.Tasks;
using TutorsOfMogilev_NetCore.Services;

namespace TutorsOfMogilev_NetCore.Controllers.MVC
{
    public class TutorsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly TutorService _tutorService;

        public TutorsController(IConfiguration configuration, TutorService tutorService)
        {
            _configuration = configuration;
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
            model.Tutors.ForEach(x =>
            {
                if (!string.IsNullOrWhiteSpace(x.PhotoPath))
                {
                    var prefix = _configuration.GetSection("PhotoSettings").GetValue<string>("smallPrefix");
                    x.PhotoPath = $"{x.PhotoPath}{prefix}{ImageService.photoExtension}";
                }
            });

            return View(model);
        }

        [Route("Tutors/Item/{key}")]
        public async Task<IActionResult> Item(string key)
        {
            var tutorId = Convert.ToInt64(key.Split('-')[0]);
            var model = await _tutorService.GetItemVM(tutorId);

            if (!string.IsNullOrWhiteSpace(model.PhotoPath))
            {
                var prefix = _configuration.GetSection("PhotoSettings").GetValue<string>("originalPrefix");
                model.PhotoPath = $"{model.PhotoPath}{prefix}{ImageService.photoExtension}";
            }

            return View(model);
        }
    }
}