using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.Drawing.Printing;

namespace PrintLib
{
    /** <summary>  GridPrintManager provides the ability to print a Grid object, splitting
      * it among multiple pages if necessary. </summary>A GridPrintManager is attached 
      * to a single Grid, even if there are more than one Grids
      * on the form.
      */
    public class GridPrintManager
    {
        // DIAG Move this to sep project/solution if it works
        ///<summary> Grid is the Grid to print</summary>
        public DataGridView Grid { get { return m_Grid; } }
        DataGridView m_Grid;

        public Decimal ScaleFactor = 1; // can be set by caller

        // events:
        public delegate void PrintingCellHandler(GridPrintManager sender,
            GridPrintingCellEventArgs args);
        public event PrintingCellHandler PrintingCell;

        protected const int PaddingAboveGrid = 8;
        const int cellTextPadding = 1;
        static int doubleCellTextPadding = cellTextPadding * 2;

        const int gridHeadingHeight = 20; // vertical space to allocate for heading above grid 
        const int gridSeparation = 20; // separation between grids if there's >1 on same page

        // Properties for how much space to allow for row and/or column headers:
        int ColumnHeaderHeight { get { return ScaleRowHeight(Grid.ColumnHeadersHeight); } }
        int RowHeaderWidth
        {
            get
            {
                if (Grid.RowHeadersVisible)
                    return ScaleColumnWidth(Grid.RowHeadersWidth);
                else
                    return 0;
            }
        }

        protected Pen thickBorderPen;
        protected Pen borderPen;
        protected Pen cellBorderPen;

        public GridPrintManager(Grid grid)
        {
            m_Grid = grid;

            thickBorderPen = PenCache.GetPen(Color.Black, 2);
            borderPen = PenCache.GetPen(Color.Black, 1);
            cellBorderPen = PenCache.GetPen(Color.Gray, 1);
        }

        /* PrintThisRow and PrintThisColumn are overrideable functions with which
         a derived class can tell the document not to print certain rows and/or 
         certain columns. 
         
         * ```They should maybe be events instead...
         */

        public virtual bool PrintThisRow(int rowIndex)
        {
            if (m_Grid.IsADataRow(rowIndex) &&
                Grid.RowSelectionCheckboxManager != null &&
                Grid.RowSelectionCheckboxManager.ForPrinting)
                return Grid.RowSelectionCheckboxManager.IsRowSelected(rowIndex);
            else
                return true;
        }

        protected virtual bool PrintThisColumn(int columnDisplayIndex)
        {
            int columnIndex = Grid.ColumnsInDisplayOrder[columnDisplayIndex].Index;
            if (Grid.RowSelectionCheckboxManager != null &&
                Grid.RowSelectionCheckboxManager.ForPrinting)
            {
                return (columnIndex != Grid.RowSelectionCheckboxManager.ColumnIndex);
            }
            else
                return true;
        }

