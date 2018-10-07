namespace Xamarin.EmguCV.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new EmguCV.App());
        }
    }
}
