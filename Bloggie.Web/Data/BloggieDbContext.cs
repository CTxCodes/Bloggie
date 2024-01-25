using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class BloggieDbContext : DbContext //When creating a DbContext class you must inherit the DbContext class from the EntityFrameWorkCore package
    {
        public BloggieDbContext(DbContextOptions<BloggieDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPost { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet <BlogPostLike> BlogPostLike { get; set; }

        public DbSet<BlogPostComment> BlogPostComments { get; set; }
    }
}