        /** <summary>  SetUpCoords ...</summary>
         * Parameters: 
         * - printDoc: the GridPrintDocument used in this print command
         * - graphics: Graphics object to print the grid to
         * - parentOffset: position of the DataGridView's parent relative to the form, 
         *   to use as an offset to get the DataGridView's position relative to the form
         */
        GridPrintBookmark SetUpCoords(PBIPrintDocument printDoc, Graphics graphics, Point parentOffset)
        {
            // Initialize things:
            int nFirstDisplayColumnIndexToPrint; // DISPLAY column index, not Grid.Columns column index
            int nLastDisplayColumnIndexToPrint; // DISPLAY column index, not Grid.Columns column index
            int nFirstRowToPrint = 0;
            int nLastRowToPrint = Grid.RowCount - 1;

            DataGridViewColumn[] columnsInDisplayOrder = Grid.ColumnsInDisplayOrder;

            int firstDisplayColumnIndexOnEachPage = -1; // DISPLAY column index, not Grid.Columns column index
            /* If we can't fit all columns on one page, this is the index of the first 
             * printable frozen column (-1 if there is none), which is the first column 
             * we print on every page, to tie the whole multi-page grid together.
             * */

            if (printDoc.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                /* Go through the selected columns and find the indices of the first and last ones.
                 * (Note that if selected columns are non-contiguous, it will scrunch them 
                 * together into one big contiguous list -- including non-selected columns if 
                 * they're between selected columns.)
                 * */
                nFirstDisplayColumnIndexToPrint = Grid.ColumnCount - 1;
                nLastDisplayColumnIndexToPrint = 0;
                foreach (DataGridViewColumn col in Grid.SelectedColumns)
                {
                    if (col.DisplayIndex < nFirstDisplayColumnIndexToPrint)
                        nFirstDisplayColumnIndexToPrint = col.DisplayIndex;
                    if (col.DisplayIndex > nLastDisplayColumnIndexToPrint)
                        nLastDisplayColumnIndexToPrint = col.DisplayIndex;
                }
            }
            else
            {
                nFirstDisplayColumnIndexToPrint = 0;
                nLastDisplayColumnIndexToPrint = Grid.ColumnCount - 1;
            }

            // Find first column to print on every page: 
            for (int nDisplayCol = nFirstDisplayColumnIndexToPrint; nDisplayCol <= nLastDisplayColumnIndexToPrint; nDisplayCol++)
            {
                if (PrintThisColumn(nDisplayCol))
                {
                    if (firstDisplayColumnIndexOnEachPage == -1 // if not already set
                        && columnsInDisplayOrder[nDisplayCol].Frozen)
                        firstDisplayColumnIndexOnEachPage = nDisplayCol;
                }
            }

            // Calculate space available for the grid, given the page space 
            // constraints:
            int leftGridPos = printDoc.MarginBounds.Left; // must skooch grid all the way to left for maximum horiz. room
            int rightmostGridPos = printDoc.MarginBounds.Right;
            int lowestGridPos = printDoc.MarginBounds.Bottom - PBIPrintDocument.m_FooterHeight;

            int gridRow; // index var used in loops
            int gridColDisplayIndex; // index var used in loops

            // Calculate row heights - they must be tall enough to hold the tallest cells as drawn:
            Dictionary<int, int> rowHeights = new Dictionary<int, int>();
            for (gridRow = nFirstRowToPrint; gridRow <= nLastRowToPrint; gridRow++)
            {
                int rowHeight = 15; // start with minumum
                for (int nDispCol = nFirstDisplayColumnIndexToPrint; nDispCol <= nLastDisplayColumnIndexToPrint; nDispCol++)
                {
                    if (PrintThisColumn(nDispCol))
                    {
                        int nCol = columnsInDisplayOrder[nDispCol].Index;
                        int cellHeight = MeasureCellHeight(printDoc, gridRow, nCol, graphics);
                        if (rowHeight < cellHeight)
                            rowHeight = cellHeight;
                    }
                }
                rowHeights[gridRow] = rowHeight;
            }

            // The first group of rows need to printed under anything else already printed on that page.
            // (Note that this does not necessarily mean the rows printed on the first page; 
            // if not all columns fit on one page, it could be the 2nd or 3rd page, but in 
            // that case we need to make sure the rows line up with the rows on the 1st page.)
            // Later, maxGridWidth is reset to the top of the page.
            int topGridPos_FirstPage;
            int highestPrintablePosition = printDoc.LowestPrintingOnPage + PaddingAboveGrid;
            topGridPos_FirstPage = parentOffset.Y + Grid.Location.Y + gridHeadingHeight;
            if (topGridPos_FirstPage < highestPrintablePosition)
                topGridPos_FirstPage = highestPrintablePosition;
            int topGridPos_RestOfPages = printDoc.TopOfContentPrintableArea + PaddingAboveGrid;

            // Go through each column and figure out how to group them so they'll all fit across each page:
            List<ColumnCoords> columnsPerPage = new List<ColumnCoords>();
            ColumnCoords currentPageColumns = null;
            gridColDisplayIndex = nFirstDisplayColumnIndexToPrint;
            int colPos = RowHeaderWidth; // column position relative to left edge of grid
            int maxGridWidth = rightmostGridPos - leftGridPos;

            // go through all columns:
            while (true)
            {
                if (currentPageColumns == null)
                {
                    // need to create a new set of columns, for another page:
                    currentPageColumns = new ColumnCoords();
                    columnsPerPage.Add(currentPageColumns);
                    colPos = RowHeaderWidth;
                    //`` was: colPos = leftGridPos + nRowHeaderWidth;
                    //``currentPageColumns.ColumnPositions.Add(colPos);

                    // add firstDisplayColumnIndexOnEachPage, if there is one, to columns to print:
                    if (firstDisplayColumnIndexOnEachPage != -1)
                    {
                        colPos += ScaleColumnWidth(columnsInDisplayOrder[firstDisplayColumnIndexOnEachPage].Width);
                        currentPageColumns.ColumnRightPositions.Add(colPos);
                        int firstColIndex = columnsInDisplayOrder[firstDisplayColumnIndexOnEachPage].Index;
                        currentPageColumns.ColumnsToPrint.Add(firstColIndex);
                    }
                }

                // now, add the next column in the list, if there's room:
                if (gridColDisplayIndex != firstDisplayColumnIndexOnEachPage && // already printed 1st column above
                    PrintThisColumn(gridColDisplayIndex))
                {
                    colPos += ScaleColumnWidth(columnsInDisplayOrder[gridColDisplayIndex].Width);
                    if (colPos > maxGridWidth && currentPageColumns.Count > 0)
                    // - check Count of currentPageColumns to ensure that we add at least one column 
                    // no matter what, so we don't get into an endless loop
                    {
                        // ran out of horizontal room for the column
                        currentPageColumns = null; // to indicate a new one needs to be created
                    }
                    else
                    {
                        // add to lists of columns:
                        currentPageColumns.ColumnRightPositions.Add(colPos);
                        int gridColIndex = columnsInDisplayOrder[gridColDisplayIndex].Index;
                        currentPageColumns.ColumnsToPrint.Add(gridColIndex);

                        // increment counter:
                        gridColDisplayIndex++;
                    }
                }
                else
                {
                    // increment counter:
                    gridColDisplayIndex++;
                }
                if (gridColDisplayIndex > nLastDisplayColumnIndexToPrint)
                    break; // break out of the loop if we are done
            }

            // Go through each row and figure out how to group them so they'll all fit in each page:
            List<RowCoords> rowsPerPage = new List<RowCoords>();
            RowCoords currentPageRows = null;
            gridRow = nFirstRowToPrint;
            int rowPos = ColumnHeaderHeight; // row position relative to top edge of grid
            int maxGridHeight = lowestGridPos - topGridPos_FirstPage;

            // go through all rows:
            while (true)
            {
                if (currentPageRows == null)
                {
                    // need to create a new set of rows, for another page:
                    currentPageRows = new RowCoords();
                    rowsPerPage.Add(currentPageRows);
                    rowPos = ColumnHeaderHeight;
                }

                // now, add the next row in the list, if there's room:
                if (gridRow <= nLastRowToPrint && PrintThisRow(gridRow))
                {
                    rowPos += rowHeights[gridRow];
                    if (rowPos > maxGridHeight && currentPageRows.Count > 0)
                    // - check Count of currentPageRows to ensure that we add at least one row 
                    // no matter what, so we don't get into an endless loop
                    {
                        // ran out of horizontal room for the row
                        currentPageRows = null; // to indicate a new one needs to be created

                        // now that the initial value of maxGridHeight has been used, 
                        // reset it to take up almost the whole page, for subsequent pages:
                        maxGridHeight = lowestGridPos - topGridPos_RestOfPages;
                    }
                    else
                    {
                        // add to lists of rows:
                        currentPageRows.RowBottomPositions.Add(rowPos);
                        currentPageRows.RowsToPrint.Add(gridRow);

                        // increment counter:
                        gridRow++;
                    }
                }
                else
                {
                    // increment counter:
                    gridRow++;
                }
                if (gridRow > nLastRowToPrint)
                    break; // break out of the loop if we are done
            }

            // Create a GridPrintBookmark that will hold the pages list, and be passed back:
            GridPrintBookmark bookmark = new GridPrintBookmark(printDoc, this);

            // Now, create a list of PageCoords objects for the pages that'll contain these grid fragments:
            PageCoords currentPage = null;
            int topOfNextGrid = 0;
            for (int rowSetIndex = 0; rowSetIndex < rowsPerPage.Count; rowSetIndex++)
            {
                RowCoords rowSet = rowsPerPage[rowSetIndex];
                int firstGridTop;
                if (rowSetIndex == 0)
                    firstGridTop = topGridPos_FirstPage;
                else
                    firstGridTop = topGridPos_RestOfPages;
                for (int columnSetIndex = 0; columnSetIndex < columnsPerPage.Count; columnSetIndex++)
                {
                    ColumnCoords columnSet = columnsPerPage[columnSetIndex];

                    // create a grid coords object with this row set and column set:
                    GridCoords gridCoords = new GridCoords(rowSet, columnSet, RowHeaderWidth, ColumnHeaderHeight);

                    // Will this grid fragment fit in current (previously added) page (along with the 
                    // grid(s) already added to the page? (This is the case where we can fit all rows 
                    // on one page, but not all columns.)
                    bool gridFitsOnCurrentPage = false;
                    if (currentPage != null) // is there a current page at all? 
                    {
                        if (topOfNextGrid + gridCoords.GridHeight <= lowestGridPos)
                            gridFitsOnCurrentPage = true;
                    }

                    if (!gridFitsOnCurrentPage)
                    {
                        // create a new page, add it to the page list:
                        currentPage = new PageCoords();
                        bookmark.PageCoordsList.Add(currentPage);

                        // reset gridTop:
                        topOfNextGrid = firstGridTop;
                    }

                    // Add grid to current page:
                    currentPage.Grids.Add(gridCoords);

                    // Set grid's position (in current page):
                    gridCoords.Left = leftGridPos;
                    gridCoords.Top = topOfNextGrid;

                    // Advance gridTop, to find vert. position next grid would occupy on current page:
                    topOfNextGrid += gridCoords.GridHeight + gridSeparation;
                }
            }
            return bookmark;
        }

