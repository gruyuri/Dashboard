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
                Name = "TKB",
                Items = new List<ModuleUI>()
                {
                    new ModuleUI() { Name = "Tour-Stammdaten" },
                    new ModuleUI() { Name = "Tour-Disposition" }
                }
            });

            list.Add(new ModuleUI()
            {
                Name = "Module A",
                Items = new List<ModuleUI>()
                {
                    new ModuleUI() { Name = "Module A - SubItem 01" },
                    new ModuleUI() { Name = "Module A - SubItem 02" },
                    new ModuleUI() { Name = "Module A - SubItem 03" }
                }
            });

            list.Add(new ModuleUI()
            {
                Name = "Module B",
                Items = new List<ModuleUI>()
                {
                    new ModuleUI() { Name = "Module B - xxxx 1" },
                    new ModuleUI() { Name = "Module B - xxxx 2" },
                    new ModuleUI() { Name = "Module B - xxxx 3" },
                    new ModuleUI() { Name = "Module B - xxxx 4" },
                    new ModuleUI() { Name = "Module B - xxxx 5" },
                    new ModuleUI() { Name = "Module B - xxxx 6" },
                    new ModuleUI() { Name = "Module B - xxxx 7" }
                }
            });

            return list;
        }

    }
}
