using DataEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TutorsOfMogilev_NetCore.Services;

namespace TutorsOfMogilev_NetCore.Controllers.MVC
{
    public class AdministrationController : Controller
    {
        private readonly ImageService _imageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationContext _db;

        public AdministrationController(ImageService imageService, IWebHostEnvironment webHostEnvironment,
            ApplicationContext applicationContext)
        {
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
            _db = applicationContext;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public void OptimizePhotos()
        {
            var oldPhotos = Directory.GetFiles($@"{_webHostEnvironment.WebRootPath}\uploads\UsersPhotos");
            oldPhotos = oldPhotos.Where(x => !x.EndsWith(".webp")).ToArray();

            foreach (var photo in oldPhotos)
            {
                var stream = new FileStream(photo, FileMode.Open);
                _imageService.SavePhoto(stream, Path.GetFileNameWithoutExtension(photo));
            }

            _db.Tutors
                .Where(x => !string.IsNullOrWhiteSpace(x.PhotoPath))
                .ToList()
                .ForEach(x => x.PhotoPath = Path.GetFileNameWithoutExtension(x.PhotoPath));
            _db.SaveChanges();
        }
    }
}