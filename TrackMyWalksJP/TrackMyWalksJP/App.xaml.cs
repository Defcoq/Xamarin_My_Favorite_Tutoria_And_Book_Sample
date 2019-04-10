using System;
using TrackMyWalksJP.Models;
using TrackMyWalksJP.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TrackMyWalksJP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

           // MainPage = new MainPage();
        }
        // Declare our WalkDataModel that will store our Walk Trail details
        public static WalkDataModel SelectedItem { get; set; }

        protected override void OnStart()
        {
            // Check what Target OS Platform we are running on whenever the app starts
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                MainPage = new SplashPage();
            }
            else
            {
                // Set the root page for our application
                MainPage = new NavigationPage(new WalksMainPage());
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
