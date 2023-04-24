using Microsoft.AspNetCore.Mvc.Rendering;
using Octokit;

namespace MyClassroom.Application.Services
{
    public interface IOrganizationService
    {
        Task<IReadOnlyList<Organization>> GetUserOrganisationsAsync(GitHubClient client);
        Task<Organization> GetUserOrganisationAsync(GitHubClient client, string name);
        Task<IEnumerable<SelectListItem>> GetSelectListOrganizations(GitHubClient client);
    }
}
