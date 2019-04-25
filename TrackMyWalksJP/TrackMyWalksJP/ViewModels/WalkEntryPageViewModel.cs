using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalksJP.Models;
using TrackMyWalksJP.Services;

namespace TrackMyWalksJP.ViewModels
{
    public class WalkEntryPageViewModel : BaseViewModel
    {
        // Handle Adding/Editing of Walk Entry Items
        bool isAdding;
        #region chap 05
        //public WalkEntryPageViewModel()
        //{
        //    // Update the title if we are creating a new Walk Entry
        //    if (App.SelectedItem == null)
        //    {
        //        PageTitle = "Adding Trail Details";
        //        App.SelectedItem = new WalkDataModel();

        //        // Set the default values when creating a new Trail
        //        Title = "New Trail Entry";
        //        Difficulty = "Easy";
        //        Distance = 1.0;
        //    }
        //    else
        //    {
        //        // Otherwise, we must be editing an existing entry
        //        PageTitle = "Editing Trail Details";
        //    }
        //}

        //// Checks to see if we have provided a Title and Description
        //public bool ValidateFormDetailsAndSave()
        //{
        //    if (App.SelectedItem != null && !string.IsNullOrEmpty(App.SelectedItem.Title) && !string.IsNullOrEmpty(App.SelectedItem.Description))
        //    {
        //        // Save the selected item to our database and/or model
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //// Update each EntryCell on the WalkEntryPage with values from our Model
        //public string Title
        //{
        //    get { return App.SelectedItem.Title; }
        //    set { App.SelectedItem.Title = value; OnPropertyChanged(); }
        //}
        //public string Description
        //{
        //    get { return App.SelectedItem.Description; }
        //    set { App.SelectedItem.Description = value; OnPropertyChanged(); }
        //}
        //public double Latitude
        //{
        //    get { return App.SelectedItem.Latitude; }
        //    set { App.SelectedItem.Latitude = value; OnPropertyChanged(); }
        //}
        //public double Longitude
        //{
        //    get { return App.SelectedItem.Longitude; }
        //    set { App.SelectedItem.Longitude = value; OnPropertyChanged(); }
        //}
        //public double Distance
        //{
        //    get { return App.SelectedItem.Distance; }
        //    set { App.SelectedItem.Distance = value; OnPropertyChanged(); }
        //}
        //public String Difficulty
        //{
        //    get { return App.SelectedItem.Difficulty; }
        //    set { App.SelectedItem.Difficulty = value; OnPropertyChanged(); }
        //}
        //public String ImageUrl
        //{
        //    get { return App.SelectedItem.ImageUrl; }
        //    set { App.SelectedItem.ImageUrl = value; OnPropertyChanged(); }
        //}

        //// Instance method to initialise the WalkEntryPageViewModel
        //public override async Task Init()
        //{
        //    await Task.Factory.StartNew(() =>
        //    {
        //    });
        //}

        #endregion

        #region chap 06
        public WalkEntryPageViewModel(INavigationService navService) : base(navService)
        {
            // Update the title if we are creating a new Walk Entry
            if (App.SelectedItem == null)
            {
                PageTitle = "Adding Trail Details";
                App.SelectedItem = new WalkDataModel();

                // We are adding a new Walk Entry to our Azure Database
                isAdding = true;
                // Set the default values when creating a new Trail
                Title = "New Trail Entry";
                Difficulty = "Easy";
                Distance =  1.0;
            }
            else
            {
                // Otherwise, we must be editing an existing entry
                PageTitle = "Editing Trail Details";
                isAdding = false;
            }
        }

        // Checks to see if we have provided a Title and Description
        // Checks to see if we have provided a Title and Description
        public async Task<bool> ValidateFormDetailsAndSave()
        {
            if (App.SelectedItem != null &&
                !string.IsNullOrEmpty(App.SelectedItem.Title) &&
                !string.IsNullOrEmpty(App.SelectedItem.Description) &&
                !string.IsNullOrEmpty(App.SelectedItem.ImageUrl))
            {
                // Check our IsProcessBusy property to see if we are already processing
                if (IsProcessBusy)
                    return false;

                // If we aren't processing, we need to set our IsProcessBusy property to true
                IsProcessBusy = true;

                // Save our Walk Entry details to our Microsoft Azure Database
                await AzureDatabase.SaveWalkEntryFirebase(App.SelectedItem, isAdding);
                IsProcessBusy = false;
            }
            else
            {
                // Initialise our IsProcessBusy property to false
                IsProcessBusy = false;
                return false;
            }
            // Initialise our IsProcessBusy property to false
            IsProcessBusy = false;
            return true;
        }

        #region introdocted from chap 07 for get location
        // Get the current device GPS location Coordinates
        public async Task GetMyLocation()
        {
            // Get the current determined GPS position coordinates 
            // from the device
            var position = await new LocationService().GetCurrentPosition();

            if (position == null) return;

            // If we are Adding a new Walk Entry, update the Latitude 
            // and Longitude Coordinates
            if (App.SelectedItem.Latitude.Equals(0) && App.SelectedItem.Longitude.Equals(0))
            {
                Latitude = position.Latitude;
                Longitude = position.Longitude;
            }
        }
           

        #endregion

        // Update each EntryCell on the WalkEntryPage with values from our Model
        public string Title
        {
            get => App.SelectedItem.Title;
            set { App.SelectedItem.Title = value; OnPropertyChanged(); }
        }
        public string Description
        {
            get => App.SelectedItem.Description;
            set { App.SelectedItem.Description = value; OnPropertyChanged(); }
        }
        public double Latitude
        {
            get => App.SelectedItem.Latitude;
            set { App.SelectedItem.Latitude = value; OnPropertyChanged(); }
        }
        public double Longitude
        {
            get => App.SelectedItem.Longitude;
            set { App.SelectedItem.Longitude = value; OnPropertyChanged(); }
        }
        public double Distance
        {
            get => App.SelectedItem.Distance;
            set { App.SelectedItem.Distance = value; OnPropertyChanged(); }
        }
        public String Difficulty
        {
            get => App.SelectedItem.Difficulty;
            set { App.SelectedItem.Difficulty = value; OnPropertyChanged(); }
        }
        public String ImageUrl
        {
            get => App.SelectedItem.ImageUrl;
            set { App.SelectedItem.ImageUrl = value; OnPropertyChanged(); }
        }

        // Instance method to initialise the WalkEntryPageViewModel
        public override async Task Init()
        {
            await Task.Factory.StartNew(async() =>
            {
                await GetMyLocation();
            });
        }
    }
    #endregion
}