        /// <summary>
        /// Prints the Grid, or does a pre-print "dry run" of it, or of the portion of the Grid that fits on this particular page.
        /// </summary>
        /// <param name="printDoc">The GridPrintDocument used in this print command</param>
        /// <param name="graphics">Graphics object to print the grid to</param>
        /// <param name="parentOffset">Position of the DataGridView's parent relative to the form, to use as an offset to get the DataGridView's position relative to the form</param>
        /// <param name="printPage">True if called from the actual printing, false if called during the "dry run" to calculate number of pages.</param>
        /// <param name="bookmark">Ref parameter which specifies, after the call, if the grid was completely printed, and if not, where  it "left off"; i.e., the next row and column 
        /// it needs to print when it calls PrintGrid for the next page</param>
        /// <param name="printedAnything">Output paramater which returns whether any part of the grid was able to be printed</param>
        /// <returns>True if it was able to finish printing the grid, false if not</returns>
        public bool PrintGrid(PBIPrintDocument printDoc, Graphics graphics, Point parentOffset, bool printPage, ref PrintBookmark bookmark, out bool printedAnything)
        {
            // If bookmark was not passed in as a parameter (i.e. when called for first page of 
            // grid printing), use SetUpCoords to create one: 
            if (bookmark == null)
                bookmark = SetUpCoords(printDoc, graphics, parentOffset);
            GridPrintBookmark gridBookmark = (GridPrintBookmark)bookmark;

            // We can confidently say that printedAnything = true, since SetUpCoords() has code (commented with "check Count of currentPageColumns to ensure that we add at least one column 
            // no matter what, so we don't get into an endless loop") that ensures we don't go thru page after page without anything that will fit to print:
            printedAnything = true;

            if (printPage && bookmark.PrintingIncomplete)
            {
                PrintGridPage(printDoc, graphics, gridBookmark);
            }

            // set lowest printing on page, so other stuff can be printed beneath grid:
            printDoc.LowestPrintingOnPage = gridBookmark.CurrentPageCoords.LowestPrintingOnPage;

            // advance page index:
            gridBookmark.CurrentPageIndex++;

            return !gridBookmark.PrintingIncomplete;
        }

