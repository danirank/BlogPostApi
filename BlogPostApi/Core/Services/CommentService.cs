using AutoMapper;
using BlogPostApi.Core.Interfaces;
using BlogPostApi.Data.DTO;
using BlogPostApi.Data.Entities;
using BlogPostApi.Data.Interfaces;

namespace BlogPostApi.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IBlogPostRepo _blogPostRepo;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepo commentRepo, IBlogPostRepo blogPostRepo, IMapper mapper)
        {
            _commentRepo = commentRepo;
            _blogPostRepo = blogPostRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CommentAddResponseDto>> AddComment(int postId, CommentAddDto dto, string userId)
        {
            var postEntity = await _blogPostRepo.GetPostByIdAsync(postId);

            if (postEntity is null)
                return ServiceResult<CommentAddResponseDto>.Fail("The post you are trying to comment does not exist");

            if (postEntity.UserId == userId)
                return ServiceResult<CommentAddResponseDto>.Fail("You can't commment on your own post");

            var commentEntity = new Comment
            {
                UserId = userId,
                CommentText = dto.CommentText,
                BlogPostId = postId,

            };

            await _commentRepo.AddCommentAsync(commentEntity);

            var resDto = _mapper.Map<CommentAddResponseDto>(commentEntity);

            return ServiceResult<CommentAddResponseDto>.Ok(resDto);


        }
    }
}
