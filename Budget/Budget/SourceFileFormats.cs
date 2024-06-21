using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    enum SourceFileFormats { AccountAdvantis, 
        AccountBofA, Amex, CreditCardBofA, 
        CreditCardBofANarrowPDF, // same as CreditCardBofA, but PDF Statements, when copied as text, contain only one field per line
        Handwritten, 
        None }
    // based on keys in table
}
