using Microsoft.AspNetCore.Mvc.Rendering;
using Octokit;

namespace MyClassroom.Application.Services
{
    public interface IOrganizationService
    {
        Task<IReadOnlyList<Organization>> GetUserOrganisationsAsync(GitHubClient client);
        Task<IEnumerable<SelectListItem>> GetSelectListOrganizations(GitHubClient client);
    }
}
