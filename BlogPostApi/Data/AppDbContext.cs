using BlogPostApi.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogPostApi.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // BlogPost -> Comments (CASCADE)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.BlogPost)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