        void PrintGridPage(PBIPrintDocument printDoc, Graphics graphics, GridPrintBookmark gridBookmark)
        {
            for (int gridIndex = 0; gridIndex < gridBookmark.CurrentPageCoords.Grids.Count; gridIndex++)
            {
                GridCoords gridCoords = gridBookmark.CurrentPageCoords.Grids[gridIndex];
                int leftGridPos = gridCoords.Left;
                int rightGridPos = gridCoords.Left + gridCoords.GridWidth;
                int topGridPos = gridCoords.Top;
                int bottomGridPos = gridCoords.Top + gridCoords.GridHeight;

                bool onFirstPage = (gridBookmark.CurrentPageIndex == 0);
                bool firstGridOnPage = (gridIndex == 0);

                // Create the rectangle for the border of the grid:
                Rectangle gridBounds = new Rectangle(leftGridPos, topGridPos,
                    rightGridPos - leftGridPos, bottomGridPos - topGridPos);

                if (onFirstPage && firstGridOnPage)
                {
                    // Draw heading just above grid:
                    Rectangle headingRect = new Rectangle(gridBounds.X, gridBounds.Y - gridHeadingHeight,
                        gridBounds.Width, gridHeadingHeight);
                    StringFormat headingFormat = new StringFormat();
                    headingFormat.Alignment = StringAlignment.Far;
                    headingFormat.LineAlignment = StringAlignment.Far;
                    graphics.DrawString(Grid.GridPrintHeading,
                        printDoc.MainPrintFont, printDoc.BrushBlack, headingRect, headingFormat);
                }

                // We need to make sure to print each banner in the grid only once. So, create a dictionary of grids
                // already printed, so we don't re-print a banner when we're handling another cell that's in the banner:
                Dictionary<GridBanner, object> bannersPrinted = new Dictionary<GridBanner, object>();

                // Draw cells:
                for (int nr = 0; nr < gridCoords.RowCoords.Count; nr++)
                {
                    int nRowNum = gridCoords.RowCoords.RowsToPrint[nr];
                    int nCellTop = gridCoords.RowTop(nr);
                    int nCellHeight = gridCoords.RowBottom(nr) - nCellTop;
                    for (int nc = 0; nc < gridCoords.ColumnCoords.Count; nc++)
                    {
                        int nColNum = gridCoords.ColumnCoords.ColumnsToPrint[nc];
                        if (Grid.Banners.ContainsCell(nRowNum, nColNum))
                        {
                            // Print banner in one long cell -- otherwise the banners will have repeated letters and 
                            // fragments of letters in the vertical separaters between cells, because of text rendering
                            // differences between displayed font and printed font: 
                            GridBanner banner = Grid.Banners.GetBannerContainingCell(nRowNum, nColNum);

                            // Print banner only if it's not already been printed:
                            if (!bannersPrinted.ContainsKey(banner))
                            {
                                PrintBanner(banner, nColNum, gridCoords, nCellTop, nCellHeight, graphics);
                                bannersPrinted.Add(banner, null); // note that this banner's been printed
                            }
                        }
                        else
                        {
                            int nCellLeft = gridCoords.ColumnLeft(nc);
                            int nCellWidth = gridCoords.ColumnRight(nc) - nCellLeft;
                            PrintCell(printDoc, nRowNum, nColNum,
                                new Rectangle(nCellLeft, nCellTop, nCellWidth, nCellHeight),
                                graphics);
                        }
                    }
                }

                // Draw box border around the while thing:
                graphics.DrawRectangle(borderPen, gridBounds);

                // Draw rows of column headers and row headers: 
                graphics.DrawLine(borderPen, leftGridPos,
                    topGridPos + ColumnHeaderHeight, rightGridPos, topGridPos + ColumnHeaderHeight);
                if (RowHeaderWidth > 0)
                    graphics.DrawLine(borderPen, leftGridPos + RowHeaderWidth,
                        topGridPos, leftGridPos + RowHeaderWidth, bottomGridPos);
                for (int nc = 0; nc < gridCoords.ColumnCoords.Count; nc++)
                {
                    int nColNum = gridCoords.ColumnCoords.ColumnsToPrint[nc];
                    int nColumnLeft = gridCoords.ColumnLeft(nc);
                    int nColumnRight = gridCoords.ColumnRight(nc);
                    int nColumnWidth = nColumnRight - nColumnLeft;
                    DataGridViewColumn col = Grid.Columns[nColNum];

                    Rectangle columnHeaderBounds = new Rectangle(nColumnLeft + cellTextPadding,
                        (int)topGridPos + cellTextPadding, nColumnWidth - doubleCellTextPadding,
                        ColumnHeaderHeight - doubleCellTextPadding);

                    // Fill in column header cell:
                    SolidBrush br = BrushCache.GetBrush(PBIColor.LighterSage);
                    graphics.FillRectangle(br, columnHeaderBounds);

                    // Paint column header image, if applicable:
                    Grid.PaintColumnHeaderImage(columnHeaderBounds,
                        nColNum, graphics, true);

                    // Write column heading:
                    Grid.PaintColumnHeaderText(columnHeaderBounds,
                        nColNum, graphics, ScaleFactor, true);

                    // Draw right border of column heading:
                    graphics.DrawLine(borderPen, nColumnRight, topGridPos,
                        nColumnRight, topGridPos + ColumnHeaderHeight);
                }

                // Print row headers:
                StringFormat rowHeaderStringFormat = new StringFormat();
                rowHeaderStringFormat.LineAlignment = StringAlignment.Center;
                rowHeaderStringFormat.Alignment = StringAlignment.Center;
                for (int nr = 0; nr < gridCoords.RowCoords.Count; nr++)
                {
                    int nRowNum = gridCoords.RowCoords.RowsToPrint[nr];
                    int nRowTop = gridCoords.RowTop(nr);
                    int nRowBottom = gridCoords.RowBottom(nr);
                    int nRowHeight = nRowBottom - nRowTop;

                    if (Grid.RowHeadersVisible)
                    {
                        // Write row heading:
                        string headerCellText = null;
                        if (Grid.Rows[nRowNum].HeaderCell.Value != null)
                            headerCellText = (string)Grid.Rows[nRowNum].HeaderCell.Value;
                        else
                        {
                            if (Grid.ShowRowNumberInRowHeadings)
                                if (nRowNum >= Grid.DisplayedRowNumberOffset)
                                    headerCellText = (nRowNum + 1 - Grid.DisplayedRowNumberOffset).ToString();
                        }
                        if (headerCellText != null)
                        {
                            graphics.DrawString(headerCellText,
                                ScaleFont(Grid.RowHeadersDefaultCellStyle.Font), Brushes.Black,
                                new Rectangle((int)leftGridPos + cellTextPadding, nRowTop + cellTextPadding,
                                RowHeaderWidth - doubleCellTextPadding, nRowHeight - doubleCellTextPadding),
                                rowHeaderStringFormat);
                        }
                        // Draw bottom border of row heading:
                        graphics.DrawLine(borderPen, leftGridPos, nRowBottom,
                            leftGridPos + RowHeaderWidth, nRowBottom);
                    }
                }
            }
        }

