using Incipire.Mobile.Primitives;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.Graphics;
using Android.Views;

namespace Incipire.Mobile.Droid.Primitives
{
    public class EllipseRenderer : ViewRenderer<Ellipse, EllipseView>
    {
        EllipseView _view;

        protected override void OnElementChanged(ElementChangedEventArgs<Ellipse> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                _view = new EllipseView(Context, e.NewElement);
                SetNativeControl(_view);
            }
        }

        public static void Initialize() { }
    }

    public class EllipseView : View
    {
        readonly Ellipse _ellipse;

        public EllipseView(Context ctx, Ellipse ellipse) : base(ctx)
        {
            _ellipse = ellipse;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
			Matrix inverse = new Matrix();
			Matrix.Invert(inverse);
			float[] touchPoint = { e.GetX(), e.GetY() };
			inverse.MapPoints(touchPoint);
			var xCoord = (int)touchPoint[0];
			var yCoord = (int)touchPoint[1];
            Bitmap b = Bitmap.CreateBitmap(Width, Height, Bitmap.Config.Argb8888);
			Canvas c = new Canvas(b);
			Draw(c);
			var colorTouched = b.GetPixel(xCoord, yCoord);

			return Color.GetAlphaComponent(colorTouched) != 0 ? base.OnTouchEvent(e) : true;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            var paint = new Paint();
            var strokeWidth = _ellipse.StrokeWidth;
            var offset = strokeWidth / 2;
            RectF oval1 = new RectF(offset, offset, canvas.Width-strokeWidth, canvas.Height-strokeWidth);
            paint.StrokeWidth = _ellipse.StrokeWidth;
            ApplyFill(_ellipse.Fill, paint);
            canvas.DrawOval(oval1, paint);
            ApplyStroke(_ellipse.Stroke, paint);
            canvas.DrawOval(oval1, paint);
        }

        static void ApplyFill(Brush fill, Paint paint)
        {
            if (fill is SolidColorBrush brush)
            {
                paint.SetStyle(Paint.Style.Fill);
                paint.Color = brush.Color.ToAndroid();
            }
        }

        static void ApplyStroke(Brush stroke, Paint paint)
        {
            if (stroke is SolidColorBrush brush)
            {
                paint.SetStyle(Paint.Style.Stroke);
                paint.Color= brush.Color.ToAndroid();
            }
        }
    }
}
