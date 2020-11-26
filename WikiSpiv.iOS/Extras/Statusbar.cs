using System;
using Foundation;
using UIKit;
using WikiSpiv.Extras;
using WikiSpiv.iOS.Extras;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(Statusbar))]
namespace WikiSpiv.iOS.Extras
{
    public class Statusbar : IStatusBarPlatformSpecific
    {
        public Statusbar()
        {
        }

        public void SetStatusBarColor(Color color)
        {
            // This seems to happen magically with Xamarin Shell header already
            // so just ignore it
        }
    }
}
