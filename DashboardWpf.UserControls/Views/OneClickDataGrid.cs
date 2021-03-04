using System;
using System.Windows.Controls;

namespace DashboardWpf.UserControls.Views
{
    public class OneClickDataGrid : DataGrid
    {
        protected override void OnCurrentCellChanged(EventArgs e)
        {
            if (CurrentCell != null)
                this.BeginEdit();

            base.OnCurrentCellChanged(e);
        }
    }
}
