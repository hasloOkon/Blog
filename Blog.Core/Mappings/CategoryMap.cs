using Blog.Core.Models;
using FluentNHibernate.Mapping;

namespace Blog.Core.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(category => category.Id);

            Map(category => category.Name)
                .Length(100)
                .Not.Nullable();

            Map(category => category.UrlSlug)
                .Length(100)
                .Not.Nullable();

            Map(category => category.Description)
                .Length(255);

            HasMany(category => category.Posts)
                .Inverse()
                .Cascade.All()
                .KeyColumn("CategoryId");

        }
    }
}