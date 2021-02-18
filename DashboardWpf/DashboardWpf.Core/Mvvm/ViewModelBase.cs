﻿using Prism.Mvvm;
using Prism.Navigation;

namespace DashboardWpf.Core.Mvvm
{
    public abstract class ViewModelBase : BindableBase, IDestructible
    {
        protected ViewModelBase()
        {

        }

        public virtual void Destroy()
        {

        }
    }
}