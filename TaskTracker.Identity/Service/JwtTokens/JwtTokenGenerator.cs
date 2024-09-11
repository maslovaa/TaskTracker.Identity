using Microsoft.Extensions.Options;
using TaskTracker.Identity.Model;
using TaskTracker.Identity.Service.IService;
using TaskTracker.Identity.Service.JwtTokens.JwtGeneration;

namespace TaskTracker.Identity.Service.JwtTokens
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(IJwtCreatable jwt, ApplicationUser appUser)
        {
            return jwt.GetToken(appUser, _jwtOptions);
        }
    }
}
