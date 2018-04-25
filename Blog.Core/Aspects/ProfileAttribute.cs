using Ninject;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Attributes;
using Ninject.Extensions.Interception.Request;
using Ninject.Parameters;
using System;

namespace Blog.Core.Aspects
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ProfileAttribute : InterceptAttribute
    {
        public string NamePrefix { get; set; }

        public ProfileAttribute()
        {
            NamePrefix = string.Empty;
        }

        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            return request.Kernel.Get<ProfileInterceptor>(new ConstructorArgument("namePrefix", NamePrefix));
        }
    }
}