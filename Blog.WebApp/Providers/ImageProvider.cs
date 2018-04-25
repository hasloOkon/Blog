using Blog.Core.Aspects;
using Blog.Core.Repositories;
using Blog.WebApp.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Image = Blog.Core.Models.Image;
using SystemImage = System.Drawing.Image;

namespace Blog.WebApp.Providers
{
    public class ImageProvider : IImageProvider
    {
        private const int ThumnailWidth = 80;
        private const int ThumnailHeight = 80;
        private readonly IImageRepository imageRepository;
        private static HttpServerUtility Server
        {
            get { return HttpContext.Current.Server; }
        }

        public ImageProvider(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;

            EnsureDirectoriesExist();
        }

        public void SavePostedImage(HttpPostedFileBase postedImage)
        {
            var image = new Image
            {
                UploadTime = DateTime.Now,
                Data = new byte[postedImage.ContentLength]
            };

            postedImage.InputStream.Read(image.Data, 0, image.Data.Length);

            imageRepository.AddOrUpdate(image);
        }

        public string GetImageUrl(Image image)
        {
            return string.Format("~/Content/uploads/originals/{0}", GetImageFilename(image));
        }

        public string GetThumbnailUrl(Image image)
        {
            return string.Format("~/Content/uploads/thumbnails/{0}", GetImageFilename(image));
        }

        [Profile(NamePrefix = "ImageProvider")]
        public virtual IList<Image> GetImages()
        {
            var images = imageRepository.GetAll().ToList();

            foreach (var image in images)
            {
                EnsureImageAndThumbnailExists(image);
            }

            return images;
        }

        [Transaction]
        public virtual void Delete(int id)
        {
            var image = imageRepository.GetById(id);

            var imagePath = GetImagePath(image);
            var thumbnaPath = GetThumbnailPath(image);

            imageRepository.Delete(id);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            if (File.Exists(thumbnaPath))
            {
                File.Delete(thumbnaPath);
            }
        }

        private void EnsureImageAndThumbnailExists(Image image)
        {
            if (!File.Exists(GetImagePath(image)) || !File.Exists(GetThumbnailPath(image)))
            {
                PersistImageAndThumbnail(image);
            }
        }

        private void EnsureDirectoriesExist()
        {
            var uploadFolderPath = Server.MapPath("~/Content/uploads");
            var originalsFolderPath = Server.MapPath("~/Content/uploads/originals");
            var thumbnailsFolderPath = Server.MapPath("~/Content/uploads/thumbnails");

            EnsureDirectoryExist(uploadFolderPath);
            EnsureDirectoryExist(originalsFolderPath);
            EnsureDirectoryExist(thumbnailsFolderPath);
        }

        private void EnsureDirectoryExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public string GetImagePath(Image image)
        {
            var filename = GetImageFilename(image);
            var originalsPath = Server.MapPath("~/Content/uploads/originals");
            return Path.Combine(originalsPath, filename);
        }

        public string GetThumbnailPath(Image image)
        {
            var filename = GetImageFilename(image);
            var thumbnailsPath = Server.MapPath("~/Content/uploads/thumbnails");
            return Path.Combine(thumbnailsPath, filename);
        }

        private static string GetImageFilename(Image image)
        {
            return string.Format("{0}.png", image.Id);
        }

        private void PersistImageAndThumbnail(Image image)
        {
            var imagePath = GetImagePath(image);
            var thumbnailPath = GetThumbnailPath(image);

            using (var stream = new MemoryStream(image.Data))
            using (var systemImage = SystemImage.FromStream(stream))
            {
                systemImage.FixRotation();
                systemImage.Save(imagePath);

                using (var thumbnailSystemImage = systemImage
                    .GetThumbnailImage(ThumnailWidth, ThumnailHeight, () => false, IntPtr.Zero))
                {
                    thumbnailSystemImage.Save(thumbnailPath);
                }
            }
        }
    }
}
