

using BlogPostApi.Data.Entities;
using BlogPostApi.Data.Interfaces;

namespace BlogPostApi.Data.Repos
{
    public class BlogPostRepo : IBlogPostRepo
    {
        private readonly AppDbContext _dbContext;

        public BlogPostRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogPost> AddPostAsync(BlogPost post)
        {

            var result = await _dbContext.AddAsync(post);

            await _dbContext.SaveChangesAsync();

            return result.Entity;

        }

        public Task<bool> DeletePostAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> GetAllPostsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePostAsync(int id, BlogPost post)
        {
            throw new NotImplementedException();
        }
    }
}
