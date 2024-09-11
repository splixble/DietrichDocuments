using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

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
                    _DbConnection.ConnectionString = Budget.Properties.Settings.Default.SongbookConnectionString;
                }
                return _DbConnection;
            }
        }
        static SqlConnection _DbConnection = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            _LookupTableSet = new LookupTableSet();

            Application.Run(new Form1());
        }
    }
}
