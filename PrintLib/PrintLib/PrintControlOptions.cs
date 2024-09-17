using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintLib
{
    /// <summary>
    /// Specifies whether a control should be printed (despite DNPrintDocument.PrintAllNonGridControls == true) or not printed 
    /// (despite DNPrintDocument.PrintAllNonGridControls == false)
    /// </summary>
    /// Used with DNPrintDocument. Setting DNPrintDocument.PrintAllNonGridControls and calling DNPrintDocument.SetControlToPrint() allow a developer to 
    /// specify either that all controls in a form should be printed except those specified (by setting DNPrintDocument.PrintAllNonGridControls == true 
    /// and calling SetControlToPrint(ctrl, PrintControlOptions.DontPrint), or SetControlToPrint(ctrl, PrintControlOptions.DontPrintThisOrItsChildren)),
    /// or that NONE of the controls in a form should be printed except those specified (by setting DNPrintDocument.PrintAllNonGridControls == false 
    /// and calling SetControlToPrint(ctrl, PrintControlOptions.Print)).
    /// (Grids are always printed, unless their PrintCtrlOptions.DoNotPrint is specified.)
    public enum PrintControlOptions
    {
        Print,  // ctrl will be printed even if PrintAllNonGridControls=false;
        DontPrint, // ctrl will not be printed, but its children might, depending on their PrintControlOptions
        DontPrintThisOrItsChildren,  // ctrl will never be printed, nor its children, even if PrintAllNonGridControls=true
    }
}
