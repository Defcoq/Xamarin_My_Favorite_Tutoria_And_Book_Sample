using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalksJP.Services;

namespace TrackMyWalksJP.ViewModels
{
    public class TwitterSignInPageViewModel : BaseViewModel
    {
        public TwitterSignInPageViewModel(INavigationService navService) :base(navService)
        {
        }
        // Instance method to initialise the TwitterSignInPageViewModel
        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
    }
}
