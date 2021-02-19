using DashboardWpf.Core.Models;
using DashboardWpf.ViewModels;
using System.Windows.Controls;

namespace DashboardWpf.Views
{
    /// <summary>
    /// Interaction logic for NavigationMenuView
    /// </summary>
    public partial class NavigationMenuView : UserControl
    {
        public NavigationMenuView()
        {
            InitializeComponent();
        }

        private void OnSelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            ModuleUI newModel = e.NewValue as ModuleUI;
            NavigateToModel(newModel);
        }

        private void NavigateToModel(ModuleUI model)
        {
            var dataContext = this.DataContext as NavigationMenuViewModel;

            if (model != null)
            {
                dataContext.NavigateCommand.Execute(model.ModuleName);
            }
        }

    }
}
