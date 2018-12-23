using Microsoft.AspNetCore.Mvc;

namespace TutorsOfMogilev_NetCore.Components.Pagination
{
    public class PaginationOneStepLink : ViewComponent
    {
        public IViewComponentResult Invoke(
            string symbol, 
            string label, 
            int page, 
            string subject,
            bool disabled
            )
        {
            ViewBag.Symbol = symbol;
            ViewBag.Label = label;
            ViewBag.Page = page;
            ViewBag.Subject = subject;
            ViewBag.Disabled = disabled;

            return View();
        }
    }
}
