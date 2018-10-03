using Xamarin.EmguCV.Models.Algorithm;

namespace Xamarin.EmguCV.Services.Algorithm
{
    public interface IFeatureDetectService
    {
        AlgorithmResult DetectKaze(
            string filename,
            KeypointType kpsType,
            float threshold,
            int octaves,
            int sublevels);

        AlgorithmResult DetectSift(
            string filename,
            KeypointType kpsType,
            int features,
            int octaveLayers,
            double contrastThreshold,
            double edgeThreshold,
            double sigma);

        AlgorithmResult DetectSurf(
            string filename,
            KeypointType kpsType,
            double hessianThresh,
            int octaves,
            int octaveLayers);
    }
}
