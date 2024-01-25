using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            await bloggieDbContext.AddAsync(post);
            await bloggieDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await bloggieDbContext.BlogPost.FindAsync(id);
            if(existingBlog != null)
            {
                bloggieDbContext.BlogPost.Remove(existingBlog);
                await bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPost.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await bloggieDbContext.BlogPost.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await bloggieDbContext.BlogPost.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {

            var existingBlog = await bloggieDbContext.BlogPost.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.Author = blogPost.Author;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.Tags = blogPost.Tags;

                await bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }
    }
}
