using BlogPostApi.Core.Services;
using BlogPostApi.Data.DTO;

namespace BlogPostApi.Core.Interfaces
{
    public interface ICommentService
    {
        Task<ServiceResult<CommentAddResponseDto>> AddComment(int postId, CommentAddDto dto, string userId);
    }
}
