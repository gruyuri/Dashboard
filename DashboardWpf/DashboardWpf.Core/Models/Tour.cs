using DashboardWpf.Core.Enums;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DashboardWpf.Core.Models
{
    public class Tour: BindableBase, INotifyDataErrorInfo
    {

        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        private DateTime _date = DateTime.Today;

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private ObservableCollection<Box> _boxes = new ObservableCollection<Box>();

        public ObservableCollection<Box> Boxes 
        {
            get => _boxes;
            set
            {
                if (_boxes != null)
                    foreach (var b in _boxes)
                        b.PropertyChanged -= RaiseChangesEvent;

                SetProperty(ref _boxes, value);
                RefreshSelectedEmployee();

                if (_boxes != null)
                    foreach (var b in _boxes)
                        b.PropertyChanged += RaiseChangesEvent;
            }
        }

        private void RaiseChangesEvent(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RefreshSelectedEmployee();
            RaisePropertyChanged("Boxes");
            RaisePropertyChanged("Status");
            RaisePropertyChanged("MainEmployee");
        }

        private void RefreshSelectedEmployee()
        {
            if (Boxes.All(x => x.IsAssigned && x.Employee.IsDummy == false) && Boxes.GroupBy(x => x.Employee).Count() == 1)
                SelectedEmployee = Boxes.FirstOrDefault()?.Employee;
            else
                SelectedEmployee = null;
        }

        private int _planH = 8;
        public int PlanH 
        {
            get => _planH;
            set
            {
                SetProperty(ref _planH, value);
                ValidatePlanH();
            }
        }

        private int _factH = 0;
        public int FactH
        {
            get => _factH;
            set
            {
                SetProperty(ref _factH, value);
                ValidateFactH();
            }

        }

        public TourStatus Status
        {
            get
            {
                if (Boxes.All(x => !x.IsAssigned) ||
                    Boxes.All(x => x.IsAssigned && x.Employee.IsDummy))
                    return TourStatus.Liegend;

                if (Boxes.All(x => x.IsAssigned && !x.Employee.IsDummy) && Boxes.GroupBy(x => x.Employee).Count() == 1)
                    return TourStatus.Komplett;

                return TourStatus.Mix;
            }
        }

        private Employee _selectedEmployee;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (value != null)
                    foreach (var box in Boxes)
                        box.Employee = value;

                SetProperty(ref _selectedEmployee, value);
            }
        }

        public string MainEmployee
        {
            get
            {
                switch (this.Status)
                {
                    case TourStatus.Komplett:
                        return Boxes.FirstOrDefault()?.Employee?.Name;

                    case TourStatus.Mix:
                        return "MIX";

                    default:
                        return "NN";
                }
            }
        }

        #region INotifyDataErrorInfo      .

        public bool HasErrors => _errorsByPropertyName.Any();

        public IEnumerable GetErrors(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName)) return null;

            return _errorsByPropertyName.ContainsKey(propertyName) ?
                _errorsByPropertyName[propertyName] : null;
        }

        //private void OnErrorsChanged(string propertyName)
        //{
        //    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        //}

        private void ValidatePlanH()
        {
            ClearErrors(nameof(PlanH));
            if (PlanH < 0)
                AddError(nameof(PlanH), "Geplant Stunden sollten 0 oder positiv sein.");
            if (PlanH > 10)
                AddError(nameof(PlanH), "Geplant Stunden sollten weniger als 10 sein.");
        }

        private void ValidateFactH()
        {
            ClearErrors(nameof(FactH));
            if (FactH < 0)
                AddError(nameof(FactH), "Aktual Stunden sollten 0 oder positiv sein.");
            if (FactH > 10)
                AddError(nameof(FactH), "Aktuale Stunden sollten weniger als 10 sein.");
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
                _errorsByPropertyName[propertyName] = new List<string>();

            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                RaisePropertyChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                RaisePropertyChanged(propertyName);
            }
        }

        #endregion
    }
}
