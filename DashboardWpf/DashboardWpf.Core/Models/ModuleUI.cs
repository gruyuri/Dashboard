using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.Core.Models
{
    public class ModuleUI
    {
        public string Name { get; set; }

        public List<ModuleUI> Items { get; set; } = new List<ModuleUI>();
    }
}