        void PrintBanner(GridBanner banner, int colNum, GridCoords gridCoords,
            int rowTop, int rowHeight, Graphics graphicsOb)
        {
            // Find leftmost and rightmost grid columns in the banner, that are printed on this page:
            int nLeftmostBannerColumnIndex = -1;
            int nRightmostBannerColumnIndex = -1;
            for (int nc = 0; nc < gridCoords.ColumnCoords.ColumnsToPrint.Count; nc++)
            //```was: foreach (int nCol in gridCoords.ColumnCoords.ColumnsToPrint)
            {
                int nCol = gridCoords.ColumnCoords.ColumnsToPrint[nc];
                if (nCol >= banner.FirstColumnDisplayIndex && nLeftmostBannerColumnIndex == -1) // only set leftmost once
                    nLeftmostBannerColumnIndex = nc;

                if (nCol <= banner.LastColumnDisplayIndex) // reset as longe as there's a greater column index
                    nRightmostBannerColumnIndex = nc;
            }
            if (nLeftmostBannerColumnIndex == -1 || nRightmostBannerColumnIndex == -1) // one or both has not been set -- should never happen
                return;

            // Get the dimensions of the rectangle on the page (composed of one or more cells in a row) 
            // to print the banner onto:
            Rectangle bannerBounds = new Rectangle();
            bannerBounds.X = gridCoords.ColumnLeft(nLeftmostBannerColumnIndex);
            bannerBounds.Width = gridCoords.ColumnRight(nRightmostBannerColumnIndex) - bannerBounds.X;
            bannerBounds.Y = rowTop;
            bannerBounds.Height = rowHeight;

            banner.PaintCell(banner.LastColumnDisplayIndex, bannerBounds, graphicsOb, true);
        }

