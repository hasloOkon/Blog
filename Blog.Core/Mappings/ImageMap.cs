using Blog.Core.Models;
using FluentNHibernate.Mapping;

namespace Blog.Core.Mappings
{
    public class ImageMap : ClassMap<Image>
    {
        public ImageMap()
        {
            Id(image => image.Id);

            Map(image => image.UploadTime)
                .Not.Nullable();

            Map(image => image.Data)
                .Length(64 * 1024 * 1024)
                .LazyLoad()
                .Not.Nullable();
        }
    }
}