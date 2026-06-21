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
            return perfNotesText.Replace("\\n", pdf? "\n" : "\v").Replace("\\t", "    ");
            // -- replace \n in strings from database with line break (soft line break for text going into Word file), and \t with consectutive spaces

        }
    }
}
