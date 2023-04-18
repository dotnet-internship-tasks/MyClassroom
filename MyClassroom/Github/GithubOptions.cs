using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using MyClassroom.Application.Services;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MyClassroom.MVC.Github
{
    public class GithubOptions : OAuthOptions
    {
        public GithubOptions()
        {
            AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
            TokenEndpoint = "https://github.com/login/oauth/access_token";
            CallbackPath = new PathString("/signin-github");
            UserInformationEndpoint = "https://api.github.com/user";
            Scope.Add("repo");
            Scope.Add("project");
            Scope.Add("user");
            Scope.Add("read:org");
        }
    }
}
