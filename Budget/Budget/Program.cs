using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget
{
    internal static class Program
    {
        static LookupTableSet _LookupTableSet;
        public static LookupTableSet LookupTableSet => _LookupTableSet;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            _LookupTableSet = new LookupTableSet();
            _LookupTableSet.Load();

            Application.Run(new Form1());
        }
    }
}
