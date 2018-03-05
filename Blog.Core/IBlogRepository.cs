using System.Collections.Generic;
using Blog.Core.DomainObjects;

namespace Blog.Core
{
    public interface IBlogRepository
    {
        IList<Post> Posts(int pageNumber, int pageSize);
        int TotalPosts();
    }
}
