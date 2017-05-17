﻿using Incipire.Mobile.Primitives;
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

    public class EllipseView: View
    {
        readonly Ellipse _ellipse;

        public EllipseView(Context ctx, Ellipse ellipse):base(ctx)
        {
            _ellipse = ellipse;
        }

        protected override void OnDraw(Canvas canvas)
        {
            //Step one, get the damn thing to draw.
            base.OnDraw(canvas);
            var paint = new Paint();
            ApplyBrush(_ellipse.Fill, paint);
            RectF oval1 = new RectF(0, 0, canvas.Width, canvas.Height);
            canvas.DrawOval(oval1, paint);
        }

        private static void ApplyBrush(Brush brush, Paint paint)
        {
            paint.SetStyle(Paint.Style.Fill);
            var fill = brush as SolidColorBrush;
            if (fill!=null)
            {
                paint.Color = fill.Color.ToAndroid();
            }
        }
    }
}
