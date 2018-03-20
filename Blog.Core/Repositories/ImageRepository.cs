using Blog.Core.Models;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ISession session;

        public ImageRepository(ISession session)
        {
            this.session = session;
        }

        public Image GetById(int id)
        {
            return session.Query<Image>().First(image => image.Id == id);
        }

        public void Save(Image image)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(image);
                transaction.Commit();
            }
        }

        public IEnumerable<Image> Images()
        {
            return session.Query<Image>();
        }
    }
}