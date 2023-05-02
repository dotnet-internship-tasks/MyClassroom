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

        public async Task<IEnumerable<SelectListItem>> GetSelectListOrganizations(GitHubClient client)
        {
            var organizations = await GetUserOrganisationsAsync(client);
            var selectList = organizations                        
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Login
                                });
            return new SelectList(selectList, "Value", "Text");
        }
    }
}
