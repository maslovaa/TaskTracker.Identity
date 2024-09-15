using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TaskTracker.Identity.Model;
using TaskTracker.Identity.Service.IService;
using TaskTracker.Identity.Service.JwtTokens.JwtGeneration;

namespace TaskTracker.Identity.Service.JwtTokens
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        private static readonly object _semaphoreLock = new object();

        private static readonly Dictionary<string, SemaphoreSlim> _userSemaphores = new Dictionary<string, SemaphoreSlim>();

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<string> GenerateToken(IJwtCreatable jwt, ApplicationUser appUser)
        {
            SemaphoreSlim semaphore;

            lock (_semaphoreLock)
            {
                if (!_userSemaphores.TryGetValue(appUser.Id, out semaphore))
                {
                    semaphore = new SemaphoreSlim(1, 1);
                    _userSemaphores[appUser.Id] = semaphore;
                }
            }

            await semaphore.WaitAsync();
            try
            {
                // Генерация токена
                return jwt.GetToken(appUser, _jwtOptions);
            }
            finally
            {
                semaphore.Release();

                // Удаление семафора
                lock (_semaphoreLock)
                {
                    _userSemaphores.Remove(appUser.Id);
                }
            }

        }
    }
}
