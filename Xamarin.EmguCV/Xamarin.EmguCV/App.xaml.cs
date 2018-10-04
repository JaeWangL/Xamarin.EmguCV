using System.Threading.Tasks;
using Xamarin.EmguCV.Services.Navigation;
using Xamarin.EmguCV.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xamarin.EmguCV
{
    public partial class App : Application
    {
        static App() => ViewModelLocator.Instance.Build();

        public App()
        {
            InitializeComponent();

            InitNavigation();
        }

        Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }
    }
}
