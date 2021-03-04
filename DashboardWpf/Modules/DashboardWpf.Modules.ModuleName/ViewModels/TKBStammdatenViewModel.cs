using DashboardWpf.Core.Models;
using DashboardWpf.Core.Views;
using DashboardWpf.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace DashboardWpf.Modules.TKB.ViewModels
{
    public class TKBStammdatenViewModel : BindableBase
    {
        private IDepotService dataService;
        public TKBStammdatenViewModel(IDepotService depoService)
        {
            dataService = depoService;

            ReloadTours();

            Employees = new ObservableCollection<Employee>(dataService.GetDepoEmployees(string.Empty));

            HighlightedDates = new List<HighlightedDate>
            {
                new HighlightedDate(DateTime.Today.AddDays(3), "Beschreibung"),
                new HighlightedDate(DateTime.Today.AddDays(20), "Geburtstag")
            };

            Save = new DelegateCommand(SaveData, CanSave)
                .ObservesProperty(() => HasChanges);

            Cancel = new DelegateCommand(RollbackChanges, CanCancel)
                .ObservesProperty(() => HasChanges);

        }

        private bool _hasChanges;

        public bool HasChanges
        {
            get => _hasChanges;
            set => SetProperty(ref _hasChanges, value);
        }

        private IList<HighlightedDate> _highlightedDates;
        public IList<HighlightedDate> HighlightedDates
        {
            get => _highlightedDates;
            set => SetProperty(ref _highlightedDates, value);
        }

        private ObservableCollection<Tour> _tours;

        public ObservableCollection<Tour> Tours
        {
            get => _tours;
            set {
                if (_tours != null)
                    foreach (var t in _tours)
                        t.PropertyChanged -= ChildItemWasChanged;

                HasChanges = false;
                SetProperty(ref _tours, value);

                foreach (var t in _tours)
                    t.PropertyChanged += ChildItemWasChanged;
            }
        }

        private void ChildItemWasChanged(object sender, PropertyChangedEventArgs e)
        {
            HasChanges = true;
        }

        public ICommand Save { get; set; }
        public ICommand Cancel { get; set; }

        private bool CanSave()
        {
            return HasChanges;
        }

        private bool CanCancel()
        {
            return HasChanges;
        }

        /// <summary>
        /// Actual save changes
        /// </summary>
        private void SaveData()
        {

        }

        /// <summary>
        /// Rollback changes und restore initial state
        /// </summary>
        private void RollbackChanges()
        {

        }


        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees
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
