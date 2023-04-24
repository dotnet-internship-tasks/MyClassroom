using Octokit;

namespace MyClassroom.Application.Services
{
    public interface IOrganizationService
    {
        Task<IReadOnlyList<Organization>> GetUserOrganisationsAsync(GitHubClient client);
    }
}
