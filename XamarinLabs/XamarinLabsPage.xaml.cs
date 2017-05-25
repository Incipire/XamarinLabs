﻿using Xamarin.Forms;
using XamarinLabs.Views;

namespace XamarinLabs
{
    public partial class XamarinLabsPage : ContentPage
    {
        public XamarinLabsPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EllipseDemo());
        }
    }
}
