using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.Core.Models
{
    public class ModuleUI
    {
        public ModuleUI()
        {

        }

        public ModuleUI (string displayName, string moduleName, PackIconKind icon)
        {
            this.DisplayName = displayName;
            this.ModuleName = moduleName;
            this.Icon = icon;
        }

        public string DisplayName { get; set; }

        public string ModuleName { get; set; }

        public List<ModuleUI> Items { get; set; } = new List<ModuleUI>();

        public PackIconKind Icon { get; private set; }
    }
}
