using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;
using Xamarin.EmguCV.Models.Algorithm;
using Xamarin.EmguCV.Services.Algorithm;
using Xamarin.EmguCV.Wpf.Helpers;
using Xamarin.EmguCV.Wpf.Services.Algorithm;
using static Emgu.CV.Features2D.Features2DToolbox;

[assembly: Xamarin.Forms.Dependency(typeof(FeatureMatchService))]
namespace Xamarin.EmguCV.Wpf.Services.Algorithm
{
    public class FeatureMatchService : IFeatureMatchService
    {
        public AlgorithmResult DetectFeatureMatch(
            string modelName,
            string observedeName,
            FeatureDetectType detectType,
            FeatureMatchType matchType,
            int k,
            double uniquenessThreshold)
        {
            AlgorithmResult result = new AlgorithmResult();
            Image<Bgr, byte> modelImage = ImageHelper.GetImage(modelName);
            Image<Bgr, byte> observedImage = ImageHelper.GetImage(observedeName);
            Mat resultImage = new Mat();

            // Get features from modelImage
            var modelKeyPoints = new VectorOfKeyPoint();
            var modelDescriptors = new UMat();
            GetDetector(detectType).DetectAndCompute(
                modelImage.Convert<Gray, byte>(),
                null,
                modelKeyPoints,
                modelDescriptors,
                false);

            // Get features from observedImage
            var observedKeyPoints = new VectorOfKeyPoint();
            var observedDescriptors = new UMat();
            GetDetector(detectType).DetectAndCompute(
                observedImage.Convert<Gray, byte>(),
                null,
                observedKeyPoints,
                observedDescriptors,
                false);

            // Perform match with selected matcher
            var matches = new VectorOfVectorOfDMatch();
            if (matchType == FeatureMatchType.Flann)
            {
                // TODO: Add parameters for GetIndexParams
                var flannMatcher = new FlannBasedMatcher(new AutotunedIndexParams(), new SearchParams());
                flannMatcher.Add(modelDescriptors);
                flannMatcher.KnnMatch(
                    observedDescriptors,
                    matches,
                    k,
                    null);
            }
            else
            {
                // TODO: Add parameters for DistanceType
                var bfMatcher = new BFMatcher(DistanceType.L2);
                bfMatcher.Add(modelDescriptors);
                bfMatcher.KnnMatch(
                    observedDescriptors,
                    matches,
                    k,
                    null);
            }

            // Find homography
            Mat homography = null;
            var mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
            mask.SetTo(new MCvScalar(255));

            VoteForUniqueness(matches, uniquenessThreshold, mask);

            // If 4 or more patches continue
            int nonZeroCount = CvInvoke.CountNonZero(mask);
            if (nonZeroCount >= 4)
            {
                // Filter for majority scale and rotation
                nonZeroCount = VoteForSizeAndOrientation(
                    modelKeyPoints, observedKeyPoints, matches, mask, 1.5, 20);

                // If 4 or more patches continue
                if (nonZeroCount >= 4)
                {
                    homography = GetHomographyMatrixFromMatchedFeatures(modelKeyPoints,
                       observedKeyPoints, matches, mask, 2);
                }
            }

            // Draw the matched keypoints
            DrawMatches(
                modelImage, modelKeyPoints, observedImage, observedKeyPoints,
                matches, resultImage, new MCvScalar(255, 255, 255), new MCvScalar(255, 255, 255), mask);

            // Draw the projected region on the image
            if (homography != null)
            {
                // Draw a rectangle along the projected model
                Rectangle rect = new Rectangle(Point.Empty, modelImage.Size);
                PointF[] pts = new PointF[]
                {
                  new PointF(rect.Left, rect.Bottom),
                  new PointF(rect.Right, rect.Bottom),
                  new PointF(rect.Right, rect.Top),
                  new PointF(rect.Left, rect.Top)
                };

                // Transform the perspective of the points array based on the homography
                // And get a rotated rectangle for the homography
                pts = CvInvoke.PerspectiveTransform(pts, homography);

                Point[] points = Array.ConvertAll(pts, Point.Round);
                using (VectorOfPoint vp = new VectorOfPoint(points))
                {
                    CvInvoke.Polylines(resultImage, vp, true, new MCvScalar(255, 0, 0, 255), 5);
                }
            }

            result.ImageArray = ImageHelper.SetImage(resultImage.ToImage<Bgr, byte>());

            return result;
        }

        Feature2D GetDetector(FeatureDetectType detectType)
        {
            switch (detectType)
            {
                case FeatureDetectType.KAZE:
                    return new KAZE();
                case FeatureDetectType.SIFT:
                    return new SIFT();
                case FeatureDetectType.SURF:
                    return new SURF(300);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
