namespace BlogPostApi.Data.DTO
{
    public class BlogPostUpdateResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

    }
}
