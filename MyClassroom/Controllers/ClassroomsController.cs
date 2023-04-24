using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyClassroom.Application.Services;
using MyClassroom.Application.ViewModels;
using MyClassroom.MVC.Github;

namespace MyClassroom.MVC.Controllers
{
    [Authorize]
    public class ClassroomsController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IClassroomService _classroomService;
        private readonly IGitHubClientService _gitHubClientService;
        private readonly ITokenService _tokenService;
        private readonly GithubProductHeaderOptions _githubProductHeaderOptions;

        public ClassroomsController(
            IOrganizationService organizationService,
            IClassroomService classroomService, 
            IGitHubClientService gitHubClientService, 
            ITokenService tokenService, 
            IOptions<GithubProductHeaderOptions> githubProductHeaderOptions)
        {
            _organizationService = organizationService;
            _classroomService = classroomService;
            _gitHubClientService = gitHubClientService;
            _tokenService = tokenService;
            _githubProductHeaderOptions = githubProductHeaderOptions.Value;
        }

        public async Task<IActionResult> Index()
        {
            var classrooms = await _classroomService.GetClassroomsAsync();
            return View(classrooms);
        }

        public async Task<IActionResult> Create()
        {
            var session_id = HttpContext?.User?.FindFirst("session_id")?.Value;
            if (Guid.TryParse(session_id, out Guid id))
            {
                var token = await _tokenService.GetTokenById(id);
                var productHeader = _githubProductHeaderOptions.ProductHeader;
                var client = _gitHubClientService.GetClient(token!.AccessToken, productHeader);
                var newClassroom = new NewClassroomViewModel()
                {
                    OrganizationsViewModel = new OrganizationsViewModel()
                    {
                        Organizations = await _organizationService.GetSelectListOrganizations(client)
                    }
                };
                return View(newClassroom);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewClassroomViewModel viewModel)
        {
            var newClassroom = await _classroomService.CreateClassroomAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }        
    }
}
