using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Songs
{
    static class SongsUtils
    {
        public static string FormatBandRepertoirePerformanceNotes(string perfNotesText, bool pdf)
        {
            // DIAGBO  hey! \u200C duddin work. Google soft line break char in PdfSharp.
            return perfNotesText.Replace("\\n", pdf? "\u200C" : "\v").Replace("\\t", "    ");
            // -- replace \n in strings from database with soft line break, and \t with consectutive spaces

        }
    }
}
