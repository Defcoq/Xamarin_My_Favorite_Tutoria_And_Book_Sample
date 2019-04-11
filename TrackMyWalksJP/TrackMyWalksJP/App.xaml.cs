using System;
using TrackMyWalksJP.Models;
using TrackMyWalksJP.Services;
using TrackMyWalksJP.ViewModels;
using TrackMyWalksJP.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TrackMyWalksJP
{
    public partial class App : Application
    {
        #region chap 04-05
        //public App()
        //{
        //    InitializeComponent();

        //    // MainPage = new MainPage();
        //    // Initialise and create an instance of our navigation service class
        //    //NavService = DependencyService.Get<INavigationService>() as NavigationService;
        //}
        //// Declare our WalkDataModel that will store our Walk Trail details
        //public static WalkDataModel SelectedItem { get; set; }

        //protected override void OnStart()
        //{
        //    // Check what Target OS Platform we are running on whenever the app starts
        //    if (Device.RuntimePlatform.Equals(Device.Android))
        //    {
        //        MainPage = new SplashPage();
        //    }
        //    else
        //    {
        //        // Set the root page for our application
        //        MainPage = new NavigationPage(new WalksMainPage());
        //    }
        //}

        //protected override void OnSleep()
        //{
        //    // Handle when your app sleeps
        //}

        //protected override void OnResume()
        //{
        //    // Handle when your app resumes
        //}
        #endregion

        #region chap 06
        public App()
        {
            InitializeComponent();

            // Initialise and create an instance of our navigation service class
            NavService = DependencyService.Get<INavigationService>() as NavigationService;
        }

        protected override void OnStart()
        {
            // Check what Target OS Platform we are running on whenever the app starts
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                // Set the Root Page for our application
                MainPage = new NavigationPage(new SplashPage());
            }
            else
            {
                // Set the Root Page and update the NavigationBar color for our app
                MainPage = new NavigationPage(new WalksMainPage())
                {
                    BarBackgroundColor = Color.IndianRed,
                    BarTextColor = Color.White,
                };
            }

            // Set the current main page to our Navigation Service
            NavService.XFNavigation = MainPage.Navigation;

            // Register each of our View Models on our Navigation Stack
            NavService.RegisterViewMapping(typeof(WalksMainPageViewModel), typeof(WalksMainPage));
            NavService.RegisterViewMapping(typeof(WalkEntryPageViewModel), typeof(WalkEntryPage));
            NavService.RegisterViewMapping(typeof(WalkTrailInfoPageViewModel), typeof(WalkTrailInfoPage));
            NavService.RegisterViewMapping(typeof(WalkDistancePageViewModel), typeof(WalkDistancePage));
        }

        // Declare our SelectedItem property that will store our Walk Trail details
        public static WalkDataModel SelectedItem { get; set; }

        // Declare our NavService property that will be used to navigate between ViewModels
        public static NavigationService NavService { get; set; }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
