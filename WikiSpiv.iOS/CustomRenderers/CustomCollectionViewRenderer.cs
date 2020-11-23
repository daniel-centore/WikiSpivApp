using System;
using UIKit;
using WikiSpiv.CustomViews;
using WikiSpiv.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomCollectionView), typeof(CustomCollectionViewRenderer))]
namespace WikiSpiv.iOS.CustomRenderers
{
    public class CustomCollectionViewRenderer : CollectionViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<GroupableItemsView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                AnimationsEnabled = false;
            }
        }
        //protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        //{
        //    this.OnElementChanged
        //    if (e.NewElement != null)
        //    {
        //        InsertRowsAnimation = UITableViewRowAnimation.None;
        //        DeleteRowsAnimation = UITableViewRowAnimation.None;
        //        ReloadRowsAnimation = UITableViewRowAnimation.None;
        //    }
        //}
    }
}
