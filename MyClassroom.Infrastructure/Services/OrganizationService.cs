using MyClassroom.Application.Services;
using Octokit;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyClassroom.Infrastructure.Services
{
    public class OrganizationService : IOrganizationService
    {
        public async Task<IReadOnlyList<Organization>> GetUserOrganisationsAsync(GitHubClient client)
        {
            return await client.Organization.GetAllForCurrent();
        }

        public async Task<Organization> GetUserOrganisationAsync(GitHubClient client, string name)
        {
            return await client.Organization.Get(name);
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListOrganizations(GitHubClient client)
        {
            var organizations = await GetUserOrganisationsAsync(client);
            var roles = organizations                        
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Login
                                });
            return new SelectList(roles, "Value", "Text");
        }
    }
}
