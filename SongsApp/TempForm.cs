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
    public partial class TempForm : Form
    {
        public TempForm()
        {
            InitializeComponent();
        }

        private void TempForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'azureDataSet.songperformances' table. You can move, or remove it, as needed.
            this.songperformancesTableAdapter.Fill(this.azureDataSet.songperformances);
        }
    }
}
