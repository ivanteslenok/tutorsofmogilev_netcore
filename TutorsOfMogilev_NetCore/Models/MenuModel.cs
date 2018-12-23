using Core.Models;

namespace TutorsOfMogilev_NetCore.Models
{
    public class MenuModel
    {
        public MenuItem Tutors { get; set; }
        public MenuItem Contacts { get; set; }
        public MenuItem AboutUs { get; set; }
        public MenuItem Tariffs { get; set; }
        public MenuItem Resume { get; set; }
        public MenuItem Administration { get; set; }

        public MenuModel()
        {
            Tutors = new MenuItem("Репетиторы", "nav__item_tutors", false, "Tutors", "List");
            Contacts = new MenuItem("Контакты", "nav__item_contacts", false, "Info", "Contacts");
            AboutUs = new MenuItem("О нас", "nav__item_about-us", false, "Info", "AboutUs");
            Tariffs = new MenuItem("Тарифы", "nav__item_tariff", false, "Info", "Tariffs");
            Resume = new MenuItem("Оставить заявку", "nav__item_resume", false, "Resume", "Index");
            Administration = new MenuItem("Administration", "nav__item_administration", false, "Administration", "Index");
        }
    }
}