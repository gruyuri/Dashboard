using System;
using System.Windows.Controls;

namespace DashboardWpf.Core.Views
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
