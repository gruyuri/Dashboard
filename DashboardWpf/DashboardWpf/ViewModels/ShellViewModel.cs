using DashboardWpf.Core;
using DashboardWpf.Core.Events;
using Prism.Commands;
using Prism.Events;
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

        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;

            eventAggregator.GetEvent<ModuleSelected>().Subscribe(OnModuleSelected);

            Initialize();
        }

        private void OnModuleSelected(string moduleName)
        {
            _regionManager.RequestNavigate(RegionNames.CONTENT_REGION, moduleName);
        }

        private void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.NAVIGATION_MENU_REGION, typeof(Views.NavigationMenuView));
            _regionManager.RegisterViewWithRegion(RegionNames.CONTENT_REGION, typeof(Views.DefaultView));
        }
    }
}
