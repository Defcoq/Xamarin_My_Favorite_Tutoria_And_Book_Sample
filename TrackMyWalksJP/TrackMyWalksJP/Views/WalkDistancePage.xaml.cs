using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalksJP.Services;
using TrackMyWalksJP.ViewModels;
using TrackMyWalksJP.Views.MapOverlay;
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

        #region chap 06 -07
        // Return the Binding Context for the ViewModel
        WalkDistancePageViewModel _viewModel => BindingContext as WalkDistancePageViewModel;

        #region coming from chap 07
        // Create a variable that will store our original saved Position
        Task<Plugin.Geolocator.Abstractions.Position> origPosition;
        #endregion

        public WalkDistancePage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            Title = "Distance Travelled Information";
            this.BindingContext = new WalkDistancePageViewModel(DependencyService.Get<INavigationService>());
            #region coming from chapo 07
            // Get the current GPS location coordinates and listen for updates
            origPosition = _viewModel.GetCurrentLocation();
            _viewModel.CoordsChanged += Location_PositionChanged;
            _viewModel.OnStartUpdate();

            // Instantiate our Custom Map Overlay
            customMap = new CustomMapOverlay
            {
                MapType = MapType.Street
            };

            // Clear all previously created Pins on our CustomMap
            customMap.Pins.Clear();

            // Create the Pin placeholder that will represent our current location
            CreatePinPlaceholder(PinType.Place,
                        origPosition.Result.Latitude,
                        origPosition.Result.Longitude,
            "",
            "My Location", 1);

            // Create the Pin placeholder that will represent our ending location
            CreatePinPlaceholder(PinType.Place,
                                 _viewModel.Latitude,
                                 _viewModel.Longitude,
                                 _viewModel.Title,
                                 "Difficulty: " + _viewModel.Difficulty + " Total Distance: " + _viewModel.Distance,
                                 2);

            // Add the Starting and Ending Latitude and Longitude Coordinates
            customMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(origPosition.Result.Latitude, origPosition.Result.Longitude));
            customMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(_viewModel.Latitude, _viewModel.Longitude));

            // Create and Initialise a map region within a one-kilometre radius
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(origPosition.Result.Latitude, origPosition.Result.Longitude), Distance.FromKilometers(1)));

            // Display our Custom Map for the detected device Platform
            Content = customMap;
            #endregion
            // Create a pin placeholder within the map containing the walk information
            #region coming from chap 06 comment
            //customMap.Pins.Add(new Pin
            //{
            //    Type = PinType.Place,
            //    Position = new Position(_viewModel.Latitude, _viewModel.Longitude),
            //    Label = _viewModel.Title,
            //    Address = "Difficulty: " + _viewModel.Difficulty + "  Total Distance: " + _viewModel.Distance,
            //    Id = _viewModel.Title
            //});

            //// Create a region around the map within a one-kilometer radius
            //customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_viewModel.Latitude, _viewModel.Longitude), Distance.FromKilometers(1.0)));

            #endregion
        }
        #region coming from chap 07
        // Instance method to handle updating the UI whenever the location changes
        void Location_PositionChanged(object sender, PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                // Calculate the total distance travelled from the origPosition to the Current GPS Coordinate
                var distanceTravelled = origPosition.Result.CalculateDistance(e.Position, GeolocatorUtils.DistanceUnits.Kilometers);

                // Create a new Pin Placeholder, showing the current GPS Coordinate and the distance travelled
                CreatePinPlaceholder(PinType.SavedPin,
                                     e.Position.Latitude,
                                     e.Position.Longitude,
                                     String.Format("Travelled: {0:0.00} KM", distanceTravelled),
                                     "",
                                     3);
            });
        }

        // Instance method to create a pin placeholder to the custom map
        public void CreatePinPlaceholder(PinType pinType, double latitude, double longitude, String label, String address, int Id)
        {
            customMap.Pins.Add(new Pin
            {
                Type = pinType,
                Position = new Xamarin.Forms.Maps.Position(latitude, longitude),
                Label = label,
                Address = address,
                Id = Id
            });

            // Show the users current location on the map
            customMap.IsShowingUser = true;
        }

        // Instance method that ends the current trail and returns back to the main screen
        public async void EndTrailButton_Clicked(object sender, EventArgs e)
        {
            // Stop listening for location updates prior to navigating
            App.SelectedItem = null;
            _viewModel.OnStopUpdate();
            await _viewModel.Navigation.BackToMainPage();
        }
        #endregion

        #region coming from chap 06 comment
        // Instance method that ends the current trail and returns back to the main screen
        //public async void EndThisTrailButton_Clicked(object sender, EventArgs e)
        //{
        //    App.SelectedItem = null;
        //    await _viewModel.Navigation.BackToMainPage();
        //}
        #endregion
        #endregion
    }
}