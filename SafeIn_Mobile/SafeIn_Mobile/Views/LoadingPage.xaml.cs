﻿using SafeIn_Mobile.ViewModels;
using Splat;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SafeIn_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
        }
        internal LoadingViewModel ViewModel { get; set; } = Locator.Current.GetService<LoadingViewModel>();

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Init();
        }

        
    }
}