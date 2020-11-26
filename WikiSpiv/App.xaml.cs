using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WikiSpiv.Views;
using System.Collections.Generic;
//using WikiSpiv.Themes;
using WikiSpiv.Data;
using WikiSpiv.Extras;
using System.Threading.Tasks;

namespace WikiSpiv
{
    public partial class App : Application
    {

        public App()
        {
            // Dark mode theming tutorial:
            // https://mitchelsellers.com/blog/article/adding-dark-theme-support-for-xamarin-forms-shell
            Xamarin.Forms.Device.SetFlags(new string[] { "AppTheme_Experimental" });

            InitializeComponent();

            // Handle system theme
            // TODO: "To respond to theme changes on Android you must include the ConfigChanges.UiMode flag in the Activity attribute of your MainActivity class."
            //updateThemeFromSystem(Application.Current.RequestedTheme);
            //Application.Current.RequestedThemeChanged += (s, a) =>
            //{
            //    updateThemeFromSystem(a.RequestedTheme);
            //};

            WikiSpivDatabase data = new WikiSpivDatabase();

            //DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();



            var statusbar = DependencyService.Get<IStatusBarPlatformSpecific>();
            //statusbar.SetStatusBarColor(Color.Green);

            UpdateStatusBarTheme(Application.Current.RequestedTheme);
            Application.Current.RequestedThemeChanged += (s, a) =>
            {
                UpdateStatusBarTheme(a.RequestedTheme);
            };

            //Task.Delay(2000); //wait for two milli seconds

            //var statusbar = DependencyService.Get<IStatusBarPlatformSpecific>();
            //statusbar.SetStatusBarColor(Color.Green);
        }

        private void UpdateStatusBarTheme(OSAppTheme currentTheme)
        {
            var statusbar = DependencyService.Get<IStatusBarPlatformSpecific>();
            if (currentTheme == OSAppTheme.Dark)
            {
                statusbar.SetStatusBarColor(Color.Black);
            }
            else
            {
                statusbar.SetStatusBarColor(Color.FromHex("1775D1"));
            }
        }


        //private void updateThemeFromSystem(OSAppTheme currentTheme)
        //{
        //    Console.WriteLine("Updating theme");
        //    ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        //    if (mergedDictionaries != null)
        //    {
        //        mergedDictionaries.Clear();

        //        if (currentTheme == OSAppTheme.Dark)
        //        {
        //            Console.WriteLine("Making theme dark");
        //            mergedDictionaries.Add(new DarkTheme());
        //        }
        //        else
        //        {
        //            Console.WriteLine("Making theme light");
        //            mergedDictionaries.Add(new LightTheme());
        //        }
        //    }
        //}

        protected override void OnStart()
        {
            Console.WriteLine("Start");
        }

        protected override void OnSleep()
        {
            Console.WriteLine("Sleep");
        }

        protected override void OnResume()
        {
            Console.WriteLine("Resume");
        }
    }
}
