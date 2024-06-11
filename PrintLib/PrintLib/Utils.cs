using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PrintLib
{
    // should go in general lib?

    /** <summary>  Utils is a static class containing miscellaneous, unclassifiable functions. 
     * Some of its functions were originally copied from StudyTracker's util class.</summary>
     */
    public static class Utils
    {
        public static string NonNullObjectToString(object ob)
        {
            if (ob == null)
                return "";
            else
                return ob.ToString();
        }

        public static string NonNullString(string str)
        {
            if (str == null)
                return "";
            else
                return str;
        }

        /// <summary>
        /// Appends a string value to the end of a string variable, with a delimiter separating it from the other values. 
        /// </summary>
        /// <param name="list">String variable to add the string value to</param>
        /// <param name="item">String value to add</param>
        /// <param name="delimiter">Delimiter to separate the list items</param>
        /// This overload does not add blank or null items to the list. 
        public static void AddToStringList(ref string list, string item, string delimiter)
        {
            AddToStringList(ref list, item, delimiter, true, list == "");
        }

        /// <summary>
        /// Appends a string value to the end of a string variable, with a delimiter separating it from the other values. 
        /// </summary>
        /// <param name="list">String variable to add the string value to</param>
        /// <param name="item">String value to add</param>
        /// <param name="delimiter">Delimiter to separate the list items</param>
        /// <param name="omitBlankItems">If true, an item of value of "" or null will be ignored; if false, it will be added as a delimiter with nothing before it</param>
        /// <param name="addingFirstItem">True if this is the first item added to this string variable; false if not</param>
        /// Constructs a list of string values with delimiters between the items, but not at the beginning or end. Therefore, if addingFirstItem is true, only the item is added; 
        /// if it's false, the delimiter is added followed by the item.
        /// Parameter addingFirstItem was added 5/30/17, because if we call this function with omitBlankItems set to false, we can't tell the difference between an empty list and 
        /// a list with one or more blank items - therefore, we don't know whether or not to add the delimiter to the end before adding the next item.
        public static void AddToStringList(ref string list, string item, string delimiter, bool omitBlankItems, bool addingFirstItem)
        // convenience function
        {
            if (list == null)
                return;

            // check for null or blank item:
            if (item == null || item == "")
            {
                if (omitBlankItems)
                    return;
                else
                    item = ""; // in case it was null, it needs to be an actual string
            }

            // Now, add item to list finally -- along with a delimiter if it's not the first item:
            if (!addingFirstItem)
                list += delimiter;
            list += item;
        }

        /// <summary>
        /// Appends a string value to the end of a string variable, with a comma and space separating it from the other values. 
        /// </summary>
        /// <param name="list">String variable to add the string value to</param>
        /// <param name="item">String value to add</param>
        /// This overload does not add blank or null items to the list. 
        public static void AddToCommaSeparatedList(ref string list, string item)
        {
            AddToStringList(ref list, item, ", ");
        }

        /// <summary>
        /// Appends a string value to the end of a string variable, with a comma and space separating it from the other values. 
        /// </summary>
        /// <param name="list">String variable to add the string value to</param>
        /// <param name="item">String value to add</param>
        /// <param name="omitBlankItems">If true, an item of value of "" or null will be ignored; if false, it will be added as a delimiter with nothing before it</param>
        /// <param name="addingFirstItem">True if this is the first item added to this string variable; false if not</param>
        /// Constructs a list of string values with delimiters between the items, but not at the beginning or end. Therefore, if addingFirstItem is true, only the item is added; 
        /// if it's false, the comma-space delimiter is added followed by the item.
        /// Parameter addingFirstItem was added 5/30/17, because if we call this function with omitBlankItems set to false, we can't tell the difference between an empty list and 
        /// a list with one or more blank items - therefore, we don't know whether or not to add the comma-space delimiter to the end before adding the next item.
        public static void AddToCommaSeparatedList(ref string list, string item, bool omitBlankItems, bool addingFirstItem)
        {
            AddToStringList(ref list, item, ", ", omitBlankItems, addingFirstItem);
        }

        // ```GridDateUTCToLocal, etc. should maybe go into a new static UTCTimeUtils class...

        /* GridDateUTCToLocal is an function that can be called from a DataGridView's 
         * CellFormatting event, for cells bound to date/time database columns stored in UTC 
         * (Universal time), to display the value in Local time.
         * Parameter: 
         * - e: the DataGridViewCellFormattingEventArgs object passed to the CellFormatting
         *   even handler that calls this function
         */
        public static void GridDateUTCToLocal(DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is DateTime)
            {
                DateTime utcDateValue = (DateTime)e.Value;
                DateTime localDateValue = utcDateValue.ToLocalTime();
                e.Value = localDateValue.ToString(e.CellStyle.Format);

                // Let the DataGridView know we've handled the formatting, 
                // so it doesn't have to:
                e.FormattingApplied = true;
            }
        }

        /* MakeDateBindingLocalToUTC is a convenience function that adds handlers
         * to the specified control's Binding object, if it's bound to a DateTime, 
         * so that it displays a local time but stores a UTC time. If the 
         * control contains no binding, it exits the function. 
         * It only looks at the first Bindings object in the control's
         * DataBindings member, assuming that there's only one binding.
         * 
         * WARNING: This converts the date based on the client computer's Time Zone setting, 
         * rather than the server's, which is not good -- I don't think it's 21 CFR Part 11 compliant.
         */
        public static void MakeDateBindingLocalToUTC(Control ctrl)
        {
            // Make sure there's at least one binding:
            if (ctrl.DataBindings.Count < 1)
                return;
            Binding binding = ctrl.DataBindings[0];

            // Now, link to the two handlers:
            binding.Parse += new ConvertEventHandler(DateBindingParseHandler_LocalToUTC);
            binding.Format += new ConvertEventHandler(DateBindingFormatHandler_UTCToLocal);
        }

        /* DateBindingParseHandler_LocalToUTC is an event handler for a DateBinding's 
         * Parse event that converts the Local date/time value the user entered to a
         * Universal (UTC) time to be stored in the database. It is bound to a 
         * control's Binding object by Utilities.MakeDateBindingLocalToUTC.
         * 
         * WARNING: This converts the date based on the client computer's Time Zone setting, 
         * rather than the server's, which is not good -- I don't think it's 21 CFR Part 11 compliant.
         */
        static void DateBindingParseHandler_LocalToUTC(object sender, ConvertEventArgs e)
        {
            if (e.Value is DateTime)
            {
                DateTime localDateValue = (DateTime)e.Value;
                DateTime utcDateValue = localDateValue.ToUniversalTime();
                e.Value = utcDateValue;
            }
        }

        /* DateBindingFormatHandler_UTCToLocal is an event handler for a DateBinding's 
         * Format event that converts a Universal (UTC) time stored in the database 
         * to a Local date/time value to be displayed to the user. It is bound to a 
         * control's Binding object by Utilities.MakeDateBindingLocalToUTC.
         * 
         * WARNING: This converts the date based on the client computer's Time Zone setting, 
         * rather than the server's, which is not good -- I don't think it's 21 CFR Part 11 compliant.
         */
        static void DateBindingFormatHandler_UTCToLocal(object sender, ConvertEventArgs e)
        {
            if (e.Value is DateTime)
            {
                DateTime utcDateValue = (DateTime)e.Value;
                DateTime localDateValue = utcDateValue.ToLocalTime();
                e.Value = localDateValue;
            }
        }

        /* SetChildControlsWritability sets the ReadOnly property of the specified 
         * control, and of all its child controls and "descendent" controls, to the 
         * specified boolean value, and adjusts the appearance of each control to indicate 
         * the new ReadOnly state. If a control is of a type that does not have a ReadOnly 
         * property, an equivalent property is set, such as Enabled.
         * Parameters: 
         * - containingCtrl: Control to set the ReadOnly property of, and to set all 
         *   "descendent" controls' ReadOnly properties of
         * - readOnly: Boolean value to set the ReadOnly property to
         */
        public static void SetChildControlsWritability(Control containingCtrl, bool readOnly)
        {
            SetControlWritability(containingCtrl, readOnly);
            foreach (Control subctrl in containingCtrl.Controls)
                SetChildControlsWritability(subctrl, readOnly); // recursively call for all chgild controls
        }

        /* SetControlWritability sets the specified control's ReadOnly property to the 
         * specified boolean value, and adjusts the appearance of the control to indicate 
         * the new ReadOnly state. If the control is of a type that does not have a ReadOnly 
         * property, an equivalent property is set, such as Enabled.
         * Parameters: 
         * - ctrl: Control to set the ReadOnly property of
         * - readOnly: Boolean value to set the ReadOnly property to
         */
        public static void SetControlWritability(Control ctrl, bool readOnly)
        //sets color too
        {
            /* REMOVED
            if (ctrl is DateTimeNullablePicker)
            {
                if (readOnly) // don't set it if it's already readonly
                    ((DateTimeNullablePicker)ctrl).ReadOnly = true;
                ctrl.BackColor = ((DateTimeNullablePicker)ctrl).ReadOnly ? DNColor.ReadOnlyCells : SystemColors.Window;
            }
            else
            */
            {
                if (ctrl is TextBoxBase)
                {
                    if (readOnly) // don't set it if it's already readonly
                        ((TextBoxBase)ctrl).ReadOnly = true;
                    ctrl.BackColor = ((TextBoxBase)ctrl).ReadOnly ? DNColor.ReadOnlyCells : SystemColors.Window;
                }
                else
                {
                    if (ctrl is NumericUpDown)
                    {
                        if (readOnly) // don't set it if it's already readonly
                        {
                            ((NumericUpDown)ctrl).ReadOnly = true;
                            /* Setting ReadOnly to True doesn't prevent the user from clicking the 
                             * increment and decrement buttons. How stupid is that? The way to 
                             * disable that is to set Increment to 0, so that even if they do 
                             * click a button, it does nothing:
                             */
                            ((NumericUpDown)ctrl).Increment = 0;
                        }
                        ctrl.BackColor = ((NumericUpDown)ctrl).ReadOnly ? DNColor.ReadOnlyCells : SystemColors.Window;
                    }
                    else
                    {
                        if (ctrl is ComboBox || ctrl is CheckBox)
                        {
                            if (readOnly) // don't set it if it's already readonly
                                ctrl.Enabled = !readOnly;
                        }
                    }
                }
            }
        }

        /* GetButtonCellStandardStyle returns the standard DataGridViewCellStyle that 
         * DataGridViewButtonCells should be -- i.e. the same color and alignment 
         * as Button controls.
         */
        public static DataGridViewCellStyle GetButtonCellStandardStyle()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.BackColor = System.Drawing.SystemColors.Control;
            style.SelectionBackColor = System.Drawing.SystemColors.Control;
            return style;
        }

        // ShowDataEntryError is a convenience function that displays a message box
        // titled "Data Entry Error" that displays the specified text of the error.
        public static void ShowDataEntryError(string errorText)
        {
            MessageBox.Show(errorText, "Data Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /* NumericUpDownIntValue returns the integral value a NumericUpDown is set to.
         * One problem with the NumericUpDown control is that its Value property can never be null, 
         * even though a user can blank out the ctrl... this partially solves that. 
         * Although I should create a SmartNumericUpDown. Or should I just use the SmartTextBox?
        */
        public static int? NumericUpDownIntValue(NumericUpDown nud)
        {
            if (nud.Text == null || nud.Text == "")
                return null;
            else
                return (int)nud.Value;
        }

        /* Hash Code Functions (CreateHashCode_2Int16s, CreateHashCode_4Bytes)
        
            According to MSDN, a hash function must have the following properties: 
            1. If two objects compare as equal, the GetHashCode method for each object must return the same value. 
               However, if two objects do not compare as equal, the GetHashCode methods for the two object do not have 
               to return different values.
            2. The GetHashCode method for an object must consistently return the same hash code as long as there is 
               no modification to the object state that determines the return value of the object's Equals method. Note 
               that this is true only for the current execution of an application, and that a different hash code can 
               be returned if the application is run again.
            3. For the best performance, a hash function must generate a random distribution for all input.
         */

        /* 
         * CreateHashCode_2Int16s supplies a value for the override of GetHashCode by "stuffing" two int values --
         * actually, the lower order 16 bits of them -- into one 32-bit int value.
         */
        public static Int32 CreateHashCode_2Int16s(int int1, int int2)
        {
            const int twoToThe16th = 0x10000;
            UInt32 uHashCode = ((uint)int1 % twoToThe16th);
            uHashCode = (uHashCode << 16) | ((uint)int2 % twoToThe16th);
            return (Int32)uHashCode;
        }

        /* 
         * CreateHashCode_4Bytes supplies a value for the override of GetHashCode by "stuffing" four int values --
         * actually, the lower order 8 bits of them -- into one 32-bit int value.
         */
        public static Int32 CreateHashCode_4Bytes(int int1, int int2, int int3, int int4)
        {
            const int twoToThe8th = 0x100;
            UInt32 uHashCode = ((uint)int1 % twoToThe8th);
            uHashCode = (uHashCode << 8) | ((uint)int2 % twoToThe8th);
            uHashCode = (uHashCode << 8) | ((uint)int3 % twoToThe8th);
            uHashCode = (uHashCode << 8) | ((uint)int4 % twoToThe8th);
            return (Int32)uHashCode;
        }

        public static Font ScaleFont(Font displayedFont, decimal scaleFactor)
        {
            // Scale the font only if necessary:
            if (scaleFactor == 1)
                return displayedFont;
            else
            {
                return new Font(displayedFont.FontFamily,
                    displayedFont.Size * (float)scaleFactor,
                    displayedFont.Style, displayedFont.Unit);
            }
        }

        /* Compare is an implementation of IComparable.CompareTo. It enables a brief single function call to 
         * do a comparison even when one or both comparison values are null and their type is unknown.
         * For example, if variables p1 and p2 are declared as type IComparable, to set to any variable that's 
         * IComparable, you can't compare them by simply calling p1.CompareTo(p2), because that will throw if
         * p1 is null. This function does the necessary several lines of checking for null values all in one place.
         * 
         * I would think that there's a function in the .NET framework that does this. Maybe there is; I haven't
         * found one yet (as of 5/13/11, in .NET 2.0).
         * 
         * Compare returns:
         *   Less than zero if val1 is less than val2. 
         *   Zero if val1 and val2 are equal. 
         *   Greater than zero if val1 is greater than val2. 
         *   
         * This function compares strings case-sensitive, with a null string evaluated as "lesser" than a blank string. 
         * If the operands are, or contain, strings that need to be compared case-insensitive  
         * or null = blank, call the overload function, Compare(IComparable, IComparable, bool, bool).
         */

        /// <summary>
        /// Compares two values, even when one or both comparison values are null and their type is unknown.
        /// </summary>
        /// <param name="val1">First value to compare</param>
        /// <param name="val2">Second value to compare</param>
        /// <returns>Less than zero if val1 is less than val2, zero if val1 and val2 are equal, or 
        /// greater than zero if val1 is greater than val2.</returns>
        /// Compare is an implementation of IComparable.CompareTo. It enables a brief single function call to 
        /// do a comparison even when one or both comparison values are null and their type is unknown.
        /// For example, if variables p1 and p2 are declared as type IComparable, to set to any variable that's 
        /// IComparable, you can't compare them by simply calling p1.CompareTo(p2), because that will throw if
        /// p1 is null. This function does the necessary several lines of checking for null values all in one place.
        /// 
        /// I would think that there's a function in the .NET framework that does this. Maybe there is; I haven't
        /// found one yet (as of 5/13/11, in .NET 2.0).
        /// 
        /// Compare returns:
        ///   Less than zero if val1 is less than val2. 
        ///   Zero if val1 and val2 are equal. 
        ///   Greater than zero if val1 is greater than val2. 
        ///   
        /// This function compares strings case-insensitive, with a null string evaluated as "lesser" than a blank string. 
        /// If the operands are, or contain, strings that need to be compared case-insensitive  
        /// or null = blank, call the overload function, Compare(IComparable, IComparable, bool, bool).
        public static int Compare(IComparable val1, IComparable val2)
        {
            return Compare(val1, val2, ComparisonFlags.CaseSensitive);
        }

        /// <summary>
        /// Compares two values, even when one or both comparison values are null and their type is unknown.
        /// </summary>
        /// <param name="val1">First value to compare</param>
        /// <param name="val2">Second value to compare</param>
        /// <param name="caseSensitive">Whether comparisons of string values, in the parameters of in members of the parameters, are case-sensitive</param>
        /// <param name="nullStringMatchesBlank">Whether a comparison of string values in the parameters of in members of the parameters, 
        /// considers a null string as equivalent to a blank string ("")</param>
        /// <returns>Less than zero if val1 is less than val2, zero if val1 and val2 are equal, or 
        /// greater than zero if val1 is greater than val2.</returns>
        /// This overload of Compare is a convenience function for backward compatibility.
        /// 
        /// Compare is an implementation of IComparable.CompareTo. It enables a brief single function call to 
        /// do a comparison even when one or both comparison values are null and their type is unknown.
        /// For example, if variables p1 and p2 are declared as type IComparable, to set to any variable that's 
        /// IComparable, you can't compare them by simply calling p1.CompareTo(p2), because that will throw if
        /// p1 is null. This function does the necessary several lines of checking for null values all in one place.
        /// 
        /// Compare returns:
        ///   Less than zero if val1 is less than val2. 
        ///   Zero if val1 and val2 are equal. 
        ///   Greater than zero if val1 is greater than val2. 
        public static int Compare(IComparable val1, IComparable val2, bool caseSensitive, bool nullStringEqualsBlank)
        {
            return Compare(val1, val2, (caseSensitive ? ComparisonFlags.CaseSensitive : 0) | (nullStringEqualsBlank ? ComparisonFlags.BlankStringMatchesNull : 0));
        }

        /// <summary>
        /// Compares two values, even when one or both comparison values are null and their type is unknown.
        /// </summary>
        /// <param name="val1">First value to compare</param>
        /// <param name="val2">Second value to compare</param>
        /// <param name="flags">Flags specifying modes of comparison, such as CaseSensitive and NullStringMatchesBlank</param>
        /// <returns>Less than zero if val1 is less than val2, zero if val1 and val2 are equal, or 
        /// greater than zero if val1 is greater than val2.</returns>
        /// This overload of Compare allows specification of how string values, either as parameters or members of the parameters, 
        /// are to be compared.
        /// 
        /// Compare is an implementation of IComparable.CompareTo. It enables a brief single function call to 
        /// do a comparison even when one or both comparison values are null and their type is unknown.
        /// For example, if variables p1 and p2 are declared as type IComparable, to set to any variable that's 
        /// IComparable, you can't compare them by simply calling p1.CompareTo(p2), because that will throw if
        /// p1 is null. This function does the necessary several lines of checking for null values all in one place.
        /// 
        /// Compare returns:
        ///   Less than zero if val1 is less than val2. 
        ///   Zero if val1 and val2 are equal. 
        ///   Greater than zero if val1 is greater than val2. 
        public static int Compare(IComparable val1, IComparable val2, ComparisonFlags flags)
        {
            // Check equivalency flags and reset values accordingly so they'll "match":  
            if (flags.HasFlag(ComparisonFlags.BlankStringMatchesNull) && val1 as string == "")
                val1 = null;
            if (flags.HasFlag(ComparisonFlags.BlankStringMatchesNull) && val2 as string == "")
                val2 = null;

            if (flags.HasFlag(ComparisonFlags.DBNullMatchesNull) && DBNull.Value.Equals(val1))
                val1 = null;
            if (flags.HasFlag(ComparisonFlags.DBNullMatchesNull) && DBNull.Value.Equals(val2))
                val2 = null;

            if (val1 != null)
            {
                if (val2 != null)
                {
                    // if they're both strings and it's a caseSensitive==false, do a special case-insensitive string compare:
                    if (!flags.HasFlag(ComparisonFlags.CaseSensitive) && val1 is string && val2 is string)
                        return string.Compare((string)val1, (string)val2, true);
                    else if (val1 is IComparableContainingStrings)
                        return ((IComparableContainingStrings)val1).CompareTo(val2, flags);
                    else
                        return val1.CompareTo(val2);
                }
                else
                    return 1;
            }
            else
            {
                if (val2 != null)
                    return -1;
                else
                    return 0;
            }
        }

        public static void UnitTest()
        {
            // uncomment these when unit testing is necessary:
            // UnitTestEquals();
            // UnitTestCompare();
        }

        /// <summary>
        /// Returns a human-readable string representation of any value of any type, even if it's null.
        /// </summary>
        /// <param name="val">Value to show</param>
        /// <param name="includeType">True to include type in string returned (less ambiguous), false to not (briefer)</param>
        /// <returns>Human-readable string representation</returns>
        /// For use in testing (such as unit testing) and diagnostics
        public static string StringRepresentation(object val, bool includeType)
        {
            if (val == null)
                return "<NULL>";
            else if (val == DBNull.Value)
                return "<DBNULL>";
            else if (val is string)
                return "\"" + (string)val + "\"";
            else
                return val.ToString() + (includeType ? " <" + val.GetType().ToString() + ">" : "");
        }

        // Used in UnitTestCompare()
        static void UnitTestCompareCase(IComparable val1, IComparable val2, ComparisonFlags flags)
        {
            Console.WriteLine("Compare (" + StringRepresentation(val1, false) + ", \t" + StringRepresentation(val2, false) + ", \t" +
                flags.ToString() + "\t" + Compare(val1, val2, flags).ToString());
        }

        // Unit test for Compare method
        static void UnitTestCompare()
        {
            IComparable[] stringVals = new IComparable[] { null, "", "ab", "Ab", "aa", "Aba", "aba" };
            IComparable[] intVals = new IComparable[] { null, 1, 2 };

            foreach (IComparable[] valSet in new IComparable[][] { stringVals, intVals })
                foreach (IComparable val1 in valSet)
                    foreach (IComparable val2 in valSet)
                        foreach (bool caseSensitive in new bool[] { true, false })
                            foreach (bool nullStringMatchesBlank in new bool[] { true, false })
                                UnitTestCompareCase(val1, val2,
                                    (caseSensitive ? ComparisonFlags.CaseSensitive : 0) |
                                    (nullStringMatchesBlank ? ComparisonFlags.BlankStringMatchesNull : 0));
        }

        // Used in UnitTestEquals()
        static void UnitTestEqualsCase(object val1, object val2, ComparisonFlags flags)
        {
            Console.WriteLine("Equals (" + StringRepresentation(val1, false) + ", \t" + StringRepresentation(val2, false) + ", \t" +
                flags.ToString() + "\t" + Equals(val1, val2, flags).ToString());
        }

        // Unit test for Equals method
        static void UnitTestEquals()
        {
            object[] vals = new object[] { null, DBNull.Value, "", "ab", "Ab", 1, 2 };
            foreach (object val1 in vals)
                foreach (object val2 in vals)
                    foreach (bool caseSensitive in new bool[] { true, false })
                        foreach (bool nullStringMatchesBlank in new bool[] { true, false })
                            foreach (bool dbNullMatchesNull in new bool[] { true, false })
                                UnitTestEqualsCase(val1, val2,
                                    (caseSensitive ? ComparisonFlags.CaseSensitive : 0) |
                                    (nullStringMatchesBlank ? ComparisonFlags.BlankStringMatchesNull : 0) |
                                    (dbNullMatchesNull ? ComparisonFlags.DBNullMatchesNull : 0));
        }

        /// <summary>
        /// Checks two values for equality, even when one or both values are null and their type is unknown.
        /// </summary>
        /// <param name="val1">First value to compare for equality</param>
        /// <param name="val2">Second value to compare for equality</param>
        /// <param name="flags">Flags specifying modes of comparison, such as CaseSensitive and NullStringMatchesBlank</param>
        /// <returns>True if values are equal, false if not</returns>
        /// Allows specification of how string values, either as parameters or members of the parameters, are to be compared.
        public static bool Equals(object val1, object val2, ComparisonFlags flags)
        {
            // Check equivalency flags and reset values accordingly so they'll "match":  
            if (flags.HasFlag(ComparisonFlags.BlankStringMatchesNull) && val1 as string == "")
                val1 = null;
            if (flags.HasFlag(ComparisonFlags.BlankStringMatchesNull) && val2 as string == "")
                val2 = null;

            if (flags.HasFlag(ComparisonFlags.DBNullMatchesNull) && DBNull.Value.Equals(val1))
                val1 = null;
            if (flags.HasFlag(ComparisonFlags.DBNullMatchesNull) && DBNull.Value.Equals(val2))
                val2 = null;

            if (val1 != null)
            {
                if (val2 != null)
                {
                    // if they're both strings and it's a caseSensitive==false, do a special case-insensitive string compare:
                    if (!flags.HasFlag(ComparisonFlags.CaseSensitive) && val1 is string && val2 is string)
                        return string.Equals((string)val1, (string)val2, StringComparison.CurrentCultureIgnoreCase);
                    else if (val1 is IComparableContainingStrings)
                        return ((IComparableContainingStrings)val1).Equals(val2, flags);
                    else
                        return val1.Equals(val2);
                }
                else
                    return false;
            }
            else
                return (val2 == null);
        }

        /// <summary>
        /// Compares two string values, and if they both parse to a decimal (numeric) values, then compare them as decimal; otherwise compare them as string. 
        /// </summary>
        /// <param name="val1">First string value to compare</param>
        /// <param name="val2">Second string value to compare</param>
        /// <param name="flags">Flags specifying modes of comparison, such as CaseSensitive and NullStringMatchesBlank</param>
        /// <returns>Less than zero if val1 is less than val2, zero if val1 and val2 are equal, or 
        /// greater than zero if val1 is greater than val2.</returns>
        /// For example, returns 0 (equal) if comparing "0.7" and ".7".
        /// Not used as of 2/23/15 - thought I needed it but didn't
        public static int CompareAsNumericOrString(string val1, string val2, ComparisonFlags flags)
        {
            decimal dVal1, dVal2;
            if (Decimal.TryParse(val1, out dVal1) && Decimal.TryParse(val2, out dVal2))
                return Decimal.Compare(dVal1, dVal2);
            else
                return Compare(val1, val2, flags);
        }

        public static string TimeSpanWrittenOut(TimeSpan timeSpan,
            bool showDays, bool showHours, bool showMinutes, bool showSeconds, bool showMilliseconds)
        {
            string str = "";
            if (showDays && timeSpan.Days > 0)
                AddToCommaSeparatedList(ref str, timeSpan.Days.ToString() + (timeSpan.Days == 1 ? " day" : " days"));
            if (showHours && timeSpan.Hours > 0)
                AddToCommaSeparatedList(ref str, timeSpan.Hours.ToString() + " hr.");
            if (showMinutes && timeSpan.Minutes > 0)
                AddToCommaSeparatedList(ref str, timeSpan.Minutes.ToString() + " min.");
            if (showSeconds)
            {
                if (timeSpan.Seconds > 0 || showMilliseconds)
                {
                    string strSeconds = timeSpan.Seconds.ToString();
                    if (showMilliseconds && timeSpan.Milliseconds > 0)
                        strSeconds += "." + timeSpan.Milliseconds.ToString();
                    AddToCommaSeparatedList(ref str, strSeconds + " sec.");
                }
            }
            return str;
        }

        // 
        /// <summary>
        /// Recursively copies the contents of one directory to another directory 
        /// </summary>
        /// <param name="sourceDir">Absolute path of directory to copy</param>
        /// <param name="destDir">Absolute path of directory that sourceDir is to be copied to</param>
        /// <param name="onlyIfNewer">If true, copies a file only if the source file is newer than the destination file</param>
        public static void CopyDirectory(string sourceDir, string destDir, bool onlyIfNewer)
        {
            String[] sourceFiles;

            if (destDir[destDir.Length - 1] != Path.DirectorySeparatorChar)
                destDir += Path.DirectorySeparatorChar;
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);
            sourceFiles = Directory.GetFileSystemEntries(sourceDir);
            foreach (string sourceFilePath in sourceFiles)
            {
                // Sub directories
                if (Directory.Exists(sourceFilePath))
                    CopyDirectory(sourceFilePath, destDir + Path.GetFileName(sourceFilePath), onlyIfNewer);
                // Files in directory
                else
                {
                    string destFilePath = destDir + Path.GetFileName(sourceFilePath);
                    bool copyTheFile = true;
                    if (onlyIfNewer)
                    {
                        // don't copy if dest. file is just as new as source:
                        if (File.Exists(destFilePath) && File.GetLastWriteTime(destFilePath) >= File.GetLastWriteTime(sourceFilePath))
                            copyTheFile = false;
                    }
                    if (copyTheFile)
                        File.Copy(sourceFilePath, destFilePath, true);
                }
            }
        }

        /** <summary> Returns a "brief" version number -- only 2 elements, unless the 3rd or 
         * 4th element is non-zero. </summary>
         * <param name="version">version number</param>
         * <returns>Brief string representation of version number</returns>
         * Examples:
         *   Full version   BriefVersionString
         *   2.0.0.0        2.0
         *   10.5.0.0       10.5
         *   3.2.1.0        3.2.1
         *   3.3.12.1       3.3.12.1
         * */
        public static string BriefVersionString(Version version)
        {
            string verString = version.Major.ToString() + "." + version.Minor.ToString();
            if (version.Build != 0 || version.Revision != 0)
                verString += "." + version.Build.ToString();
            if (version.Revision != 0)
                verString += "." + version.Revision.ToString();
            return verString;
        }

        /** <summary> Returns a "brief" version number -- only 2 elements, unless the 3rd or 
         * 4th element is non-zero. </summary>
         * <param name="versionString">version number string</param>
         * <returns>Brief string representation of version number</returns>
         * Examples:
         *   Full version   BriefVersionString
         *   2.0.0.0        2.0
         *   10.5.0.0       10.5
         *   3.2.1.0        3.2.1
         *   3.3.12.1       3.3.12.1
         * */
        public static string BriefVersionString(string versionString)
        {
            string[] verArray = versionString.Split('.');

            // Add elements back to front: 
            bool writeElements = false;
            string briefString = "";
            for (int n = 3; n >= 0; n--)
            {
                if (n == 1)
                    writeElements = true; // write at least 2 elements
                string element = "0";
                if (verArray.Length > n && verArray[n] != "0") // if there's an element that isn't 0, start writing elements
                {
                    element = verArray[n];
                    writeElements = true;
                }

                if (writeElements)
                {
                    if (briefString == "")
                        briefString = element;
                    else
                        briefString = element + "." + briefString; // append element to the front
                }
            }

            return briefString;
        }

        /* REMOVED
        /// <summary>
        /// Finds the innermost IDNForm containing a control.
        /// </summary>
        /// <param name="ctrl">A control</param>
        /// <returns>An IDNForm, or Null if the control is not contained in an IDNForm</returns>
        /// Returns the parent, grandparent, or other ancestor that's an IDNForm, or Null if there isn't one.
        public static IDNForm FindIDNFormParent(Control ctrl)
        {
            Control nextParent = ctrl.Parent;
            while (nextParent != null)
            {
                if (nextParent is IDNForm)
                    return (IDNForm)nextParent;
                else
                    nextParent = nextParent.Parent;
            }
            return null; // if it go all the way to the top without finding an IDNForm 
        }
        */

        /// <summary>
        /// Sets or clears a flag from a [Flags]-attributed Enum value.
        /// </summary>
        /// <typeparam name="T">Type of the [Flags]-attributed Enum</typeparam>
        /// <param name="value">Enum value</param>
        /// <param name="flag">The flag to set or clear</param>
        /// <param name="set">True to set, false to clear</param>
        /// <returns>The enum value with the flag set or cleared</returns>
        /// C# doesn't have a concise, intuitive function for this - Microsoft apparently assumes that every programmer loves writing x |= y; else x &= ~y all the time.
        /// This elegant little method is by Eric Ouellet, from http://stackoverflow.com/questions/5850873/enum-hasflag-why-no-enum-setflag/5851178#5851178.
        public static T SetFlag<T>(this Enum value, T flag, bool set)
        {
            Type underlyingType = Enum.GetUnderlyingType(value.GetType());

            // note: AsInt mean: math integer vs enum (not the c# int type)
            dynamic valueAsInt = Convert.ChangeType(value, underlyingType);
            dynamic flagAsInt = Convert.ChangeType(flag, underlyingType);
            if (set)
            {
                valueAsInt |= flagAsInt;
            }
            else
            {
                valueAsInt &= ~flagAsInt;
            }

            return (T)valueAsInt;
        }

        /// <summary>
        /// Returns filename with "(1)", "(2)", etc. appended to it, if it conflicts with the name of an existing file, otherwise returns file
        /// </summary>
        /// <param name="directory">Directory in which to check for file with conflicting name</param>
        /// <param name="filename">Name of file to check (without extension)</param>
        /// <param name="extension">Filename extension of file to check</param>
        /// <returns>New filename</returns>
        public static string NextNumberedFilenameIfExists(string directory, string filename, string extension)
        {
            string filenameToTry = filename;
            while (File.Exists(Path.Combine(directory, filenameToTry + "." + extension)))
            {
                // Does it have a number in parentheses already appended to it? Check with Regex:
                Regex appendedNumRegex = new Regex(@"^(.*)\((\d+)\)$");
                Match match = appendedNumRegex.Match(filenameToTry);
                if (match == Match.Empty)
                    filenameToTry = filenameToTry + "(1)"; // no appended number already there; append one
                else
                {
                    // A non-empty match must return three groups: the whole match, the filename without the number, and the extracted string of digits. 
                    // Parse and increment the extracted string of digits, and re-form the filename:
                    int numToAppend = int.Parse(match.Groups[2].Value) + 1;
                    filenameToTry = match.Groups[1].Value + "(" + numToAppend.ToString() + ")";
                }
            }
            return filenameToTry; // once it breaks out of the loop, as it inevitably must
        }
    }
}
