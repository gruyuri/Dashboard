using DashboardWpf.Core;
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

            ModuleUI moduleUI = new ModuleUI("TKB", string.Empty, MaterialDesignThemes.Wpf.PackIconKind.Schedule);
            moduleUI.Items = new List<ModuleUI>()
                {
                    new ModuleUI("Tour-Stammdaten", ModuleNames.TKB_TOUR_STAMMDATEN, MaterialDesignThemes.Wpf.PackIconKind.None),
                    new ModuleUI("Tour-Disposition", ModuleNames.TKB_DISPOSITION_VIEW, MaterialDesignThemes.Wpf.PackIconKind.NoteAddOutline)
                };

            list.Add(moduleUI);

            return list;
        }

    }
}
