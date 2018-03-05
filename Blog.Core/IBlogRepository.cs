﻿using System.Collections.Generic;
using Blog.Core.DomainObjects;

namespace Blog.Core
{
    public interface IBlogRepository
    {
        IList<Post> Posts(int pageNumber, int pageSize);
        int TotalPosts();

        IList<Post> PostsForCategory(string categorySlug, int pageNumber, int pageSize);
        int TotalPostsForCategory(string categorySlug);
        Category Category(string categorySlug);

        IList<Post> PostsForTag(string tagSlug, int pageNumber, int pageSize);
        int TotalPostsForTag(string tagSlug);
        Tag Tag(string tagSlug);
    }
}
