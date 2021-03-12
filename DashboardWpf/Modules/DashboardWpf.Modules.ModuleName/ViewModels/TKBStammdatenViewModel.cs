using DashboardWpf.Core;
using DashboardWpf.Core.Events;
using DashboardWpf.Core.Models;
using DashboardWpf.Core.Views;
using DashboardWpf.Services.Interfaces;
using DashboardWpf.UserControls.HighlightDatePicker;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DashboardWpf.Modules.TKB.ViewModels
{
    public class TKBStammdatenViewModel : BindableBase, INavigationAware, IConfirmNavigationRequest
    {
        private IDepotService dataService;
        private IEventAggregator _eventAggregator;

        public TKBStammdatenViewModel(IDepotService depoService, IEventAggregator eventAggregator)
        {
            dataService = depoService;
            _eventAggregator = eventAggregator;

            eventAggregator.GetEvent<DepotSelected>().Subscribe(OnDepotSelected);

            ReloadData();



            Save = new DelegateCommand(SaveData, CanSave)
                .ObservesProperty(() => HasChanges);

            Cancel = new DelegateCommand(RollbackChanges, CanCancel)
                .ObservesProperty(() => HasChanges);

        }

        private void ReloadData()
        {
            ReloadTours();

            Employees = new ObservableCollection<Employee>(dataService.GetDepoEmployees(MainDepot?.Code));

            HighlightedDates = dataService.GetTourDates(MainDepot?.Code)
                .Select(x => new HighlightedDate(x, "Reserved Tour"))
                .ToList();
        }

        private void OnDepotSelected(Depot depot)
        {
            MainDepot = depot;

            ReloadData();
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
            //HasChanges = false;
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
                if (HasChanges && _selectedDate != value)
                {
                    var saveConfirm = SaveDataDialog;
                    if (saveConfirm == MessageBoxResult.Yes || saveConfirm == MessageBoxResult.OK)
                        SaveData();
                }
                SetProperty(ref _selectedDate, value);
                ReloadTours();
            }
        }

        private void ReloadTours()
        {
            Tours = new ObservableCollection<Tour>(dataService.GetDepoTours(MainDepot?.Code, SelectedDate));
            HasChanges = false;
        }

        #region  INavigationAware

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            MainDepot = navigationContext.Parameters.GetValue<Depot>(NavigationParameterNames.DEPOT);
            ReloadData();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion

        private MessageBoxResult SaveDataDialog =>
            MessageBox.Show("Möchten Sie Ihre Datei speichern?", "Speichern", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

        #region IConfirmNavigationRequest

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;

            if (HasChanges)
            {
                var confirm = SaveDataDialog;
                switch (confirm)
                {
                    case MessageBoxResult.Cancel:
                        result = false; break;

                    case MessageBoxResult.OK:
                    case MessageBoxResult.Yes:
                        SaveData(); break;

                    case MessageBoxResult.No:
                        ReloadData(); break;
                }
            }

            continuationCallback(result);
        }

        #endregion
        
        
        private Depot _mainDepot;

        public Depot MainDepot
        {
            get => _mainDepot;
            set 
            {
                if (HasChanges && _mainDepot != value)
                {
                    var saveConfirm = SaveDataDialog;
                    if (saveConfirm == MessageBoxResult.Yes || saveConfirm == MessageBoxResult.OK)
                        SaveData();
                }

                SetProperty(ref _mainDepot, value);

            }
        }

    }
}
