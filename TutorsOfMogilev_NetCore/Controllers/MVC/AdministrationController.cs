using Microsoft.AspNetCore.Mvc;

namespace TutorsOfMogilev_NetCore.Controllers.MVC
{
    public class AdministrationController : Controller
    {
        // GET: Administration
        public ActionResult Index()
        {
            return View();
        }
    }
}