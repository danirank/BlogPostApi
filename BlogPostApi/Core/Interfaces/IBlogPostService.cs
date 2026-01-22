using BlogPostApi.Core.Services;
using BlogPostApi.Data.DTO;

namespace BlogPostApi.Core.Interfaces
{
    public interface IBlogPostService
    {
        Task<ServiceResult<BlogPostAddResponseDto>> AddBlogPostAsync(BlogPostAddDto dto, string userId);

        Task<ServiceResult<List<BlogPostsGetDto>>> GetPostsAsync(BlogPostSearchFilterDto filter);

        Task<ServiceResult<BlogPostUpdateResponseDto>> UpdatePostAsync(BlogPostUpdateDto dto, int postId, string userId);

        Task<ServiceResult<BlogPostsGetDetailsDto>> GetDetailedPostAsync(int id);
    }
}
