using Blog.Core.Models;
using NHibernate;

namespace Blog.Core.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(ISession session)
            : base(session)
        {
        }
    }
}