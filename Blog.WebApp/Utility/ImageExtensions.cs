using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace Blog.WebApp.Utility
{
    public static class ImageExtensions
    {
        public static void FixRotation(this Image image)
        {
            if (image.PropertyIdList.Contains(0x0112))
            {
                int rotationValue = image.GetPropertyItem(0x0112).Value[0];
                const int rotated90Right = 8;
                const int rotated180 = 3;
                const int rotated90Left = 6;

                switch (rotationValue)
                {
                    case rotated90Right:
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    case rotated180:
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case rotated90Left:
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    default:
                        break;
                }
            }
        }

        public static Image ShrinkedToFit(this Image image, int maxWidth, int maxHeight)
        {
            var srcWidth = image.Width;
            var srcHeight = image.Height;

            var ratioX = (float)maxWidth / srcWidth;
            var ratioY = (float)maxHeight / srcHeight;
            var ratio = Math.Min(ratioX, ratioY);

            var destWidth = (int)(srcWidth * ratio);
            var destHeight = (int)(srcHeight * ratio);

            var shrinkedImage = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);

            using (var graphics = Graphics.FromImage(shrinkedImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, destWidth, destHeight);
            }

            return shrinkedImage;
        }

        public static void SaveAsJpg(this Image image, string filePath, int quality)
        {
            var imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);
            var encoderParameters = new EncoderParameters(1)
            {
                Param = new[] { new EncoderParameter(Encoder.Quality, quality) }
            };

            image.Save(filePath, imageCodecInfo, encoderParameters);
        }

        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo
                .GetImageDecoders()
                .SingleOrDefault(codecInfo => codecInfo.FormatID == format.Guid);
        }
    }
}