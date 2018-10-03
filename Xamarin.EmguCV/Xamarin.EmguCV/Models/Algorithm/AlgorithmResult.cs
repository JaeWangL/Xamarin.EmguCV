using System.Collections.Generic;

namespace Xamarin.EmguCV.Models.Algorithm
{
    public class AlgorithmResult
    {
        public byte[] ImageArray { get; set; }

        public List<KeyPointModel> Datas { get; set; }
    }
}
