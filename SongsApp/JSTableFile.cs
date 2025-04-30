using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Songs
{
    class JSTableFile
    {
        public JSTableFile(DataTable tbl, IList< DataColumn > columns, string tableName) 
        {
            _DataTable = tbl;
            _Columns = columns;
            _TableName = tableName;
        }

        DataTable _DataTable;
        IList<DataColumn> _Columns; // NOTE: FIRST COLUMN IN _Columns MUST be primary key.
        string _TableName;

        public List<string> IntroComments => _IntroComments;
        List<string> _IntroComments = new List<string>();

        List<GroupingMap> _GroupingMaps = new List<GroupingMap>();

        public void AddGroupingMap(string mapName, DataColumn groupingColumn, IList<DataColumn> orderingColumns)
        {
            _GroupingMaps.Add(new GroupingMap(mapName, groupingColumn, orderingColumns));
        }

        void WriteGroupingMapItemLine(StreamWriter writer, string jsMapName, string groupingColumnValue, string rowIndexList)
        {
            writer.WriteLine("    " + jsMapName + ".set(" + groupingColumnValue + ", [" + rowIndexList + "]);");
        }

        public void WriteFile()
        {
            try
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.InitialDirectory = @"D:\Dietrich\SoftwareDevelopment\GitHubRepository\NetlifyWebsite\Tables"; // TODO should configure this
                sd.FileName = _TableName;
                sd.DefaultExt = "js";

                if (sd.ShowDialog() == DialogResult.OK)
                {
                    Stream stream;
                    if ((stream = sd.OpenFile()) != null)
                    {
                        StreamWriter writer = new StreamWriter(stream);

                        if (IntroComments != null && IntroComments.Count > 0)
                        {
                            foreach (string comment in IntroComments)
                                writer.WriteLine(@"// " + comment);
                            writer.WriteLine(@"");
                        }

                        writer.WriteLine(@"class " + _TableName + "Table");
                        writer.WriteLine(@"{");

                        // write out columns:
                        writer.WriteLine(@"  static Columns = ");
                        writer.WriteLine(@"    [");
                        for (int iCol = 0; iCol < _Columns.Count; iCol++)
                        {
                            DataColumn column = _Columns[iCol];
                            writer.WriteLine(@"      """ + column.ColumnName + @"""" + (iCol < _Columns.Count - 1 ? "," : ""));
                        }
                        writer.WriteLine(@"    ];");

                        // write out records, and track mapping of key to row index:
                        Dictionary<string, int> _KeyValueToRowIndex = new Dictionary<string, int>();
                        DataColumn primaryKeyColumn = _DataTable.PrimaryKey[0];

                        writer.WriteLine(@"  static Rows = ");
                        writer.WriteLine(@"    [");
                        for (int iRow = 0; iRow < _DataTable.Rows.Count; iRow++)
                        {
                            DataRow row = _DataTable.Rows[iRow];
                            string recLine = @"      [";
                            for (int iCol = 0; iCol < _Columns.Count; iCol++)
                            {
                                DataColumn recColumn = _Columns[iCol];
                                recLine += FieldValueToJavascript(row[recColumn]);
                                if (iCol < _Columns.Count - 1)
                                    recLine += ", ";
                            }
                            recLine += @"]";

                            // track mapping of key to row index: (NOTE: FIRST COLUMN IN _Columns MUST be primary key)
                            _KeyValueToRowIndex[FieldValueToJavascript(row[0])] = iRow;

                            if (iRow < _DataTable.Rows.Count - 1)
                                recLine += ",";
                            writer.WriteLine(recLine);
                        }
                        writer.WriteLine(@"    ];");
                        writer.WriteLine(@"");

                        writer.WriteLine(@"    static GetFieldValue(recArray, columnName)");
                        writer.WriteLine(@"    {");
                        writer.WriteLine(@"      // TODO check for validity so it dont crash!!!!");
                        writer.WriteLine(@"      const fieldIndex = this.ColumnMap.get(columnName);");
                        writer.WriteLine(@"      return recArray[fieldIndex];");
                        writer.WriteLine(@"    }");
                        writer.WriteLine(@"");

                        writer.WriteLine(@"  static");
                        writer.WriteLine(@"  {");
                        writer.WriteLine(@"    // create maps:");
                        writer.WriteLine(@"    this.ColumnMap = new Map();");
                        writer.WriteLine(@"    for (let i = 0; i < this.Columns.length; i++)");
                        writer.WriteLine(@"      this.ColumnMap.set(this.Columns[i], i);");
                        writer.WriteLine(@"");
                        writer.WriteLine(@"    this.RowMap = new Map();");
                        writer.WriteLine(@"    for (let i = 0; i < this.Rows.length; i++)");
                        writer.WriteLine(@"      this.RowMap.set(this.Rows[i][0], this.Rows[i]);");
                        writer.WriteLine(@"");

                        // Create any GroupingMaps defined:
                        if (_GroupingMaps.Count > 0)
                            writer.WriteLine(@"    // Create Grouping Maps:");

                        foreach (GroupingMap groupingMap in _GroupingMaps)
                        {
                            // Create a list of sort fields: DIAG will need a DESC fields too right?
                            string viewSortFields = groupingMap._GroupingColumn.ColumnName + " ASC";
                            foreach (DataColumn col in groupingMap._OrderingColumns)
                                viewSortFields += ", " + col.ColumnName + " ASC";

                            // Create a DataView with those sort fields:
                            DataView dataView = new DataView(_DataTable);
                            dataView.Sort = viewSortFields;

                            // Go through all the rows in the DataView, and write out a 2-level array, breaking on change of _GroupingColumn: 
                            string JSMapName = "this." + groupingMap._MapName + "_GroupingMap";
                            writer.WriteLine(@"    " + JSMapName +" = new Map();");
                            string lastGroupingColumnValue = null;
                            string rowIndexList = "";
                            foreach (DataRowView rowView in dataView)
                            {
                                string groupingColValue = FieldValueToJavascript(rowView.Row[groupingMap._GroupingColumn]);
                                if (groupingColValue != lastGroupingColumnValue)
                                {
                                    if (lastGroupingColumnValue != null) // if it's not on the very first field
                                    {
                                        WriteGroupingMapItemLine(writer, JSMapName, lastGroupingColumnValue, rowIndexList);
                                        rowIndexList = "";
                                    }
                                    lastGroupingColumnValue = groupingColValue;
                                }

                                // Determine the JS row index of this row, by primary key field value:
                                string primaryKeyValue = FieldValueToJavascript(rowView.Row[primaryKeyColumn]);
                                int rowIndex = _KeyValueToRowIndex[primaryKeyValue];

                                // Add row index to rowIndexList:
                                if (rowIndexList != "")
                                    rowIndexList += ", ";
                                rowIndexList += "\"" + rowIndex.ToString() + "\"";
                            }
                            // Write the last line out:
                            WriteGroupingMapItemLine(writer, JSMapName, lastGroupingColumnValue, rowIndexList);
                            writer.WriteLine(@"");
                        }

                        writer.WriteLine(@"  }");
                        writer.WriteLine(@"}");

                        // row class:
                        writer.WriteLine(@"");
                        writer.WriteLine(@"class " + _TableName + "Row");
                        writer.WriteLine(@"{");
                        writer.WriteLine(@"  constructor(searchParam, searchType)");
                        writer.WriteLine(@"  {");
                        writer.WriteLine(@"  	if (searchType == ""I"")");
                        writer.WriteLine(@"  	  this.FieldValues = " + _TableName + @"Table.Rows[searchParam];");
                        writer.WriteLine(@"  	else if (searchType == ""K"")");
                        writer.WriteLine(@"      this.FieldValues = " + _TableName + @"Table.RowMap.get(searchParam);");
                        writer.WriteLine(@"  }");
                        writer.WriteLine(@"");
                        for (int iCol = 0; iCol < _Columns.Count; iCol++)
                            writer.WriteLine(@"  get " + _Columns[iCol] + @"() { return this.FieldValues[" + iCol.ToString() + @"]; }");
                        writer.WriteLine(@"}");
                        writer.Close();

                        stream.Close();
                    }
                }
                MessageBox.Show(_TableName + " module has been written.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("WriteJSTableFile failed:" + ex.Message);
            }
        }

        static string FieldValueToJavascript(object fieldVal)
        {
            string valStr = fieldVal.ToString(); // TODO gotta account for null, and for string formatting of odd types?
            valStr = valStr.Replace(@"\", @"\\"); // escape \ with \\
            valStr = valStr.Replace(@"""", @"\"""); // escape " with \"
            return "\"" + valStr + "\"";
        }

        class GroupingMap
        {
            public string _MapName;
            public DataColumn _GroupingColumn;
            public IList<DataColumn> _OrderingColumns;

            public GroupingMap(string mapName, DataColumn groupingColumn, IList<DataColumn> orderingColumns)
            {
                _MapName = mapName;
                _GroupingColumn = groupingColumn;
                _OrderingColumns = orderingColumns;
            }
        }
    }
}
