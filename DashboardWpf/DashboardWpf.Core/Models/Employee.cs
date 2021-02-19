using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.Core.Models
{
    public class Employee
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsAvailableForSubstitution { get; set; } = false;
    }
}
