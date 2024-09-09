using Microsoft.AspNetCore.Mvc;
using TaskTracker.Identity.Model.Dto;
using TaskTracker.Identity.Service.IService;

namespace TaskTracker.Identity.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        protected ResponseDto _response;

        public AuthAPIController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {

            var result = await _authService.Register(model);
            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                _response.ErrorMessage = result.ErrorMessage;
                return BadRequest(_response);
            }
            _response.Token = result.Token;
            _response.UserId = result.UserId;

            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var authResponse = await _authService.Login(model);
            if (!string.IsNullOrEmpty(authResponse.ErrorMessage))
            {
                _response.ErrorMessage = authResponse.ErrorMessage;
                return BadRequest(_response);
            }
            _response.Token = authResponse.Token;
            _response.UserId = authResponse.UserId;

            return Ok(_response);

        }

    }
}
