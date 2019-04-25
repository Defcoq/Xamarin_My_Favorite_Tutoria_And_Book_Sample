using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalksJP.Services;
using TrackMyWalksJP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackMyWalksJP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalkTrailInfoPage : ContentPage
	{
        #region chap 04
        //      public WalkTrailInfoPage ()
        //{
        //	InitializeComponent ();
        //          // Update the page title for our Walk Information Page
        //          Title = "Trail Walk Information";

        //          // Set the Binding Context for our ContentPage
        //          this.BindingContext = App.SelectedItem;
        //      }


        //      // Instance method that proceeds to begin a new walk trail
        //      public void BeginTrailWalk_Clicked(object sender, EventArgs e)
        //      {
        //          if (App.SelectedItem == null)
        //              return;
        //          Navigation.PushAsync(new WalkDistancePage());
        //          Navigation.RemovePage(this);
        //      }

        #endregion

        #region chap 05
        //// Return the Binding Context for the ViewModel
        //WalkTrailInfoPageViewModel _viewModel => BindingContext as WalkTrailInfoPageViewModel;

        //public WalkTrailInfoPage()
        //{
        //    InitializeComponent();

        //    // Update the Title and Initialise our BindingContext for the Page
        //    this.Title = "Trail Walk Information";
        //    this.BindingContext = new WalkTrailInfoPageViewModel();
        //}

        //// Instance method that proceeds to begin a new walk trail
        //public void BeginTrailWalk_Clicked(object sender, EventArgs e)
        //{
        //    if (App.SelectedItem == null)
        //        return;

        //    Navigation.PushAsync(new WalkDistancePage());
        //    Navigation.RemovePage(this);
        //}
        #endregion

        #region chap 06 - 09
        // Return the Binding Context for the ViewModel
        WalkTrailInfoPageViewModel _viewModel => BindingContext as WalkTrailInfoPageViewModel;

        public WalkTrailInfoPage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            this.Title = "Trail Walk Information";
            this.BindingContext = new WalkTrailInfoPageViewModel(DependencyService.Get<INavigationService>());
        }
        // Method to initialise our View Model when the ContentPage appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Create a SlidingEntrance Animation for WalkTrailInfoPage
            double offset = 1000;
            foreach (View view in TrailInfoScrollView.Children)
            {
                view.TranslationX = offset;
                offset *= -1;
                await Task.WhenAny(view.TranslateTo(0, 0, 1000,Easing.SpringOut), Task.Delay(100));
            }

            // Create a Custom Animation for our BeginTrailWalk button
            var animation = new Animation(v => BeginTrailWalk.BackgroundColor = Color.FromHsla(v, 1, 0.5), start: 0, end: 1);

            animation.Commit(this, "BeginWalkCustomAnimation",
                             16,
                             5000,
                             Easing.Linear, (v, c) =>
                             BackgroundColor = Color.Default, () => true);
        }
            // Instance method that proceeds to begin a new walk trail
         public async void BeginTrailWalk_Clicked(object sender, EventArgs e)
        {
            if (App.SelectedItem == null)
                return;

            //chap 06
            //await _viewModel.Navigation.NavigateTo<WalkDistancePageViewModel>();

            //chap 09
            // Create a Simple Animation to rotate our Begin Trail 
            // Walk Button
            await BeginTrailWalk.RotateTo(360, 1000);
            BeginTrailWalk.Rotation = 0;

            // Create and Apply an Easing Function to our Button
            await BeginTrailWalk.RotateTo(15, 1000, new Easing(t =>
                                     Math.Sin(Math.PI * t) *
                                     Math.Sin(Math.PI * 20 * t)));
            await _viewModel.Navigation.NavigateTo<WalkDistancePageViewModel>();

        }
        #endregion


        #region chap 07

        #endregion

        #region chap 08

        #endregion

        #region chap 09

        #endregion

        #region chap 10

        #endregion
    }
}