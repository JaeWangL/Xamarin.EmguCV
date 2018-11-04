using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.EmguCV.Models.Algorithm;
using Xamarin.EmguCV.Services.Algorithm;
using Xamarin.EmguCV.Services.Picker;
using Xamarin.EmguCV.ViewModels.Base;
using Xamarin.Forms;

namespace Xamarin.EmguCV.ViewModels
{
    public class FeatureMatchViewModel : ViewModelBase
    {
        readonly IFeatureMatchService matchService;
        readonly IPickerService pickerService;
        FeatureDetectType detectType = FeatureDetectType.SURF;
        FeatureMatchType matchType = FeatureMatchType.BruteForce;
        ImageSource resultImage;
        int k = 2;
        double uniquenessThreshold = 0.8;
        string modelName, observedName;
        bool isDone = false;
        IEnumerable<FeatureDetectType> detectTypes;
        IEnumerable<FeatureMatchType> matchTypes;

        public ICommand MatchCommand => new Command(MatchFeature);
        public ICommand PickModelCommand => new Command(PickModel);
        public ICommand PickObservedCommand => new Command(PickObserved);

        public FeatureMatchViewModel()
        {
            matchService = DependencyService.Get<IFeatureMatchService>();
            pickerService = DependencyService.Get<IPickerService>();

            DetectTypes = (FeatureDetectType[])Enum.GetValues(typeof(FeatureDetectType));
            MatchTypes = (FeatureMatchType[])Enum.GetValues(typeof(FeatureMatchType));
        }

        public FeatureDetectType DetectType
        {
            get => detectType;
            set => SetProperty(ref detectType, value);
        }

        public FeatureMatchType MatchType
        {
            get => matchType;
            set => SetProperty(ref matchType, value);
        }

        public ImageSource ResultImage
        {
            get => resultImage;
            set => SetProperty(ref resultImage, value);
        }

        public int K
        {
            get => k;
            set => SetProperty(ref k, value);
        }

        public double UniquenessThreshold
        {
            get => uniquenessThreshold;
            set => SetProperty(ref uniquenessThreshold, value);
        }

        public string ModelName
        {
            get => modelName;
            set => SetProperty(ref modelName, value);
        }

        public string ObservedName
        {
            get => observedName;
            set => SetProperty(ref observedName, value);
        }

        public bool IsDone
        {
            get => isDone;
            set => SetProperty(ref isDone, value);
        }

        public IEnumerable<FeatureDetectType> DetectTypes
        {
            get => detectTypes;
            set => SetProperty(ref detectTypes, value);
        }

        public IEnumerable<FeatureMatchType> MatchTypes
        {
            get => matchTypes;
            set => SetProperty(ref matchTypes, value);
        }

        void MatchFeature()
        {
            if (ModelName != null && ObservedName != null)
            {
                IsBusy = true;

                AlgorithmResult result  = matchService.DetectFeatureMatch(
                    ModelName,
                    ObservedName,
                    FeatureDetectType.SURF,
                    FeatureMatchType.BruteForce,
                    K,
                    UniquenessThreshold);

                ResultImage = ImageSource.FromStream(() => new MemoryStream(result.ImageArray));

                IsBusy = false;
                IsDone = true;
            }
            else
            {
                Application.Current?.MainPage?.DisplayAlert("Warning", "Please select images", "OK");
            }
        }

        void PickModel()
        {
            IsDone = false;
            ModelName = pickerService.PickImageFile();
        }

        void PickObserved()
        {
            IsDone = false;
            ObservedName = pickerService.PickImageFile();
        }
    }
}
