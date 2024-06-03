using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.MainDataSetTableAdapters;

namespace Budget
{
    internal class LookupTableSet
    {
        public MainDataSet MainDataSet => _MainDataSet;
        MainDataSet _MainDataSet= new MainDataSet();

        BudgetSourceFileFormatTableAdapter _BudgetSourceFileFormatAdapter = new BudgetSourceFileFormatTableAdapter();
        BudgetAccountTableAdapter _BudgetAccountAdapter = new BudgetAccountTableAdapter();
        BudgetTypeGroupingsTableAdapter _GroupingsAdapter = new BudgetTypeGroupingsTableAdapter();

        public LookupTableSet()
        {
        }

        public void Load()
        {
            _BudgetSourceFileFormatAdapter.Fill(_MainDataSet.BudgetSourceFileFormat);
            _BudgetAccountAdapter.Fill(_MainDataSet.BudgetAccount);
            _GroupingsAdapter.Fill(_MainDataSet.BudgetTypeGroupings);
        }
    }
}
