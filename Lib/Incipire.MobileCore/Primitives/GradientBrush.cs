using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Incipire.Mobile.Primitives
{
    public class GradientBrush : Brush
    {
        public static readonly BindableProperty ColorStopsProperty =
            BindableProperty.Create(
                nameof(ColorStops),
                typeof(ObservableCollection<ColorStop>),
                typeof(GradientBrush),
                null
            );

        public ObservableCollection<ColorStop> ColorStops
        {
            get { return (ObservableCollection<ColorStop>)GetValue(ColorStopsProperty); }
            set { SetValue(ColorStopsProperty, value); }
        }

        public GradientBrush()
        {
            ColorStops = new ObservableCollection<ColorStop>();
        }
    }
}
