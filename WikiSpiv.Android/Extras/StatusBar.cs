using System;
using Android.OS;
using WikiSpiv.Droid.Extras;
using WikiSpiv.Extras;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(Statusbar))]
namespace WikiSpiv.Droid.Extras
{
    public class Statusbar : IStatusBarPlatformSpecific
    {
        public void SetStatusBarColor(Color color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var androidColor = color.ToAndroid();
                Xamarin.Essentials.Platform.CurrentActivity.Window.SetStatusBarColor(androidColor);
            }
        }
    }

}
