using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using PrintLib;

namespace PrintLib
{
    /// <summary>
    /// Represents a Form Control to be printed, along with their child controls if specified, as they appear on the form
    /// </summary>
    class PrintedTopLevelControl
    {
        Control _CtrlToPrint;

        PrintedTopLevelControlFlags _Flags;
        public PrintedTopLevelControlFlags Flags { get { return _Flags; } }

        public bool HasFlag(PrintedTopLevelControlFlags flag)
        {
            // convenience function
            return ((Flags & flag) != 0);
        }

        ///<summary> Controls that will be printed, in layout order (top to bottom, then left to right)</summary>
        public List<Control> _PrintableCtrls = null;

        public int _PrintableCtrlIndex = -1;

        DNPrintDocument _PrintDoc;

        public PrintedTopLevelControl(DNPrintDocument printDoc, Control containerToPrint, PrintedTopLevelControlFlags flags)
        {
            _PrintDoc = printDoc;
            _CtrlToPrint = containerToPrint;
            _Flags = flags;
        }

        public void RequestTitles(List<String> titles)
        {
            // PLACEHOLD
            titles.Add("Title");
            //if (_CtrlToPrint is IDNForm)
            //    ((IDNForm)_CtrlToPrint).SupplyPrintTitles(titles);
        }

        public void SetUpPrintableControls()
        {
            _PrintableCtrls = new List<Control>();
            AddToPrintableCtrlList(_CtrlToPrint, false);
            _PrintableCtrls.Sort(new PrintableCtrlLayoutComparer(this));
        }

        /** <summary>  AddToPrintableCtrlList: ``</summary>
         * setToPrint means this specific ctrl (or its ancestor) has been specified 
         * by SetControlToPrint() to be printed regardless of 
         * PrintAllNonGridControls=false
         */
        void AddToPrintableCtrlList(Control ctrl, bool setToPrint)
        {
            bool callFuctionForChildren;
            bool childrenSetToPrint = setToPrint;
            if (ctrl == _CtrlToPrint && !HasFlag(PrintedTopLevelControlFlags.PrintTopLevelControl))
            {
                // don't add the form itself, just its children
                callFuctionForChildren = true;
                childrenSetToPrint = _PrintDoc.PrintAllNonGridControls;
            }
            else
            {
                // go thru all the reasons not to show this ctrl:
                if (!ctrl.Visible)
                    return;
                callFuctionForChildren = _PrintDoc.PrintAllNonGridControls; // unless set otherwise

                // Can we add it to the list of printable controls?
                bool printIt = (_PrintDoc.PrintAllNonGridControls || setToPrint) ||
                    ctrl is DataGridView; // || ctrl is IDNForm;

                if (_PrintDoc.ControlsToPrint.ContainsKey(ctrl))
                {
                    PrintControlOptions printOption = _PrintDoc.ControlsToPrint[ctrl];
                    switch (printOption)
                    {
                        case PrintControlOptions.Print:
                            printIt = true;
                            break;
                        case PrintControlOptions.DontPrint:
                            printIt = false;
                            break;
                        case PrintControlOptions.DontPrintThisOrItsChildren:
                            if (_PrintDoc.PrintAllNonGridControls)
                                return; // skip code to print this ctrl or its children
                            break;
                    }
                }
                // if it's IDNPrintable, it has to NOT have the flag DoNotPrint:
                if (ctrl is IDNPrintable && ((IDNPrintable)ctrl).PrintCtrlOptions.HasFlag(DNPrintCtrlOptions.PrintFlags.DoNotPrint))
                    printIt = false;

                if (printIt)
                    _PrintableCtrls.Add(ctrl);
            }

            // Now, recursively call this to add the ctrl's children, if directed to:
            if (callFuctionForChildren && !(ctrl is IDNPrintable)) // DN ctrls must be printed as a unit...
            {
                foreach (Control childCtrl in ctrl.Controls)
                    AddToPrintableCtrlList(childCtrl, childrenSetToPrint);
            }
        }

        /** <summary>  GetLocationOnForm returns a the location of the specified control on its form,
         * relative to the form (rather than relative to the control's parent control, 
         * which Control.Location represents).</summary>
         */
        public Point GetLocationOnForm(Control ctrl)
        // convenience function
        {
            Point location = new Point(0, 0);
            for (Control c = ctrl; c != null; c = c.Parent)
            {
                if (c == _CtrlToPrint)
                    return location;
                else
                    location.Offset(c.Location);
            }
            return location;
        }

        class PrintableCtrlLayoutComparer : IComparer<Control>
        {
            PrintedTopLevelControl _TopLevelCtrl;

            public PrintableCtrlLayoutComparer(PrintedTopLevelControl topLevelCtrl)
            {
                _TopLevelCtrl = topLevelCtrl;
            }

            int IComparer<Control>.Compare(Control ctrl1, Control ctrl2)
            {
                // Compare vertical location on form, then horizontal:
                Point loc1 = _TopLevelCtrl.GetLocationOnForm(ctrl1);
                Point loc2 = _TopLevelCtrl.GetLocationOnForm(ctrl2);
                if (loc1.Y == loc2.Y)
                    return loc1.X - loc2.X;
                else
                    return loc1.Y - loc2.Y;
            }
        }
    }
}
