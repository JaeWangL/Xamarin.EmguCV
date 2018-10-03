using Microsoft.Win32;
using Xamarin.EmguCV.Services.Picker;
using Xamarin.EmguCV.Wpf.Services.Picker;

[assembly: Xamarin.Forms.Dependency(typeof(PickerService))]
namespace Xamarin.EmguCV.Wpf.Services.Picker
{
    public class PickerService : IPickerService
    {
        public string PickImageFile()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.gif, *.png) | *.jpg; *.jpeg; *.gif; *.png"
            };

            if (dlg.ShowDialog() == true)
            {
                return dlg.FileName;
            }

            return null;
        }
    }
}
