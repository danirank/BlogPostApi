using BlogPostApi.Data.Entities;

namespace BlogPostApi.Data.Interfaces
{
    public interface IBlogPostRepo
    {


        Task<IEnumerable<BlogPost>> GetAllPostsAsync();
        Task<BlogPost> AddPostAsync(BlogPost post);

        Task<bool> DeletePostAsync(int id);


        Task<bool> UpdatePostAsync(int id, BlogPost post);





    }
}
