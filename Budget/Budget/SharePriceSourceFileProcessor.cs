using Budget.MainDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Budget.MainDataSet;

namespace Budget
{
    internal class SharePriceSourceFileProcessor : SourceFileProcessor
    {
        public SharePriceDataTable SharePriceTable => _SharePriceTable;
        SharePriceDataTable _SharePriceTable;

        SharePriceTableAdapter _SharePriceAdapter;

        protected override DataTable ImportedTable => SharePriceTable;

        public SharePriceSourceFileProcessor(SharePriceDataTable sharePriceTable, string selectedFileFormat, bool isManuallyEntered)
             : base(selectedFileFormat, isManuallyEntered)
        {
            _SharePriceAdapter = new SharePriceTableAdapter();
            _SharePriceAdapter.Connection = Program.DbConnection;
            _SharePriceTable = sharePriceTable;
        }

        protected override void ReadInSourceFile()
        {
            // Read in the full SharePrice table, to check for duplicates: - DIAG need to?
            _SharePriceAdapter.Fill(_SharePriceTable);
            //_MatchingSharePriceIDs.Clear();

            base.ReadInSourceFile();
        }

        string FundIDFromSymbol(string stockSymbol)
        {
            int ix = Program.LookupTableSet.FundViewBySymbol.Find(stockSymbol);
            if (ix != -1)
                return ((FundRow)Program.LookupTableSet.FundViewBySymbol[ix].Row).FundID;
            else
                return null;
        }

        protected override DataRow NewImportedRow()
        {
            return _SharePriceTable.NewSharePriceRow();
        }

        protected override bool ExtractFields(string[] fileFields, ColumnValueList fieldsByColumnName)
        {
            // TODO only from the one kind of file - make it work with historical list too!

            // Get Fund (from ticker symbol) from column 0:
            string fundID = FundIDFromSymbol(fileFields[0]);
            if (fundID != null)
                fieldsByColumnName["Fund"] = fundID;
            else
                return false;

            // Get share price from column 1:
            Decimal sharePrice;
            if (Decimal.TryParse(fileFields[1], out sharePrice))
                fieldsByColumnName["PricePerShare"] = sharePrice;
            else
                return false;

            // Get date of price from column 2:
            DateTime dateValue;
            if (DateTime.TryParse(fileFields[2], out dateValue))
                fieldsByColumnName["SPDate"] = dateValue.Date;
            else
                return false;

            return true;
        }

        protected override void PointSourceFileItemRowToImportedRow(ImportedAndSourceItemRows rowsOb)
        {
            // fill in new Items rows with updated, permanent key field values:
            rowsOb._ItemsRow.SharePriceFund = ((SharePriceRow)rowsOb._ImportedRow).Fund;
            rowsOb._ItemsRow.SharePriceDate = ((SharePriceRow)rowsOb._ImportedRow).SPDate;
        }

        protected override DataRow[] FindDuplicateRows(ColumnValueList fieldsByColumnName)
        {
            // TODO maybe a custom made DataView per format, depending on what fields it captures...or just linear search...
            SharePriceRow sharePriceRow = _SharePriceTable.FindBySPDateFund((DateTime)fieldsByColumnName["SPDate"], (string)fieldsByColumnName["Fund"]);
            if (sharePriceRow != null)
                return new DataRow[] { sharePriceRow };
            else
                return new DataRow[0];
        }

        protected override void SaveImportedTable()
        {
            _SharePriceAdapter.Update(_SharePriceTable);
        }
    }
}
