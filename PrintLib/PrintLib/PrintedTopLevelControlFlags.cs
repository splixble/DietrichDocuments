using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintLib
{
    [Flags]
    public enum PrintedTopLevelControlFlags : ushort
    {
        PrintTopLevelControl = 0x1, // usually, we don't print this top level control, because that'll put an unwanted frame around it - we just want to print contents
        PageBreakBefore = 0x2,
        PageBreakAfter = 0x4,
    }
}
