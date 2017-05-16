
using Android.App;
using Android.Content.PM;
using Android.OS;
using Incipire.Mobile.Droid.Primitives;

namespace XamarinLabs.Droid
{
    [Activity(Label = "XamarinLabs.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            EllipseRenderer.Initialize();

            LoadApplication(new App());
        }
    }
}
