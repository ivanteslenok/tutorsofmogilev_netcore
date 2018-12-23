using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace TutorsOfMogilev_NetCore.Components.Menu
{
    public class MenuItemComponent : ViewComponent
    {
        public IViewComponentResult Invoke(MenuItem menuItem)
        {
            return View(menuItem);
        }
    }
}