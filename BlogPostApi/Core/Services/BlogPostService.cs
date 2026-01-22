using AutoMapper;
using BlogPostApi.Core.Interfaces;
using BlogPostApi.Data.DTO;
using BlogPostApi.Data.Entities;
using BlogPostApi.Data.Interfaces;

namespace BlogPostApi.Core.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostRepo _repo;

        public BlogPostService(IMapper mapper, IBlogPostRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<ServiceResult<BlogPostAddResponseDto>> AddBlogPostAsync(BlogPostAddDto dto, string UserId)
        {

            var entity = _mapper.Map<BlogPost>(dto);
            entity.UserId = UserId;

            var createdEntity = await _repo.AddPostAsync(entity);

            var responseDto = _mapper.Map<BlogPostAddResponseDto>(createdEntity);

            return createdEntity is not null ?
                ServiceResult<BlogPostAddResponseDto>.Ok(responseDto)
                : ServiceResult<BlogPostAddResponseDto>.Fail("Failed adding post");

        }
    }
}
