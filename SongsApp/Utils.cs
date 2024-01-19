using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Songs
{
    class Utils
    {
        public static bool DataTableIsModified(DataTable tbl)
        {
            foreach (DataRow row in tbl.Rows)
            {
                if (row.RowState != DataRowState.Unchanged)
                    return true;
            }
            return false; // if no changes found
        }

        public static void AllowNullFields(DataTable dataTable)
        /* AllowNullFields takes all the columns in the given DataTable, except for the
         * primary key(s), and removes the AllowDBNull=false restriction from each column, 
         * if that restriction exists.
         * 
         * This is necessary because of a conundrum an app developer faces due to poor 
         * design of Microsoft .NET data access and data binding. When I use the Dataset 
         * Designer to define a DataTable based on a table in the SQL Server database, 
         * it will take any field defined as "Allow Nulls=No" and define it as a DataColumn
         * with AllowDBNull = False. This is desirable when saving a record to the database.
         * However, .NET enforces this when using DataRowCollection.AddRow to add a newly 
         * created DataRow to the DataTable, and if the user hasn't yet gottten the chance 
         * to edit the record and fill in the required (non-null-allowing) fields, AddRow
         * will throw a NoNullAllowedException, with the message "Column 
         * <column-name> does not allow nulls."
         * 
         * Microsoft's rationale for this is that you're supposed use DataTable.NewRow 
         * to create a new, blank DataRow, then fill in the necessary fields, 
         * THEN use DataRowCollection.AddRow to finally add the completed, 
         * database-UPDATE-ready row to the DataTable. The problem with this is that in 
         * many cases, we want the user to edit the new record in a data-bound .NET Form, 
         * with each field bound to a DataColumn with a Systems.Windows.Forms.Binding
         * object. As far as I could tell, if the DataRow the user is to edit has not
         * yet been added to the DataTable, there's no way to set the Form's 
         * BindingContext.Position to the postion of this new row. Therefore, I can't use 
         * the same binding to edit new records as to edit existing records, with 
         * Binding.DataSource = the DataTable. And that pretty much defeats the purpose of 
         * Data Binding. (See this discussion thread on http://thedotnet.com/nntp/6785/showpost.aspx .)
         * 
         * This was not a problem when the DB was on Access97, because of another Microsoft 
         * bug: the Dataset Designer, working with System.Data.OleDb objects, would ignore 
         * the "Required" attribute of an Access 97 table field, and DataColumn.AllowDBNull
         * was never set except on PrimaryKey fields, so it never threw the 
         * NoNullAllowedException.
         * 
         * After pondering this problem -- which exists when the new record is to be edited
         * either in a Form, or in a Grid -- I decided the best solution was the simplest
         * solution: implement a way to remove the AllowDBNull=False restriction from 
         * all fields (except the Primary Key) of any table. There's not a real easy way
         * to automatically restore that restriction once the record the user is editing is
         * no longer a new record, but this simple way is at least consistent. We lose the 
         * ability to have the .NET Framework check automatically for required fields before
         * we attempt to store the new record in the database. But at least the required 
         * field check is done in two other places -- by the DB Server when we attempt to
         * UPDATE the table, and earlier by StudyTracker's DBCol.IsARequiredField -- 
         * provided the field is defined as required in DBCol_Definitions.cs, and the 
         * code calls IsARequiredField before updating the DB. 
         */
        {
            foreach (DataColumn col in dataTable.Columns)
            {
                if (!col.AllowDBNull)
                {
                    bool relaxRequirement = true;
                    // don't relax the AllowNull restriction in primary keys:
                    foreach (DataColumn keyCol in dataTable.PrimaryKey)
                    {
                        if (keyCol == col)
                        {
                            relaxRequirement = false;
                            break;
                        }
                    }
                    if (relaxRequirement)
                        col.AllowDBNull = true;
                }
            }
        }
    }
}
