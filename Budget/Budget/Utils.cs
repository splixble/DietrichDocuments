using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget
{
    internal class Utils
    {
        public static void SetImportedDataGridRowColors(DataGridView grid)
        {
            for (int rowNum = 0; rowNum < grid.Rows.Count; rowNum++)
            {
                grid.Rows[rowNum].HeaderCell.Value = (rowNum + 1).ToString();
                SetImportedDataGridRowColor(grid, rowNum);
            }
        }

        static void SetImportedDataGridRowColor(DataGridView grid, int rowNum)
        {
            DataGridViewRow gridRow = grid.Rows[rowNum];
            if (gridRow.DataBoundItem != null)
            {
                DataRow dataRow = (gridRow.DataBoundItem as DataRowView).Row;
                if (dataRow.RowState == DataRowState.Modified)
                    gridRow.DefaultCellStyle.BackColor = Color.LightBlue;
                else if (dataRow.RowState == DataRowState.Added)
                    gridRow.DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }
    }
}
