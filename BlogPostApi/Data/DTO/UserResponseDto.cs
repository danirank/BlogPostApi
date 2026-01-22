namespace BlogPostApi.Data.DTO
{
    public record UserResponseDto
    {
        public string? Id { get; init; } = null;
        public string? UserName { get; init; } = null;
        public string? Email { get; init; } = null;
        public string? FullName { get; init; } = null;
    }


}
