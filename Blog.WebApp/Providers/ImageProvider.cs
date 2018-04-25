using Blog.Core.Aspects;
using Blog.Core.Repositories;
using Blog.Core.Utility;
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
        private const int ThumbnailMaxWidth = 80;
        private const int ThumnbailMaxHeight = 80;
        private const int OriginalMaxWidth = 1600;
        private const int OriginalMaxHeight = 1200;

        private const int ImageQuality = 90;
        private const string ImageExtension = ".jpg";

        private const string UploadsUrl = "~/Content/uploads";
        private const string OriginalsUrl = UploadsUrl + "/originals";
        private const string ThumbnailsUrl = UploadsUrl + "/thumbnails";

        private readonly IImageRepository imageRepository;

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
            return string.Format("{0}/{1}", OriginalsUrl, GetImageFilename(image));
        }

        public string GetThumbnailUrl(Image image)
        {
            return string.Format("{0}/{1}", ThumbnailsUrl, GetImageFilename(image));
        }

        [Profile(NamePrefix = "ImageProvider")]
        public virtual IList<Image> GetImages()
        {
            var images = imageRepository.GetAll().ToList();

            EnsureImagesExist(images);

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

        private void EnsureImagesExist(IEnumerable<Image> images)
        {
            var imagesToPersist = imageRepository.FetchData(images.Where(IsImageMissing));

            var context = HttpContext.Current;
            imagesToPersist.ForEachAsync(image =>
            {
                HttpContext.Current = context;
                PersistImageAndThumbnail(image);
            });
        }

        private bool IsImageMissing(Image image)
        {
            return !File.Exists(GetImagePath(image)) || !File.Exists(GetThumbnailPath(image));
        }

        private void EnsureDirectoriesExist()
        {
            var uploadFolderPath = GetPhysicalPath(UploadsUrl);
            var originalsFolderPath = GetPhysicalPath(OriginalsUrl);
            var thumbnailsFolderPath = GetPhysicalPath(ThumbnailsUrl);

            EnsureDirectoryExist(uploadFolderPath);
            EnsureDirectoryExist(originalsFolderPath);
            EnsureDirectoryExist(thumbnailsFolderPath);
        }

        private static void EnsureDirectoryExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private string GetImagePath(Image image)
        {
            return GetPhysicalPath(GetImageUrl(image));
        }

        private string GetThumbnailPath(Image image)
        {
            return GetPhysicalPath(GetThumbnailUrl(image));
        }

        private static string GetImageFilename(Image image)
        {
            return string.Format("{0}{1}", image.Id, ImageExtension);
        }

        private void PersistImageAndThumbnail(Image image)
        {
            using (var stream = new MemoryStream(image.Data))
            using (var systemImage = SystemImage.FromStream(stream))
            {
                systemImage.FixRotation();

                using (var shrinkedSystemImage = systemImage.ShrinkedToFit(OriginalMaxWidth, OriginalMaxHeight))
                {
                    shrinkedSystemImage.SaveAsJpg(GetImagePath(image), ImageQuality);
                }

                using (var thumbnailSystemImage = systemImage.ShrinkedToFit(ThumbnailMaxWidth, ThumnbailMaxHeight))
                {
                    thumbnailSystemImage.SaveAsJpg(GetThumbnailPath(image), ImageQuality);
                }
            }
        }

        private static string GetPhysicalPath(string url)
        {
            return HttpContext.Current.Server.MapPath(url);
        }
    }
}
