using Core.Models;
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

            if (User.Identity.IsAuthenticated)
                menuModel.MenuItems.Add(
                    new MenuItem("Administration", "nav__item_administration", false, "Administration", "Index"));

            menuModel.MenuItems.FirstOrDefault(x => x.Name == activeItem).IsActive = true;

            return View(menuModel);
        }
    }
}
