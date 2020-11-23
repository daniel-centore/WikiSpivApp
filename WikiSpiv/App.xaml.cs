using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WikiSpiv.Views;
using System.Collections.Generic;
using WikiSpiv.Themes;
using WikiSpiv.Data;

namespace WikiSpiv
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            // Handle system theme
            // TODO: "To respond to theme changes on Android you must include the ConfigChanges.UiMode flag in the Activity attribute of your MainActivity class."
            updateThemeFromSystem(Application.Current.RequestedTheme);
            Application.Current.RequestedThemeChanged += (s, a) =>
            {
                updateThemeFromSystem(a.RequestedTheme);
            };

            WikiSpivDatabase data = new WikiSpivDatabase();

            //DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();

            
        }

        private void updateThemeFromSystem(OSAppTheme currentTheme)
        {
            Console.WriteLine("Updating theme");
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                if (currentTheme == OSAppTheme.Dark)
                {
                    Console.WriteLine("Making theme dark");
                    mergedDictionaries.Add(new DarkTheme());
                }
                else
                {
                    Console.WriteLine("Making theme light");
                    mergedDictionaries.Add(new LightTheme());
                }
            }
        }

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
