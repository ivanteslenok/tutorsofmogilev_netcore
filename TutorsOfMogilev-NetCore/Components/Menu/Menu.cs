using Microsoft.AspNetCore.Mvc;
using TutorsOfMogilev_NetCore.Models;

namespace TutorsOfMogilev_NetCore.Components.Menu
{
    public class Menu : ViewComponent
    {
        public IViewComponentResult Invoke(MenuModel menuModel)
        {
            return View(menuModel);
        }
    }
}
