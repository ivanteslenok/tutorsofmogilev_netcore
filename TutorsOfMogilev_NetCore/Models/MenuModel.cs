using Core.Models;
using System.Collections.Generic;

namespace TutorsOfMogilev_NetCore.Models
{
    public class MenuModel
    {
        public List<MenuItem> MenuItems { get; set; }

        public MenuModel()
        {
            MenuItems = new List<MenuItem>
            {
                new MenuItem("Репетиторы", "nav__item_tutors", false, "Tutors", "List"),
                new MenuItem("Контакты", "nav__item_contacts", false, "Info", "Contacts"),
                new MenuItem("О нас", "nav__item_about-us", false, "Info", "AboutUs"),
                new MenuItem("Тарифы", "nav__item_tariff", false, "Info", "Tariffs"),
                new MenuItem("Оставить заявку", "nav__item_resume", false, "Resume", "Add")
            };
            
            //if (User.IsInRole("Admin"))
                MenuItems.Add(new MenuItem("Administration", "nav__item_administration", false, "Administration", "Index"));
        }
    }
}