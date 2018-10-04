using System.Collections.Generic;

namespace Xamarin.EmguCV.Models.Algorithm
{
    public class AlgorithmResult
    {
        public byte[] ImageArray { get; set; }

        public List<CirclePointModel> CircleDatas { get; set; }

        public List<KeyPointModel> KeyDatas { get; set; }
    }
}
