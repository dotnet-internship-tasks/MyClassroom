using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyClassroom.Application.Services;
using MyClassroom.MVC.Github;

namespace MyClassroom.MVC.Controllers
{
    public class AuthController : Controller
    {
        public async Task LoginAsync(string? returnUrl)
        {
            await HttpContext.ChallengeAsync(GithubDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = returnUrl ?? "/",
                IsPersistent = true
            });
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string firstName, string lastName)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("firstName", firstName);
                HttpContext.Session.SetString("lastName", lastName);
                return Redirect("/Auth/Login");
            }
            return View();
        }
    }
}
