namespace BlogPostApi.Data.DTO
{
    public record BlogPostAddResponseDto
    {
        public int BlogPostId { get; set; }

        public string? Category { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? UserId { get; set; }
    }



}
