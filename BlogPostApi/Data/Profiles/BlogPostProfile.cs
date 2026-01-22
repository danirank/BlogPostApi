using AutoMapper;
using BlogPostApi.Data.DTO;
using BlogPostApi.Data.Entities;

namespace BlogPostApi.Data.Profiles
{
    public class BlogPostProfile : Profile
    {

        public BlogPostProfile()
        {

            CreateMap<BlogPostAddDto, BlogPost>();


            CreateMap<BlogPost, BlogPostAddResponseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryName));


            CreateMap<BlogPost, BlogPostsGetDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BlogPostId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryName));


            CreateMap<BlogPostUpdateDto, BlogPost>()
                 .ForMember(d => d.BlogPostId, opt => opt.Ignore())
                 .ForMember(d => d.UserId, opt => opt.Ignore())
                 .ForMember(d => d.User, opt => opt.Ignore())
                 .ForMember(d => d.Category, opt => opt.Ignore())
                 .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BlogPost, BlogPostUpdateResponseDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.BlogPostId))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(d => d.UserId, opt => opt.MapFrom(src => src.UserId));


            CreateMap<BlogPost, BlogPostsGetDetailsDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.BlogPostId))
                .ForMember(d => d.Category, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.User.UserName));
        }


    }
}
