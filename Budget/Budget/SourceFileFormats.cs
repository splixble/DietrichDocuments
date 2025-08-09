using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    public enum SourceFileFormats { 
        AmexStmt, 
        BofANarrowPDF,
        BofAWidePDF,
        BofAWidePDFWithYear,
        DateShareBalance,
        YahooHoldings,
        YahooHistoricalData,
        None }
    // Based on keys in table. As needed.
    // DIAG Implement Imperfect CSV option ("'s, if followed by other than comma, are treated literal)
}
