using System;
using CoreGraphics;
using Incipire.Mobile.Primitives;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Incipire.Mobile.iOS.Primitives
{
    public class EllipseRenderer:ViewRenderer<Ellipse, EllipseView>
    {
        private EllipseView _view;

        public override UIDynamicItemCollisionBoundsType CollisionBoundsType
        {
            get
            {
                return UIDynamicItemCollisionBoundsType.Ellipse;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Ellipse> e)
        {
            base.OnElementChanged(e);
            if(e.OldElement!=null||Element==null)
            {
                return;
            }
            _view = new EllipseView(Element);
            SetNativeControl(_view);
            _view.AddGestureRecognizer(new UITapGestureRecognizer(() => Console.WriteLine("Tapped")));
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Console.WriteLine(e.PropertyName);
        }

        public static void Initialize(){}
    }

    public class EllipseView: UIView
    {
        readonly Ellipse _ellipse;

        public override UIDynamicItemCollisionBoundsType CollisionBoundsType
        {
            get
            {
                return UIDynamicItemCollisionBoundsType.Ellipse;
            }
        }

        public EllipseView(Ellipse ellipse)
        {
            this._ellipse = ellipse;
            BackgroundColor = UIColor.Clear;

        }

        /// <summary>
        /// Determines if a Point is inside the bounds of the View.
        /// </summary>
        /// <returns><c>true</c>, if the point is inside the View, <c>false</c>
        /// otherwise.</returns>
        /// <param name="point">Point.</param>
        /// <param name="uievent">Uievent.</param>
        /// <remarks>
        /// Uses transparency to determine if the point is in bounds. Thus it
        /// could return a false negative if a point within bounds is 
        /// transparent.
        /// </remarks>
        public override bool PointInside(CGPoint point, UIEvent uievent)
        {
            byte[] pixel = new byte[4];
            var colorspace = CGColorSpace.CreateDeviceRGB();
            using(
                var context = new CGBitmapContext(
                    pixel,
                    1,
                    1,
                    8,
                    4,
                    colorspace,
                    CGImageAlphaInfo.PremultipliedLast))
			{
				context.TranslateCTM(-point.X, -point.Y);
                Layer.RenderInContext(context);
			}
            return pixel[3]!=0;
        }

        public override void Draw(CGRect rect)
        {
            using (var context = UIGraphics.GetCurrentContext())
            {
                //erase the view (renders black by default)
                context.ClearRect(rect);

                rect = CalculateBoundaries(rect);
                context.AddEllipseInRect(rect);
                context.SetLineWidth(_ellipse.StrokeWidth);
                ApplyStroke(_ellipse.Stroke, context);
                ApplyFill(_ellipse.Fill, context);
                context.DrawPath(CGPathDrawingMode.FillStroke);
            }
        }

        CGRect CalculateBoundaries(CGRect rect)
        {
            //Get the strokewidth as set by the client
            //Also get the offset (can we cheat and make this a bitshift?
            var strokeWidth = _ellipse.StrokeWidth;
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
