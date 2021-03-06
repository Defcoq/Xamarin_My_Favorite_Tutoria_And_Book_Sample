﻿using System;
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
	public partial class WalkEntryPage : ContentPage
	{
        #region chap 04
        //      public WalkEntryPage ()
        //{
        //	InitializeComponent ();
        //          Title = "New Walk Entry Page";
        //      }

        //      public void SaveWalkItem_Clicked(object sender, EventArgs e)
        //      {
        //          Navigation.PopToRootAsync(true);
        //      }
        #endregion


        #region chap 05
        // Return the Binding Context for the ViewModel
        //WalkEntryPageViewModel _viewModel => BindingContext as WalkEntryPageViewModel;

        //public WalkEntryPage()
        //{
        //    InitializeComponent();

        //    // Update the Title and Initialise our BindingContext for the Page
        //    Title = "New Walk Entry Page";
        //    BindingContext = new WalkEntryPageViewModel();
        //    SetBinding(TitleProperty, new Binding(BaseViewModel.PageTitlePropertyName));
        //}

        //// Instance method that saves the new walk entry
        //public async void SaveWalkItem_Clicked(object sender, EventArgs e)
        //{
        //    // Prompt the user with a confirmation dialog to confirm
        //    if (await DisplayAlert("Save Walk Entry Item", "Proceed and save changes?", "OK", "Cancel"))
        //    {
        //        // Attempt to save and validate our Walk Entry Item
        //        if (!_viewModel.ValidateFormDetailsAndSave())
        //            // Error Saving - Must have Title and description
        //            await DisplayAlert("Validation Error", "Title and Description are required.", "OK");
        //        else
        //            // Navigate back to the Track My Walks Listing page
        //            await Navigation.PopToRootAsync(true);
        //    }
        //    else
        //    {
        //        // Navigate back to the Track My Walks Listing page
        //        await Navigation.PopToRootAsync(true);
        //    }
        //}

        #endregion

        #region chap 06
        // Return the Binding Context for the ViewModel
        WalkEntryPageViewModel _viewModel => BindingContext as WalkEntryPageViewModel;

        public WalkEntryPage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            Title = "New Walk Entry Page";
            BindingContext = new WalkEntryPageViewModel(DependencyService.Get<INavigationService>());
            SetBinding(TitleProperty, new Binding(BaseViewModel.PageTitlePropertyName));
        }

        // Instance method that saves the new walk entry
        public async void SaveWalkItem_Clicked(object sender, EventArgs e)
        {
            // Prompt the user with a confirmation dialog to confirm
            if (await DisplayAlert("Save Walk Entry Item", "Proceed and save changes?", "OK", "Cancel"))
            {
                // Attempt to save and validate our Walk Entry Item
                if (! await _viewModel.ValidateFormDetailsAndSave())
                    // Error Saving - Must have Title and description
                    await DisplayAlert("Validation Error", "Title and Description are required.", "OK");
                else
                    // Navigate back to the Track My Walks Listing page
                    await _viewModel.Navigation.RemoveViewFromStack();
            }
            else
            {
                // Navigate back to the Track My Walks Listing page
                await _viewModel.Navigation.RemoveViewFromStack();
            }
        }

        //begin from  chap 09 Method to initialise our View Model when the ContentPage appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //// Create a SwingingEntrance Animation for our WalkDetails TableView
            //WalkDetails.RotationY = 180;
            //await WalkDetails.RotateYTo(0, 1000, Easing.BounceOut);
            //WalkDetails.AnchorX = 0.5;

            // Create a Simple Animation to rotate our Difficulty Level Image
            DifficultyLevel.AnchorY = (Math.Min(DifficultyLevel.Width, DifficultyLevel.Height) / 2) / DifficultyLevel.Height;
            await DifficultyLevel.RotateTo(360, 2000, Easing.BounceOut);

            // Create a SwingingEntrance Animation for our WalkDetails TableView
            WalkDetails.RotationY = 180;
            await WalkDetails.RotateYTo(0, 1000, Easing.BounceOut);
            WalkDetails.AnchorX = 0.5;
        }
        #endregion
    }
}