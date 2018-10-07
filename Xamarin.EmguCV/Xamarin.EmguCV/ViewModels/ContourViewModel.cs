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
    public class ContourViewModel : ViewModelBase
    {
        readonly IContourService contourService;
        readonly IPickerService pickerService;
        ImageSource resultImage;
        string filename;
        IEnumerable<ContourMethodType> methods;
        IEnumerable<ContourRetrType> retrs;
        IEnumerable<ContourPointModel> contourPoints;

        public ICommand DetectCommand => new Command(DetectContours);
        public ICommand PickCommand => new Command(PickImage);

        public ContourViewModel()
        {
            contourService = DependencyService.Get<IContourService>();
            pickerService = DependencyService.Get<IPickerService>();

            Methods = (ContourMethodType[])Enum.GetValues(typeof(ContourMethodType));
            Retrs = (ContourRetrType[])Enum.GetValues(typeof(ContourRetrType));
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

        public IEnumerable<ContourMethodType> Methods
        {
            get => methods;
            set => SetProperty(ref methods, value);
        }

        public IEnumerable<ContourRetrType> Retrs
        {
            get => retrs;
            set => SetProperty(ref retrs, value);
        }

        public IEnumerable<ContourPointModel> ContourPoints
        {
            get => contourPoints;
            set => SetProperty(ref contourPoints, value);
        }

        #region Canny

        double threshold1 = 50;
        double threshold2 = 150;
        int apertureSize = 3;

        public double Threshold1
        {
            get => threshold1;
            set => SetProperty(ref threshold1, value);
        }

        public double Threshold2
        {
            get => threshold2;
            set => SetProperty(ref threshold2, value);
        }

        public int ApertureSize
        {
            get => apertureSize;
            set => SetProperty(ref apertureSize, value);
        }

        #endregion

        #region Contours

        ContourRetrType retrType = ContourRetrType.Tree;
        ContourMethodType methodType = ContourMethodType.ChainApproxSimple;

        public ContourRetrType RetrType
        {
            get => retrType;
            set => SetProperty(ref retrType, value);
        }

        public ContourMethodType MethodType
        {
            get => methodType;
            set => SetProperty(ref methodType, value);
        }

        #endregion

        void DetectContours()
        {
            IsBusy = true;

            AlgorithmResult result = contourService.DetectContours(
                FileName,
                RetrType,
                MethodType,
                Threshold1,
                Threshold2,
                ApertureSize);

            ResultImage = ImageSource.FromStream(() => new MemoryStream(result.ImageArray));
            ContourPoints = result.ContourDatas;

            IsBusy = false;
        }

        void PickImage() => FileName = pickerService.PickImageFile();
    }
}
