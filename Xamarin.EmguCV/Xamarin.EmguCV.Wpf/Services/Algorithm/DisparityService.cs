using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Xamarin.EmguCV.Models.Algorithm;
using Xamarin.EmguCV.Services.Algorithm;
using Xamarin.EmguCV.Wpf.Helpers;
using Xamarin.EmguCV.Wpf.Services.Algorithm;

[assembly: Xamarin.Forms.Dependency(typeof(DisparityService))]
namespace Xamarin.EmguCV.Wpf.Services.Algorithm
{
    public class DisparityService : IDisparityService
    {
        public AlgorithmResult DetectDisparity(
            string filenameL,
            string filenameR,
            int numberOfDisparities,
            int blockSize)
        {
            AlgorithmResult result = new AlgorithmResult();
            Image<Bgr, byte> imageLeft = ImageHelper.GetImage(filenameL);
            Image<Bgr, byte> imageRight = ImageHelper.GetImage(filenameR);
            var resultImage = new Image<Bgr, byte>(imageLeft.Width, imageLeft.Height);

            // Create new (gray, float) image for disparity
            var imageDisparity = new Image<Gray, float>(imageLeft.Size);

            StereoBM stereoBM = new StereoBM(numberOfDisparities, blockSize);
            StereoMatcherExtensions.Compute(
                stereoBM,
                imageLeft.Convert<Gray, byte>(),
                imageRight.Convert<Gray, byte>(),
                imageDisparity);

            // Normalize
            CvInvoke.Normalize(imageDisparity, imageDisparity, 0, 255, NormType.MinMax, DepthType.Cv8U);

            // Set resultImage after normalizing
            resultImage = imageDisparity.Convert<Bgr, byte>();

            result.ImageArray = ImageHelper.SetImage(resultImage);

            return result;
        }
    }
}
