using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // Return the Binding Context for the ViewModel
        WalkTrailInfoPageViewModel _viewModel => BindingContext as WalkTrailInfoPageViewModel;

        public WalkTrailInfoPage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            this.Title = "Trail Walk Information";
            this.BindingContext = new WalkTrailInfoPageViewModel();
        }

        // Instance method that proceeds to begin a new walk trail
        public void BeginTrailWalk_Clicked(object sender, EventArgs e)
        {
            if (App.SelectedItem == null)
                return;

            Navigation.PushAsync(new WalkDistancePage());
            Navigation.RemovePage(this);
        }
    }
}