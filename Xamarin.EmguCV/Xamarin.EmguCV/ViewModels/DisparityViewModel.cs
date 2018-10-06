using System.IO;
using System.Windows.Input;
using Xamarin.EmguCV.Models.Algorithm;
using Xamarin.EmguCV.Services.Algorithm;
using Xamarin.EmguCV.Services.Picker;
using Xamarin.EmguCV.ViewModels.Base;
using Xamarin.Forms;

namespace Xamarin.EmguCV.ViewModels
{
    public class DisparityViewModel : ViewModelBase
    {
        readonly IDisparityService disparityService;
        readonly IPickerService pickerService;
        ImageSource resultImage;
        string filenameL;
        string filenameR;
        bool isDone = false;

        public ICommand DetectCommand => new Command(DetectDisparity);
        public ICommand PickLeftCommand => new Command(PickImageL);
        public ICommand PickRightCommand => new Command(PickImageR);

        public DisparityViewModel()
        {
            disparityService = DependencyService.Get<IDisparityService>();
            pickerService = DependencyService.Get<IPickerService>();
        }

        public ImageSource ResultImage
        {
            get => resultImage;
            set => SetProperty(ref resultImage, value);
        }

        public string FileNameL
        {
            get => filenameL;
            set => SetProperty(ref filenameL, value);
        }

        public string FileNameR
        {
            get => filenameR;
            set => SetProperty(ref filenameR, value);
        }

        public bool IsDone
        {
            get => isDone;
            set => SetProperty(ref isDone, value);
        }

        #region Disparity

        int numberOfDisparities = 0;
        int blockSize = 21;

        public int NumberOfDisparities
        {
            get => numberOfDisparities;
            set => SetProperty(ref numberOfDisparities, value);
        }

        public int BlockSize
        {
            get => blockSize;
            set => SetProperty(ref blockSize, value);
        }

        #endregion

        void DetectDisparity()
        {
            IsBusy = true;

            // TODO: Add Points
            AlgorithmResult result = disparityService.DetectDisparity(
                FileNameL,
                FileNameR,
                NumberOfDisparities,
                BlockSize);

            ResultImage = ImageSource.FromStream(() => new MemoryStream(result.ImageArray));

            IsBusy = false;
            IsDone = true;
        }

        void PickImageL()
        {
            IsDone = false;

            FileNameL = pickerService.PickImageFile();
        }

        void PickImageR()
        {
            IsDone = false;

            FileNameR = pickerService.PickImageFile();
        }
    }
}
