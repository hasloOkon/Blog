using System.Collections.Generic;
using System.Linq;
using Blog.Core.Models;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Blog.Core.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(ISession session)
            : base(session)
        {
        }

        public IList<Image> FetchData(IEnumerable<Image> images)
        {
            var result = (Image)null;
            var ids = images.Select(image => image.Id).ToArray();

            return Session.QueryOver<Image>()
                .Where(image => image.Id.IsIn(ids))
                .SelectList(list => list
                    .Select(image => image.Id).WithAlias(() => result.Id)
                    .Select(image => image.UploadTime).WithAlias(() => result.UploadTime)
                    .Select(image => image.Data).WithAlias(() => result.Data))
                .TransformUsing(Transformers.AliasToBean<Image>())
                .List<Image>();
        }
    }
}