using System;
using System.Linq;
using Blog.Core;
using Blog.WebApp.Providers;
using Blog.WebApp.ViewModels;
using Ninject;
using Ninject.Web.Common;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Blog.Core.Models;
using Blog.Core.Utility;
using Blog.WebApp.ViewModels.Forms;

namespace Blog.WebApp
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(new CoreModule());
            kernel.Bind<IViewModelFactory>().To<ViewModelFactory>();
            kernel.Bind<ILoginProvider>().To<LoginProvider>();
            kernel.Bind<IImageProvider>().To<ImageProvider>();

            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ConfigureMapper();
            base.OnApplicationStarted();
        }

        private static void ConfigureMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<int, Post>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

                config.CreateMap<int, Category>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

                config.CreateMap<int, Tag>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
                
                config.CreateMap<AddPostForm, Post>()
                    .ForMember(dest => dest.UrlSlug, opt => opt.MapFrom(src => src.Title.Slugify()))
                    .ForMember(dest => dest.PostedOn, opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember(desc => desc.Category, opt => opt.MapFrom(src => src.CategoryId))
                    .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.TagIds));

                config.CreateMap<Post, EditPostForm>()
                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                    .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.Tags.Select(tag => tag.Id)));

                config.CreateMap<AddCategoryForm, Category>()
                    .ForMember(dest => dest.UrlSlug, opt => opt.MapFrom(src => src.Name.Slugify()));

                config.CreateMap<EditCategoryForm, Category>()
                    .ForMember(dest => dest.UrlSlug, opt => opt.MapFrom(src => src.Name.Slugify()))
                    .ReverseMap();

                config.CreateMap<AddTagForm, Tag>()
                    .ForMember(dest => dest.UrlSlug, opt => opt.MapFrom(src => src.Name.Slugify()));

                config.CreateMap<EditTagForm, Tag>()
                    .ForMember(dest => dest.UrlSlug, opt => opt.MapFrom(src => src.Name.Slugify()))
                    .ReverseMap();
            });
        }
    }
}
