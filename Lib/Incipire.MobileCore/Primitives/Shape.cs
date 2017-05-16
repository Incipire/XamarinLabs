using System;
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

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(WidthRequest, HeightRequest);
        }
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(new Size(WidthRequest, HeightRequest));
        }
    }

}