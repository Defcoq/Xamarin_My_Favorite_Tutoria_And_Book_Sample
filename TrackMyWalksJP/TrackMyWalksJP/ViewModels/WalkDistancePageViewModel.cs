using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalksJP.Services;
using Plugin.Geolocator.Abstractions;

namespace TrackMyWalksJP.ViewModels
{
    public class WalkDistancePageViewModel : BaseViewModel
    {
        #region chap 05
        //public WalkDistancePageViewModel()
        //{
        //}

        //// Update each control on the WalkDistancePage with values from our Model
        //public string Title => App.SelectedItem.Title;
        //public string Description => App.SelectedItem.Description;
        //public double Latitude => App.SelectedItem.Latitude;
        //public double Longitude => App.SelectedItem.Longitude;
        //public double Distance => App.SelectedItem.Distance;
        //public String Difficulty => App.SelectedItem.Difficulty;
        //public String ImageUrl => App.SelectedItem.ImageUrl;

        //// Instance method to initialise the WalkDistancePageViewModel
        //public override async Task Init()
        //{
        //    await Task.Factory.StartNew(() =>
        //    {
        //    });
        //}

        #endregion

        #region coming from chap 07
        // Initialise our location service variable that points to 
        // our LocationService class
        LocationService location;
        public event EventHandler<PositionEventArgs> CoordsChanged;


        // Instance method to get the current GPS location 
        // Coordinates from device 
              public async Task<Position> GetCurrentLocation()
              {
                  // Initialise our location service variable that points to 
                  // our LocationService class
                  location = new LocationService();
                  location.PositionChanged += (sender, e) =>
                  {
                      // Raise our PositionChanged EventHandler, using 
                      // the Coordinates
                      CoordsChanged.Invoke(sender, e);
                  };

                // Get the current device GPS location coordinates
                var position = await location.GetCurrentPosition();
                              return position;
              }

                // Instance method to begin listening for changes in 
                // GPS coordinates
                public async void OnStartUpdate()
                {
                    await location.StartListening();
                }

                // Instance method to stop listening for changes 
                // in location
                public void OnStopUpdate()
                {
                    location.StopListening();
                }
#endregion
#region chap 06
public WalkDistancePageViewModel(INavigationService navService) : base(navService)
        {
        }

        // Update each control on the WalkDistancePage with values from our Model
        public string Title => App.SelectedItem.Title;
        public string Description => App.SelectedItem.Description;
        public double Latitude => Convert.ToDouble(App.SelectedItem.Latitude);
        public double Longitude => Convert.ToDouble( App.SelectedItem.Longitude);
        public double Distance => Convert.ToDouble(App.SelectedItem.Distance);
        public String Difficulty => App.SelectedItem.Difficulty;
        public String ImageUrl => App.SelectedItem.ImageUrl;

        // Instance method to initialise the WalkDistancePageViewModel
        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
        #endregion
    }
}
