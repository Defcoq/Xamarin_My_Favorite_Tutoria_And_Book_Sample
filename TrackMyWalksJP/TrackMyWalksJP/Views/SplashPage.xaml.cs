using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackMyWalksJP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage : ContentPage
	{
		public SplashPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Set a wait delay of 3 seconds on our Splash Screen
            await Task.Delay(3000);

            // Create a new navigation page, using the WalksMainPage
            Application.Current.MainPage = new NavigationPage(new WalksMainPage());
        }
    }
}