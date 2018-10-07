using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Xamarin.EmguCV.Models.Algorithm;
using Xamarin.EmguCV.Services.Algorithm;
using Xamarin.EmguCV.Wpf.Helpers;
using Xamarin.EmguCV.Wpf.Services.Algorithm;

[assembly: Xamarin.Forms.Dependency(typeof(ContourService))]
namespace Xamarin.EmguCV.Wpf.Services.Algorithm
{
    public class ContourService : IContourService
    {
        public AlgorithmResult DetectContours(
            string filename,
            ContourRetrType rtype,
            ContourMethodType mtype,
            double threshold1,
            double threshold2,
            int apertureSize)
        {
            AlgorithmResult result = new AlgorithmResult();
            Image<Bgr, byte> image = ImageHelper.GetImage(filename);
            var cannyImage = new Image<Gray, byte>(image.Width, image.Height);
            var resultImage = new Image<Bgr, byte>(filename);
            var contours = new VectorOfVectorOfPoint();

            // Apply canny algorithm
            CvInvoke.Canny(
                image.Convert<Gray, byte>(),
                cannyImage,
                threshold1,
                threshold2,
                apertureSize);

            // Find the contours
            CvInvoke.FindContours(
                cannyImage,
                contours,
                null,
                GetRetrType(rtype),
                GetMethodType(mtype));

            resultImage = cannyImage.Convert<Bgr, byte>();

            // Draw the contours
            resultImage.DrawPolyline(
                contours.ToArrayOfArray(),
                false,
                new Bgr(Color.FromArgb(255, 77, 77)),
                3);

            // Return collection of contours
            result.ImageArray = ImageHelper.SetImage(resultImage);
            result.ContourDatas = new List<ContourPointModel>();
            result.ContourDatas = contours.ToArrayOfArray().Select(c => new ContourPointModel()
            {
                Count = c.Select(p => new PointF(p.X, p.Y)).ToArray().Length,
                StartX = c.Length > 0 ? c[0].X : float.NaN,
                StartY = c.Length > 0 ? c[0].Y : float.NaN,
                EndX = c.Length > 0 ? c[c.Length - 1].X : float.NaN,
                EndY = c.Length > 0 ? c[c.Length - 1].Y : float.NaN,
                Length = GetLength(c.Select(p => new PointF(p.X, p.Y)).ToArray())
            }).ToList();

            return result;
        }

        ChainApproxMethod GetMethodType(ContourMethodType method)
        {
            switch (method)
            {
                case ContourMethodType.ChainApproxNone:
                    return ChainApproxMethod.ChainApproxNone;
                case ContourMethodType.ChainApproxSimple:
                    return ChainApproxMethod.ChainApproxSimple;
                case ContourMethodType.ChainApproxTc89L1:
                    return ChainApproxMethod.ChainApproxTc89L1;
                case ContourMethodType.ChainApproxTc89Kcos:
                    return ChainApproxMethod.ChainApproxTc89Kcos;
                case ContourMethodType.LinkRuns:
                    return ChainApproxMethod.LinkRuns;
                default:
                    return ChainApproxMethod.ChainApproxSimple;
            }
        }

        RetrType GetRetrType(ContourRetrType type)
        {
            switch (type)
            {
                case ContourRetrType.External:
                    return RetrType.External;
                case ContourRetrType.List:
                    return RetrType.List;
                case ContourRetrType.Ccomp:
                    return RetrType.Ccomp;
                case ContourRetrType.Tree:
                    return RetrType.Tree;
                default:
                    return RetrType.Tree;
            }
        }

        double GetLength(PointF[] pixel)
        {
            var length = 0.0;
            if (pixel.Length < 2)
            {
                return length;
            }

            for (var i = 1; i < pixel.Length; i++)
            {
                length += 
                    Math.Sqrt(Math.Pow(pixel[i].X - pixel[i - 1].X, 2)
                    + Math.Pow(pixel[i].Y - pixel[i - 1].Y, 2));
            }

            return length;
        }
    }
}
