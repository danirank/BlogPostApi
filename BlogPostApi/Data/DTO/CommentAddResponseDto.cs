namespace BlogPostApi.Data.DTO
{
    public record CommentAddResponseDto
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; } = string.Empty;

        public int BlogPostId { get; set; }

        public string UserId { get; set; } = string.Empty;

    }
}
