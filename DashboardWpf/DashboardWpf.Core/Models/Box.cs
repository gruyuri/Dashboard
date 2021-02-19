using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.Core.Models
{
    public class Box
    {
        public string BoxNumber { get; set; } = "01";

        public Employee Employee { get; set; }

        public bool IsAssigned => this.Employee != null;
    }
}
