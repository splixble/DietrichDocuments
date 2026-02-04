using PrintLib;
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
    public partial class TransacListSelector : UserControl
    {
        public string AccountOwner => comboAccountOwner.SelectedValue as string;

        public AssetType AssetType => (AssetType)comboAccountType.SelectedValue;

        public bool AdjustForRefunds => chBoxRefunds.Checked;

        public DateTime FromMonth => (DateTime)comboFromMonth.SelectedMonth;

        public DateTime ToMonth => (DateTime)comboToMonth.SelectedMonth;

        public delegate void SelectionChangedHandler(TransacListSelector sender, EventArgs args);
        public event SelectionChangedHandler SelectionChanged;

        public TransacListSelector()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // In Design mode?
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                return;

            comboAccountOwner.DataSource = Program.LookupTableSet.MainDataSet.AccountOwner;
            comboAccountOwner.ValueMember = "OwnerID";
            comboAccountOwner.DisplayMember = "OwnerDescription";
            comboAccountOwner.SelectedValue = "D";// initialize it

            comboAccountType.DataSource = AssetTypeClass.List;
            comboAccountType.ValueMember = "AssetType";
            comboAccountType.DisplayMember = "Label";
            comboAccountType.SelectedValue = AssetType.BankAndCash; // initialize it to Bank/Cash          

            DateTime minMonth = new DateTime(2022, 1, 1); // DIAG get from config!
            DateTime maxMonth = DateTime.Today.AddMonths(2);
            DateTime fromMonth = DateTime.Today.AddMonths(-14);
            DateTime toMonth = DateTime.Today;

            comboFromMonth.Populate(minMonth, maxMonth, fromMonth);
            comboToMonth.Populate(minMonth, maxMonth, toMonth);

        }

        private void CtrlSelectionChanged(object sender, EventArgs e)
        {
            // Call the SelectionChanged event handler, if any:
            if (SelectionChanged != null)
                SelectionChanged(this, new EventArgs());
        }
    }
}
