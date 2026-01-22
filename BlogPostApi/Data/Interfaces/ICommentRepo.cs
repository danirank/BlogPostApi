using BlogPostApi.Data.Entities;

namespace BlogPostApi.Data.Interfaces
{
    public interface ICommentRepo
    {
        Task<Comment> AddCommentAsync(Comment comment);
    }
}
