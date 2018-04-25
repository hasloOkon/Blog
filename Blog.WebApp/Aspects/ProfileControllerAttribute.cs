using Blog.Core.Repositories;
using Ninject;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace Blog.WebApp.Aspects
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ProfileControllerAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch stopwatch = new Stopwatch();

        [Inject]
        public Func<IPerformanceRepository> PerformanceRepositoryFactory { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopwatch.Restart();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stopwatch.Stop();

            var name = string.Format("{0}.{1}",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName);

            PerformanceRepositoryFactory.Invoke().AddMeasurement(name, stopwatch.Elapsed);
        }
    }
}