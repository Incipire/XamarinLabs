using CoreGraphics;
using Incipire.Mobile.iOS.Primitives;
using Incipire.Mobile.Primitives;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Ellipse), typeof(EllipseRenderer))]
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
            using (var context = UIGraphics.GetCurrentContext())
            {
                //erase the view (renders black by default)
                context.ClearRect(rect);

                rect = CalculateBoundaries(rect);
                context.AddEllipseInRect(rect);
                context.SetLineWidth(ellipse.StrokeWidth);
                ApplyStroke(ellipse.Stroke, context);
                ApplyFill(ellipse.Fill, context);
                context.DrawPath(CGPathDrawingMode.FillStroke);
            }
        }

        CGRect CalculateBoundaries(CGRect rect)
        {
            //Get the strokewidth as set by the client
            //Also get the offset (can we cheat and make this a bitshift?
            var strokeWidth = ellipse.StrokeWidth;
            var offset = strokeWidth / 2;

            //Adjust the drawing rect to accomodate the stroke.
            rect.Width -= strokeWidth;
            rect.Height -= strokeWidth;
            rect.X += offset;
            rect.Y += offset;
            return rect;
        }

        private void ApplyStroke(Brush stroke, CGContext context)
        {
            if (stroke is SolidColorBrush brush)
            {
                var color = brush.Color.ToUIColor();
                color.SetStroke();
            }
        }

        void ApplyFill(Brush fill, CGContext context)
        {
            if (fill is SolidColorBrush brush)
            {
                var color = brush.Color.ToUIColor();
                color.SetFill();
            }
        }
    }
}
