using Blog.Core.Repositories;
using Ninject.Extensions.Interception;
using System.Diagnostics;

namespace Blog.Core.Aspects
{
    public class ProfileInterceptor : IInterceptor
    {
        private readonly IPerformanceRepository performanceRepository;
        private readonly string namePrefix;

        public ProfileInterceptor(IPerformanceRepository performanceRepository, string namePrefix)
        {
            this.performanceRepository = performanceRepository;
            this.namePrefix = namePrefix;
        }

        public virtual void Intercept(IInvocation invocation)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            invocation.Proceed();
            stopwatch.Stop();

            var name = string.Format("{0}.{1}", namePrefix, invocation.Request.Method.Name);

            performanceRepository.AddMeasurement(name, stopwatch.Elapsed);
        }
    }
}