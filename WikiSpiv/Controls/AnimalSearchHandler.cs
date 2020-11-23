using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using WikiSpiv.Models;

namespace WikiSpiv.Controls
{
    public class AnimalSearchHandler : SearchHandler
    {
        public IList<Animal> Animals { get; set; }
        public Type SelectedItemNavigationTarget { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Animals
                    .Where(animal => animal.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList<Animal>();
            }
        }

        public AnimalSearchHandler()
        {
        }

        protected override async void OnItemSelected(object item)
        {
            // TODO: Remove this hack once xamarin fixes the issue
            // https://github.com/xamarin/Xamarin.Forms/issues/5713
            // Still not fixed - 21 Nov, 2020
            if (Device.RuntimePlatform == Device.iOS)
            {
                // 300 - Fails
                // 305 - Sometimes works
                // 315 - Works
                await Task.Delay(400);
            }

            await Shell.Current.GoToAsync(
                $"{GetNavigationTarget()}?name={((Animal)item).Name}",
                false // animate
            );
        }

        private string GetNavigationTarget()
        {
            return (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
        }
    }
}
