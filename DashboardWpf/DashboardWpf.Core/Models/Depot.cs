using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.Core.Models
{
    public class Depot
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public override string ToString() => $"{Code}: {Name}";

        public string DisplayName => $"{Name.ToUpper()}";
    }
}
