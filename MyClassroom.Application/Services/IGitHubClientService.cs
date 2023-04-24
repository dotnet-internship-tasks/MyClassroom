using Octokit;

namespace MyClassroom.Application.Services
{
    public interface IGitHubClientService
    {
        GitHubClient GetClient(string token, string header);
    }
}