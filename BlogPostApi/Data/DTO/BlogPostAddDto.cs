using System.ComponentModel.DataAnnotations;

namespace BlogPostApi.Data.DTO
{
    public record BlogPostAddDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

    }



}
