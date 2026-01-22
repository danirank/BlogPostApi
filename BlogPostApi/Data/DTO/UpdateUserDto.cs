using System.ComponentModel.DataAnnotations;
namespace BlogPostApi.Data.DTO
{

    public class UpdateUserDto
    {

        [StringLength(50, MinimumLength = 2)]
        public string? FirstName { get; set; } = null;

        [StringLength(50, MinimumLength = 2)]
        public string? LastName { get; set; } = null;

        [StringLength(30, MinimumLength = 3)]
        public string? UserName { get; set; } = null;

        [EmailAddress]
        public string? Email { get; set; } = null;

        [StringLength(50, MinimumLength = 5)]
        public string? Password { get; set; } = null;
    }

}
