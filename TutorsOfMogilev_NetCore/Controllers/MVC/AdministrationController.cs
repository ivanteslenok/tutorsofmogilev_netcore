using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TutorsOfMogilev_NetCore.Controllers.MVC
{
    public class AdministrationController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}