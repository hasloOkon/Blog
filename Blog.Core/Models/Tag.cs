﻿using System.Collections.Generic;

namespace Blog.Core.Models
{
    public class Tag : IKeyedEntity
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string UrlSlug { get; set; }

        public virtual IList<Post> Posts { get; set; }
    }
}