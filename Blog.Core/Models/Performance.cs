using System;

namespace Blog.Core.Models
{
    public class Performance : IKeyedEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Measurements { get; set; }
        public virtual TimeSpan AverageTime { get; set; }
    }
}