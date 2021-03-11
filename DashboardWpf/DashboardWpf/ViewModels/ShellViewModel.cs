using DashboardWpf.Core;
using DashboardWpf.Core.Events;
using DashboardWpf.Core.Models;
using DashboardWpf.Services.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DashboardWpf.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;

        private readonly IDepotService dataService;

        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IDepotService svcDepot)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            dataService = svcDepot;

            Depots = new ObservableCollection<Depot>(dataService.GetAllDepot());

            // Set default Depot
            if (Depots.Count > 0)
                MainDepot = Depots[0];

            eventAggregator.GetEvent<ModuleSelected>().Subscribe(OnModuleSelected);

            Initialize();

        }

        private ObservableCollection<Depot> _depots;

        public ObservableCollection<Depot> Depots
        {
            get => _depots;
            set => SetProperty(ref _depots, value);
        }

        private Depot _mainDepot;

        public Depot MainDepot
        {
            get => _mainDepot;
            set
            {
                SetProperty(ref _mainDepot, value);
                _eventAggregator.GetEvent<DepotSelected>().Publish(_mainDepot);
            }
        }


        private void OnModuleSelected(string moduleName)
        {
            var navigationParams = new NavigationParameters
            {
                { NavigationParameterNames.DEPOT, MainDepot }
            };

            _regionManager.RequestNavigate(RegionNames.CONTENT_REGION, moduleName, navigationParams);
        }

        private void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.NAVIGATION_MENU_REGION, typeof(Views.NavigationMenuView));
            _regionManager.RegisterViewWithRegion(RegionNames.CONTENT_REGION, typeof(Views.DefaultView));
        }
    }
}
