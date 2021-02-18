using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashboardWpf.Modules.TKB.ViewModels
{
    public class TKBStammdatenViewModel : BindableBase
    {
        private string _message = "TKB Stammdaten View";

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

    }
}
