using System;
using Xamarin.Forms;

namespace WikiSpiv.CustomViews
{
    /// <summary>
    /// There is some customization in the native renderers
    /// iOS
    ///  - Disables update animations (they were buggy and looked terrible)
    /// </summary>
    public class CustomListView : ListView
    {

        public CustomListView(): base(ListViewCachingStrategy.RecycleElement)
        {
        }
    }
}
