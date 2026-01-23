using System.ComponentModel.DataAnnotations;
namespace BlogPostApi.Data.DTO
{

    public class UpdateUserDto
    {

        [StringLength(50, MinimumLength = 2)]
        public string? FirstName { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string? LastName { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string? UserName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(50, MinimumLength = 5)]
        public string? Password { get; set; }
    }

}
