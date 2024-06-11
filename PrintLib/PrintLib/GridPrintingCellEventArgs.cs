using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using PrintLib;

namespace PrintLib
{
    /** <summary>  GridPrintingCellEventArgs is an Event Args object used by the handler delagate 
     * for the GridPrintManager.PrintingCell event.</summary>
     */
    public class GridPrintingCellEventArgs
    {
        ///<summary> PrintDoc is the GridPrintDocument currently used to print.</summary>
        public DNPrintDocument PrintDoc { get { return m_printDoc; } }
        DNPrintDocument m_printDoc;

        ///<summary> RowIndex is the grid row index of the cell about to be printed.</summary>
        public int RowIndex { get { return m_nRowIndex; } }
        int m_nRowIndex;

        ///<summary> ColumnIndex is the grid column index of the cell about to be printed.</summary>
        public int ColumnIndex { get { return m_nColumnIndex; } }
        int m_nColumnIndex;

        ///<summary> CellBounds is the bounds on the printed page of the cell about to be printed.</summary>
        public Rectangle CellBounds { get { return m_cellBounds; } }
        Rectangle m_cellBounds;

        ///<summary> GraphicsOb is the Graphics object being printed to.</summary>
        public Graphics GraphicsOb { get { return m_graphicsOb; } }
        Graphics m_graphicsOb;

        ///<summary> Handled indicates whether the event handler has completely handled the event</summary>
        /// or whether the system should continue its own processing:
        public bool Handled = false;

        /** <summary>Constructor for GridPrintingCellEventArgs </summary> It has the following parameters:
         * - printDoc: the GridPrintDocument currently used to print
         * - nRowIndex: the grid row index of the cell about to be printed
         * - nColumnIndex: the grid column index of the cell about to be printed
         * - cellBounds: the bounds on the printed page of the cell about to be printed
         * - graphicsOb: the Graphics object being printed to
         * */
        public GridPrintingCellEventArgs(
            DNPrintDocument printDoc, int nRowIndex, int nColumnIndex,
            Rectangle cellBounds, Graphics graphicsOb)
        {
            m_printDoc = printDoc;
            m_nRowIndex = nRowIndex;
            m_nColumnIndex = nColumnIndex;
            m_cellBounds = cellBounds;
            m_graphicsOb = graphicsOb;
        }
    }
}
