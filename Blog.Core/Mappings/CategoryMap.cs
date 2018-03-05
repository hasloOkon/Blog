﻿using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.DomainObjects;
using FluentNHibernate.Mapping;

namespace Blog.Core.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(category => category.Id);

            Map(category => category.Name)
                .Length(50)
                .Not.Nullable();

            Map(category => category.UrlSlug)
                .Length(50)
                .Not.Nullable();

            Map(category => category.Description)
                .Length(200);

            HasMany(category => category.Posts)
                .Inverse()
                .Cascade.All()
                .KeyColumn("Category");

        }
    }
}