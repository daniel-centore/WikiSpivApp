﻿using System;
using System.Linq;
using Xamarin.Forms;
using WikiSpiv.Data;

namespace WikiSpiv.Views
{
    [QueryProperty("Name", "name")]
    public partial class DogDetailPage : ContentPage
    {
        public string Name
        {
            set
            {
                BindingContext = DogData.Dogs.FirstOrDefault(m => m.Name == Uri.UnescapeDataString(value));
            }
        }

        public DogDetailPage()
        {
            InitializeComponent();
        }
    }
}
