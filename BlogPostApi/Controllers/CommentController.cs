using BlogPostApi.Core.Interfaces;
using BlogPostApi.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace BlogPostApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }

        #region AddComment


        [HttpPost]
        #region Doc
        [SwaggerOperation(
            Summary = "Make a comment on someone elses blogpost",
            Description = "You have to be logged in to use this endpoint. " +
            "Use the Login endpoint to create jwt token and paste in the authorize. You can't comment your own post"
            )]
        [ProducesResponseType(typeof(CommentAddResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Description = "Not Authorized")]

        #endregion
        public async Task<IActionResult> AddComment(int postId, [FromBody] CommentAddDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return Unauthorized();



            var result = await _service.AddComment(postId, dto, userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);

        }

        #endregion

    }
}
