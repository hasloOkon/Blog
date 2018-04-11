using Blog.Core.Models;
using FluentNHibernate.Mapping;

namespace Blog.Core.Mappings
{
    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Id(tag => tag.Id);

            Map(tag => tag.Name)
                .Length(100)
                .Not.Nullable();

            Map(tag => tag.UrlSlug)
                .Length(100)
                .Not.Nullable();

            HasManyToMany(tag => tag.Posts)
                .Cascade.All().Inverse()
                .Table("PostTagMap");
        }
    }
}