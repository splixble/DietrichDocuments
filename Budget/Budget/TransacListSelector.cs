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
        public string AccountOwner 
        {
            get { return comboAccountOwner.SelectedValue as string; }
            set { comboAccountOwner.SelectedValue = value; }
        }

        public AssetType AssetType
        {
            get { return (AssetType) comboAccountType.SelectedValue; }
            set { comboAccountType.SelectedValue = value; }
        }

        public bool AdjustForRefunds
        {
            get { return chBoxRefunds.Checked; }
            set { chBoxRefunds.Checked = value; }
        }

        bool _Quarterly = false;

        public DateTime FromMonth
        {
            get { return comboFromMonth.SelectedMonth; }
            set { comboFromMonth.SelectedMonth = value; }
        }

        public DateTime ToMonth
        {
            get
            { 
                // if combo is quarterly, return the last month of the quarter:
                if (_Quarterly)
                    return comboToMonth.SelectedMonth.AddMonths(2);
                else
                    return comboToMonth.SelectedMonth;
            }
            set { comboToMonth.SelectedMonth = value; }
        }

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

            comboAccountType.DataSource = AssetTypeClass.List;
            comboAccountType.ValueMember = "AssetType";
            comboAccountType.DisplayMember = "Label";
        }

        public void Initialize(string accountOwner, AssetType assetType, bool adjustForRefunds, DateTime fromMonth, DateTime toMonth, bool quarterly)
        {
            _Initializing = true;

            AccountOwner = accountOwner;
            AssetType = assetType;
            AdjustForRefunds = adjustForRefunds;
            _Quarterly = quarterly;

            comboFromMonth.Quarterly = _Quarterly;
            comboToMonth.Quarterly = _Quarterly;

            DateTime minMonth = new DateTime(2022, 1, 1); // DIAG get from config!
            DateTime maxMonth = DateTime.Today.AddMonths(2);

            comboFromMonth.Populate(minMonth, maxMonth);
            comboToMonth.Populate(minMonth, maxMonth);

            // have to set combo box selections after they're populated:
            FromMonth = fromMonth;
            ToMonth = toMonth;

            _Initializing = false;
        }

        bool _Initializing = false;

        private void CtrlSelectionChanged(object sender, EventArgs e)
        {
            // Call the SelectionChanged event handler, if any:
            if (!_Initializing && SelectionChanged != null)
                SelectionChanged(this, new EventArgs());
        }
    }
}
