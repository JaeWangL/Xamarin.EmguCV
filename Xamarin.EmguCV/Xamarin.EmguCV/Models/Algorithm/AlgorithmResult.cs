using System.Collections.Generic;

namespace Xamarin.EmguCV.Models.Algorithm
{
    public class AlgorithmResult
    {
        public byte[] ImageArray { get; set; }

        public List<CirclePointModel> CircleDatas { get; set; }

        public List<ContourPointModel> ContourDatas { get; set; }

        public List<KeyPointModel> KeyDatas { get; set; }
    }

    public class CirclePointModel
    {
        public float CenterX { get; set; }

        public float CenterY { get; set; }

        public float Radius { get; set; }

        public double Area { get; set; }
    }

    public class ContourPointModel
    {
        public int Count { get; set; }

        public float StartX { get; set; }

        public float StartY { get; set; }

        public float EndX { get; set; }

        public float EndY { get; set; }

        public double Length { get; set; }
    }

    public class KeyPointModel
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Size { get; set; }

        public float Angle { get; set; }

        public float Response { get; set; }

        public int Octave { get; set; }

        public int ClassId { get; set; }
    }
}
