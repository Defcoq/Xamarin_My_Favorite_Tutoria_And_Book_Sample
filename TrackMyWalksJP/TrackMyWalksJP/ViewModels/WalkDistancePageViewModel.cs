﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalksJP.Services;

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

        #region chap 06
        public WalkDistancePageViewModel(INavigationService navService) : base(navService)
        {
        }

        // Update each control on the WalkDistancePage with values from our Model
        public string Title => App.SelectedItem.Title;
        public string Description => App.SelectedItem.Description;
        public double Latitude => App.SelectedItem.Latitude;
        public double Longitude => App.SelectedItem.Longitude;
        public double Distance => App.SelectedItem.Distance;
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
