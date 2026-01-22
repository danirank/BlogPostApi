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

        #region GetBlogposts
        [HttpGet]

        #region Doc

        [SwaggerOperation(Summary = "Returns Blogpost in db",
            Description = "Search by Title or Category (Both optional)")]
        [ProducesResponseType(typeof(BlogPostsGetDto), StatusCodes.Status200OK)]
        #endregion
        public async Task<IActionResult> GetPosts([FromQuery] BlogPostSearchFilterDto filter)
        {
            var result = await _service.GetPostsAsync(filter);

            return Ok(result);
        }

        #endregion


        #region GetBlogPost
        [HttpGet("{id}")]

        #region Doc
        [SwaggerOperation(Summary = "Returns a detailed blogpost",
            Description = "Search bi Blogpost id")]
        [ProducesResponseType(typeof(BlogPostsGetDto), StatusCodes.Status200OK)]
        #endregion
        public async Task<IActionResult> GetBlogPost(int id)
        {
            var result = await _service.GetDetailedPostAsync(id);

            return Ok(result);
        }

        #endregion


        #region AddBlogPost

        [Authorize]
        [HttpPost]

        #region Doc
        [SwaggerOperation(
            Summary = "Create a new blog post.",
            Description = "You have to be logged in to use this endpoint. " +
            "Use the Login endpoint to create jwt token and paste in the authorize..."
            )]
        [ProducesResponseType(typeof(BlogPostAddResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, Description = "Check input")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Description = "Not Authorized")]
        #endregion
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


        #region UpdateBlogPost

        [Authorize]
        [HttpPut]
        #region Doc


        [SwaggerOperation(
            Summary = "Update a blogpost",
            Description = "Only authorized user can acces this endpoint. " +
            "Only user who created the post can update it. " +
            "All fields are optional (titel, content, categoryId)"
            )]
        [ProducesResponseType(typeof(BlogPostUpdateResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UnauthorizedAccessException), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        #endregion
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

        #region DeletePost

        [Authorize]
        [HttpDelete]

        #region Doc 

        [SwaggerOperation(
            Summary = "Delete a blogpost",
            Description = "Only authorized user can acces this endpoint. " +
            "Only user who created the post can delete it."
            )]

        [ProducesResponseType(typeof(UnauthorizedAccessException), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]

        #endregion
        public async Task<IActionResult> DeletePost(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId)) return Unauthorized();

            var result = await _service.DeletePostAsync(postId, userId);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        #endregion


    }
}
