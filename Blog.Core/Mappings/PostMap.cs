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
                .Length(100)
                .Not.Nullable();

            Map(post => post.Description)
                .Length(255)
                .Not.Nullable();

            Map(post => post.Content)
                .Length(5000)
                .Not.Nullable();

            Map(post => post.UrlSlug)
                .Length(100)
                .Not.Nullable();

            Map(post => post.PostedOn)
                .Not.Nullable();

            Map(post => post.ModifiedOn);

            References(post => post.Category)
                .Column("CategoryId")
                .Not.Nullable();

            HasManyToMany(post => post.Tags)
                .Table("PostTagMap");
        }
    }
}