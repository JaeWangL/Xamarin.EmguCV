using Xamarin.EmguCV.Models.Algorithm;

namespace Xamarin.EmguCV.Services.Algorithm
{
    public interface IFeatureMatchService
    {
        AlgorithmResult DetectFeatureMatch(
            string modelName,
            string observedeName,
            FeatureDetectType detectType,
            FeatureMatchType matchType,
            int k,
            double uniquenessThreshold);
    }
}
