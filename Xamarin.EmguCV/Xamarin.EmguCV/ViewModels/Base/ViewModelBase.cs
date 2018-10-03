using System.Threading.Tasks;
using Xamarin.EmguCV.Services.Navigation;

namespace Xamarin.EmguCV.ViewModels.Base
{
    public class ViewModelBase : MvvmHelpers.BaseViewModel
    {
        protected readonly INavigationService NavigationService;

        public ViewModelBase()
        {
            NavigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData) => Task.FromResult(false);
    }
}
