using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.Core.Models
{
    public class ModuleUI
    {
        public string DisplayName { get; set; }

        public string ModuleName { get; set; }

        public List<ModuleUI> Items { get; set; } = new List<ModuleUI>();
    }
}
