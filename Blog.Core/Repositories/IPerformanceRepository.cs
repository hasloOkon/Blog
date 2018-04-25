using Blog.Core.Models;
using System;

namespace Blog.Core.Repositories
{
    public interface IPerformanceRepository : IRepository<Performance>
    {
        void AddMeasurement(string performanceName, TimeSpan time);
    }
}