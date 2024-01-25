﻿namespace Bloggie.Web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; } //Guid type makes a property a unique identifier 
        public string? Heading { get; set; }
        public string? PageTitle { get; set; }
        public string? Content { get; set; }
        public string? ShortDescription { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public string? UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? Author { get; set; }
        public bool Visible { get; set; }
        public ICollection<Tag>? Tags { get; set; } //blog post can have multiple/many tags

        public ICollection<BlogPostLike> Likes { get; set; }

    }
}
