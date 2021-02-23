using System.Windows;
using System.Windows.Controls;

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
