using DashboardWpf.Core.Events;
using DashboardWpf.Core.Models;
using DashboardWpf.Services.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashboardWpf.ViewModels
{
    public class NavigationMenuViewModel : BindableBase
    {
        private IModuleService _moduleService;
        private IEventAggregator _eventAggregator;

        private IList<ModuleUI> moduleItems;

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public NavigationMenuViewModel(IModuleService moduleService, IEventAggregator eventAggregator)
        {
            _moduleService = moduleService;
            _eventAggregator = eventAggregator;

            moduleItems = _moduleService.GetModules();
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string moduleName)
        {
            if (string.IsNullOrEmpty(moduleName))
                moduleName = "DefaultView";

            _eventAggregator.GetEvent<ModuleSelected>().Publish(moduleName);
        }

        public IList<ModuleUI> ModuleItems
        {
            get => moduleItems;
        }

    }
}
