namespace BlogPostApi.Data.DTO
{
    public record BlogPostsGetDetailsDto
    {

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public List<CommentGetDto> Comments { get; set; } = new();

    }
}
