using TaskTracker.Identity.Model;
using TaskTracker.Identity.Service.JwtTokens.JwtGeneration;

namespace TaskTracker.Identity.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IJwtCreatable jwtCreateImplementation, ApplicationUser applicationUser);
    }
}
