using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.EmguCV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomNavigationView : NavigationPage
    {
        public CustomNavigationView() : base() => InitializeComponent();

        public CustomNavigationView(Page root) : base(root) => InitializeComponent();
    }
}