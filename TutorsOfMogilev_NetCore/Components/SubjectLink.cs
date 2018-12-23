using Microsoft.AspNetCore.Mvc;

namespace TutorsOfMogilev_NetCore.Components
{
    public class SubjectLink : ViewComponent
    {
        public IViewComponentResult Invoke(string subjectName = null, string currentSubj = null)
        {
            ViewBag.SubjectName = subjectName;
            ViewBag.CurrentSubj = currentSubj;

            return View();
        }
    }
}
