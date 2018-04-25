using Blog.Core.Models;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace Blog.Core.Mappings
{
    public class PerformanceMap : ClassMap<Performance>
    {
        public PerformanceMap()
        {
            Id(performance => performance.Id);

            Map(performance => performance.Name)
                .Length(255)
                .Not.Nullable();

            Map(performance => performance.Measurements)
                .Not.Nullable();

            Map(performance => performance.AverageTime)
                .CustomType<TimeAsTimeSpanType>()
                .Not.Nullable();
        }
    }
}