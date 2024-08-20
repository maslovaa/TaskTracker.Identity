using TaskTracker.Identity.Model.Dto;

namespace TaskTracker.Identity.Service.IService
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Register(RegistrationRequestDto registrationRequestDto);
        Task<AuthResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
