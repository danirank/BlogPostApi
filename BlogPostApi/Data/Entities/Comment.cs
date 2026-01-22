using System.ComponentModel.DataAnnotations;

namespace BlogPostApi.Data.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string CommentText { get; set; } = string.Empty;

        public int? BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;

        public AppUser User { get; set; } = null!;
    }
}