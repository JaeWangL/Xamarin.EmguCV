using System.Windows.Input;
using MvvmHelpers;
using Xamarin.EmguCV.Models.Menu;
using Xamarin.EmguCV.ViewModels.Base;
using Xamarin.Forms;

namespace Xamarin.EmguCV.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        ObservableRangeCollection<MenuItemModel> menuItems;

        public ICommand MenuItemSelectedCommand => new Command<MenuItemModel>(OnSelectMenuItem);

        public MenuViewModel()
        {
            MenuItems = new ObservableRangeCollection<MenuItemModel>();

            InitMenuItems();
        }

        public ObservableRangeCollection<MenuItemModel> MenuItems
        {
            get => menuItems;
            set
            {
                menuItems = value;
                OnPropertyChanged();
            }
        }

        void InitMenuItems()
        {
            MenuItems.Add(new MenuItemModel
            {
                MenuItemType = MenuItemType.Home,
                ViewModelType = typeof(MainViewModel),
                Title = "Home",
                IsEnabled = true
            });

            MenuItems.Add(new MenuItemModel
            {
                MenuItemType = MenuItemType.Contours,
                ViewModelType = typeof(ContourViewModel),
                Title = "Contours",
                IsEnabled = true
            });

            MenuItems.Add(new MenuItemModel
            {
                MenuItemType = MenuItemType.CornerHarris,
                ViewModelType = typeof(CornerHarrisViewModel),
                Title = "Corner Harris",
                IsEnabled = true
            });

            MenuItems.Add(new MenuItemModel
            {
                MenuItemType = MenuItemType.Disparity,
                ViewModelType = typeof(DisparityViewModel),
                Title = "Disparity",
                IsEnabled = true
            });

            MenuItems.Add(new MenuItemModel
            {
                MenuItemType = MenuItemType.FeatureDetection,
                ViewModelType = typeof(FeatureDetectionViewModel),
                Title = "Feature Detection",
                IsEnabled = true
            });

            MenuItems.Add(new MenuItemModel
            {
                MenuItemType = MenuItemType.FeatureMatch,
                ViewModelType = typeof(FeatureMatchViewModel),
                Title = "Feature Match",
                IsEnabled = true
            });
        }

        void OnSelectMenuItem(MenuItemModel item)
        {
            if (item.IsEnabled && item.ViewModelType != null)
            {
                item.AfterNavigationAction?.Invoke();
                NavigationService.NavigateToAsync(item.ViewModelType, item);
            }
        }
    }
}
