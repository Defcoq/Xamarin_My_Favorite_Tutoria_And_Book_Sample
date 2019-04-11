using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalksJP.Services;

namespace TrackMyWalksJP.ViewModels
{
    public abstract class BaseViewModel: INotifyPropertyChanged
    {
        #region chap 05
        //    public const string PageTitlePropertyName = "PageTitle";
        //    string pageTitle;
        //    public string PageTitle
        //    {
        //        get => pageTitle;
        //        set { pageTitle = value; OnPropertyChanged(); }
        //    }
        //    protected BaseViewModel()
        //    {
        //    }

        //    public abstract Task Init();
        //    public event PropertyChangedEventHandler PropertyChanged;
        //    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //    {
        //        PropertyChangedEventHandler handler = PropertyChanged;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        //public abstract class BaseViewModel<TParam> : BaseViewModel
        //{
        //    protected BaseViewModel()
        //    {
        //    }

        #endregion


        #region chap 06

        public INavigationService Navigation { get; set; }
        public const string PageTitlePropertyName = "PageTitle";

        string pageTitle;
        public string PageTitle
        {
            get => pageTitle;
            set { pageTitle = value; OnPropertyChanged(); }
        }

        protected BaseViewModel(INavigationService navService)
        {
            Navigation = navService;
        }

        public abstract Task Init();
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class BaseViewModel<TParam> : BaseViewModel
    {
        protected BaseViewModel(INavigationService navService) : base(navService)
        {
        }
    }
    #endregion
}



