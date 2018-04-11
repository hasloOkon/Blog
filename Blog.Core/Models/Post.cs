using System;
using System.Collections.Generic;

namespace Blog.Core.Models
{
    public class Post : IKeyedEntity
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual string Content { get; set; }

        public virtual string UrlSlug { get; set; }

        public virtual DateTime PostedOn { get; set; }

        public virtual DateTime? ModifiedOn { get; set; }

        public virtual Category Category { get; set; }

        public virtual IList<Tag> Tags { get; set; }
    }
}