using Budget.MainDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Budget.MainDataSet;

namespace Budget
{
    internal class SharePriceSourceFileProcessor : SourceFileProcessor
    {
        public SharePriceDataTable SharePriceTable => _SharePriceTable;
        SharePriceDataTable _SharePriceTable;

        SharePriceTableAdapter _SharePriceAdapter;

        string _FundID;

        protected override DataTable ImportedTable => SharePriceTable;

        // fundID can be null
        public SharePriceSourceFileProcessor(SharePriceDataTable sharePriceTable, string fundID, string selectedFileFormat, bool isManuallyEntered)
             : base(selectedFileFormat, isManuallyEntered)
        {
            _SharePriceTable = sharePriceTable;
            _FundID = fundID;

            _SharePriceAdapter = new SharePriceTableAdapter();
            _SharePriceAdapter.Connection = Program.DbConnection;
        }

        /* DIAG no need, remove
        public override void ProcessManualLines(string[] textLines)
        {
            switch (_SourceFileFormat)
            {
                case SourceFileFormats.YahooHistoricalData:
                    ProcessYahooHistoricalDataLines(textLines);
                    break;
                default:
                    MessageBox.Show("Cannot process text if source file format of account is " + _SourceFileFormat.ToString());
                    break;
            }
        }
        */

        protected override void FillImportedTable()
        {
            _SharePriceAdapter.Fill(_SharePriceTable);
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
            switch (_SourceFileFormat)
            {
                case SourceFileFormats.YahooHoldings:
                    return ExtractFields_YahooHoldings(fileFields, fieldsByColumnName);
                case SourceFileFormats.YahooHistoricalData:
                    return ExtractFields_YahooHistoricalData(fileFields, fieldsByColumnName);
                case SourceFileFormats.DateShareBalance:
                    return ExtractFields_DateShareBalance(fileFields, fieldsByColumnName);
                default:
                    return false;
            }
        }

        // TODO this and the following ExtractFields_DateShareBalance can be normalized
        bool ExtractFields_YahooHoldings(string[] fileFields, ColumnValueList fieldsByColumnName)
        { 
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

        bool ExtractFields_DateShareBalance(string[] fileFields, ColumnValueList fieldsByColumnName)
        {
            // Fund is from the Processor:
            fieldsByColumnName["Fund"] = _FundID;

            // Get date of price from column 0:
            DateTime dateValue;
            if (fileFields.Length > 0 && DateTime.TryParse(fileFields[0], out dateValue))
                fieldsByColumnName["SPDate"] = dateValue.Date;
            else
                return false;

            // Get share price (closing) from column 1:
            Decimal sharePrice;
            if (fileFields.Length > 4 && Decimal.TryParse(fileFields[1], out sharePrice))
                fieldsByColumnName["PricePerShare"] = sharePrice;
            else
                return false;

            return true;
        }

        bool ExtractFields_YahooHistoricalData (string[] fileFields, ColumnValueList fieldsByColumnName)
        {
            // Fund is from the Processor:
            fieldsByColumnName["Fund"] = _FundID;

            // Get date of price from column 0:
            DateTime dateValue;
            if (fileFields.Length > 0 && DateTime.TryParse(fileFields[0], out dateValue))
                fieldsByColumnName["SPDate"] = dateValue.Date;
            else
                return false;

            // Get share price (closing) from column 4:
            Decimal sharePrice;
            if (fileFields.Length > 4 && Decimal.TryParse(fileFields[4], out sharePrice))
                fieldsByColumnName["PricePerShare"] = sharePrice;
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
