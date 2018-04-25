using Blog.Core.Aspects;
using Blog.Core.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;

namespace Blog.Core.Repositories
{
    public class PerformanceRepository : Repository<Performance>, IPerformanceRepository
    {
        public PerformanceRepository(ISession session)
            : base(session)
        {
        }

        [Transaction]
        public void AddMeasurement(string performanceName, TimeSpan measuredTime)
        {
            var performance = Session.Query<Performance>().FirstOrDefault(p => p.Name == performanceName);

            if (performance == null)
            {
                performance = new Performance
                {
                    Name = performanceName,
                    Measurements = 1,
                    AverageTime = measuredTime
                };
            }
            else
            {
                var totalTicks = performance.AverageTime.Ticks * performance.Measurements + measuredTime.Ticks;
                performance.AverageTime = TimeSpan.FromTicks(totalTicks / ++performance.Measurements);
            }

            Session.SaveOrUpdate(performance);
        }
    }
}