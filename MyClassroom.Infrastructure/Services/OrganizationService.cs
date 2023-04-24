using MyClassroom.Application.Services;
using Octokit;

namespace MyClassroom.Infrastructure.Services
{
    public class OrganizationService : IOrganizationService
    {
        public async Task<IReadOnlyList<Organization>> GetUserOrganisationsAsync(GitHubClient client)
        {
            return await client.Organization.GetAllForCurrent();
        }
    }
}
