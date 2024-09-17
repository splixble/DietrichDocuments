using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintLib
{
    // Enables DataGridView (rather than derived Grid) to be printed as a DNPrintable control
    public class PrintableGridTag : IDNPrintable
    {
        public DataGridView GridView => _Grid;
        DataGridView _Grid;

        public GridPrintManager PrintManager => _PrintManager;
        GridPrintManager _PrintManager;

        public PrintableGridTag(DataGridView grid) 
        {
            _Grid = grid;
            _PrintManager = new GridPrintManager(_Grid);
        }

        bool IDNPrintable.Print(DNPrintDocument printDoc, Graphics graphics, Point parentOffset, bool printPage, ref PrintBookmark bookmark, out bool printedAnything)
        {
            return PrintManager.PrintGrid(printDoc, graphics, parentOffset, printPage, ref bookmark, out printedAnything);
        }

        Control IDNPrintable.Ctrl => _Grid;

        DNPrintCtrlOptions IDNPrintable.PrintCtrlOptions
        {
            get
            {
                return new DNPrintCtrlOptions(DNPrintCtrlOptions.PrintFlags.CanGrowVertically);
                // ``not using this after all  --   | DNPrintCtrlOptions.PrintFlags.CanPrintAcrossMultiplePages);
            }
        }
    }
}
