﻿using System.Linq;
using Xamarin.Forms;
using WikiSpiv.Models;
using WikiSpiv.Data;

namespace WikiSpiv.Views
{
    public partial class CatsPage : ContentPage
    {
        public CatsPage()
        {
            InitializeComponent();
            ListView.ItemsSource = WikiSpivDatabase.WikiSpivItems;
            //ListView.Anim
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string catName = (e.CurrentSelection.FirstOrDefault() as Animal).Name;
            // This works because route names are unique in this application.
            //await Shell.Current.GoToAsync($"catdetails?name={catName}");
            // The full route is shown below.
            // await Shell.Current.GoToAsync($"//animals/domestic/cats/catdetails?name={catName}");
        }
    }
}
