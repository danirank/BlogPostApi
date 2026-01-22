using BlogPostApi.Data.Entities;

namespace BlogPostApi.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateToken(AppUser user);
    }
}
