using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyClassroom.Application.Services;
using MyClassroom.MVC.Github;

namespace MyClassroom.MVC.Controllers
{
    [Authorize]
    public class OrganizationsController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IGitHubClientService _gitHubClientService;
        private readonly ITokenService _tokenService;
        private readonly GithubProductHeaderOptions _githubProductHeaderOptions;

        public OrganizationsController(
            IOrganizationService organizationService, 
            IGitHubClientService gitHubClientService, 
            ITokenService tokenService, 
            IOptions<GithubProductHeaderOptions> githubProductHeaderOptions)
        {
            _organizationService = organizationService;
            _gitHubClientService = gitHubClientService;
            _tokenService = tokenService;
            _githubProductHeaderOptions = githubProductHeaderOptions.Value;
        }

        public async Task<IActionResult> Index()
        {
            var session_id = HttpContext?.User?.FindFirst("session_id")?.Value;
            if (Guid.TryParse(session_id, out Guid id))
            {
                var token = await _tokenService.GetTokenById(id);
                var productHeader = _githubProductHeaderOptions.ProductHeader;
                var client = _gitHubClientService.GetClient(token!.AccessToken, productHeader);
                var organizations = await _organizationService.GetUserOrganisationsAsync(client);
                return View(organizations);
            }
            return View();
        }
    }
}
