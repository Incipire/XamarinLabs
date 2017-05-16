using Xamarin.Forms;

namespace Incipire.Mobile.Primitives
{
    public abstract class Brush : BindableObject
    {

        public static readonly BindableProperty OpacityProperty =
            BindableProperty.Create(
                nameof(Opacity),
                typeof(double),
                typeof(Brush),
                default(double)
            );

        public double Opacity
        {
            get { return (double)GetValue(OpacityProperty); }
            set { SetValue(OpacityProperty, value); }
        }

    }
}
