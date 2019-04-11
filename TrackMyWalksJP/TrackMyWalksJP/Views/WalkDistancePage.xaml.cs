using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalksJP.Services;
using TrackMyWalksJP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TrackMyWalksJP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalkDistancePage : ContentPage
	{
        #region chap 04
        //      public WalkDistancePage ()
        //{
        //	InitializeComponent ();

        //          // Update the page title for our Distance Travelled Page
        //          Title = "Distance Travelled Information";

        //          // Place a pin on the map for the chosen walk type
        //          MyCustomTrailMap.Pins.Add(new Pin
        //          {
        //              Type = PinType.Place,
        //              Position = new Position(App.SelectedItem.Latitude,App.SelectedItem.Longitude),
        //              Label = App.SelectedItem.Title,
        //              Address = "Difficulty: " + App.SelectedItem.Difficulty + "  Total Distance: " + App.SelectedItem.Distance,
        //              Id = App.SelectedItem.Title
        //          });

        //          // Create a region around the map within a one-kilometer radius
        //          MyCustomTrailMap.MoveToRegion(MapSpan.FromCenterAndRadius(new
        //          Position(App.SelectedItem.Latitude,
        //          App.SelectedItem.Longitude), Distance.FromKilometers(1.0)));
        //      }

        //      // Instance method that ends the current trail and returns back to the main screen.
        //      public void EndThisTrailButton_Clicked(object sender, EventArgs e)
        //      {
        //          App.SelectedItem = null;
        //          Navigation.PopToRootAsync(true);
        //      }
        #endregion

        #region chap 05
        //// Return the Binding Context for the ViewModel
        //WalkDistancePageViewModel _viewModel => BindingContext as WalkDistancePageViewModel;

        //public WalkDistancePage()
        //{
        //    InitializeComponent();

        //    // Update the Title and Initialise our BindingContext for the Page
        //    Title = "Distance Travelled Information";
        //    this.BindingContext = new WalkDistancePageViewModel();

        //    // Create a pin placeholder within the map containing the walk information
        //    customMap.Pins.Add(new Pin
        //    {
        //        Type = PinType.Place,
        //        Position = new Position(_viewModel.Latitude, _viewModel.Longitude),
        //        Label = _viewModel.Title,
        //        Address = "Difficulty: " + _viewModel.Difficulty + "  Total Distance: " + _viewModel.Distance,
        //        Id = _viewModel.Title
        //    });

        //    // Create a region around the map within a one-kilometer radius
        //    customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_viewModel.Latitude, _viewModel.Longitude), Distance.FromKilometers(1.0)));
        //}

        //// Instance method that ends the current trail and returns back to the main screen
        //public void EndThisTrailButton_Clicked(object sender, EventArgs e)
        //{
        //    App.SelectedItem = null;
        //    Navigation.PopToRootAsync(true);
        //}

        #endregion

        #region chap 06
        // Return the Binding Context for the ViewModel
        WalkDistancePageViewModel _viewModel => BindingContext as WalkDistancePageViewModel;

        public WalkDistancePage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            Title = "Distance Travelled Information";
            this.BindingContext = new WalkDistancePageViewModel(DependencyService.Get<INavigationService>());

            // Create a pin placeholder within the map containing the walk information
            customMap.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Position = new Position(_viewModel.Latitude, _viewModel.Longitude),
                Label = _viewModel.Title,
                Address = "Difficulty: " + _viewModel.Difficulty + "  Total Distance: " + _viewModel.Distance,
                Id = _viewModel.Title
            });

            // Create a region around the map within a one-kilometer radius
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_viewModel.Latitude, _viewModel.Longitude), Distance.FromKilometers(1.0)));
        }

        // Instance method that ends the current trail and returns back to the main screen
        public async void EndThisTrailButton_Clicked(object sender, EventArgs e)
        {
            App.SelectedItem = null;
            await _viewModel.Navigation.BackToMainPage();
        }
        #endregion
    }
}