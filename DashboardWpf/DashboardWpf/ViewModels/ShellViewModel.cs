using DashboardWpf.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashboardWpf.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            Initialize();
        }

        private void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.NAVIGATION_MENU_REGION, typeof(Views.NavigationMenuView));
            _regionManager.RegisterViewWithRegion(RegionNames.CONTENT_REGION, typeof(Views.DefaultView));
        }
    }
}
