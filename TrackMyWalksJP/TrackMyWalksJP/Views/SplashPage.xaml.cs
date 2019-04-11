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
        #region chap 04- 05
        //      public SplashPage ()
        //{
        //	InitializeComponent ();
        //}

        //      protected override async void OnAppearing()
        //      {
        //          base.OnAppearing();

        //          // Set a wait delay of 3 seconds on our Splash Screen
        //          await Task.Delay(3000);

        //          // Create a new navigation page, using the WalksMainPage
        //          Application.Current.MainPage = new NavigationPage(new WalksMainPage());
        //      }
        #endregion

        #region chap 06
        public SplashPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Set a wait delay of 3 seconds on our Splash Screen
            await Task.Delay(3000);

            // Update the Main Page and update the NavigationBar color for our app
            Application.Current.MainPage = new NavigationPage(new WalksMainPage())
            {
                BarBackgroundColor = Color.CadetBlue,
                BarTextColor = Color.White,
            };
            // Update the Application's Navigation Service property
            App.NavService.XFNavigation = Application.Current.MainPage.Navigation;
        }
        #endregion
    }
}