using System;
using Android.Content;
using WikiSpiv.CustomViews;
using WikiSpiv.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

[assembly: ExportRenderer(typeof(CustomListView), typeof(CustomListViewRenderer))]
namespace WikiSpiv.Droid.CustomRenderers
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        public CustomListViewRenderer(Context context): base(context)
        {
        }

        //protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        //{
        //    base.OnElementChanged(e);
        //    if (e.NewElement != null)
        //    {
        //        AnimationsEnabled = false;
        //    }
        //}
    }
}
