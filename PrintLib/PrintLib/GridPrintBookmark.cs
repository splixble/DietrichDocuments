using PrintLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PrintLib
{
    public class GridPrintBookmark : PrintBookmark
    {
        GridPrintManager m_printManager;

        List<GridPrintManager.PageCoords> _PageCoordsList;
        public List<GridPrintManager.PageCoords> PageCoordsList { get { return _PageCoordsList; } }

        DataGridView Grid { get { return m_printManager.Grid; } }

        public override bool PrintingIncomplete { get { return (CurrentPageIndex < PageCoordsList.Count); } }

        public int CurrentPageIndex = 0; // 0-relative, index into _PageCoordsList, settable from outside class

        public GridPrintManager.PageCoords CurrentPageCoords
        {
            get { return PrintingIncomplete ? PageCoordsList[CurrentPageIndex] : null; }
        }

        public GridPrintBookmark(DNPrintDocument printDoc, GridPrintManager printManager)
            : base(printDoc)
        {
            m_printManager = printManager;
            _PageCoordsList = new List<GridPrintManager.PageCoords>();
        }
    }
}
