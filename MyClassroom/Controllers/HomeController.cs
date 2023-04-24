using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyClassroom.Application.Services;
using MyClassroom.Application.ViewModels;
using System.Diagnostics;

namespace MyClassroom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenService _tokenService;

        public HomeController(ILogger<HomeController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "user")]
        public async Task<IActionResult> Privacy()
        {
            var session_id = HttpContext?.User?.FindFirst("session_id")?.Value;
            if (Guid.TryParse(session_id, out Guid id))
            {
                var token = await _tokenService.GetTokenById(id);
                return View(token);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}