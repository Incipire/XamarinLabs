using System;
using CoreGraphics;
using Incipire.Mobile.iOS.Primitives;
using Incipire.Mobile.Primitives;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Ellipse),typeof(EllipseRenderer))]
namespace Incipire.Mobile.iOS.Primitives
{
    public class EllipseRenderer:ViewRenderer<Ellipse, EllipseView>
    {
        private EllipseView _view;

        protected override void OnElementChanged(ElementChangedEventArgs<Ellipse> e)
        {
            base.OnElementChanged(e);
            if(e.NewElement!=null)
            {
				_view = new EllipseView(e.NewElement);
				SetNativeControl(_view);
			}
        }

        public static void Initialize(){}
    }

    public class EllipseView: UIView
    {
        readonly Ellipse ellipse;

        public EllipseView(Ellipse ellipse)
        {
            this.ellipse = ellipse;
            BackgroundColor = UIColor.Clear;
        }


        public override void Draw(CGRect rect)
        {
            var context = UIGraphics.GetCurrentContext();
            context.ClearRect(rect);
			context.AddEllipseInRect(rect);
            ApplyBrush(ellipse.Fill, context);
            context.FillPath();
        }

        void ApplyBrush(Brush fill, CGContext context)
        {
            var brush = fill as SolidColorBrush;
            if (brush!=null)
            {
                var color = brush.Color.ToCGColor();
                context.SetFillColor(color);
            }
        }
    }
}
