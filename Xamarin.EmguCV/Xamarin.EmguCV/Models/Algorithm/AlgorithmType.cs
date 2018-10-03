namespace Xamarin.EmguCV.Models.Algorithm
{
    public enum FeatureDetectType
    {
        KAZE,
        SIFT,
        SURF,
    }

    public enum FeatureMatchType
    {
        Flann,
        BruteForce,
    }

    public enum KeypointType
    {
        NotDrawSinglePoints,
        DrawRichKeypoints
    }
}
