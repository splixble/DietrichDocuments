using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
// using System.Windows.Forms;

namespace TypeLib
{
    /** <summary> DBUtils contains database-related static utility functions. </summary>
     */
    public static class DBUtils
    {
        /* Safe<type> functions return the typed value of an object returned from a DataTable
         * row as a database field value. However, unlike a simple cast, these 
         * functions doesn't throw an error if the field value is null or DBNull -- they return 
         * defaultValue in that case.
         
         * I later added default-less Safe<type> functions. They return null if the 
         * field value is null or DBNull.
         * 
         * I later added DBNullToNull as a 
         */

        public static Object DBNullToNull(Object ob)
        {
            if (ob == DBNull.Value)
                return null;
            else
                return ob;
        }

        public static DateTime SafeDate(Object ob, DateTime defaultValue)
        {
            if (ob == DBNull.Value || ob == null)
                return defaultValue;
            else
                return (DateTime)ob;
        }

        public static DateTime? SafeDate(Object ob)
        {
            return (DateTime?)DBNullToNull(ob);
        }

        public static int SafeInt(Object ob, int defaultValue)
        {
            if (ob == DBNull.Value || ob == null)
                return defaultValue;
            else
                return TypeUtils.ToInt(ob);
        }

        public static int? SafeInt(Object ob)
        {
            return (int?)DBNullToNull(ob);
        }

        public static decimal SafeDecimal(Object ob, decimal defaultValue)
        {
            if (ob == DBNull.Value || ob == null)
                return defaultValue;
            else
                return (decimal)ob;
        }

        public static decimal? SafeDecimal(Object ob)
        {
            return (decimal?)DBNullToNull(ob);
        }

        public static bool SafeBool(Object ob)
        {
            return SafeBool(ob, false);
        }

        public static bool SafeBool(Object ob, bool defaultValue)
        {
            if (ob == DBNull.Value || ob == null)
                return defaultValue;
            else
                return (bool)ob;
        }

        public static string SafeString(Object ob, string defaultValue)
        {
            if (ob == DBNull.Value || ob == null)
                return defaultValue;
            else
                return (string)ob;
        }

        public static string SafeFieldValueString(Object ob, string defaultValue)
        {
            if (ob == DBNull.Value || ob == null)
                return defaultValue;
            else
                return ob.ToString();
        }

        public static string FieldValueDiagnosticString(object fieldValue)
        {
            // Always returns a string value, and distinguishes between blank, null, and DBNull for diagnostic clarity
            if (fieldValue == DBNull.Value)
                return "<dbnull>";
            else if ((string)fieldValue == "")
                return "<blank>";
            else if (fieldValue == null) // should never happen
                return "<null>";
            else
                return fieldValue.ToString();
        }

        public static string SQLServerQueryConstant(object val)
        {
            if (val == null)
                return "NULL";
            else if (val is string)
                // place in quotes, and replace single quote with double quote:
                return "'" + ((string)val).Replace("'", "''") + "'";
            else if (val is bool)
                return ((bool)val) ? "1" : "0";
            else if (TypeUtils.IsIntegral(val) || val is Decimal)
                return val.ToString();
            else if (val is DateTime)
                return ((DateTime)val).SQLDateLiteral();
            else
                throw (new Exception("DBUtils.SQLServerQueryConstant is not implemented for type " + val.GetType().ToString()));
        }

