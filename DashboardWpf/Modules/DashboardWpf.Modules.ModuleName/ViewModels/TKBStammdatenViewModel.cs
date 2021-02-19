using DashboardWpf.Core.Models;
using DashboardWpf.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashboardWpf.Modules.TKB.ViewModels
{
    public class TKBStammdatenViewModel : BindableBase
    {
        private IDepoService dataService;
        public TKBStammdatenViewModel(IDepoService depoService)
        {
            dataService = depoService;

            Tours = dataService.GetDepoTours(string.Empty);
            Employees = dataService.GetDepoEmployees(string.Empty);
        }
        private IList<Tour> _tours;

        public IList<Tour> Tours
        {
            get => _tours;
            set => SetProperty(ref _tours, value);
        }

        private IList<Employee> _employees;

        public IList<Employee> Employees
        {
            get => _employees;
            set => SetProperty(ref _employees, value);
        }

    }
}
