using System.ComponentModel.DataAnnotations;

namespace BlogPostApi.Data.DTO
{
    public class RegisterUserDto
    {



        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(5)]
        public string Password { get; set; } = string.Empty;

        [StringLength(50, MinimumLength = 2)]
        public string? FirstName { get; set; } = null;

        [StringLength(50, MinimumLength = 2)]
        public string? LastName { get; set; } = null;
    }
}
