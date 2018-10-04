using Xamarin.EmguCV.Models.Algorithm;

namespace Xamarin.EmguCV.Services.Algorithm
{
    public interface ICornerHarrisService
    {
        AlgorithmResult DetectCornerHarris(
            string filename,
            byte threshold,
            int blockSize,
            int apertureSize,
            double k,
            HarrisBorderType borderType);
    }
}