        public virtual int MeasureCellHeight(PBIPrintDocument printDoc,
            int nRowIndex, int nColumnIndex, Graphics graphicsOb)
        {
            // Unless it's a text cell that wraps, the height is the same height as the cell: 
            DataGridViewCell cell = Grid[nColumnIndex, nRowIndex];

            if (!Grid.Banners.ContainsCell(nRowIndex, nColumnIndex) &&
                cell is DataGridViewTextBoxCell &&
                cell.InheritedStyle.WrapMode == DataGridViewTriState.True)
            {
                string cellsString = cell.FormattedValue.ToString();
                StringFormat strFormat = new StringFormat();
                strFormat.Trimming = StringTrimming.Word;
                Grid.CellAlignmentToStringFormatAlignment(
                    cell.InheritedStyle.Alignment, strFormat);

                // The text will be padded a bit, to simulate how the text in the cell on the screen 
                // is slightly padded:
                int cellWidth = ScaleColumnWidth(cell.Size.Width) - doubleCellTextPadding;
                SizeF stringSize = graphicsOb.MeasureString(cellsString, ScaleFont(cell.InheritedStyle.Font),
                    cellWidth, strFormat);

                return (int)Math.Ceiling(stringSize.Height) + doubleCellTextPadding;
            }
            else
            {
                return ScaleRowHeight(cell.Size.Height);
            }
        }

