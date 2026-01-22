using System.ComponentModel.DataAnnotations;

namespace BlogPostApi.Data.Entities
{
    public class BlogPost
    {
        [Key]
        public int BlogPostId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public List<Comment> Comments { get; set; } = new();


        public string UserId { get; set; } = string.Empty;

        public AppUser User { get; set; } = null!;


        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

    }
}
