using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.EmguCV.Models.Menu
{
    public class MenuItemModel : BindableObject
    {
        MenuItemType menuItemType;
        Type viewModelType;
        string title;
        bool isEnabled;

        public MenuItemType MenuItemType
        {
            get => menuItemType;
            set
            {
                menuItemType = value;
                OnPropertyChanged();
            }
        }

        public Type ViewModelType
        {
            get => viewModelType;
            set
            {
                viewModelType = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }

        public Func<Task> AfterNavigationAction { get; set; }
    }
}