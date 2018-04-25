using System.Drawing;
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
    }
}