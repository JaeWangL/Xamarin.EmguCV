namespace Xamarin.EmguCV.Models.Algorithm
{
    public enum FeatureDetectType
    {
        KAZE,
        SIFT,
        SURF
    }

    public enum FeatureMatchType
    {
        Flann,
        BruteForce
    }

    public enum HarrisBorderType
    {
        Constant,
        Replicate,
        Reflect,
        Wrap,
        Reflect101,
        Transparent,
        Isolated
    }

    public enum KeypointType
    {
        NotDrawSinglePoints,
        DrawRichKeypoints
    }
}
