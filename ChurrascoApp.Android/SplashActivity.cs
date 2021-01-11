using Android.App;
using Android.Content;
using Android.Content.PM;
using AndroidX.AppCompat.App;

namespace ChurrascoApp.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", Icon = "@mipmap/icon", RoundIcon = "@mipmap/icon", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
