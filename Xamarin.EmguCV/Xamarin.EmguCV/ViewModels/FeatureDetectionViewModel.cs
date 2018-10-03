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
    public class FeatureDetectionViewModel : ViewModelBase
    {
        readonly IFeatureDetectService detectService;
        readonly IPickerService pickerService;
        FeatureDetectType? selectedAlgorithm;
        ImageSource resultImage;
        string filename;
        bool isKaze;
        bool isSift;
        bool isSurf;
        List<KeyPointModel> keyPoints;
        ObservableRangeCollection<AlgorithmModel> algorithms;

        public ICommand DetectCommand => new Command(DetectFeature);
        public ICommand PickCommand => new Command(PickImage);
        public ICommand AlgorithmSelectedCommand => new Command<AlgorithmModel>(OnSelectAlgorithm);

        public FeatureDetectionViewModel()
        {
            detectService = DependencyService.Get<IFeatureDetectService>();
            pickerService = DependencyService.Get<IPickerService>();

            Algorithms = new ObservableRangeCollection<AlgorithmModel>();

            InitAlgorithms();
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

        public bool IsKaze
        {
            get => isKaze;
            set => SetProperty(ref isKaze, value);
        }

        public bool IsSift
        {
            get => isSift;
            set => SetProperty(ref isSift, value);
        }

        public bool IsSurf
        {
            get => isSurf;
            set => SetProperty(ref isSurf, value);
        }

        public List<KeyPointModel> KeyPoints
        {
            get => keyPoints;
            set => SetProperty(ref keyPoints, value);
        }

        public ObservableRangeCollection<AlgorithmModel> Algorithms
        {
            get => algorithms;
            set
            {
                algorithms = value;
                OnPropertyChanged();
            }
        }

        #region KAZE

        float k_threshold = 0.001f;
        int k_octaves = 4;
        int k_sublevel = 4;

        public float K_threshold
        {
            get => k_threshold;
            set => SetProperty(ref k_threshold, value);
        }

        public int K_octaves
        {
            get => k_octaves;
            set => SetProperty(ref k_octaves, value);
        }

        public int K_sublevel
        {
            get => k_sublevel;
            set => SetProperty(ref k_sublevel, value);
        }

        #endregion

        #region SIFT

        int i_features = 0;
        int i_octaveLayers = 3;
        double i_contrastThreshold = 0.04;
        double i_edgeThreshold = 10;
        double i_sigma = 1.6;

        public int I_features
        {
            get => i_features;
            set => SetProperty(ref i_features, value);
        }

        public int I_octaveLayers
        {
            get => i_octaveLayers;
            set => SetProperty(ref i_octaveLayers, value);
        }

        public double I_contrastThreshold
        {
            get => i_contrastThreshold;
            set => SetProperty(ref i_contrastThreshold, value);
        }

        public double I_edgeThreshold
        {
            get => i_edgeThreshold;
            set => SetProperty(ref i_edgeThreshold, value);
        }

        public double I_sigma
        {
            get => i_sigma;
            set => SetProperty(ref i_sigma, value);
        }

        #endregion

        #region SURF

        double u_hessianThresh = 300;
        int u_octaves = 4;
        int u_octaveLayers = 2;

        public double U_hessianThresh
        {
            get => u_hessianThresh;
            set => SetProperty(ref u_hessianThresh, value);
        }

        public int U_octaves
        {
            get => u_octaves;
            set => SetProperty(ref u_octaves, value);
        }

        public int U_octaveLayers
        {
            get => u_octaveLayers;
            set => SetProperty(ref u_octaveLayers, value);
        }

        #endregion

        void DetectFeature()
        {
            if (selectedAlgorithm != null)
            {
                IsBusy = true;

                AlgorithmResult result = null;
                if (IsKaze)
                {
                    result = detectService.DetectKaze(
                        FileName,
                        KeypointType.DrawRichKeypoints,
                        k_threshold,
                        k_octaves,
                        k_sublevel);
                }
                if (IsSift)
                {
                    result = detectService.DetectSift(
                        FileName,
                        KeypointType.DrawRichKeypoints,
                        i_features,
                        i_octaveLayers,
                        i_contrastThreshold,
                        i_edgeThreshold,
                        i_sigma);
                }
                if (IsSurf)
                {
                    result = detectService.DetectSurf(
                        FileName,
                        KeypointType.DrawRichKeypoints,
                        u_hessianThresh,
                        u_octaves,
                        u_octaveLayers);
                }

                ResultImage = ImageSource.FromStream(() => new MemoryStream(result.ImageArray));
                KeyPoints = result.Datas;

                IsBusy = false;
            }
            else
            {
                Application.Current?.MainPage?.DisplayAlert("Warning", "Please select algorithm", "OK");
            }
        }

        void PickImage() => FileName = pickerService.PickImageFile();

        void SetKaze()
        {
            IsKaze = true;
            IsSift = false;
            IsSurf = false;
        }

        void SetSift()
        {
            IsKaze = false;
            IsSift = true;
            IsSurf = false;
        }

        void SetSurf()
        {
            IsKaze = false;
            IsSift = false;
            IsSurf = true;
        }

        void InitAlgorithms()
        {
            Algorithms.Add(new AlgorithmModel
            {
                AlgorithmType = FeatureDetectType.KAZE,
                Name = "KAZE"
            });

            Algorithms.Add(new AlgorithmModel
            {
                AlgorithmType = FeatureDetectType.SIFT,
                Name = "SIFT"
            });

            Algorithms.Add(new AlgorithmModel
            {
                AlgorithmType = FeatureDetectType.SURF,
                Name = "SURF"
            });
        }

        void OnSelectAlgorithm(AlgorithmModel item)
        {
            if (item != null)
            {
                selectedAlgorithm = item.AlgorithmType;

                if (selectedAlgorithm.Value == FeatureDetectType.KAZE)
                {
                    SetKaze();
                }
                if (selectedAlgorithm.Value == FeatureDetectType.SIFT)
                {
                    SetSift();
                }
                if (selectedAlgorithm.Value == FeatureDetectType.SURF)
                {
                    SetSurf();
                }
            }
        }
    }
}
