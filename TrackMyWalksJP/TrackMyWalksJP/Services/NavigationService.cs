﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalksJP.Services;
using TrackMyWalksJP.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationService))]
namespace TrackMyWalksJP.Services
{
   public class NavigationService : INavigationService
    {
        public INavigation XFNavigation { get; set; }
        readonly IDictionary<Type, Type> _viewMapping = new Dictionary<Type, Type>();

        // Register our ViewModel and View within our Dictionary
        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _viewMapping.Add(viewModel, view);
        }

        // Removes the most recent Page from the navigation stack.
        public Task<Page> RemoveViewFromStack()
        {
            return XFNavigation.PopAsync();
        }

        // Returns to the Root Page after removing the current page from the navigation stack
        public Task BackToMainPage()
        {
            return XFNavigation.PopToRootAsync(true);
        }

        // Navigates navigates to a specific ViewModel 
        public async Task NavigateTo<TVM>()
            where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));

            if (XFNavigation.NavigationStack.Last().BindingContext is BaseViewModel)
                await ((BaseViewModel)(XFNavigation.NavigationStack.Last().BindingContext)).Init();
        }

        // Navigates to a specific ViewModel within our dictionary viewMapping
        public async Task NavigateToView(Type viewModelType)
        {
            Type viewType;

            if (!_viewMapping.TryGetValue(viewModelType, out viewType))
                throw new ArgumentException("No view found in View Mapping for " + viewModelType.FullName + ".");

            var constructor = viewType.GetTypeInfo()
                .DeclaredConstructors.FirstOrDefault(dc => !dc.GetParameters().Any());

            var view = constructor.Invoke(null) as Page;
            await XFNavigation.PushAsync(view, true);
        }
    }
}