        /// <summary>
        /// Prints a single Grid cell on the page, in its proper page-relative position.
        /// </summary>
        /// <param name="printDoc">The GridPrintDocument used in this print command</param>
        /// <param name="nRowIndex">Row index in the DataGridView of the cell</param>
        /// <param name="nColumnIndex">Column index in the DataGridView of the cell</param>
        /// <param name="cellBounds">A Rectangle specifying the position of the cell on the page</param>
        /// <param name="graphicsOb">Graphics object on which to draw the cell</param>
        protected virtual void PrintCell(PBIPrintDocument printDoc,
            int nRowIndex, int nColumnIndex, Rectangle cellBounds, Graphics graphicsOb)
        {
            GridPrintingCellEventArgs args =
                new GridPrintingCellEventArgs(printDoc, nRowIndex, nColumnIndex,
                cellBounds, graphicsOb);

            // Call the PrintingCell event handler, if any:
            if (PrintingCell != null)
            {
                PrintingCell(this, args);
                if (args.Handled)
                    return;
            }

            if (Grid.Banners.ContainsCell(nRowIndex, nColumnIndex)) // banner printing is handled outside of this function
                return;

            DataGridViewCell cell = Grid[nColumnIndex, nRowIndex];

            // Allow GridManger to print the cell, in case it's a custom painted cell: 
            m_Grid.OnCellPrinting(args);
            if (args.Handled)
                return;

            // Now that no other code has handled printing this cell, print it normally:
            // Call GetCellStyleAndColumnType - that's the only way to get the REAL CellStyle, after CellFormatting has been applied:
            DataGridViewCellStyle cellStyle;
            EColumnType eColType;
            m_Grid.GetCellStyleAndColumnType(nRowIndex, nColumnIndex, out cellStyle, out eColType);

            // Draw cell background: 
            graphicsOb.FillRectangle(BrushCache.GetBrush(cellStyle.BackColor), cellBounds);

            // Draw text in the cell, with the specified foreground color:
            string cellsString = "";
            try
            {
                if (m_Grid.IsCellValueDisplayed(nRowIndex, nColumnIndex))
                {
                    // Is cell a check box cell?
                    if (cell is DataGridViewCheckBoxCell)
                    {
                        DataGridViewCellCancelEventArgs checkboxArgs = new DataGridViewCellCancelEventArgs(nColumnIndex, nRowIndex);
                        m_Grid.OnCellCheckboxDisplaying(checkboxArgs);
                        if (!checkboxArgs.Cancel)
                        {
                            DataGridViewCheckBoxCell cbCell = (DataGridViewCheckBoxCell)cell;
                            const int boxImageSize = 10; // later, might wanna scale it
                            int halfBoxImageSize = boxImageSize / 2;
                            int cellCenterHoriz = cellBounds.X + (cellBounds.Width / 2);
                            int cellCenterVert = cellBounds.Y + (cellBounds.Height / 2);

                            // handle the case where cell value is null, rather than a valid bool value:
                            bool cellValue = false;
                            if (cbCell.Value is bool)
                                cellValue = (bool)cbCell.Value;

                            printDoc.DrawCheckBox(new Point(cellCenterHoriz - halfBoxImageSize, cellCenterVert - halfBoxImageSize), boxImageSize,
                                graphicsOb, cellValue);
                            /* `` The following code does not work -- I don't know why, but CheckBoxRenderer
                             * does not seem to want to draw to a Print Preview medium. 
                             * */
                            //``CheckBoxRenderer.DrawCheckBox(graphicsOb, cellBounds.Location ),
                            //``    (bool)cell.Value ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal );
                        }
                    }
                    // eventually we'll probly want to test for other strings that need to be rendered as a non-string
                    else
                    {
                        // if contents of the cell can be rendered as a string, then render it...
                        cellsString = cell.FormattedValue.ToString();
                        StringFormat strFormat = new StringFormat();
                        strFormat.Trimming = StringTrimming.Word;
                        if (cell is DataGridViewTextBoxCell || cell is DataGridViewComboBoxCell)
                        {
                            // Convert the cell style attributes to StringFormat attributes:
                            Grid.CellAlignmentToStringFormatAlignment(
                                cellStyle.Alignment, strFormat);
                            if (cellStyle.WrapMode != DataGridViewTriState.True)
                            {
                                strFormat.FormatFlags = StringFormatFlags.NoWrap;
                                strFormat.Trimming = StringTrimming.EllipsisCharacter;
                            }
                        }
                        // Pad the text a bit, to simulate how the text in the cell on the screen 
                        // is slightly padded:
                        Rectangle paddedCellBounds = new Rectangle(
                            cellBounds.X + cellTextPadding, cellBounds.Y + cellTextPadding,
                            cellBounds.Width - doubleCellTextPadding, cellBounds.Height - doubleCellTextPadding);

                        graphicsOb.DrawString(cellsString, ScaleFont(cellStyle.Font),
                            BrushCache.GetBrush(cellStyle.ForeColor), paddedCellBounds, strFormat);
                    }
                }

                // Draw right and bottom borders (left and top are drawn by drawing previous cells):
                graphicsOb.DrawLine(cellBorderPen, cellBounds.Right, cellBounds.Top,
                    cellBounds.Right, cellBounds.Bottom);
                graphicsOb.DrawLine(cellBorderPen, cellBounds.Left, cellBounds.Bottom,
                    cellBounds.Right, cellBounds.Bottom);
            }
            catch (Exception ex)
            {
                ExceptionMessageBox.Show("Error printing cell", ex, new System.Diagnostics.StackTrace(true));
                return;
            }
        }

        Font ScaleFont(Font displayedFont)
        {
            // Scale the font only if necessary:
            if (ScaleFactor == 1)
                return displayedFont;
            else
            {
                return new Font(displayedFont.FontFamily,
                    displayedFont.Size * (float)ScaleFactor,
                    displayedFont.Style, displayedFont.Unit);
            }
        }

