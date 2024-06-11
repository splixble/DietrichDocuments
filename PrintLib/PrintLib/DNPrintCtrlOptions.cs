using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;

namespace PrintLib
{
    public class DNPrintCtrlOptions
    {
        [Flags]
        public enum PrintFlags : uint
        {
            /// DoNotPrint: do not print this control, even though it's displayed
            DoNotPrint = 1,

            /**   CanGrowVertically: Control can be taller when it prints than when it's displayed
             on the screen -- e.g. vertically scrollable controls like Grid.
             NOTE: if this option is set, IDNPrintable.Print for this control
             must set DNPrintDocument.LowestPrintingOnPage
            * */
            CanGrowVertically = 2,

            /* ``` NOT USING THIS AFTER ALL: 
            CanPrintAcrossMultiplePages = 4,  
                    when printing, can be distributed on multiple pages in case it's too 
                    tall or wide to fit on the current page. If true, the ctrl must create a GridPrintBookmark object 
                    * to return as output parameter bookmark in the IDNPrintable.Print function.
                    * */
        }
        public PrintFlags Flags = 0; // can be set by app

        public DNPrintCtrlOptions(PrintFlags flags)
        {
            Flags = flags;
        }

        public DNPrintCtrlOptions() // create a default object
            : this(0)
        {
        }

        public bool HasFlag(PrintFlags flag)
        {
            // convenience function
            return ((Flags & flag) != 0);
        }
    }
}
