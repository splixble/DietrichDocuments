using PrintLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintLib
{
    /// <summary>
    /// Implements comparisons of two objects' values where the nature of comparison of string member values can be specified.
    /// </summary>
    public interface IComparableContainingStrings : IComparable
    {
        int CompareTo(object other, ComparisonFlags flags);
        bool Equals(object other, ComparisonFlags flags);
    }
}
