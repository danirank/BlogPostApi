using AutoMapper;
using BlogPostApi.Core.Interfaces;
using BlogPostApi.Data.DTO;
using BlogPostApi.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlogPostApi.Core.Services
{

    public class UserService : IUserService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IAuthService authService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authService = authService;


        }

        //Register
        public async Task<ServiceResult<UserResponseDto>> RegisterUserAsync(RegisterUserDto dto)
        {

            var userEntity = _mapper.Map<AppUser>(dto);
            var result = await _userManager.CreateAsync(userEntity, dto.Password);

            if (!result.Succeeded)
            {
                var errorMeassages = result.Errors.Select(e => e.Description).ToList();
                return ServiceResult<UserResponseDto>.Fail(errorMeassages);
            }

            var response = _mapper.Map<UserResponseDto>(userEntity);

            return ServiceResult<UserResponseDto>.Ok(response);
        }

        //Delete - TODO: Delete byEmail?? 
        public async Task<ServiceResult<string>> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return ServiceResult<string>.Fail(new List<string> { "User not found" });

            var result = await _userManager.DeleteAsync(user);
            var errors = result.Errors.Select(e => e.Description).ToList();

            return result.Succeeded
                ? ServiceResult<string>.Ok(userId)
                : ServiceResult<string>.Fail(errors);


        }

        //Update
        public async Task<ServiceResult<UserResponseDto>> UpdateUserAsync(string id, UpdateUserDto dto)
        {

            var userEntity = await _userManager.FindByIdAsync(id);

            if (userEntity is null)
            {
                return ServiceResult<UserResponseDto>.Fail(new List<string> { "User not found" });
            }

            _mapper.Map(dto, userEntity);


            var result = await _userManager.UpdateAsync(userEntity);



            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ServiceResult<UserResponseDto>.Fail(errors);
            }

            var response = _mapper.Map<UserResponseDto>(userEntity);

            return ServiceResult<UserResponseDto>.Ok(response);




        }

    }
}
