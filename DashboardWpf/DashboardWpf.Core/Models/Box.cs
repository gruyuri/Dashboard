using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.Core.Models
{
    public class Box: BindableBase
    {
        public string BoxNumber { get; set; } = "01";

        private Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set => SetProperty(ref _employee, value);
        }

        public bool IsAssigned => this.Employee != null;
    }
}
