﻿using Xamarin.Forms;

namespace XamarinLabs
{
    public partial class XamarinLabsPage : ContentPage
    {
        public XamarinLabsPage()
        {
            InitializeComponent();
        }

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            //This check prevents the animation from crashing if the user taps
            //twice.
            if (!ellipse.AnimationIsRunning("RotateTo"))
            {
                await ellipse.RotateTo(360, 2000);
                await ellipse.RotateTo(0, 1);
            }
        }
    }
}
