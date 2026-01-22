namespace BlogPostApi.Data.DTO
{
    public record BlogPostUpdateDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? CategoryId { get; set; }
    }



}
