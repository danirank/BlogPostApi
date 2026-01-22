using BlogPostApi.Core.Services;
using BlogPostApi.Data.DTO;

namespace BlogPostApi.Core.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<UserResponseDto>> RegisterUserAsync(RegisterUserDto dto);

        Task<ServiceResult<string>> DeleteUserAsync(string id);

        Task<ServiceResult<UserResponseDto>> UpdateUserAsync(string id, UpdateUserDto dto);




    }
}
