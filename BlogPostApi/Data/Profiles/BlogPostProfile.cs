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


            CreateMap<BlogPost, BlogPostAddResponseDto>();
        }


    }
}
