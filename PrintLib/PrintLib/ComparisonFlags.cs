using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintLib
{
    /// <summary>
    /// Flags used in comparison functions (such as Utils.Compare and Utils.Equals) that specify the mode of comparison of objects, or of components of objects such as ComparableList)
    /// </summary>
    [Flags]
    public enum ComparisonFlags
    {
        /// <summary>
        /// If both compared objects are strings, specifies whether the comparison will be case-sensitive or case-insensitive
        /// </summary>
        CaseSensitive = 0x1,

        /// <summary>
        /// If true, considers a blank ("") string as equal to a null string
        /// </summary>
        BlankStringMatchesNull = 0x2,

        /// <summary>
        /// If true, considers DBNull.Value as equal to null
        /// </summary>
        DBNullMatchesNull = 0x4,

        /// <summary>
        /// If true, compares parsed numeric values when the column type has a numeric IntendedType, and the values can be parsed, rather than string values.  
        /// </summary>
        /// For example, if the EColumn has a ColType of String and an ColType.IntendedType of Decimal, judges that values "3.0" and "3" are equal, since their parsed 
        /// Decimal values are qual, even though their value as Strings are not.
        /// NOTE: Only handles numeric IntendedTypes (integer and decimal), not DateTime. Normalization of IntendedType=DateTime values has not been implemented yet
        /// (as of 7/24/17); we would have to define a new ComparisonFlags enum value.
        NormalizeIntendedTypeNumerics = 0x8
    }
}