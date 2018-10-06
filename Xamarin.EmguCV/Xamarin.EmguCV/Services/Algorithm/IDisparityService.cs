using Xamarin.EmguCV.Models.Algorithm;

namespace Xamarin.EmguCV.Services.Algorithm
{
    public interface IDisparityService
    {
        AlgorithmResult DetectDisparity(
            string filenameL,
            string filenameR,
            int numberOfDisparities,
            int blockSize);
    }
}
