using System.Linq;
using Blog.Core.Models;
using NHibernate;

namespace Blog.Core.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(ISession session)
            : base(session)
        {
        }

        public Tag GetBySlug(string tagSlug)
        {
            return Session.Query<Tag>().First(tag => tag.UrlSlug == tagSlug);
        }
    }
}
