using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyClassroom.Application.Services;
using MyClassroom.Application.ViewModels;
using MyClassroom.MVC.Github;

namespace MyClassroom.MVC.Controllers
{
    [Authorize]
    public class AssignmentsController : Controller
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IRepositoryService _repositoryService;
        private readonly IClassroomService _classroomService;
        private readonly IGitHubClientService _gitHubClientService;
        private readonly ITokenService _tokenService;
        private readonly GithubProductHeaderOptions _githubProductHeaderOptions;

        public AssignmentsController(
            IAssignmentService assignmentService, 
            IRepositoryService repositoryService,
            IClassroomService classroomService,
            IGitHubClientService gitHubClientService, 
            ITokenService tokenService, 
            IOptions<GithubProductHeaderOptions> githubProductHeaderOptions)
        {
            _assignmentService = assignmentService;
            _repositoryService = repositoryService;
            _classroomService = classroomService;
            _gitHubClientService = gitHubClientService;
            _tokenService = tokenService;
            _githubProductHeaderOptions = githubProductHeaderOptions.Value;
        }

        public async Task<IActionResult> Index(int id)
        {
            var assignments = await _assignmentService.GetAssignmentsAsync(id);
            ViewBag.ClassroomId = id;
            var classroom = await _classroomService.GetClassroomAsync(id);
            ViewBag.ClassroomName = classroom.Name;
            return View(assignments);
        }

        public async Task<IActionResult> Create(int classroomId)
        {
            var session_id = HttpContext?.User?.FindFirst("session_id")?.Value;
            if (Guid.TryParse(session_id, out Guid id))
            {
                var token = await _tokenService.GetTokenById(id);
                var productHeader = _githubProductHeaderOptions.ProductHeader;
                var client = _gitHubClientService.GetClient(token!.AccessToken, productHeader);                
                var classroom = await _classroomService.GetClassroomAsync(classroomId);                
                ViewBag.ClassId = classroomId;
                var repositories = await _repositoryService.GetSelectListRepositories(client, classroom.OrganizationId);
                ViewBag.RepositoryCount = repositories.Count();
                var newAssignment = new NewAssignmentViewModel()
                {
                    RepositoriesViewModel = new RepositoriesViewModel()
                    {
                        Repositories = repositories
                    }
                };
                return View(newAssignment);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int classroomId, NewAssignmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newAssignment = await _assignmentService.CreateAssignmentAsync(classroomId, viewModel);
                return RedirectToAction(nameof(Index), new { id = classroomId });
            }
            return View();
        }
    }
}
