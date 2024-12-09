using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLib
{
    /// <summary>
    /// Contains methods added to the System.Data classes (DataTable, DataRow, DataColumn) as Extension Methods.
    /// </summary>
    /// Even though these methods, internally, are members of this class rather than of System.Data classes, these methods allows the developer to call custom-designed Data class functions 
    /// with the same syntax as actual Data class methods, and make use of IntelliSense and other Visual Studio features that make software development so much quicker and more intuitive. 
    public static class DataObjectExtensions
    {
        /// <summary>
        /// Returns whether a given DataColumn is a Primary Key of its database table.
        /// </summary>
        /// <param name="dataCol">A DataColumn from a DataTable</param>
        /// <returns>True if the DataColumn is a Primary Key of its database table, false if not.</returns>
        public static bool IsPrimaryKey(this DataColumn dataCol)
        {
            foreach (DataColumn tblCol in dataCol.Table.PrimaryKey)
            {
                if (tblCol == dataCol)
                    return true;
            }
            return false; // if no match found
        }

        /** <summary>AllowNullFields takes all the columns in the given DataTable, except for the
         * primary key(s), and removes the AllowDBNull=false restriction from each column, 
         * if that restriction exists.</summary>
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
         * we attempt to store the new record in the database. 
         * 
         * UPDATE 9/18/15: I added a way to record which columns were originally set as non-null (provided the table is an IEDataTable): 
         * the method EDataTable.SetOriginallyNonNullColumns(). Now, a field can be checked for a non-null restriction ("Required") by 
         * calling EDataTable.IsNonNullColumn(), which checks both DataColumn.AllowDBNull and EDataTable._OriginallyNonNullColumns,  as 
         * well as EColumn.Required.
         * 
         * Note: I considered moving this method to EDataTable on 9/18/15, but did not because it's still called for DataTables that are
         * not IEDataTables; also it would require changes to all the apps that call it.
         */
        public static void AllowNullFields(this DataTable dataTable)
        {
            List<DataColumn> originallyNonNullFields = new List<DataColumn>();
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
                    {
                        col.AllowDBNull = true;
                        originallyNonNullFields.Add(col);
                    }
                }
            }
        }

        /** <summary>MakeAutoIncrementKeySafe ensured that a DataTable with an AutoIncremented primary key will behave
         * safely if incremented in memory.</summary> 
         * MakeAutoIncrementKeySafe works around a design flaw in Microsoft .NET 2.0's Data Designer and 
         * its generation DataSet classes. Adding records to a table with an AutoIncremented primary key 
         * would throw an error: 
         *      ConstraintException was unhandled. Column <column_name> is constrained 
         *      to be unique. Value <numeric_value> is already present.
         * This would happen especially when
         * multiple users were adding to the same table, such as one of the EditLog tables. 
         * 
         * This site, http://davidhayden.com/blog/dave/archive/2006/07/17/ConstraintExceptionWasUnhandled.aspx,
         * explains it: "One might think SQL Server is returning this error, but it is actually the DataTable 
         * that throws the exception." When an app adds records to a table, it first adds records to the
         * DataTable object in memory. For an AutoIncremented primary key, the DataTable internal code 
         * generates a value based on the last value added to the SQL Server table, or the last value the
         * DataTable generated. In reality, that value is a guess, which has nothing necessarily to do 
         * with the actual value that SQL Server will generate. 
         *
         * David Hayden's blog goes on: "There are actually two SQL statements being sent to SQL Server. 
         * The first one inserts the record, the second one retrieves all the values in the newly inserted 
         * row to populate the [table]." When there's another user adding records to that same table, that 
         * second statement will retrive a record whose key value will probably be different than the 
         * key value generated by the DataTable. And when more than one new record is added, that newly 
         * retrieved key value could be the same as a key value the DataTable generated (i.e. guessed at)
         * for a record about to be added -- therefore it will throw a bogus "constrained to be unique"
         * error.
         * 
         * For example, suppose the app is adding three records to a table with an AutoIncremented primary 
         * key. The last key value generated by SQL Server -- or by the DataTable itself (yes, I've seen 
         * it increment values based on that!) is 99. So, the DataTable will generated the following key 
         * values for the records: 100, 101, 102. Meanwhile, another app adds a record to that same 
         * table -- SQL Server will generate the key value 100 for that record. Then, our app calls 
         * SqlDataAdapter.Update to insert the record in the database. It inserts the first record, which
         * replaces the key value 100 with the next available key value, 101. When the .NET client code reads 
         * that record back, it discovers that value, 101, already exists in the DataTable, in the second
         * record. So, it thinks that's a violation of primary key uniqueness, and it throws an error.
         * 
         * This is kind of a tough Microsoft bug to work around, since you can't just remove the Unique
         * constraint from the DataTable -- .NET will throw an error, since it's a primary key. But
         * David Hayden's blog gives a quirky solution, but one that works, and is the simplest solution
         * I've found: make the DataTable "increment" the key downward from -1, rather than upward from 1.
         * This way, the DataTable will never generate a key value that conflicts with an actual key 
         * value from the SQL Server table.
         * 
         * To deal with temporary keys being replaced with permanent keys in a foreign key situation, 
         * the ForeignKeyAddedRecordList class is recommended.
         */
        public static void MakeAutoIncrementKeySafe(this DataColumn column)
        {
            if (column.AutoIncrement && column.AutoIncrementStep > 0)
            {
                column.AutoIncrementSeed = -1;
                column.AutoIncrementStep = -1;
            }
        }

    }
}


