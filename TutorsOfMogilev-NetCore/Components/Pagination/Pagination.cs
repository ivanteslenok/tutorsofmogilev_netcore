using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace TutorsOfMogilev_NetCore.Components.Pagination
{
    public class Pagination : ViewComponent
    {
        public IViewComponentResult Invoke(PaginationInfo paginationInfo)
        {
            if (paginationInfo.TotalPages <= 1) return Content(string.Empty);

            return View(paginationInfo);
        }
    }
}