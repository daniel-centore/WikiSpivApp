using Xamarin.Forms;
using WikiSpiv.ViewModels;

namespace WikiSpiv.Views
{
    public partial class MonkeyDetailPage : ContentPage
    {
        public MonkeyDetailPage()
        {
            InitializeComponent();
            BindingContext = new MonkeyDetailViewModel();
        }
    }
}
