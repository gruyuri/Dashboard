using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DashboardWpf.Modules.TKB.Views
{
    /// <summary>
    /// Interaction logic for TKBStammdatenView
    /// </summary>
    public partial class TKBStammdatenView : UserControl
    {
        public TKBStammdatenView()
        {
            InitializeComponent();

            SetDefaultProperties();
        }

        private void SetDefaultProperties()
        {
            this.dateFrom.DisplayDateStart = DateTime.Today;
        }

        private void OnExpanded(object sender, RoutedEventArgs e)
        {
            if (sender is Expander expander)
            {
                var row = DataGridRow.GetRowContainingElement(expander);

                row.DetailsVisibility = expander.IsExpanded ? Visibility.Visible
                                                            : Visibility.Collapsed;
            }
        }

    }
}
