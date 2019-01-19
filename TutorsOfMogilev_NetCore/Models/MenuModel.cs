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
                new MenuItem("О нас", "nav__item_about-us", false, "Info", "AboutUs"),
                new MenuItem("Преимущества", "nav__item_advantages", false, "Info", "Advantages"),
                new MenuItem("Оставить заявку", "nav__item_resume", false, "Resume", "Add")
            };
        }
    }
}