        int ScaleColumnWidth(int gridPixels)
        {
            // textFitFactor is used because, when printed, the characters are a little bit wider in comparison to the 
            // column width, than when displayed on a grid. Thus, on wrapping cells, the words wrap earlier on print 
            // than when displayed, and may spill over to an extra line. Thus, we make the column just a little bit wider. 
            const decimal textFitFactor = 1.1M;
            return (int)(ScaleFactor * textFitFactor * gridPixels);
        }

        int ScaleRowHeight(int gridPixels)
        {
            return (int)(ScaleFactor * gridPixels);
        }

        // Sub-classes used in this class and in GridPrintBookmark: 
        public class RowCoords
        {
            public List<int> RowsToPrint = new List<int>(); // indexes of rows to print on this page
            public List<int> RowBottomPositions = new List<int>(); // bottom boundaries, in pixel position, of the rows printed on this page
            public int Count { get { return RowsToPrint.Count; } }
        }

        public class ColumnCoords
        {
            public List<int> ColumnsToPrint = new List<int>(); // indexes of columns to print on this page
            public List<int> ColumnRightPositions = new List<int>(); // right boundaries, in pixel position, of the columns printed on this page
            public int Count { get { return ColumnsToPrint.Count; } }
        }

        public class GridCoords
        {
            public int Left = 0; // relative to printed page
            public int Top = 0; // relative to printed page
            public int RowHeaderWidth;
            public int ColumnHeaderHeight;
            public RowCoords RowCoords;
            public ColumnCoords ColumnCoords;

            public int GridHeight
            {
                get
                {
                    if (RowCoords.Count > 0)
                        return RowCoords.RowBottomPositions[RowCoords.Count - 1];
                    else
                        return 0;
                }
            }

            public int GridWidth
            {
                get
                {
                    if (ColumnCoords.Count > 0)
                        return ColumnCoords.ColumnRightPositions[ColumnCoords.Count - 1];
                    else
                        return 0;
                }
            }

            public GridCoords(RowCoords rowCoords, ColumnCoords columnCoords,
                int rowHeaderWidth, int columnHeaderHeight)
            {
                RowCoords = rowCoords;
                ColumnCoords = columnCoords;
                RowHeaderWidth = rowHeaderWidth;
                ColumnHeaderHeight = columnHeaderHeight;
            }

            ///<summary> RowTop returns the Y-coordinate, on the printed page, of the top of the indicated grid row.</summary>
            /// NOTE: Parameter rowListIndex is the index in RowsToPrint (same as the index in RowBottomPositions), 
            /// NOT the grid row index.
            public int RowTop(int rowListIndex)
            {
                if (rowListIndex == 0)
                    return ColumnHeaderHeight + Top;
                else
                    return RowCoords.RowBottomPositions[rowListIndex - 1] + Top;
            }

            ///<summary> RowBottom returns the Y-coordinate, on the printed page, of the bottom of the indicated grid row.</summary>
            /// NOTE: Parameter rowListIndex is the index in RowsToPrint (same as the index in RowBottomPositions), 
            /// NOT the grid row index.
            public int RowBottom(int rowListIndex)
            {
                return RowCoords.RowBottomPositions[rowListIndex] + Top;
            }

            ///<summary> ColumnLeft returns the X-coordinate, on the printed page, of the left edge of the indicated grid column.</summary>
            /// NOTE: Parameter columnListIndex is the index in ColumnsToPrint (same as the index in ColumnRightPositions), 
            /// NOT the grid column index.
            public int ColumnLeft(int columnListIndex)
            {
                if (columnListIndex == 0)
                    return RowHeaderWidth + Left;
                else
                    return ColumnCoords.ColumnRightPositions[columnListIndex - 1] + Left;
            }

            ///<summary> ColumnRight returns the X-coordinate, on the printed page, of the right edge of the indicated grid column.</summary>
            /// NOTE: Parameter columnListIndex is the index in ColumnsToPrint (same as the index in ColumnRightPositions), 
            /// NOT the grid column index.
            public int ColumnRight(int columnListIndex)
            {
                return ColumnCoords.ColumnRightPositions[columnListIndex] + Left;
            }
        }

        public class PageCoords
        {
            public List<GridCoords> Grids = new List<GridCoords>();
            // - if > 1 grid can be printed on this page, Grids has >1 element 

            public int LowestPrintingOnPage
            {
                get
                {
                    GridCoords bottomGrid = Grids[Grids.Count - 1];
                    return bottomGrid.Top + bottomGrid.GridHeight;
                }
            }
        }
    }
}
