using MyClassroom.Application.Services;
using Octokit;

namespace MyClassroom.Infrastructure.Services
{
    public class GitHubClientService : IGitHubClientService
    {        
        public GitHubClient GetClient(string token, string header) 
        {
            var productInformation = GetProductHeaderValue(header);            
            var credentials = GetCredentials(token);
            return new GitHubClient(productInformation) { Credentials = credentials };
        }

        private static ProductHeaderValue GetProductHeaderValue(string header) => new(header);

        private static Credentials GetCredentials(string token) => new(token);
    }
}
