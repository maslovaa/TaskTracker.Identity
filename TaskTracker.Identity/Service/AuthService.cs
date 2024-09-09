using Microsoft.AspNetCore.Identity;
using TaskTracker.Identity.Data;
using TaskTracker.Identity.Model.Dto;
using TaskTracker.Identity.Model;
using TaskTracker.Identity.Service.IService;

namespace TaskTracker.Identity.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
        }

        public async Task<AuthResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var authResponseDto = new AuthResponseDto();

            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                authResponseDto.ErrorMessage = "The user was not found or the password was incorrect";

                return authResponseDto;
            }

            //если пользователь был найден, создаём jwt токен
            var token = _jwtTokenGenerator.GenerateToken(user);

            authResponseDto.Token = token;
            authResponseDto.UserId = Guid.Parse(user.Id);

            return authResponseDto;
        }

        public async Task<AuthResponseDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            var authResponseDto = new AuthResponseDto();

            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.UserName,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                Surname = registrationRequestDto.Surname,
                Patronymic = registrationRequestDto?.Patronymic
                
            };

            try
            {
                
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var newUser = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.UserName);

                    //создаём jwt токен для зарегистрированного пользователя
                    var token = _jwtTokenGenerator.GenerateToken(newUser);


                    authResponseDto.Token= token;
                    authResponseDto.UserId = Guid.Parse(user.Id);  

                    return authResponseDto;

                }
                else
                {
                    authResponseDto.ErrorMessage = result.Errors.FirstOrDefault().Description;

                    return authResponseDto;
                }

            }
            catch (Exception ex)
            {
                authResponseDto.ErrorMessage = $"Error Encountered: {ex.Message}";

                return authResponseDto;
            }
        }
    }
}
