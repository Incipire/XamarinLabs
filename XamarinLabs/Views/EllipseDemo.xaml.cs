using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinLabs.Views
{
    public partial class EllipseDemo : ContentPage
    {
        public EllipseDemo()
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
