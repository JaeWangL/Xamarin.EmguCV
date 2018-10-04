using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using Xamarin.EmguCV.Models.Algorithm;
using Xamarin.EmguCV.Services.Algorithm;
using Xamarin.EmguCV.Services.Picker;
using Xamarin.EmguCV.ViewModels.Base;
using Xamarin.Forms;

namespace Xamarin.EmguCV.ViewModels
{
    public class CornerHarrisViewModel : ViewModelBase
    {
        readonly ICornerHarrisService harrisService;
        readonly IPickerService pickerService;
        ImageSource resultImage;
        string filename;
        IEnumerable<HarrisBorderType> borders;
        IEnumerable<CirclePointModel> circlePoints;

        public ICommand DetectCommand => new Command(DetectFeature);
        public ICommand PickCommand => new Command(PickImage);

        public CornerHarrisViewModel()
        {
            harrisService = DependencyService.Get<ICornerHarrisService>();
            pickerService = DependencyService.Get<IPickerService>();

            Borders = (HarrisBorderType[])Enum.GetValues(typeof(HarrisBorderType));
        }

        public ImageSource ResultImage
        {
            get => resultImage;
            set => SetProperty(ref resultImage, value);
        }

        public string FileName
        {
            get => filename;
            set => SetProperty(ref filename, value);
        }
        
        public IEnumerable<HarrisBorderType> Borders
        {
            get => borders;
            set => SetProperty(ref borders, value);
        }

        public IEnumerable<CirclePointModel> CirclePoints
        {
            get => circlePoints;
            set => SetProperty(ref circlePoints, value);
        }

        #region CornerHarris

        byte threshold = 127;
        int blockSize = 2;
        int apertureSize = 3;
        double k = 0.04;
        HarrisBorderType borderType = HarrisBorderType.Reflect101;

        public byte Threshold
        {
            get => threshold;
            set => SetProperty(ref threshold, value);
        }

        public int BlockSize
        {
            get => blockSize;
            set => SetProperty(ref blockSize, value);
        }

        public int ApertureSize
        {
            get => apertureSize;
            set => SetProperty(ref apertureSize, value);
        }

        public double K
        {
            get => k;
            set => SetProperty(ref k, value);
        }

        public HarrisBorderType BorderType
        {
            get => borderType;
            set => SetProperty(ref borderType, value);
        }

        #endregion

        void DetectFeature()
        {
            IsBusy = true;

            // TODO: Add 'edit kpsType' features
            AlgorithmResult result = harrisService.DetectCornerHarris(
                FileName,
                Threshold,
                BlockSize,
                ApertureSize,
                K,
                BorderType);

            ResultImage = ImageSource.FromStream(() => new MemoryStream(result.ImageArray));
            CirclePoints = result.CircleDatas;

            IsBusy = false;
        }

        void PickImage() => FileName = pickerService.PickImageFile();
    }
}
