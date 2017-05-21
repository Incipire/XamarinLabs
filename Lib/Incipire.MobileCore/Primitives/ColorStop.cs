using Xamarin.Forms;

namespace Incipire.Mobile.Primitives
{
    /// <summary>
    /// Represents a color and position within a gradient.
    /// </summary>
    public class ColorStop:BindableObject
    {
        /// <summary>
        /// The backing <see cref="BindableProperty"/> for the Color property.
        /// </summary>
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(
                nameof(Color),
                typeof(Color),
                typeof(ColorStop),
                Color.Transparent
            );

        /// <summary>
        /// The color for this color stop.
        /// </summary>
        /// <value>The color.</value>
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly BindableProperty PostionProperty =
            BindableProperty.Create(
                nameof(Postion),
                typeof(float),
                typeof(ColorStop),
                1.0F
            );

        public float Postion
        {
            get { return (float)GetValue(PostionProperty); }
            set { SetValue(PostionProperty, value); }
        }
    }
}