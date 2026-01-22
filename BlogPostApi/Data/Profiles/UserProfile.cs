using AutoMapper;
using BlogPostApi.Data.DTO;
using BlogPostApi.Data.Entities;

namespace BlogPostApi.Data.Profiles
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {

            CreateMap<UpdateUserDto, AppUser>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<RegisterUserDto, AppUser>();

            CreateMap<AppUser, UserResponseDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()));


        }
    }
}
