using Microsoft.AspNetCore.Authentication;
using MyClassroom.Application.Services;
using System.Security.Claims;

namespace MyClassroom.MVC.Auth
{
    public class RoleClaimsTransformation : IClaimsTransformation
    {
        private readonly IUserService _userService;
        private readonly ILogger<RoleClaimsTransformation> _logger;
        public RoleClaimsTransformation(IUserService userService, ILogger<RoleClaimsTransformation> logger) 
        {
            _userService = userService;
            _logger = logger;
        }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var clone = principal.Clone();
            var identity = clone.Identity as ClaimsIdentity;
            var github_id = principal.Claims.SingleOrDefault(c => c.Type == "github_id")?.Value;
            int id;
            if(int.TryParse(github_id, out id))
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user is not null)
                {
                    foreach (var role in user.Roles)
                    {
                        identity?.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                    }
                }
            }

            return clone;
        }
    }
}
