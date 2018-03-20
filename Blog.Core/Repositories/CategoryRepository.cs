using System.Linq;
using Blog.Core.Models;
using NHibernate;

namespace Blog.Core.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ISession session)
            : base(session)
        {
        }

        public Category GetBySlug(string categorySlug)
        {
            return Session.Query<Category>().First(category => category.UrlSlug == categorySlug);
        }
    }
}