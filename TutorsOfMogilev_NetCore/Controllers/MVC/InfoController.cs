using Microsoft.AspNetCore.Mvc;

namespace TutorsOfMogilev_NetCore.Controllers.MVC
{
    public class InfoController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Advantages()
        {
            return View();
        }
    }
}