using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TutorsOfMogilev_NetCore.Models.Account;

namespace TutorsOfMogilev_NetCore.Controllers.MVC
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == _configuration.GetSection("AdminPassword").Value)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, "Admin")
                    };

                    ClaimsIdentity id = new ClaimsIdentity(
                        claims, 
                        "ApplicationCookie", 
                        ClaimsIdentity.DefaultNameClaimType, 
                        ClaimsIdentity.DefaultRoleClaimType
                        );

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                    return RedirectToAction("Index", "Administration");
                }

                ModelState.AddModelError("", "Неверный пароль");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("List", "Tutors");
        }
    }
}