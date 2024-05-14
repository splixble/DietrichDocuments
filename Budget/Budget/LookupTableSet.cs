using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    internal class LookupTableSet
    {
        public MainDataSet MainDataSet => _MainDataSet;
        MainDataSet _MainDataSet= new MainDataSet();

        MainDataSetTableAdapters.BudgetSourceFileFormatTableAdapter _BudgetSourceFileFormatAdapter = new MainDataSetTableAdapters.BudgetSourceFileFormatTableAdapter();
        MainDataSetTableAdapters.BudgetAccountTableAdapter _BudgetAccountAdapter = new MainDataSetTableAdapters.BudgetAccountTableAdapter();

        public LookupTableSet()
        {
        }

        public void Load()
        {
            _BudgetSourceFileFormatAdapter.Fill(_MainDataSet.BudgetSourceFileFormat);
            _BudgetAccountAdapter.Fill(_MainDataSet.BudgetAccount);
        }
    }
}
