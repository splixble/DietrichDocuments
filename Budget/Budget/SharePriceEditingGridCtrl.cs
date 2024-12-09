using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget
{
    public partial class SharePriceEditingGridCtrl : UserControl
    {
        public MainDataSet.SharePriceDataTable SharePriceTable => mainDataSet.SharePrice;

        public BindingSource BindingSrc => sharePriceBindingSource;

        public SharePriceEditingGridCtrl()
        {
            InitializeComponent();
        }

        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Utils.SetImportedDataGridRowColors(this.grid);
        }
    }
}
