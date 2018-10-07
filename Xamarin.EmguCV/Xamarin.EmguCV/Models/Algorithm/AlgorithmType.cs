namespace Xamarin.EmguCV.Models.Algorithm
{
    public enum ContourMethodType
    {
        ChainApproxNone,
        ChainApproxSimple,
        ChainApproxTc89L1,
        ChainApproxTc89Kcos,
        LinkRuns
    }

    public enum ContourRetrType
    {
        External,
        List,
        Ccomp,
        Tree
    }

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
