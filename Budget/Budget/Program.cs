using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using MySql.Installer.Dialogs;

namespace Budget
{
    internal static class Program
    {
        static LookupTableSet _LookupTableSet;
        public static LookupTableSet LookupTableSet => _LookupTableSet;

        // Single common Connection object, so it doesn't have to prompt for credentials multiple times:
        public static SqlConnection DbConnection
        {
            get
            {
                // initialize if it hasn't already been:
                if (_DbConnection == null)
                {
                    _DbConnection = new SqlConnection();
                    _DbConnection.ConnectionString = Budget.Properties.Settings.Default.BudgetConnectionString;
                }
                return _DbConnection;
            }
        }
        static SqlConnection _DbConnection = null;

        static Form1 _MainForm;

        public static string AccountOwner => _MainForm.AccountOwner;

        public static string CashAccountID
        {
            get
            {
                int acctRowIndex = _LookupTableSet.MainDataSet.Account.CashAccountsView.Find(AccountOwner);
                // DIAG check this, throw if cash acc not found for account owner
                MainDataSet.AccountRow acctRow = _LookupTableSet.MainDataSet.Account.CashAccountsView[acctRowIndex].Row as MainDataSet.AccountRow;
                return acctRow.AccountID;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            _LookupTableSet = new LookupTableSet();

            _MainForm = new Form1();
            Application.Run(_MainForm);
        }
    }
}
