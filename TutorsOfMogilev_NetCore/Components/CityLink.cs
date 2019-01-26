using Microsoft.AspNetCore.Mvc;

namespace TutorsOfMogilev_NetCore.Components
{
    public class CityLink : ViewComponent
    {
        public IViewComponentResult Invoke(string cityName = null, string currentCity = null)
        {
            ViewBag.CityName = cityName;
            ViewBag.CurrentCity = currentCity;

            return View();
        }
    }
}
