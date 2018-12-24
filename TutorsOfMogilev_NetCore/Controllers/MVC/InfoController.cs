using Microsoft.AspNetCore.Mvc;
using TutorsOfMogilev_NetCore.Models;

namespace TutorsOfMogilev_NetCore.Controllers.MVC
{
    public class InfoController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Tariffs()
        {
            return View();
        }
    }
}