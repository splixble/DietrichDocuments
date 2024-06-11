using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintLib
{
    /// <summary>
    /// Derivable class to send custom arguments to a Print or Print Preview command.
    /// </summary>
    public class PrintArgs
    {
        /// <summary>
        /// Scale to which to enlarge or reduce printed components. Defaults to 1.
        /// </summary>
        /// Scales all elements printed on the page except for the headers and footers.
        public float? PrintScale = null;

        /// <summary>
        /// Field to attach custom information to the object.
        /// </summary>
        public object Tag = null;

        public PrintArgs(float? printScale)
        {
            PrintScale = printScale;
        }

        public PrintArgs()
        {
            PrintScale = 1; // default
        }
    }
}
