using PrintLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintLib
{
    /** <summary>  PrintBookmark describes where we are in the printing of a control (notably, a grid)
     * that we have to split up over several pages, at the end of printing a particular page</summary>
     */
    public class PrintBookmark
    {
        // PrintingIncomplete indicates we reached end of page before completing printing.
        // False by default, initialized to false
        public virtual bool PrintingIncomplete { get { return false; } }

        public DNPrintDocument PrintDocument { get { return _PrintDocument; } }
        DNPrintDocument _PrintDocument;

        public PrintBookmark(DNPrintDocument printDocument)
        {
            _PrintDocument = printDocument;
        }
    }
}
