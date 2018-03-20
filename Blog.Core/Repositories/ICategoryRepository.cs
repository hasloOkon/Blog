using Blog.Core.Models;

namespace Blog.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetBySlug(string categorySlug);
    }
}