using Microsoft.AspNetCore.Mvc;
using TricyleFareAndPassengerManagementSystem.Api.DTOs;
using TricyleFareAndPassengerManagementSystem.Api.Services;

namespace TricyleFareAndPassengerManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Fields

        private readonly IAuthService _authService;

        #endregion Fields

        #region Public Constructors

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        #endregion Public Methods
    }
}