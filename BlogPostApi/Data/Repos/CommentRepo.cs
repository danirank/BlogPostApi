using BlogPostApi.Data.Entities;
using BlogPostApi.Data.Interfaces;

namespace BlogPostApi.Data.Repos
{
    public class CommentRepo : ICommentRepo
    {
        private readonly AppDbContext _dbContext;


        public CommentRepo(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            var result = await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}
