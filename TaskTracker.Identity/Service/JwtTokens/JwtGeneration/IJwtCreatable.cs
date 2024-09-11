using Microsoft.Extensions.Options;
using TaskTracker.Identity.Model;

namespace TaskTracker.Identity.Service.JwtTokens.JwtGeneration
{
    public interface IJwtCreatable
    {
        string GetToken(ApplicationUser applicationUser, JwtOptions jwtOptions);
    }
}
