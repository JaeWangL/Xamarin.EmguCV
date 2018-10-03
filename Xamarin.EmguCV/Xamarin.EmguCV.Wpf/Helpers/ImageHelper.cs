using System.Drawing.Imaging;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Xamarin.EmguCV.Wpf.Helpers
{
    public static class ImageHelper
    {
        public static Image<Bgr, byte> GetImage(string filename)
        {
            if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
            {
                return new Image<Bgr, byte>(filename);
            }

            return new Image<Bgr, byte>(640, 480);
        }

        public static byte[] SetImage(Image<Bgr, byte> resultImage)
        {
            if (resultImage != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    resultImage.Bitmap.Save(memoryStream, ImageFormat.Bmp);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    return memoryStream.ToArray();
                }
            }

            return null;
        }
    }
}
