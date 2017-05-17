using Xamarin.Forms;

namespace Incipire.Mobile.Primitives
{
    public class Shape : View
    {
        public static readonly BindableProperty StrokeProperty =
            BindableProperty.Create(
                nameof(Stroke),
                typeof(Brush),
                typeof(Shape),
                default(Brush));

        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value);}
        }

        public static readonly BindableProperty FillProperty =
            BindableProperty.Create(
                nameof(Fill),
                typeof(Brush),
                typeof(Shape),
                default(Brush)
            );

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly BindableProperty StrokeWidthProperty =
            BindableProperty.Create(
                nameof(StrokeWidth),
                typeof(float),
                typeof(Shape),
                0.0F
            );

        public float StrokeWidth
        {
            get { return (float)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }
    }

}