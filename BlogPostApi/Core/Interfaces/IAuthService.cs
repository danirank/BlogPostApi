using BlogPostApi.Core.Services;
using BlogPostApi.Data.DTO;
using BlogPostApi.Data.Entities;

namespace BlogPostApi.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateToken(AppUser user);
        Task<ServiceResult<LoginResponseDto>> LoginAsync(LoginUserDto dto);

    }
}
