using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget
{
    internal class TransacTableBindingSource : BindingSource
    {
        public MainDataSet.TransacDataTable TransacTable => _TransacTable;
        MainDataSet.TransacDataTable _TransacTable;

        public TransacTableBindingSource()
        {
            _TransacTable = new MainDataSet.TransacDataTable();

            this.DataSource = _TransacTable;
            this.DataMember = "";
            this.Sort = "";
        }
    }
}
