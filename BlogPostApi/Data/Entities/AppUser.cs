using Microsoft.AspNetCore.Identity;

namespace BlogPostApi.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public List<BlogPost> BlogPosts { get; set; } = new();
    }
}
