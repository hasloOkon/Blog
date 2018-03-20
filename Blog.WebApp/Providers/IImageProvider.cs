using Blog.Core.Models;
using System.Web;

namespace Blog.WebApp.Providers
{
    public interface IImageProvider
    {
        void SavePostedImage(HttpPostedFileBase postedImage, HttpServerUtilityBase server);
        string GetImageUrl(Image image);
        string GetThumbnailUrl(Image image);
    }
}