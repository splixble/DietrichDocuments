using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Songs
{
    public partial class BandPromptBox : Form
    {
        public int? BandID => comboBand.SelectedValue as int?;

        AzureDataSet.bandsDataTable _BandsDataTable; 

        public BandPromptBox()
        {
            InitializeComponent();

            // In Design mode?
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                return;

            _BandsDataTable = new AzureDataSet.bandsDataTable();
            AzureDataSetTableAdapters.bandsTableAdapter adap = new AzureDataSetTableAdapters.bandsTableAdapter();
            adap.Fill(_BandsDataTable);

            comboBand.DataSource = _BandsDataTable;
            comboBand.ValueMember = "BandID";
            comboBand.DisplayMember = "BandName";
        }
    }
}
