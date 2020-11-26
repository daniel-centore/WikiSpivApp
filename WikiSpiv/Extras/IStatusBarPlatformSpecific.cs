using System;
using Xamarin.Forms;

namespace WikiSpiv.Extras
{
    // Stolen from https://sturla.io/change-statusbar-color-from-xamarin-forms/
    public interface IStatusBarPlatformSpecific
    {
        public void SetStatusBarColor(Color color);
    }
}
