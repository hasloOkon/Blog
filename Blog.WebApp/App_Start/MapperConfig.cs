using AutoMapper;
using Blog.Core.Models;
using Blog.Core.Utility;
using Blog.WebApp.ViewModels.Forms;
using System;
using System.Linq;

namespace Blog.WebApp
{
    public static class MapperConfig
    {
        public static void CreateMaps()
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