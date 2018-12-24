using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TutorsOfMogilev_NetCore.Models;

namespace TutorsOfMogilev_NetCore.Components.Menu
{
    public class Menu : ViewComponent
    {
        public IViewComponentResult Invoke(string activeItem)
        {
            var menuModel = new MenuModel();
            menuModel.MenuItems.FirstOrDefault(x => x.Name == activeItem).IsActive = true;

            return View(menuModel);
        }
    }
}
