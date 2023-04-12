using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using MyClassroom.Application.Services;
using MyClassroom.Core.Models;
using MyClassroom.MVC.Github;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MyClassroom.MVC.Auth
{
    public class AuthEventsFunctions
    {
        public static async Task OnCreatingTicket(OAuthCreatingTicketContext context)
        {
            var tokenService = context.HttpContext.RequestServices.GetService<ITokenService>();

            using var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
            using var result = await context.Backchannel.SendAsync(request);

            var user = await result.Content.ReadFromJsonAsync<JsonElement>();
            context.RunClaimActions(user);

            var session_id = Guid.Parse(context?.Identity?.Claims?.SingleOrDefault(c => c.Type == "session_id")?.Value ?? "");
            var expireDate = DateTime.UtcNow.AddSeconds(int.Parse(context.TokenResponse.ExpiresIn!));
            var refreshTokenExpireDate = DateTime.UtcNow.AddSeconds(GithubDefaults.RefreshTokenExpireIn);
            await tokenService!.AddToken(new Token()
            {
                Id = session_id,
                AccessToken = context.AccessToken!,
                RefreshToken = context.RefreshToken!,
                ExpireDate = expireDate,
                RefreshTokenExpireDate = refreshTokenExpireDate
            });
        }

        public static async Task OnSigningIn(CookieSigningInContext context)
        {
            var userService = context.HttpContext.RequestServices.GetService<IUserService>();
            var tokenService = context.HttpContext.RequestServices.GetService<ITokenService>();

            var githubId = context.Principal?.Claims?.SingleOrDefault(claim => claim.Type == "github_id")?.Value;

            var firstName = context.HttpContext.Session.GetString("firstName") ?? "";
            var lastName = context.HttpContext.Session.GetString("lastName") ?? "";

            int id;
            if(int.TryParse(githubId, out id))
            {
                User? user = await userService!.GetUserByIdAsync(id);
                if (user is null)
                {
                    user = new User()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Id = int.Parse(githubId)
                    };
                    await userService.AddUser(user);

                    if(!string.IsNullOrWhiteSpace(user.FirstName) && !string.IsNullOrWhiteSpace(user.LastName))
                    {
                        await userService.AddRole(user.Id, "user");
                    }
                }
            }
        }
    }
}
