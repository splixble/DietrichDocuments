using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    internal class DBRowsList // DIAG rename maybe - if I even use it? For tables that could have multiple primary keys
    {
        DataTable _Table;

        public DBRowsList(DataTable table)
        {
            _Table = table;
        }

        public DataRow FindRowByKey(ColumnValueList colValueList)
        {
            // returns null if no matching row exists
            // make an array of primary key values: 
            object[] primaryKeyValues = new object[_Table.PrimaryKey.Length];
            for (int n = 0; n < _Table.PrimaryKey.Length; n++)
                primaryKeyValues[n] = colValueList[_Table.PrimaryKey[n].ColumnName];

            return _Table.Rows.Find(primaryKeyValues);
        }
    }

    internal class ColumnValueList : Dictionary<string, object>
    {

    }

    // DIAG give this and others their own modules, in library

    public class PrimaryKeyValue : IComparable
    {
        IComparable[] _Values;
        public IComparable[] Values => _Values;

        DataRow _DataRow;

        public PrimaryKeyValue(DataRow dataRow)
        {
            _DataRow = dataRow;
            
            // copy the primary key value(s) from this DataRow to the element(s) of the _Values array:
            _Values = new IComparable[_DataRow.Table.PrimaryKey.Length];
            for (int i = 0; i < _Values.Length; i++)
                _Values[i] = _DataRow[_DataRow.Table.PrimaryKey[i]] as IComparable;
        }

        public int CompareTo(object obj) // for IComparable interface
        {
            for (int i = 0; i < _Values.Length; i++)
            { 
                int elementComp = _Values[i].CompareTo(((PrimaryKeyValue)obj).Values[i]);
                if (elementComp != 0)
                    return elementComp;
            }
            return 0; // if it got to here
        }

        public override bool Equals(object obj)
        {
            return this.CompareTo(obj) == 0;
        }

        public override int GetHashCode()
        {
            return _Values.GetHashCode();
        }

        public override string ToString()
        {
            string str = "";
            foreach (IComparable val  in _Values) 
            {
                if (str != "")
                    str += "/";
                str += val.ToString();
            }
            return str;
        }

        public string SQLFilterExpression
        {
            get 
            {
                string expr = "";
                for (int i = 0; i < _Values.Length; i++)
                {
                    string comparison = _DataRow.Table.PrimaryKey[i].ColumnName + " = " + TypeLib.DBUtils.SQLServerQueryConstant(_Values[i]);
                    if (expr != "")
                        expr += " AND ";
                    expr += comparison;
                }
                return expr;
            }
        }
    }

    /* needed?
    public static class DataObjectExtensions
    {
        public static PrimaryKeyValue PrimaryKeyValue(this DataRow dataRow)
        {
            return new PrimaryKeyValue(dataRow);
        }
    }
    */
}
