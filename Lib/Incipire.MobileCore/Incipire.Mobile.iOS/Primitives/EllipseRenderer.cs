using System;
using System.Linq;
using CoreGraphics;
using Incipire.Mobile.Primitives;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;

namespace Incipire.Mobile.iOS.Primitives
{
    public class EllipseRenderer:ViewRenderer<Ellipse, EllipseView>
    {
        private EllipseView _view;
        //SendTapped method on TapGestureRecognizer is internal so let's fix that.
        private static readonly System.Reflection.MethodInfo _tappedMethod=
                        typeof(TapGestureRecognizer).GetMethod(
                            "SendTapped",
                            System.Reflection.BindingFlags.NonPublic|
                            System.Reflection.BindingFlags.Instance);

        public override UIDynamicItemCollisionBoundsType CollisionBoundsType
        {
            get
            {
                return UIDynamicItemCollisionBoundsType.Ellipse;
            }
        }

        public override bool PointInside(CGPoint point, UIEvent uievent)
        {
            return _view.PointInside(point, uievent);
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
            InitializeGestureRecognizers(Element);
        }

        private void InitializeGestureRecognizers(Ellipse element)
        {
            foreach (var recognizer in element.GestureRecognizers)
            {
                if(recognizer is TapGestureRecognizer tapRecognizer)
                {
                       var uiTapRecognizer= new UITapGestureRecognizer(
                            (obj) => _tappedMethod.Invoke(tapRecognizer, new object[] { element }));
                    uiTapRecognizer.NumberOfTapsRequired = (nuint)tapRecognizer.NumberOfTapsRequired;
                    _view.AddGestureRecognizer(uiTapRecognizer);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
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
            _ellipse = ellipse;
            BackgroundColor = UIColor.Clear;
            UserInteractionEnabled = true;
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
                context.SaveState();
                context.AddEllipseInRect(rect);
                ApplyFill(_ellipse.Fill, context, rect);
                context.RestoreState();
                context.SetLineWidth(_ellipse.StrokeWidth);
                context.AddEllipseInRect(rect);
                ApplyStroke(_ellipse.Stroke, context, rect);
                context.DrawPath(CGPathDrawingMode.Stroke);
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

        private void ApplyStroke(Brush stroke, CGContext context, CGRect rect)
        {
            if (stroke is SolidColorBrush solidBrush)
            {
                var color = solidBrush.Color.ToUIColor();
                color.SetStroke();
            }
        }

        void ApplyFill(Brush fill, CGContext context, CGRect rect)
        {
            if (fill is SolidColorBrush brush)
            {
                var color = brush.Color.ToUIColor();
                color.SetFill();
                context.DrawPath(CGPathDrawingMode.Fill);
            }
            if (fill is GradientBrush gradientBrush)
            {
                var colors = gradientBrush.ColorStops.Select(cs => cs.Color.ToCGColor()).ToArray();
                var stops = gradientBrush.ColorStops.Select(cs => (nfloat)cs.Postion).ToArray();
                using (var colorSpace = CGColorSpace.CreateGenericRgb())
                {
                    using (var gradient = new CGGradient(colorSpace, colors, stops))
                    {
                        var startPoint = new CGPoint(rect.GetMidX(), rect.GetMinY());
                        var endPoint = new CGPoint(rect.GetMidX(), rect.GetMaxY());
                        context.Clip();
                        context.DrawLinearGradient(gradient, startPoint, endPoint, CGGradientDrawingOptions.None);
                    }
                }
            }
        }
    }
}
