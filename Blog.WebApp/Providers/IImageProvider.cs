using Blog.Core.Models;
using System.Collections.Generic;
using System.Web;

namespace Blog.WebApp.Providers
{
    public interface IImageProvider
    {
        void SavePostedImage(HttpPostedFileBase postedImage);
        string GetImageUrl(Image image);
        string GetThumbnailUrl(Image image);
        IList<Image> GetImages();
        void Delete(int id);
    }
}