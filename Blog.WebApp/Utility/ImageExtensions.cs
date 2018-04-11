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
                switch (rotationValue)
                {
                    case 8: // rotated 90 right
                        image.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                        break;
                    case 3: // bottoms up
                        image.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                        break;
                    case 6: // rotated 90 left
                        image.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                        break;
                    case 1: // landscape, do nothing
                    default:
                        break;
                }
            }
        }
    }
}