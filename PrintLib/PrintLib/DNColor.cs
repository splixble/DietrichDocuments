using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintLib
{
    public static class DNColor
    {
        public static Color TotalCells { get { return Color.LightGreen; } }
        public static Color NullCells { get { return Color.DarkGray; } }
        public static Color ReadOnlyCells { get { return Color.BlanchedAlmond; } }
        public static Color OperationForms { get { return Color.CadetBlue; } }

    }
}
