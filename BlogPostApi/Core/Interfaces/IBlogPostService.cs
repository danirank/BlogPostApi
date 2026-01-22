using BlogPostApi.Core.Services;
using BlogPostApi.Data.DTO;

namespace BlogPostApi.Core.Interfaces
{
    public interface IBlogPostService
    {
        Task<ServiceResult<BlogPostAddResponseDto>> AddBlogPostAsync(BlogPostAddDto dto, string userId);
    }
}
