using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using PrintLib;

namespace PrintLib
{
    public interface IDNPrintable
    {
        /// <summary>
        /// Prints a control, or part of a control on a page, or does a "dry run" to calculate the number of pages.
        /// </summary>
        /// <param name="printDoc">Print Document that's handling the printing</param>
        /// <param name="graphics">Graphics object to print the control to</param>
        /// <param name="parentOffset">Vertical & horizontal amount to shift it so it's in the right place relative to parent</param>
        /// <param name="printPage">true=really print; false=just a dry run to calculate number of pages</param>
        /// <param name="bookmark">PrintBookmark the deriving method generates to handle prints of a control that spread out over multiple pages. 
        /// Input and output parameter</param>
        /// <param name="printedAnything">Output paramater which returns whether any part of the control was able to be printed on this page</param>
        /// <returns>True if it was able to print the whole control on the page, false if not</returns>
        /// Returns true if it was able to print the whole control on the page, false if not. (Implementations of this function 
        /// can determine what value to return by checking the return value of each call to DNPrintDocument.PrintControl(), which 
        /// returns a boolean value of whether the individual control fits on the page.)
        /// If Print returns false, and non-null bookmark parameter is returned, the DNPrintDocument will attempt to 
        /// print the rest of the control on the next page.
        /// When you override Print, be sure to set LowestPrintingOnPage.
        bool Print(DNPrintDocument printDoc, Graphics graphics, Point parentOffset, bool printPage, ref PrintBookmark bookmark, out bool printedAnything);

        DNPrintCtrlOptions PrintCtrlOptions { get; }

        Control Ctrl { get; } // Not in original Lib; there, you just cast the IDNPrintable to Control. But in this lib, you can make the control's Tag
                              // a derivation of IDNPrintable, rather than having to derive the control itself.
    }
}
