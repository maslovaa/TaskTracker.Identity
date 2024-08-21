using TaskTracker.Identity.Model;

namespace TaskTracker.Identity.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
