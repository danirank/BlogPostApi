namespace BlogPostApi.Data.DTO
{
    public record BlogPostAddResponseDto(int BlogPostId, string Category, string Title, string Content, string UserId);

}
