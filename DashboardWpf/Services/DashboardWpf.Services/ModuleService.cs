﻿using DashboardWpf.Core;
using DashboardWpf.Core.Models;
using DashboardWpf.Services.Interfaces;
using System.Collections.Generic;

namespace DashboardWpf.Services
{
    public class ModuleService : IModuleService
    {
        public IList<ModuleUI> GetModules()
        {
            IList<ModuleUI> list = new List<ModuleUI>();

            list.Add(new ModuleUI()
            {
                DisplayName = "TKB",
                Items = new List<ModuleUI>()
                {
                    new ModuleUI() { DisplayName = "Tour-Stammdaten", ModuleName = ModuleNames.TKB_TOUR_STAMMDATEN },
                    new ModuleUI() { DisplayName = "Tour-Disposition", ModuleName = ModuleNames.TKB_DISPOSITION_VIEW }
                }
            });

            list.Add(new ModuleUI()
            {
                DisplayName = "Module A",
                Items = new List<ModuleUI>()
                {
                    new ModuleUI() { DisplayName = "Module A - SubItem 01" },
                    new ModuleUI() { DisplayName = "Module A - SubItem 02" },
                    new ModuleUI() { DisplayName = "Module A - SubItem 03" }
                }
            });

            list.Add(new ModuleUI()
            {
                DisplayName = "Module B",
                Items = new List<ModuleUI>()
                {
                    new ModuleUI() { DisplayName = "Module B - xxxx 1" },
                    new ModuleUI() { DisplayName = "Module B - xxxx 2" },
                    new ModuleUI() { DisplayName = "Module B - xxxx 3" },
                    new ModuleUI() { DisplayName = "Module B - xxxx 4" },
                    new ModuleUI() { DisplayName = "Module B - xxxx 5" },
                    new ModuleUI() { DisplayName = "Module B - xxxx 6" },
                    new ModuleUI() { DisplayName = "Module B - xxxx 7" }
                }
            });

            return list;
        }

    }
}