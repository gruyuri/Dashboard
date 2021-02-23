using DashboardWpf.Core.Enums;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DashboardWpf.Core.Models
{
    public class Tour: BindableBase
    {
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

                if (_boxes != null)
                    foreach (var b in _boxes)
                        b.PropertyChanged += RaiseChangesEvent;
            }
        }

        private void RaiseChangesEvent(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Boxes");
        }

        private int _planH = 8;
        public int PlanH 
        {
            get => _planH;
            set => SetProperty(ref _planH, value);
        }

        private int _factH = 0;
        public int FactH
        {
            get => _factH;
            set => SetProperty(ref _factH, value);
        }

        public TourStatus Status
        {
            get
            {
                if (Boxes.All(x => x.IsAssigned))
                    return TourStatus.Komplett;

                if (Boxes.All(x => !x.IsAssigned))
                    return TourStatus.Liegend;

                return TourStatus.Mix;
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
    }
}