        /// <summary>
        /// Sets a DataColumn in a DataRow to a value only if that would change its value.
        /// </summary>
        /// <param name="row">Row in which to set column value</param>
        /// <param name="col">Column to set</param>
        /// <param name="newVal">Value to set the column to</param>
        /// <returns>True if column value changed; false if not.</returns>
        /// Useful when you don't want to set RowState to Modified unless it's really modified (set to a different value). 
        /// Microsoft implemented the DataRow such that if DataRow.DataRowState is Unchanged, and you set DataRow[column] to anything,
        /// it sets DataRowState to Modified, even if you set it to the same value it already has. Seems dumb, but that's how it is.
        public static bool SetColumnIfChanged(DataRow row, DataColumn col, object newVal)
        {
            if (!row[col].Equals(newVal))
            {
                row[col] = newVal;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Sets a DataColumn in a DataRow to a value only if that would change its value.
        /// </summary>
        /// <param name="row">Row in which to set column value</param>
        /// <param name="colName">Name of column to set</param>
        /// <param name="newVal">Value to set the column to</param>
        /// <returns>True if column value changed; false if not.</returns>
        /// Useful when you don't want to set RowState to Modified unless it's really modified (set to a different value). 
        /// Microsoft implemented the DataRow such that if DataRow.DataRowState is Unchanged, and you set DataRow[column] to anything,
        /// it sets DataRowState to Modified, even if you set it to the same value it already has. Seems dumb, but that's how it is.
        public static bool SetColumnIfChanged(DataRow row, string colName, object newVal)
        {
            if (!row[colName].Equals(newVal))
            {
                row[colName] = newVal;
                return true;
            }
            else
                return false;
        }

        /** <summary>SetDataAdapterTransaction is a convenience function for using 
         * Transactions with Adapters. Its sets the Transactions of all the given 
         * SqlDataAdapter's SqlCommands to the given Transaction.</summary>
         * Parameters:
         * - adapter: SqlDataAdapter containing the Transactions to reset
         * - transaction: SqlTransaction to set the SqlDataAdapter's Transactions to
         * */
        public static void SetDataAdapterTransaction(
            SqlDataAdapter adapter, SqlTransaction transaction)
        {
            if (adapter.SelectCommand != null)
                adapter.SelectCommand.Transaction = transaction;
            if (adapter.UpdateCommand != null)
                adapter.UpdateCommand.Transaction = transaction;
            if (adapter.InsertCommand != null)
                adapter.InsertCommand.Transaction = transaction;
            if (adapter.DeleteCommand != null)
                adapter.DeleteCommand.Transaction = transaction;
        }

        /// <summary>MaxDate is a date that's greater than any DateTime value we'll ever use, but
        /// that won't overflow any of our DB platforms.</summary> 
        /// It's defined as 12/31/9999, the maximum value of SQL Server's DateTime type.
        /// DateTime.MaxValue overflows SQLServer, so we can't use that.
        public static DateTime MaxDate
        {
            get { return new DateTime(9999, 12, 31); }
        }

        /// <summary>MinDate is a date that's less than any DateTime value we'll ever use, but
        /// that won't overflow any of our DB platforms.</summary>
        /// It's defined as 1/1/1753, the minimum value of SQL Server's DateTime type.
        /// DateTime.MinValue overflows SQLServer, so we can't use that.
        public static DateTime MinDate
        {
            get { return new DateTime(1753, 1, 1); }
        }

        /// <summary>
        /// Returns a data type definition as displayed in SQL Server's table column definitions.
        /// </summary>
        /// <param name="typeName">SQl Server data type name</param>
        /// <param name="precision">Precision, if applicable (e.g. if Decimal type)</param>
        /// <param name="scale">Scale, if applicable (e.g. if DateTime2 type)</param>
        /// <param name="maxLength">Max length, if applicable (e.g. if string type)</param>
        /// <returns>String as displayed in SQL Server's table column definitions</returns>
        public static string SQLServerDataTypeDefinitionString(string typeName, int precision, int scale, int maxLength)
        {
            typeName = typeName.ToLower(); // just in case it's not already lower case
            string parenthesizedText = null;
            if (typeName == "char" || typeName == "nchar" || typeName == "varchar" || typeName == "nvarchar")
            {
                if (maxLength == -1)
                    parenthesizedText = "MAX";
                else
                    parenthesizedText = maxLength.ToString();
            }
            else if (typeName == "datetime2")
                parenthesizedText = scale.ToString();
            else if (typeName == "decimal")
                parenthesizedText = precision.ToString() + "," + scale.ToString();

            if (parenthesizedText != null)
                return typeName + "(" + parenthesizedText + ")";
            else
                return typeName;
        }

        /** <summary> We need stronger typing when writing to a database field than C# assignments
         * provide by default. AreTypesCompatible is a way to enforce that 
         * stronger typing.</summary>
         */
        public static bool AreTypesCompatible(Type valueType, Type typeWrittenTo)
        {
            if (typeWrittenTo == typeof(String))
                return (valueType == typeof(String));
            if (typeWrittenTo == typeof(Boolean))
                return (valueType == typeof(Boolean));
            if (typeWrittenTo == typeof(Char))
                return (valueType == typeof(Char));
            if (typeWrittenTo == typeof(DateTime))
                return (valueType == typeof(DateTime));
            if (typeWrittenTo == typeof(Decimal))
                return (valueType == typeof(Decimal));
            if (typeWrittenTo == typeof(Double) || typeWrittenTo == typeof(Single))
                return (valueType == typeof(Double) || valueType == typeof(Single));
            if (TypeUtils.IsIntegral(typeWrittenTo))
                return (TypeUtils.IsIntegral(valueType));
            if (typeWrittenTo == typeof(TimeSpan))
                return (valueType == typeof(TimeSpan));
            return false; // by default
        }

        public static bool IsTypeCompatibleWithDBColumn(Type valueType, DataColumn dataCol)
        {
            return AreTypesCompatible(valueType, dataCol.DataType);
        }


        /** <summary>IsDataModified returns whether this field (column) of this row from a DataTable 
         * has changed in value since it was read from the DB of saved to the DB.</summary>
         * 
         * Parameter editIsCommitted reflects the data-saving model -- whether edits have
         * been made to a control with a Binding, and are still DataRowVersion.Proposed 
         * changes (before EndCurrentEdit has been called), or edits are written directly 
         * to the DataTable.
         * 
         * In short: If the edits were made by a control with a Binding, and EndCurrentEdit
         * has not been called, pass False to editIsCommitted. Otherwise -- if edits are made
         * with a writable DBGrid, or written directly to the DataRow, pass True to 
         * editIsCommitted.
         */
        public static bool IsDataModified(DataRow dataRow, DataColumn dataCol, bool editIsCommitted)
        {
            /*  diagnostic line:
            string strValues = "VAL " + dataCol.ColumnName + "-- ";
            if ( lookupDataRow.HasVersion(DataRowVersion.Original))
                strValues += "Orig: " + lookupDataRow[dataCol, DataRowVersion.Original].ToString();
            if ( lookupDataRow.HasVersion(DataRowVersion.Current))
                strValues += "Curr: " + lookupDataRow[dataCol, DataRowVersion.Current].ToString();
            if ( lookupDataRow.HasVersion(DataRowVersion.Proposed))
                strValues += "Prop: " + lookupDataRow[dataCol, DataRowVersion.Proposed].ToString();
            System.Windows.Forms.PBIMessageBox.Show(strValues);
             * */

            DataRowVersion versionOld;
            DataRowVersion versionNew;

            if (editIsCommitted)
            {
                versionOld = DataRowVersion.Original;
                versionNew = DataRowVersion.Current;
            }
            else
            {
                versionOld = DataRowVersion.Current;
                versionNew = DataRowVersion.Proposed;
            }

            if (dataRow.RowState.HasFlag(DataRowState.Added) &&
                dataRow.HasVersion(versionNew) &&
                dataRow[dataCol, versionNew] != DBNull.Value)
                return true;

            object oldValue = DBNull.Value;
            object newValue = DBNull.Value;
            if (!dataRow.HasVersion(versionOld) || !dataRow.HasVersion(versionNew))
                // doesn't have both versions; can't be changed
                return false;

            oldValue = dataRow[dataCol, versionOld];
            newValue = dataRow[dataCol, versionNew];
            return IsDataModified(newValue, oldValue);
        }

        /** <summary>This overload of IsDataModified compares the given old field value 
         * (before the edit(s)) to the new field value (after the edit(s)) and
         * returns true if they're different, false if they're the same.</summary>
         * Parameters:
         * - newValue: field value after the edit(s) 
         * - oldValue: field value before the edit(s) 
         * 
         * This function should someday go in a general lib.
         */
        public static bool IsDataModified(object newValue, object oldValue)
        {
            if (oldValue == DBNull.Value && newValue == DBNull.Value)
                return false; // was null; still is null -- hasn't changed
            if (oldValue == DBNull.Value || newValue == DBNull.Value)
                return true; // changed from null to non-null, or vice versa

            // ...otherwise, neither value is null, so compare: 
            // NOTE: Gotta call newValue.Equals in following comparison, not !=
            // -- to make it do a Value comparison, not a Reference comparison:
            return !newValue.Equals(oldValue);
        }

        /* NOT USED ANYMORE
         * <summary>CheckForRequiredFields checks the specified DataRow and returns true if all
         * field values for required columns (Allow Nulls = false, according to the 
         * specified DataTable) have non-DBNull, non-blank values, and false if any required 
         * columns are set to DBNull or to a blank string ("") (indicating "missing" field 
         * data in a required field).</summary>
         * 
         * NOTE: ```Do we need this anymore? DataModifiedMonitor.ValidateChanges does all this, doesn't it?
         * 
         * Note that for string fields, a blank string value ("") is treated as equivalent 
         * to a null database value. This is arguably at odds with the model of most 
         * relational databases; however, for PBI's databases, if we specify a string field
         * as Allow Nulls = false, we invariably also intend to disallow blank string values
         * in the field.
         * 
         * It needs a separate DataTable parameter rather than just using lookupDataRow.Table. 
         * In case we've called AllowNullFields for the table, that function removed
         * the AllowDBNull specification from all the columns, so we have to use a "fresh"
         * copy of the DataTable to know which fields are required. 
         * * /
        public static bool CheckForRequiredFields(DataRow dataRow, DataTable dataTable)
        {
            string neglectedFieldList = "";
            foreach (DataColumn col in dataTable.Columns)
            {
                if (!col.AllowDBNull)
                {
                    object fieldValue = dataRow[col.ColumnName];
                    if (fieldValue == DBNull.Value || // field is null
                    (col.DataType == typeof(string) && fieldValue.Equals(""))) // field type is string, value = "" 
                    {
                        if (neglectedFieldList != "")
                            neglectedFieldList += ", ";
                        neglectedFieldList += col.ColumnName;
                    }
                }
            }
            if (neglectedFieldList != "")
            {
                PBIMessageBox.Show("You must fill in the following Required Fields:\n" +
                    neglectedFieldList, "Missing Required Field Values",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            else
                return true;
        }
         * */

        /// <summary>
        /// Adds a copy of a DataRow from one table to another table.
        /// </summary>
        /// <param name="destinationTable">Table to add the row to</param>
        /// <param name="dataRow">Row to copy and add</param>
        /// <param name="includePrimaryKey">True to copy over primary key; false to copy over all fields EXCEPT primary key (e.g. when primary key is auto-incremented)</param>
        /// Throws exception if both tables are not the same type.
        /// This function is maybe too general-purpose? I had to give it the option to skip primary keys on 26-Mar-20; there are likely other situations that could get you in trouble
        /// with certain tables.
        public static DataRow CopyRowOver(DataTable destinationTable, DataRow dataRow, bool includePrimaryKey)
        {
            if (dataRow.Table.GetType() != destinationTable.GetType())
                throw (new Exception("Error in DBUtils.CopyRowOver: tables are not the same type."));

            DataRow newRow = destinationTable.NewRow();
            foreach (DataColumn col in destinationTable.Columns)
            {
                if (includePrimaryKey || !col.IsPrimaryKey())
                    newRow[col] = dataRow[col.ColumnName];
            }
            destinationTable.Rows.Add(newRow);
            return newRow;
        }

        /// <summary>SQLServerReservedWords is a list of all words defined in SQL Server 2005 as Reserved Words. 
        /// This is taken directly from the SQL Server Management Studio help text for SQL Server 2005.</summary>
        public static string[] SQLServerReservedWords
        {
            get
            {
                return new string[] { "ADD", "ALL", "ALTER", "AND", "ANY", "AS", "ASC", "AUTHORIZATION", "BACKUP",
                    "BEGIN", "BETWEEN", "BREAK", "BROWSE", "BULK", "BY", "CASCADE", "CASE", "CHECK", "CHECKPOINT",
                    "CLOSE", "CLUSTERED", "COALESCE", "COLLATE", "COLUMN", "COMMIT", "COMPUTE", "CONSTRAINT",
                    "CONTAINS", "CONTAINSTABLE", "CONTINUE", "CONVERT", "CREATE", "CROSS", "CURRENT", "CURRENT_DATE",
                    "CURRENT_TIME", "CURRENT_TIMESTAMP", "CURRENT_USER", "CURSOR", "DATABASE", "DBCC", "DEALLOCATE",
                    "DECLARE", "DEFAULT", "DELETE", "DENY", "DESC", "DISK", "DISTINCT", "DISTRIBUTED", "DOUBLE",
                    "DROP", "DUMMY", "DUMP", "ELSE", "END", "ERRLVL", "ESCAPE", "EXCEPT", "EXEC", "EXECUTE", "EXISTS",
                    "EXIT", "FETCH", "FILE", "FILLFACTOR", "FOR", "FOREIGN", "FREETEXT", "FREETEXTTABLE", "FROM",
                    "FULL", "FUNCTION", "GOTO", "GRANT", "GROUP", "HAVING", "HOLDLOCK", "IDENTITY", "IDENTITY_INSERT",
                    "IDENTITYCOL", "IF", "IN", "INDEX", "INNER", "INSERT", "INTERSECT", "INTO", "IS", "JOIN", "KEY",
                    "KILL", "LEFT", "LIKE", "LINENO", "LOAD", "NATIONAL", "NOCHECK", "NONCLUSTERED", "NOT", "NULL",
                    "NULLIF", "OF", "OFF", "OFFSETS", "ON", "OPEN", "OPENDATASOURCE", "OPENQUERY", "OPENROWSET",
                    "OPENXML", "OPTION", "OR", "ORDER", "OUTER", "OVER", "PERCENT", "PLAN", "PRECISION", "PRIMARY",
                    "PRINT", "PROC", "PROCEDURE", "PUBLIC", "RAISERROR", "READ", "READTEXT", "RECONFIGURE",
                    "REFERENCES", "REPLICATION", "RESTORE", "RESTRICT", "RETURN", "REVOKE", "RIGHT", "ROLLBACK",
                    "ROWCOUNT", "ROWGUIDCOL", "RULE", "SAVE", "SCHEMA", "SELECT", "SESSION_USER", "SET", "SETUSER",
                    "SHUTDOWN", "SOME", "STATISTICS", "SYSTEM_USER", "TABLE", "TEXTSIZE", "THEN", "TO", "TOP", "TRAN",
                    "TRANSACTION", "TRIGGER", "TRUNCATE", "TSEQUAL", "UNION", "UNIQUE", "UPDATE", "UPDATETEXT", "USE",
                    "USER", "VALUES", "VARYING", "VIEW", "WAITFOR", "WHEN", "WHERE", "WHILE", "WITH", "WRITETEXT"
                };
            }
        }

        /// SQLServerReservedWordsDictionary is a convenience object to facilitate checking if words are Reserverd Words.
        public static Dictionary<string, object> SQLServerReservedWordsDictionary
        {
            get
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                foreach (string str in SQLServerReservedWords)
                    dict.Add(str, null);
                return dict;
            }
        }
    }
}

