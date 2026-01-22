using System.ComponentModel.DataAnnotations;

namespace BlogPostApi.Data.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = string.Empty;
    }
}
