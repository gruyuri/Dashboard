using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DashboardWpf.Core.Models
{
    public class Employee
    {
        public string Code { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAvailableForSubstitution { get; set; } = false;

        /// <summary>
        /// Sign of system record, to assign Empty Status
        /// </summary>
        public bool IsDummy { get; set; } = false;

        public string Name => $"{FirstName} {LastName}";

        public override string ToString() => $"{Name} {Code}";

        public string EmployeeDisplayName => $"{Name} {Code}";
    }
}
