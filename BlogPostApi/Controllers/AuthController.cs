using BlogPostApi.Core.Interfaces;
using BlogPostApi.Core.Services;
using BlogPostApi.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogPostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        #region LogIn

        [SwaggerOperation(
            Summary = "Login Endpoint",
            Description = "Login by userName or Email"

            )]
        [ProducesResponseType(typeof(ServiceResult<LoginResponseDto>), StatusCodes.Status200OK, Description = "Logged in succesfully")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status401Unauthorized, Description = "Error / Bad request")]

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var result = await _authService.LoginAsync(loginUserDto);

            return result.Success ? Ok(result) : Unauthorized(result.ErrorMessages);
        }


        #endregion
    }
}
