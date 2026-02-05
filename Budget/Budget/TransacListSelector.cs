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

        public DateTime FromMonth
        {
            get { return (DateTime) comboFromMonth.SelectedMonth; }
            set { comboFromMonth.SelectedMonth = value; }
        }

        public DateTime ToMonth
        {
            get { return (DateTime) comboToMonth.SelectedMonth; }
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

            DateTime minMonth = new DateTime(2022, 1, 1); // DIAG get from config!
            DateTime maxMonth = DateTime.Today.AddMonths(2);

            comboFromMonth.Populate(minMonth, maxMonth);
            comboToMonth.Populate(minMonth, maxMonth);
        }

        public void Initialize(string accountOwner, AssetType assetType, bool adjustForRefunds, DateTime fromMonth, DateTime toMonth)
        {
            _Initializing = true;

            AccountOwner = accountOwner;
            AssetType = assetType;
            AdjustForRefunds = adjustForRefunds;
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
