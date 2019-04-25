using System;
using System.Threading.Tasks;
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

        #region chap 06 -07-08-09-10-11-12
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
            NavService.RegisterViewMapping(typeof(TwitterSignInPageViewModel), typeof(TwitterSignInPage));
        }

        // Declare our SelectedItem property that will store our Walk Trail details
        public static WalkDataModel SelectedItem { get; set; }

        // Declare our NavService property that will be used to navigate between ViewModels
        public static NavigationService NavService { get; set; }
        #region Twitter Sign In Page Property and Instance methods to remove and Navigate (Android Only)

        // Action property method to remove our TwitterSignInPage from the NavigationStack
        public static Action RemoveTwitterSignInPage => new Action(() => NavService.XFNavigation.PopAsync());

        // Navigate to our WalksMainPage, once we have successfully signed in
        public async static Task NavigateToWalksMainPage()
        {
            await NavService.XFNavigation.PushAsync(new WalksMainPage());
        }
        #endregion

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
