using Blog.Core.Models;

namespace Blog.Core.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Tag GetBySlug(string tagSlug);
    }
}