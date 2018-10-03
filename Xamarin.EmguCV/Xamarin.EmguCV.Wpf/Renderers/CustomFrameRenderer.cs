using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Xamarin.EmguCV.Controls;
using Xamarin.EmguCV.Wpf.Renderers;
using Xamarin.Forms.Platform.WPF;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace Xamarin.EmguCV.Wpf.Renderers
{
    public class CustomFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
        {
            base.OnElementChanged(e);
            
            if (e.NewElement != null)
            {
                var element = Element as CustomFrame;
                if (element.HasShadow)
                {
                    Control.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 255));
                    Control.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 0, 0));
                    Control.BorderThickness = new Thickness(1);

                    var effect = new DropShadowEffect
                    {
                        Opacity = 0.6,
                        ShadowDepth = 2
                    };
                    Control.Effect = effect;
                }
            }
        }
    }
}
