using BlogPostApi.Core.Interfaces;
using BlogPostApi.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace BlogPostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {

        private readonly IBlogPostService _service;

        public BlogPostController(IBlogPostService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a blog post.",
            Description = "You have to be logged in to use this endpoint. Use the Login endpoint to create jwt token and paste in the authorize..."
            )]
        [ProducesResponseType(typeof(BlogPostAddResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, Description = "Check input")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Description = "Not Authorized")]
        public async Task<IActionResult> AddBlogPost([FromBody] BlogPostAddDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            var result = await _service.AddBlogPostAsync(dto, userId);

            if (!result.Success)
                return BadRequest(result.ErrorMessages);

            return StatusCode(StatusCodes.Status201Created, result.Data);
        }


    }
}
