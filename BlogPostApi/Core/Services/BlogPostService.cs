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

        public async Task<ServiceResult<List<BlogPostsGetDto>>> GetPostsAsync(BlogPostSearchFilterDto filter)
        {
            var result = await _repo.GetPostsAsync(filter);

            var resDto = _mapper.Map<List<BlogPostsGetDto>>(result);

            return ServiceResult<List<BlogPostsGetDto>>.Ok(resDto);
        }

        public async Task<ServiceResult<BlogPostUpdateResponseDto>> UpdatePostAsync(BlogPostUpdateDto dto, int postId, string userId)
        {


            var dbEntity = await _repo.GetPostByIdAsync(postId);

            if (dbEntity is null)
                return ServiceResult<BlogPostUpdateResponseDto>.Fail($"Entity with id: {postId} does not exists");

            if (userId != dbEntity.UserId)
                return ServiceResult<BlogPostUpdateResponseDto>.Fail($"The Blogpost you are trying to update is not yours");

            if (dto.CategoryId.HasValue)
            {
                var categoryExists = await _repo.CategoryExists(dto.CategoryId.Value);

                if (!categoryExists)
                    return ServiceResult<BlogPostUpdateResponseDto>.Fail($"Category with id {dto.CategoryId} does not exists");
            }


            _mapper.Map(dto, dbEntity);

            var result = await _repo.SaveChangesAsync();

            if (result == 0)
                return ServiceResult<BlogPostUpdateResponseDto>.Fail("No entity updated");

            var updatedEntity = await _repo.GetPostByIdAsync(dbEntity.BlogPostId);

            var resultDto = _mapper.Map<BlogPostUpdateResponseDto>(updatedEntity);

            return ServiceResult<BlogPostUpdateResponseDto>.Ok(resultDto);
        }
    }
}
