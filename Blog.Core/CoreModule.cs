using Blog.Core.Utility;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cache;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Blog.Core
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISessionFactory>()
                .ToMethod
                (
                    context =>
                        Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c =>
                            c.FromConnectionStringWithKey("BlogDbConnString")))
                        .Cache(c => c.UseQueryCache().ProviderClass<HashtableCacheProvider>())
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CoreModule>())
                        //.ExposeConfiguration(config => new SchemaExport(config).Execute(true, true, false))
                        .BuildConfiguration()
                        .BuildSessionFactory()
                )
                .InSingletonScope();

            Bind<ISession>()
                .ToMethod(context => context.Kernel.Get<ISessionFactory>().OpenSession())
                .InRequestScope();

            Kernel.BindManyByName("Repository");
        }
    }
}
