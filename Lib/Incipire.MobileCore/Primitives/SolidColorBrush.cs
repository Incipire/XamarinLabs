using Xamarin.Forms;

namespace Incipire.Mobile.Primitives
{
    public class SolidColorBrush : Brush
    {
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(
                nameof(Color),
                typeof(Color),
                typeof(SolidColorBrush),
                default(Color)
            );

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
    }
}
