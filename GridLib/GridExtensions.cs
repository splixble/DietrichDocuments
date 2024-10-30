using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GridLib
{
    public static class GridExtensions
    {
        /// <summary>
        /// Gets a SortedList (key=row index, values not used) of row indices of either selected rows, or containined in 
        /// selected cells.
        /// </summary>
        /// Returns null if all rows are selected (i.e. if a column is selected).
        public static SortedList<int, Object> RowsContainingSelectedData(this DataGridView grid)
        {
            if (grid.SelectedColumns.Count > 0)
                return null;
            SortedList<int, Object> dict = new SortedList<int, object>();
            foreach (DataGridViewRow row in grid.SelectedRows)
            {
                if (!dict.ContainsKey(row.Index))
                    dict.Add(row.Index, null);
            }
            foreach (DataGridViewCell cell in grid.SelectedCells)
            {
                if (!dict.ContainsKey(cell.RowIndex))
                    dict.Add(cell.RowIndex, null);
            }
            return dict;
        }
    }
}
