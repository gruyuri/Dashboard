using DashboardWpf.Core.Models;
using DashboardWpf.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DashboardWpf.Modules.TKB.ViewModels
{
    public class TKBStammdatenViewModel : BindableBase
    {
        private IDepoService dataService;
        public TKBStammdatenViewModel(IDepoService depoService)
        {
            dataService = depoService;

            Tours = new ObservableCollection<Tour>(dataService.GetDepoTours(string.Empty, SelectedDate));
            Employees = dataService.GetDepoEmployees(string.Empty);
        }
        private ObservableCollection<Tour> _tours;

        public ObservableCollection<Tour> Tours
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

        private DateTime _selectedDate = DateTime.Today;

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set 
            { 
                SetProperty(ref _selectedDate, value);
                ReloadTours();
            }
        }

        private void ReloadTours()
        {
            Tours = new ObservableCollection<Tour>(dataService.GetDepoTours(string.Empty, SelectedDate));
        }
    }
}
