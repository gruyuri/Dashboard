using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashboardWpf.ViewModels
{
    public class DefaultViewModel : BindableBase
    {
        private string title = "Empty View";

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
    }
}
