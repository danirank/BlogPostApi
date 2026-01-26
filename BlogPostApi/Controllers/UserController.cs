using BlogPostApi.Core.Interfaces;
using BlogPostApi.Core.Services;
using BlogPostApi.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace BlogPostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region Register

        [HttpPost]
        #region Doc
        [SwaggerOperation(
            Summary = "Register a new user",
            Description = "Email, UserName and password are required, First name and last name are optional"
            )]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status201Created, Description = "User created succesfully")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, Description = "Validation/Error")]
        #endregion

        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var result = await _userService.RegisterUserAsync(dto);

            if (!result.Success || result.Data is null)
                return BadRequest(result.ErrorMessages);


            Response.Headers.Location = $"/api/users/{result.Data.Id}";
            return StatusCode(201, new { User = result.Data, message = "User created succesfully" });

        }


        #endregion 


        #region Update

        [Authorize]
        [HttpPut]
        #region Doc
        [SwaggerOperation(
            Summary = "Update user by id (string)",
            Description = "All fields are optional, (FirstName, LastName, Email, UserName, Password)"

            )]
        [ProducesResponseType(typeof(ServiceResult<UserResponseDto>), StatusCodes.Status200OK, Description = "User updated succesfully")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest, Description = "Error / Bad request")]
        #endregion

        public async Task<IActionResult> Update(UpdateUserDto dto)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id is null)
                return Unauthorized("Login to update");

            var result = await _userService.UpdateUserAsync(id, dto);

            if (!result.Success)
                return BadRequest(result.ErrorMessages);

            return Ok(result);


        }


        #endregion


        #region Delete

        [HttpDelete]

        #region Doc
        [SwaggerOperation(
            Summary = "Delete user by id (string)",
            Description = "Deletes a user. Use an Id "

            )]
        [ProducesResponseType(StatusCodes.Status204NoContent, Description = "User deleted succesfully")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound, Description = "No user deleted / Error")]
        #endregion
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result.Success)
                return NotFound(result.ErrorMessages);

            return Ok(result.Data);

        }

        #endregion



    }
}
