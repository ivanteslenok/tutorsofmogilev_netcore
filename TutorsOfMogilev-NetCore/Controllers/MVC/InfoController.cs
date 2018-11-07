using Microsoft.AspNetCore.Mvc;
using TutorsOfMogilev_NetCore.Models;

namespace TutorsOfMogilev_NetCore.Controllers.MVC
{
    public class InfoController : Controller
    {
        public IActionResult AboutUs()
        {
            var menu = new MenuModel { AboutUs = { IsActive = true } };
            return View(menu);
        }

        public IActionResult Contacts()
        {
            var menu = new MenuModel { Contacts = { IsActive = true } };
            return View(menu);
        }

        public IActionResult Tariffs()
        {
            var menu = new MenuModel { Tariffs = { IsActive = true } };
            return View(menu);
        }
    }
}