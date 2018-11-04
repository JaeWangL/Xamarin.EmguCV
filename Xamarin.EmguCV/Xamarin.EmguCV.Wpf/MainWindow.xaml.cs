using Xamarin.EmguCV.Services.Algorithm;
using Xamarin.EmguCV.Services.Picker;
using Xamarin.EmguCV.ViewModels.Base;
using Xamarin.EmguCV.Wpf.Services.Algorithm;
using Xamarin.EmguCV.Wpf.Services.Picker;
using Xamarin.Forms.Platform.WPF;

namespace Xamarin.EmguCV.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FormsApplicationPage
    {
        public MainWindow()
        {
            InitializeComponent();

            Forms.Forms.Init();

            ViewModelLocator.Instance.Register<IContourService, ContourService>();
            ViewModelLocator.Instance.Register<ICornerHarrisService, CornerHarrisService>();
            ViewModelLocator.Instance.Register<IDisparityService, DisparityService>();
            ViewModelLocator.Instance.Register<IFeatureDetectService, FeatureDetectService>();
            ViewModelLocator.Instance.Register<IFeatureMatchService, FeatureMatchService>();
            ViewModelLocator.Instance.Register<IPickerService, PickerService>();

            LoadApplication(new EmguCV.App());
        }
    }
}
