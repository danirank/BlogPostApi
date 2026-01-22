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

        #region Get all Blogposts
        [HttpGet]
        [SwaggerOperation(Summary = "Returns Blogpost in db",
            Description = "Search by Title or Category(Both optional).")]
        [ProducesResponseType(typeof(BlogPostsGetDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPosts([FromQuery] BlogPostSearchFilterDto filter)
        {
            var result = await _service.GetPostsAsync(filter);

            return Ok(result);
        }

        #endregion


        #region AddBlogPost

        [Authorize]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new blog post.",
            Description = "You have to be logged in to use this endpoint. " +
            "Use the Login endpoint to create jwt token and paste in the authorize..."
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

        #endregion


        #region Update

        [Authorize]
        [HttpPut]
        [SwaggerOperation(
            Summary = "Update a blogpost",
            Description = "Only authorized user can acces this endpoint. " +
            "Only user who created the post can update it. " +
            "All fields are optional (titel, content, categoryId)"
            )]
        [ProducesResponseType(typeof(BlogPostUpdateResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UnauthorizedAccessException), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePost([FromBody] BlogPostUpdateDto dto, int blogPostId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();



            var result = await _service.UpdatePostAsync(dto, blogPostId, userId);

            if (!result.Success)
                return BadRequest(result.ErrorMessages);

            return Ok(result);
        }


        #endregion

    }
}
