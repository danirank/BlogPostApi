namespace BlogPostApi.Data.DTO
{
    public record CommentGetDto
    {
        public string? Id { get; set; }

        public string? UserName { get; set; }

        public string? Content { get; set; }


    }
}
