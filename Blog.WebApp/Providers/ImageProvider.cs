﻿using Blog.Core.Repositories;
using System;
using System.IO;
using System.Web;
using Image = Blog.Core.Models.Image;
using SystemImage = System.Drawing.Image;

namespace Blog.WebApp.Providers
{
    public class ImageProvider : IImageProvider
    {
        private readonly IImageRepository imageRepository;

        public ImageProvider(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public void SavePostedImage(HttpPostedFileBase postedImage, HttpServerUtilityBase server)
        {
            var image = new Image
            {
                UploadTime = DateTime.Now,
                Data = new byte[postedImage.ContentLength]
            };

            postedImage.InputStream.Read(image.Data, 0, image.Data.Length);

            imageRepository.Save(image);

            PersistImageAndThumbnail(image, server);
        }

        public string GetImageUrl(Image image)
        {
            return $"~/Content/uploads/originals/{GetImageFilename(image)}";
        }

        public string GetThumbnailUrl(Image image)
        {
            return $"~/Content/uploads/thumbnails/{GetImageFilename(image)}";
        }

        public string GetImagePath(Image image, HttpServerUtilityBase server)
        {
            var filename = GetImageFilename(image);
            var originalsPath = server.MapPath("~/Content/uploads/originals");
            return Path.Combine(originalsPath, filename);
        }

        public string GetThumbnailPath(Image image, HttpServerUtilityBase server)
        {
            var filename = GetImageFilename(image);
            var thumbnailsPath = server.MapPath("~/Content/uploads/thumbnails");
            return Path.Combine(thumbnailsPath, filename);
        }

        private static string GetImageFilename(Image image)
        {
            return $"{image.Id}.png";
        }

        private void PersistImageAndThumbnail(Image image, HttpServerUtilityBase server)
        {
            var imagePath = GetImagePath(image, server);
            var thumbnailPath = GetThumbnailPath(image, server);

            using (var stream = new MemoryStream(image.Data))
            using (var systemImage = SystemImage.FromStream(stream))
            {
                systemImage.Save(imagePath);

                using (var thumbnailSystemImage = systemImage
                    .GetThumbnailImage(80, 80, () => false, IntPtr.Zero))
                {
                    thumbnailSystemImage.Save(thumbnailPath);
                }
            }
        }
    }
}