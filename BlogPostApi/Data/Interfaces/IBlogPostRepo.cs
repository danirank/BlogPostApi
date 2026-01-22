using BlogPostApi.Data.DTO;
using BlogPostApi.Data.Entities;

namespace BlogPostApi.Data.Interfaces
{
    public interface IBlogPostRepo
    {


        Task<List<BlogPost>> GetPostsAsync(BlogPostSearchFilterDto filter);
        Task<BlogPost> AddPostAsync(BlogPost post);

        Task<bool> DeletePostAsync(BlogPost entity);

        Task<BlogPost?> GetPostByIdAsync(int id);

        Task<BlogPost?> GetDetailedPostAsync(int id);

        Task<bool> CategoryExists(int categoryId);

        Task<int> SaveChangesAsync();

    }
}
