using Blog.Core.Models;
using FluentNHibernate.Mapping;

namespace Blog.Core.Mappings
{
    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Id(post => post.Id);
            
            Map(post => post.Title)
                .Length(500)
                .Not.Nullable();

            Map(post => post.ShortDescription)
                .Length(5000)
                .Not.Nullable();

            Map(post => post.Content)
                .Length(5000)
                .Not.Nullable();

            Map(post => post.Meta)
                .Length(1000)
                .Not.Nullable();

            Map(post => post.UrlSlug)
                .Length(200)
                .Not.Nullable();

            Map(post => post.Published)
                .Not.Nullable();

            Map(post => post.PostedOn)
                .Not.Nullable();

            Map(post => post.Modified);

            References(post => post.Category)
                .Column("Category")
                .Not.Nullable();

            HasManyToMany(post => post.Tags)
                .Table("PostTagMap");
        }
    }
}