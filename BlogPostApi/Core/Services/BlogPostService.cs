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

            var result = await _repo.AddPostAsync(entity);

            if (result is null)
                return ServiceResult<BlogPostAddResponseDto>.Fail("Failed adding post");

            var createdEntity = await _repo.GetPostByIdAsync(result.BlogPostId);

            if (createdEntity is null)
                return ServiceResult<BlogPostAddResponseDto>.Fail("Failed adding post");

            var responseDto = _mapper.Map<BlogPostAddResponseDto>(createdEntity);
            responseDto.Category = createdEntity.Category.CategoryName;

            return createdEntity is not null ?
                ServiceResult<BlogPostAddResponseDto>.Ok(responseDto)
                : ServiceResult<BlogPostAddResponseDto>.Fail("Failed adding post");

        }

        public async Task<ServiceResult<string>> DeletePostAsync(int postId, string userId)
        {
            var entity = await _repo.GetPostByIdAsync(postId);

            if (entity is null)
                return ServiceResult<string>.Fail("Entity with id not found");

            if (entity.UserId != userId)
                return ServiceResult<string>.Fail("Cannot delete someone elses post");


            var result = await _repo.DeletePostAsync(entity);

            return result ?
                 ServiceResult<string>.Ok("Entity deleted succesfully")
                 : ServiceResult<string>.Fail("Something went wrong in repo");


        }

        public async Task<ServiceResult<BlogPostsGetDetailsDto>> GetDetailedPostAsync(int id)
        {
            var result = await _repo.GetDetailedPostAsync(id);
            if (result is null)
                return ServiceResult<BlogPostsGetDetailsDto>.Fail("No entity with id found");

            // var dtoComments = _mapper.Map<List<CommentGetDto>>(result.Comments);

            var resDto = _mapper.Map<BlogPostsGetDetailsDto>(result);

            //resDto.Comments = dtoComments;

            return ServiceResult<BlogPostsGetDetailsDto>.Ok(resDto);

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
