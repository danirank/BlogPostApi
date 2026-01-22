using BlogPostApi.Data.DTO;
using BlogPostApi.Data.Entities;
using BlogPostApi.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

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

            var result = await _dbContext.BlogPosts.AddAsync(post);

            await _dbContext.SaveChangesAsync();

            return result.Entity;

        }

        public async Task<bool> CategoryExists(int categoryId)
        {
            return await _dbContext.Categories.AnyAsync(c => c.CategoryId == categoryId);
        }

        public async Task<bool> DeletePostAsync(BlogPost entity)
        {
            var comments = _dbContext.Comments.Where(c => c.BlogPostId == entity.BlogPostId);
            _dbContext.Comments.RemoveRange(comments);
            _dbContext.Remove(entity);
            var res = await _dbContext.SaveChangesAsync();

            return res > 0 ? true : false;
        }

        public async Task<List<BlogPost>> GetPostsAsync(BlogPostSearchFilterDto filter)
        {

            var query = _dbContext.BlogPosts
                .Include(c => c.Category)
                .Include(u => u.User)
                .AsQueryable()
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(bp => bp.Title.Contains(filter.Title));


            if (!string.IsNullOrWhiteSpace(filter.Category))
                query = query.Where(bp => bp.Category.CategoryName.Contains(filter.Category));

            return await query.ToListAsync();

        }

        public async Task<BlogPost?> GetPostByIdAsync(int id)
        {
            return await _dbContext.BlogPosts.AsNoTracking()
                .Include(c => c.Category)
                .FirstOrDefaultAsync(p => p.BlogPostId == id);
        }

        public async Task<BlogPost?> GetDetailedPostAsync(int id)
        {
            return await _dbContext.BlogPosts
                .AsNoTracking()
                .Include(c => c.Category)
                .Include(u => u.User)
                .Include(c => c.Comments).ThenInclude(u => u.User)
                .SingleOrDefaultAsync(p => p.BlogPostId == id);

        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }




    }
}
