﻿using Xamarin.Forms;

namespace XamarinLabs
{
    public partial class XamarinLabsPage : ContentPage
    {
        public XamarinLabsPage()
        {
            InitializeComponent();
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
			ellipse.RotateTo(360, 2000);
		}
    }
}
