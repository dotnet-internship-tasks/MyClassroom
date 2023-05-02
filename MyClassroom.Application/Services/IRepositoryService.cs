using Microsoft.AspNetCore.Mvc.Rendering;
using Octokit;

namespace MyClassroom.Application.Services
{
    public interface IRepositoryService
    {
        Task<IEnumerable<SelectListItem>> GetSelectListRepositories(GitHubClient client, int organizationId);
        Task<IReadOnlyList<Repository>> GetUserRepositoriesAsync(GitHubClient client);
    }
}