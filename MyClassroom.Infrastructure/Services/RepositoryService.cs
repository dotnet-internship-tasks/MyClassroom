using Microsoft.AspNetCore.Mvc.Rendering;
using MyClassroom.Application.Services;
using Octokit;

namespace MyClassroom.Infrastructure.Services
{
    public class RepositoryService : IRepositoryService
    {
        public async Task<IReadOnlyList<Repository>> GetUserRepositoriesAsync(GitHubClient client)
        {
            return await client.Repository.GetAllForCurrent();
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListRepositories(GitHubClient client, int organizationId)
        {
            var repositories = await GetUserRepositoriesAsync(client);
            var selectList = repositories
                        .Where(r => r.Owner.Id == organizationId && r.IsTemplate == true)
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                });
            return new SelectList(selectList, "Value", "Text");
        }
    }
}
