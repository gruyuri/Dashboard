using DashboardWpf.Core.Models;
using DashboardWpf.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashboardWpf.ViewModels
{
    public class NavigationMenuViewModel : BindableBase
    {
        private IModuleService _moduleService;
        private IList<ModuleUI> moduleItems;

        public NavigationMenuViewModel(IModuleService moduleService)
        {
            _moduleService = moduleService;
            moduleItems = _moduleService.GetModules();
        }

        public IList<ModuleUI> ModuleItems
        {
            get => moduleItems;
        }

    }
}
