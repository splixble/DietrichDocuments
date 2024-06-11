using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;

namespace PrintLib
{
     public class DNPrintDocument : PrintDocument
    {
        public PrintArgs PrintArgs { get { return _PrintArgs; } }
        PrintArgs _PrintArgs = null;

        /// <summary>
        /// Scale to which to enlarge or reduce printed components. Defaults to 1.
        /// </summary>
        /// Scales all elements printed on the page except for the headers and footers.
        public float PrintScale
        {
            get
            {
                if (PrintArgs != null && PrintArgs.PrintScale != null)
                    return (float)PrintArgs.PrintScale;
                else
                    return 1;

            }
        }

        ///<summary> m_titles is an array of strings, meant to be set by derived classes and calling code</summary>
        List<String> m_titles;

        /// <summary>
        /// Specifies whether all controls (except Grids) should be printed by default, or not printed by default.
        /// </summary>
        /// Setting DNPrintDocument.PrintAllNonGridControls and calling DNPrintDocument.SetControlToPrint() allow a developer to 
        /// specify either that all controls in a form should be printed except those specified (by setting DNPrintDocument.PrintAllNonGridControls == true 
        /// and calling SetControlToPrint(ctrl, PrintControlOptions.DontPrint), or SetControlToPrint(ctrl, PrintControlOptions.DontPrintThisOrItsChildren)),
        /// or that NONE of the controls in a form should be printed except those specified (by setting DNPrintDocument.PrintAllNonGridControls == false 
        /// and calling SetControlToPrint(ctrl, PrintControlOptions.Print)).
        /// (Grids are always printed, unless their PrintCtrlOptions.DoNotPrint is specified.)
        public bool PrintAllNonGridControls = true;

        /// <summary>
        /// Hash of individual ctrls to specify whether to print, overriding PrintAllNonGridControls.
        /// </summary>
        /// key = control; value = PrintControlOptions of whether to print
        public Dictionary<Control, PrintControlOptions> ControlsToPrint { get { return _ControlsToPrint; } }
        Dictionary<Control, PrintControlOptions> _ControlsToPrint; // 

        /// <summary>
        /// Object to print if we're printing a single IDNPrintable object, instead of all the objects within a Control (ControlsToPrint).
        /// </summary>
        IDNPrintable _StandaloneObjectToPrint = null;

        bool m_FirstCallToPrintPage = true;
        int m_nPageNum = 0;
        public int PageNum { get { return m_nPageNum; } }
        int m_nTotalPages = 0;
        public int TotalPages { get { return m_nTotalPages; } }

        /// <summary>
        /// Page margin bounds, scaled to PrintScale if necessary
        /// </summary>
        public Rectangle MarginBounds { get { return _MarginBounds; } }
        Rectangle _MarginBounds;

        ///<summary> m_bookmark, if non-null, indicates we had to split up the printing of a grid 
        /// over several pages, and describes where we left off in the printing of the grid.</summary>
        PrintBookmark m_bookmark = null;

        /// <summary>
        /// List of Form Controls to be printed, along with their child controls if specified, as they appear on the form
        /// </summary>
        List<PrintedTopLevelControl> _TopLevelControls;
        int _TopLevelControlIndex;

        ///<summary> Index is page number (0-relative), value is struct with indices into index into m_PrintableCtrls</summary>
        /// Set up in dry-run print, used in printIt==true run.
        public List<FirstPrintableCtrlIndicesPerPageItem> FirstPrintableCtrlIndicesPerPage { get { return _FirstPrintableCtrlIndicesPerPage; } }
        List<FirstPrintableCtrlIndicesPerPageItem> _FirstPrintableCtrlIndicesPerPage = null;

        ///<summary> Y-coord of the top of the highest thing that can be printed on page, 
        /// excluding the page headers.</summary>
        public int TopOfContentPrintableArea;

        ///<summary> Y-coord of the bottom of the lowest thing yet printed on page, 
        /// excluding the footer</summary>
        public int LowestPrintingOnPage = 0;

        /** <summary>  CompressUnusedVerticalSpace indicates that if there are unprinted controls, we should move controls up 
         * that are underneath the unprinted controls to take up the empty space,
         * when possible</summary>
         */
        bool CompressUnusedVerticalSpace = true;

        /** <summary>  DefaultControlPadding is the minimum space between controls if they're squeezed together by 
         CompressUnusedVerticalSpace=true </summary>*/
        protected const int DefaultControlPadding = 6;

        protected int LeftMarginOnPageForCtrls { get { return (int)(56 * PrintScale); } }

        /** <summary>  m_OriginOfFormOnPage is the x- and y-coords on the printed page where 
         * the top and left edge of the form would hypothetically be. </summary>
         * Used to 
         * position controls on the page according to their position on 
         * the form, and/or relative to other controls as they fit on a 
         * single page. m_OriginOfFormOnPage.Y is adjusted when the printing
         * goes to another page, or when a control is vertically expanded
         * when printed (DNPrintCtrlOptions.PrintFlags.CanGrowVertically)
         */
        protected Point m_OriginOfFormOnPage;

        public Font TitleFont;
        public Font MainPrintFont;
        public Font MainPrintFontBold;

        protected Brush m_brushBlack;
        public Brush BrushBlack { get { return m_brushBlack; } }
        protected Pen m_penBlack;
        public Pen PenBlack { get { return m_penBlack; } }
        protected Pen m_penBlackThick;
        public Pen PenBlackThick { get { return m_penBlackThick; } }

        public const int m_HeaderHeight = 16;
        public const int m_HeaderLineHeight = 13;
        public const int m_FooterHeight = 16;

        public const int PageMargin = 40; // hundredths of an inch

        // Reusable objects, created on first use:
        PrintPreviewDialog _PrintPreviewDialog = null;
        public PrintPreviewDialog PrintPreviewDialog
        {
            get
            {
                if (_PrintPreviewDialog == null)
                    _PrintPreviewDialog = CreatePrintPreviewDialog();
                return _PrintPreviewDialog;
            }
        }

        PrintDialog _PrintDialog = null;
        public PrintDialog PrintDialog
        {
            get
            {
                if (_PrintDialog == null)
                    _PrintDialog = CreatePrintDialog();
                return _PrintDialog;
            }
        }

        // events:
        // THERE'S ALREADY A BeginPrint EVEN IN PrintDocument -- IS THS NECESSARY?````
        public new event System.Drawing.Printing.PrintEventHandler BeginPrint;
        // beginning either print or print preview

        public delegate void PrintedPageHeadingEventHandler(DNPrintDocument sender,
            PrintPageEventArgs e, bool printPage);
        public event PrintedPageHeadingEventHandler PrintedPageHeading;

        public DNPrintDocument()
        {
            _ControlsToPrint = new Dictionary<Control, PrintControlOptions>();
            _TopLevelControls = new List<PrintedTopLevelControl>();
            m_titles = new List<string>();
            TitleFont = new Font("Arial", 11);
            MainPrintFont = new Font("Arial", 8);
            MainPrintFontBold = new Font("Arial", 8, FontStyle.Bold);

            m_brushBlack = Brushes.Black;
            m_penBlack = new Pen(m_brushBlack, .1F);
            m_penBlackThick = new Pen(m_brushBlack, 2);

            /*``MOVED THIS CODE to OnBeginPrint to avoid the crash found by Jen H. on 12/19/08, where 
             * bringing up one of the Grids, like Rev Proj, throws an exception: "No printers are
             * installed". 
             * 
            // .NET positioning of printed objects relative to page margins 
            // seems to not take into account the printer's "hard margins":
            DefaultPageSettings.Margins =
                new Margins(PageMargin, PageMargin + (int)(DefaultPageSettings.HardMarginX * 2),
                PageMargin, PageMargin + (int)(DefaultPageSettings.HardMarginY * 2));
             * ```*/
        }

        /// <summary>
        /// SetControlToPrint specifies whether a control should be specially printed or skipped from printing.
        /// </summary>
        /// <param name="ctrl">Control to be printed or not printed</param>
        /// <param name="printControlOptions">Option specifying whether the control will be printed or skipped from printing.</param>
        /// Setting DNPrintDocument.PrintAllNonGridControls and calling DNPrintDocument.SetControlToPrint() allow a developer to 
        /// specify either that all controls in a form should be printed except those specified (by setting DNPrintDocument.PrintAllNonGridControls == true 
        /// and calling SetControlToPrint(ctrl, PrintControlOptions.DontPrint), or SetControlToPrint(ctrl, PrintControlOptions.DontPrintThisOrItsChildren)),
        /// or that NONE of the controls in a form should be printed except those specified (by setting DNPrintDocument.PrintAllNonGridControls == false 
        /// and calling SetControlToPrint(ctrl, PrintControlOptions.Print)).
        /// (Grids are always printed, unless their PrintCtrlOptions.DoNotPrint is specified.)
        public void SetControlToPrint(Control ctrl, PrintControlOptions printControlOptions)
        {
            if (!_ControlsToPrint.ContainsKey(ctrl))
                _ControlsToPrint.Add(ctrl, printControlOptions);
            else
                _ControlsToPrint[ctrl] = printControlOptions;
        }

        /// <summary>
        /// Clears the list of objects set to be printed by PrintWithPrompt() or PrintPreview().
        /// </summary>
        /// Useful with AddContainerToPrint when printing more than one container control.
        public void ClearObjectsToPrint()
        {
            _TopLevelControls.Clear();
            _StandaloneObjectToPrint = null;
        }

        /// <summary>
        /// Adds a container control to the list of containers that will be printed the next time PrintWithPrompt(PrintArgs) or PrintPreview(PrintArgs) is called.
        /// </summary>
        /// <param name="containerToPrint">Container control to print</param>
        /// <param name="flags">Flags (that can be or'd together) specifying print options for this container</param>
        /// Useful when printing more than one container control.
        public void AddContainerToPrint(Control containerToPrint, PrintedTopLevelControlFlags flags)
        {
            PrintedTopLevelControl printedContainerObj = new PrintedTopLevelControl(this, containerToPrint, flags);
            _TopLevelControls.Add(printedContainerObj);
        }

        /// <summary>
        /// Executes the standard menu command File | Print.
        /// </summary>
        /// <param name="containerToPrint">Container control contining the UI components to print</param>
        public virtual void PrintWithPrompt(Control containerToPrint)
        {
            ClearObjectsToPrint();
            AddContainerToPrint(containerToPrint, 0);
            PrintWithPrompt((PrintArgs)null);
        }

        /// <summary>
        /// Executes the standard menu command File | Print.
        /// </summary>
        /// <param name="standaloneObjectToPrint">IDNPrintable object to print.</param>
        public virtual void PrintWithPrompt(IDNPrintable standaloneObjectToPrint)
        {
            ClearObjectsToPrint();
            _StandaloneObjectToPrint = standaloneObjectToPrint;
            PrintWithPrompt((PrintArgs)null);
        }

        /// <summary>
        /// Executes the standard menu command File | Print to print the containers added with AddContainerToPrint().
        /// </summary>
        /// <param name="printArgs">Arguments (such as PrintScale, and custom arguments) to send to the print job.</param>
        public virtual void PrintWithPrompt(PrintArgs printArgs)
        {
            if (printArgs != null)
                _PrintArgs = printArgs;
            else
                _PrintArgs = new PrintArgs();

            DialogResult result = PrintDialog.ShowDialog();

            // If the result is OK then print the document.
            if (result == DialogResult.OK)
            {
                Print();
            }
        }

        /// <summary>
        /// Executes the standard menu command File | Print Preview.
        /// </summary>
        /// <param name="containerToPrint">Container control contining the UI components to print</param>
        public virtual void PrintPreview(Control containerToPrint)
        {
            ClearObjectsToPrint();
            AddContainerToPrint(containerToPrint, 0);
            PrintPreview((PrintArgs)null);
        }

        /// <summary>
        /// Executes the standard menu command File | Print Preview.
        /// </summary>
        /// <param name="standaloneObjectToPrint">IDNPrintable object to print.</param>
        public virtual void PrintPreview(IDNPrintable standaloneObjectToPrint)
        {
            ClearObjectsToPrint();
            _StandaloneObjectToPrint = standaloneObjectToPrint;
            PrintPreview((PrintArgs)null);
        }

        /// <summary>
        /// Executes the standard menu command File | Print Preview to print the containers added with AddContainerToPrint().
        /// </summary>
        /// <param name="printArgs">Arguments (such as PrintScale, and custom arguments) to send to the print job.</param>
        public virtual void PrintPreview(PrintArgs printArgs)
        {
            if (printArgs != null)
                _PrintArgs = printArgs;
            else
                _PrintArgs = new PrintArgs();

            _PrintPreviewDialogShowing = true;
            PrintPreviewDialog.ShowDialog();
            _PrintPreviewDialogShowing = false;

            _PrintArgs = null; // gotta clear _PrintArgs for next printing 
        }

        bool _PrintPreviewDialogShowing = false;

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            // .NET positioning of printed objects relative to page margins 
            // seems to not take into account the printer's "hard margins":
            DefaultPageSettings.Margins =
                new Margins(PageMargin, PageMargin + (int)(DefaultPageSettings.HardMarginX * 2),
                PageMargin, PageMargin + (int)(DefaultPageSettings.HardMarginY * 2));

            m_nPageNum = 0;
            m_FirstCallToPrintPage = true;
            m_bookmark = null;
            DefaultPageSettings.Landscape = true; // just about all our grids are best printed in Landscape

            if (BeginPrint != null)
                BeginPrint(this, e);
            base.OnBeginPrint(e);
        }

        void PrintHeaderAndFooter(PrintPageEventArgs e)
        {
            int nThirdWidth = (e.MarginBounds.Right - e.MarginBounds.Left) / 3;

            // Reused string formats for page header elements:
            StringFormat formatLeftAligned = new StringFormat();
            formatLeftAligned.Alignment = StringAlignment.Near;
            formatLeftAligned.LineAlignment = StringAlignment.Near;
            StringFormat formatRightAligned = new StringFormat();
            formatRightAligned.Alignment = StringAlignment.Far;
            formatRightAligned.LineAlignment = StringAlignment.Near;
            StringFormat formatCenterAligned = new StringFormat();
            formatCenterAligned.Alignment = StringAlignment.Center;
            formatCenterAligned.LineAlignment = StringAlignment.Near;

            // HEADER:
            // Company name: 
            e.Graphics.DrawString("D & L", // PLACEHOLD
                MainPrintFontBold, Brushes.Black,
                new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top, nThirdWidth, m_HeaderLineHeight),
                formatLeftAligned);


            // Main title:
            string mainTitle = "";
            if (m_titles.Count > 0)
                mainTitle = m_titles[0];

            e.Graphics.DrawString(mainTitle,
                MainPrintFontBold, Brushes.Black,
                new Rectangle(e.MarginBounds.Left + nThirdWidth, e.MarginBounds.Top, nThirdWidth, m_HeaderLineHeight),
                formatCenterAligned);

            // DateTime, time:
            DateTime now = DateTime.Now;
            e.Graphics.DrawString(now.ToShortDateString() + "  " + now.ToShortTimeString(),
                MainPrintFont, Brushes.Black,
                new Rectangle(e.MarginBounds.Right - nThirdWidth, e.MarginBounds.Top, nThirdWidth, m_HeaderLineHeight),
                formatRightAligned);

            // FOOTER:
            // Bottom title:
            e.Graphics.DrawString(mainTitle,
                MainPrintFont, Brushes.Black,
                new Rectangle(e.MarginBounds.Left, e.MarginBounds.Bottom - m_FooterHeight, nThirdWidth, m_FooterHeight),
                formatLeftAligned);

            // "Confidential":
            e.Graphics.DrawString("Confidential",
                MainPrintFontBold, Brushes.Black,
                new Rectangle(e.MarginBounds.Left + nThirdWidth, e.MarginBounds.Bottom - m_FooterHeight, nThirdWidth, m_FooterHeight),
                formatCenterAligned);

            // Pages:
            e.Graphics.DrawString("Page " + m_nPageNum.ToString() + " of " + m_nTotalPages.ToString(),
                MainPrintFont, Brushes.Black,
                new Rectangle(e.MarginBounds.Right - nThirdWidth, e.MarginBounds.Bottom - m_FooterHeight, nThirdWidth, m_FooterHeight),
                formatRightAligned);
        }

        /** <summary>  PrintOrCountPage either prints the current page, if printPage==true, 
         * or just runs through the page positioning code, doing a "dry run" of
         * printing the page just to count the total number of pages that will 
         * be printed. </summary>
         * It returns true if there are more pages yet to print, or 
         * false if it completed printing the last page.
         * 
         * When overriding this method, be sure to call the base class function.
         */
        protected virtual bool PrintOrCountPage(PrintPageEventArgs e, bool printPage)
        {
            StringFormat formatCenterAligned = new StringFormat();
            formatCenterAligned.Alignment = StringAlignment.Center;
            formatCenterAligned.LineAlignment = StringAlignment.Near;

            bool printedAnything; // passed to output param later

            // not used - int bottomOfPrintableArea = e.MarginBounds.Bottom - DNPrintDocument.m_FooterHeight;

            // Make sure _TopLevelControlIndex is set to a value. If page num > 1, _TopLevelControlIndex will already be set. 
            if (m_nPageNum == 1)
            {
                _TopLevelControlIndex = 0;
                foreach (PrintedTopLevelControl topCtrl in _TopLevelControls)
                    topCtrl._PrintableCtrlIndex = 0;
            }

            m_titles.Clear();
            if (_TopLevelControlIndex < _TopLevelControls.Count)
                _TopLevelControls[_TopLevelControlIndex].RequestTitles(m_titles);

            if (printPage)
            {
                // Print header and footer in non-scaled coordinates:
                PrintHeaderAndFooter(e);
            }

            // set LowestPrintingOnPage (which excludes the printing of the Footer): 
            LowestPrintingOnPage = e.MarginBounds.Top + m_HeaderLineHeight;

            // Here, we scale the Graphics object if specified, and all further printing on the page (until the watermark) is done in scaled coordinates:
            GraphicsState previousGraphicsState = null;
            if (PrintScale != 1 && printPage)
            {
                previousGraphicsState = e.Graphics.Save();
                e.Graphics.ScaleTransform(PrintScale, PrintScale);
                LowestPrintingOnPage = (int)(LowestPrintingOnPage / PrintScale);
            }

            // Subtitles:
            int nSubtitleTop = LowestPrintingOnPage;
            for (int nSub = 1; nSub < m_titles.Count; nSub++)
            {
                if (printPage)
                {
                    e.Graphics.DrawString(m_titles[nSub],
                    MainPrintFont, Brushes.Black,
                    new Rectangle(MarginBounds.Left, nSubtitleTop, MarginBounds.Width, m_HeaderLineHeight),
                    formatCenterAligned);
                }
                nSubtitleTop += m_HeaderLineHeight;
            }

            LowestPrintingOnPage = nSubtitleTop;
            TopOfContentPrintableArea = LowestPrintingOnPage;

            if (PrintedPageHeading != null)
                PrintedPageHeading(this, e, printPage);

            // calculate m_OriginOfFormOnPage based on the first control printed on this page:
            m_OriginOfFormOnPage.X = LeftMarginOnPageForCtrls;
            m_OriginOfFormOnPage.Y = LowestPrintingOnPage;

            if (printPage)
            {
                // Get the index of the first PrintedTopLevelControl set to print on this page:
                _TopLevelControlIndex = _FirstPrintableCtrlIndicesPerPage[m_nPageNum - 1].TopLevelCtrlIndex; //0-relative index of page
            }
            else
            {
                // Record the index of the first PrintedTopLevelControl set to print on this page:
                _FirstPrintableCtrlIndicesPerPage.Add(new FirstPrintableCtrlIndicesPerPageItem(_TopLevelControlIndex, -1));
            }

            // Print _StandaloneObjectToPrint, if it's specified:
            if (_StandaloneObjectToPrint != null)
            {
                bool ctrlFitOnPage = PrintDNControl(_StandaloneObjectToPrint, e.Graphics, new Point(0, 0), printPage, ref m_bookmark, out printedAnything);
                return (!ctrlFitOnPage && m_bookmark != null);
            }

            // Print Top Level Controls, if any are specified:
            bool resumePrintingOnNextPage = false; // initialize the function's return value
            while (_TopLevelControlIndex < _TopLevelControls.Count && !resumePrintingOnNextPage)
            {
                PrintedTopLevelControl topLevelCtrl = _TopLevelControls[_TopLevelControlIndex];

                if (printPage)
                {
                    // Get the index of the first ctrl to print on this page:
                    topLevelCtrl._PrintableCtrlIndex = FirstPrintableCtrlIndicesPerPage[PageNum - 1].SubCtrlIndex; //0-relative index of page
                }
                else
                {
                    // Record the index of the first ctrl to print on this page:
                    FirstPrintableCtrlIndicesPerPage[PageNum - 1].SubCtrlIndex = topLevelCtrl._PrintableCtrlIndex;
                }

                while (topLevelCtrl._PrintableCtrlIndex < topLevelCtrl._PrintableCtrls.Count && !resumePrintingOnNextPage)
                {
                    Control ctrl = topLevelCtrl._PrintableCtrls[topLevelCtrl._PrintableCtrlIndex];

                    Point parentContainerOffsetOnPage = topLevelCtrl.GetLocationOnForm(ctrl.Parent);
                    parentContainerOffsetOnPage.Offset(m_OriginOfFormOnPage);
                    if (CompressUnusedVerticalSpace)
                    {
                        int topCoord = ctrl.Bounds.Top + parentContainerOffsetOnPage.Y;
                        int whiteSpaceHeight = (topCoord - (LowestPrintingOnPage + DefaultControlPadding));
                        if (whiteSpaceHeight > 0)
                        {
                            // eliminate empty vertical space by adjusting m_OriginOfFormOnPage
                            m_OriginOfFormOnPage.Y -= (whiteSpaceHeight);
                            // now, we have to recalculate parentContainerOffsetOnPage:
                            parentContainerOffsetOnPage = topLevelCtrl.GetLocationOnForm(ctrl.Parent);
                            parentContainerOffsetOnPage.Offset(m_OriginOfFormOnPage);
                        }
                    }

                    bool ctrlFitOnPage = PrintControl(ctrl, e.Graphics, parentContainerOffsetOnPage, printPage, ref m_bookmark, out printedAnything);

                    if (ctrlFitOnPage || m_bookmark == null)
                    // - m_bookmark indicates the control is distributable over more than one pages, so m_bookmark == null 
                    // means go on to the next control even if this control won't fit on the page.
                    {
                        if (m_bookmark != null)
                            m_bookmark = null; // don't need to use bookmark any more
                        topLevelCtrl._PrintableCtrlIndex++; // only now can we advance the index
                    }
                    else
                    {
                        // at end of page; must resume or retry printing this control on next page
                        resumePrintingOnNextPage = true;
                    }
                }

                // If we completed printing topLevelCtrl, go to next one:
                if (!resumePrintingOnNextPage)
                {
                    _TopLevelControlIndex++;

                    // are there any more top-level controls left to print? if so, set it back to resume printing:
                    if (_TopLevelControlIndex < _TopLevelControls.Count)
                        resumePrintingOnNextPage = true;

                    // If we specified a page break after this PrintedTopLevelControl, or before the next one, then break out of this loop:
                    if (topLevelCtrl.HasFlag(PrintedTopLevelControlFlags.PageBreakAfter) ||
                        (_TopLevelControlIndex < _TopLevelControls.Count && _TopLevelControls[_TopLevelControlIndex].HasFlag(PrintedTopLevelControlFlags.PageBreakBefore)))
                        return resumePrintingOnNextPage;
                }
            }

            if (previousGraphicsState != null)
                e.Graphics.Restore(previousGraphicsState);

            /*
            if (printPage && !AppManager.AppMgr.DBContext.DBInstance.IsLiveDB)
                PrintTestDataWatermark(e);
            */

            return (resumePrintingOnNextPage);
        }

        void PrintTestDataWatermark(PrintPageEventArgs e)
        {
            Font watermarkFont = new Font("Arial", 68);
            SolidBrush watermarkBrush = new SolidBrush(Color.LightGray);
            float watermark_X = e.PageBounds.X;
            float watermark_Y = e.MarginBounds.Bottom - 250;
            GraphicsState state = e.Graphics.Save();
            e.Graphics.ResetTransform();
            e.Graphics.RotateTransform(-30);
            e.Graphics.TranslateTransform(watermark_X, watermark_Y, MatrixOrder.Append);
            e.Graphics.DrawString("  SIMULATED DATA\nFROM TEST DATABASE", watermarkFont, watermarkBrush, 0, 0);
            e.Graphics.Restore(state);
        }

        /** <summary>  InitializePrintOrCountPage initializes the GridPrintDocument's per-print-job 
        * members at the beginning of either printing or "dry run" printing to count pages.</summary>
        * Parameter:
        * - printPage: true if doing actual printing, false if doing a doing a 
        *   "dry run" to count pages
         * */
        protected virtual void InitializePrintOrCountPage(bool printPage)
        {
            m_nPageNum = 0;
            m_bookmark = null;
        }

        /** <summary>  Don't override OnPrintPage -- override PrintOrCountPage instead.</summary>
         */
        protected sealed override void OnPrintPage(PrintPageEventArgs e)
        {
            try
            {
                if (m_FirstCallToPrintPage)
                {
                    // Set up ScaledMarginBounds:
                    if (PrintScale != 1)
                    {
                        _MarginBounds = new Rectangle((int)(e.MarginBounds.X / PrintScale), (int)(e.MarginBounds.Y / PrintScale),
                            (int)(e.MarginBounds.Width / PrintScale), (int)(e.MarginBounds.Height / PrintScale));
                    }
                    else
                        _MarginBounds = e.MarginBounds;

                    /* The first time we call this, we have to do a "dry run" to 
                     * calculate page count, by running through this loop: */
                    InitializePrintOrCountPage(false);
                    _FirstPrintableCtrlIndicesPerPage = new List<FirstPrintableCtrlIndicesPerPageItem>();

                    foreach (PrintedTopLevelControl topLevelCtrl in _TopLevelControls)
                        topLevelCtrl.SetUpPrintableControls();

                    m_nTotalPages = 0;
                    m_nPageNum = 0;

                    bool hasMorePages = true;
                    while (hasMorePages)
                    {
                        m_nTotalPages++;
                        m_nPageNum++;
                        hasMorePages = PrintOrCountPage(e, false);
                    }
                    InitializePrintOrCountPage(true);
                    m_FirstCallToPrintPage = false;
                }

                // Now, do the actual printing:
                m_nPageNum++;
                bool printThisPage = true;

                // should I print this page at all?
                if (PrinterSettings.PrintRange == PrintRange.SomePages &&
                    m_nPageNum < PrinterSettings.FromPage)
                    printThisPage = false;

                if (printThisPage)
                    PrintOrCountPage(e, true);

                e.HasMorePages = m_nPageNum < m_nTotalPages;
                if (PrinterSettings.PrintRange == PrintRange.SomePages &&
                    m_nPageNum >= PrinterSettings.ToPage)
                    e.HasMorePages = false;

                base.OnPrintPage(e);
            }
            catch (Exception ex)
            {
                ExceptionMessageBox.Show("Error in DNPrintDocument.OnPrintPage", ex, new System.Diagnostics.StackTrace(true));
            }
        }

        protected override void OnEndPrint(PrintEventArgs e)
        {
            base.OnEndPrint(e);

            if (!_PrintPreviewDialogShowing) // if it is showing, leave it to the Print Preview code to clear _PrintArgs
                _PrintArgs = null; // gotta clear _PrintArgs for next printing 
        }

        /** <summary>  PrintControl attempts to print a control, or do a "dry run" of printing to determine page count.</summary>
         * Parameter bookmark is an input/output parameter. If bookmark.PrintingIncomplete
         * is true, it needs to start printing where it left off at the end of the 
         * previous page in the printing of the control.
         * If PrintControl reaches the end of the page before finishing the printing of a 
         * control, bookmark.PrintingIncomplete will be set to true.
         */
        public bool PrintControl(Control ctrl, Graphics graphics, Point parentContainerOffset, bool printPage, ref PrintBookmark bookmark)
        {
            bool printedAnything;
            return PrintControl(ctrl, graphics, parentContainerOffset, printPage, ref bookmark, out printedAnything);
        }

        bool PrintDNControl(IDNPrintable pbiCtrl, Graphics graphics, Point parentContainerOffset, bool printPage, ref PrintBookmark bookmark, out bool printedAnything)
        {
            // initialize out param:
            printedAnything = false;

            Control ctrl = pbiCtrl.Ctrl;

            if (!ctrl.Visible)
                return true;

            if (pbiCtrl.PrintCtrlOptions.HasFlag(DNPrintCtrlOptions.PrintFlags.DoNotPrint))
                return true; // if it's not printed, there's no problem fitting it on the page
            else
            {
                Rectangle ctrlBoundsOnPage = ctrl.Bounds;
                ctrlBoundsOnPage.Offset(parentContainerOffset);

                bool firstPageThisCtrlPrintedOn = (bookmark == null); // indicates we're on the first page on which we have attempted to print this control

                // IDNPrintable's Print method determines whether the control will fit on the page:
                bool canFitOnPage = pbiCtrl.Print(this, graphics, parentContainerOffset, printPage, ref bookmark, out printedAnything);

                // Make sure it's not stuck on a control, unable to print part or all of it - that will leave us in an infinite loop:
                if (!firstPageThisCtrlPrintedOn && bookmark.PrintingIncomplete && !canFitOnPage && !printedAnything)
                    throw (new Exception("Printing of DN Control object " + ctrl.Name + " was deferred to next page, with no printing on current page; this will lead to an endless loop."));

                if (pbiCtrl.PrintCtrlOptions.HasFlag(DNPrintCtrlOptions.PrintFlags.CanGrowVertically) &&
                    bookmark != null && !bookmark.PrintingIncomplete)
                {
                    // adjust m_OriginOfFormOnPage
                    m_OriginOfFormOnPage.Y += (LowestPrintingOnPage - ctrlBoundsOnPage.Bottom);
                }
                return canFitOnPage;
            }
        }

        /** <summary>  PrintControl attempts to print a control, or do a "dry run" of printing to determine page count.</summary>
         * Parameter bookmark is an input/output parameter. If bookmark.PrintingIncomplete
         * is true, it needs to start printing where it left off at the end of the 
         * previous page in the printing of the control.
         * If PrintControl reaches the end of the page before finishing the printing of a 
         * control, bookmark.PrintingIncomplete will be set to true.
         */
        public virtual bool PrintControl(Control ctrl, Graphics graphics, Point parentContainerOffset, bool printPage, ref PrintBookmark bookmark, out bool printedAnything)
        {
            // initialize out param:
            printedAnything = false;

            if (!ctrl.Visible)
                return true;

            bool canFitOnPage;

            const int ControlTextPadding = 3;

            StringFormat stringFormat = StringFormat.GenericDefault;
            if (ctrl is IDNPrintable)
                canFitOnPage = PrintDNControl((IDNPrintable)ctrl, graphics, parentContainerOffset, printPage, ref bookmark, out printedAnything);
            else if (ctrl.Tag is IDNPrintable)
                canFitOnPage = PrintDNControl(ctrl.Tag as IDNPrintable, graphics, parentContainerOffset, printPage, ref bookmark, out printedAnything);
            else // not an IDNPrintable
            {
                Rectangle ctrlBoundsOnPage = ctrl.Bounds;
                ctrlBoundsOnPage.Offset(parentContainerOffset);

                // If it's a stock .NET control -- rather than an IDNPrintable -- we need to check its dimensions 
                // to determine whether it will fit on the page (this check is meaningful only when printPage==false):
                int bottomOfCtrlOnPage = parentContainerOffset.Y + ctrl.Bottom;
                int bottomOfPrintableArea = MarginBounds.Bottom - (int)(m_FooterHeight * PrintScale);
                canFitOnPage = (bottomOfCtrlOnPage <= bottomOfPrintableArea);
                if (!canFitOnPage)
                    return false;

                if (ctrl is CheckBox)
                {
                    CheckBox chkBox = (CheckBox)ctrl;
                    const int checkBoxSize = 10;
                    DrawCheckBox(graphics, ctrlBoundsOnPage.Location, checkBoxSize,
                        chkBox.Checked, printPage);
                    stringFormat.Alignment = StringAlignment.Near;
                    ctrlBoundsOnPage.X += checkBoxSize + 2;
                    DrawString(graphics, ctrl.Text, ctrl.Font, m_brushBlack, ctrlBoundsOnPage,
                        stringFormat, printPage);
                }
                else if (ctrl is RadioButton)
                {
                    RadioButton rButton = (RadioButton)ctrl;
                    const int rButtonSize = 10;
                    DrawRadioButton(graphics, ctrlBoundsOnPage.Location, rButtonSize,
                        rButton.Checked, printPage);
                    stringFormat.Alignment = StringAlignment.Near;
                    ctrlBoundsOnPage.X += rButtonSize + 2;
                    DrawString(graphics, ctrl.Text, ctrl.Font, m_brushBlack, ctrlBoundsOnPage,
                        stringFormat, printPage);
                }
                else if (ctrl is System.Windows.Forms.DataVisualization.Charting.Chart) // Microsoft Chart Control
                {
                    // TODO: it doesn't fill up the page with the chart as it should in most situations when you're printing a Chart.
                    // Want a ctrl option for that.
                    PrintChart((System.Windows.Forms.DataVisualization.Charting.Chart)ctrl, graphics, ctrlBoundsOnPage);
                }
                else
                {
                    // ````Gotta check TextAlign, and other ctrl-specific formatting things...
                    if ((ctrl is Label) || (ctrl is TextBox) || (ctrl is ComboBox)
                        || (ctrl is DateTimePicker) || (ctrl is GroupBox))
                    {
                        if (ctrl is Label)
                        {
                            Label labelCtrl = ((Label)ctrl);
                            ContentAlignmentToStringFormat(labelCtrl.TextAlign, stringFormat);
                            if (labelCtrl.AutoSize)
                            {
                                /* If it's AutoSize, the label should not be constricted to the 
                                 * bounds of the rectangle. Instead, it should be positioned on a 
                                 * point that is the left/right/center, top/bottom/center point 
                                 * of the rectangle, depending on the string alignment:
                                 * */
                                /* BTW, .NET 2.0 MSDN documentation is WRONG about StringFormat -- 
                                 * at least when used with Graphics.DrawString! 
                                   StringFormat.LineAlignment is VERTICAL, StringFormat.Alignment is HORIZONTAL!!
                                 * */
                                Point basePoint = new Point();
                                switch (stringFormat.Alignment)
                                {
                                    case StringAlignment.Near:
                                        basePoint.X = ctrlBoundsOnPage.Left;
                                        break;
                                    case StringAlignment.Center:
                                        basePoint.X = ctrlBoundsOnPage.Right - ctrlBoundsOnPage.Left;
                                        break;
                                    case StringAlignment.Far:
                                        basePoint.X = ctrlBoundsOnPage.Right;
                                        break;
                                }
                                switch (stringFormat.LineAlignment)
                                {
                                    case StringAlignment.Near:
                                        basePoint.Y = ctrlBoundsOnPage.Top;
                                        break;
                                    case StringAlignment.Center:
                                        basePoint.Y = ctrlBoundsOnPage.Bottom - ctrlBoundsOnPage.Top;
                                        break;
                                    case StringAlignment.Far:
                                        basePoint.Y = ctrlBoundsOnPage.Bottom;
                                        break;
                                }
                                DrawString(graphics, ctrl.Text, ctrl.Font, m_brushBlack,
                                    basePoint, ctrlBoundsOnPage.Top, ctrlBoundsOnPage.Bottom,
                                    stringFormat, printPage);
                            }
                            else
                            {
                                DrawString(graphics, ctrl.Text, ctrl.Font, m_brushBlack, ctrlBoundsOnPage,
                                    stringFormat, printPage);
                            }
                        }
                        else
                        {
                            // set text alignment:
                            stringFormat.LineAlignment = StringAlignment.Near;
                            if (ctrl is TextBox)
                            {
                                switch (((TextBox)ctrl).TextAlign)
                                {
                                    case HorizontalAlignment.Left:
                                        stringFormat.Alignment = StringAlignment.Near;
                                        break;
                                    case HorizontalAlignment.Center:
                                        stringFormat.Alignment = StringAlignment.Center;
                                        break;
                                    case HorizontalAlignment.Right:
                                        stringFormat.Alignment = StringAlignment.Far;
                                        break;
                                }
                            }
                            else
                            {
                                stringFormat.Alignment = StringAlignment.Near;
                            }

                            if (!(ctrl is TextBox && ((TextBox)ctrl).BorderStyle == BorderStyle.None))
                                DrawRectangle(graphics, m_penBlack, ctrlBoundsOnPage, printPage);
                            ctrlBoundsOnPage.Offset(ControlTextPadding, ControlTextPadding);
                            ctrlBoundsOnPage.Width -= ControlTextPadding;
                            ctrlBoundsOnPage.Height -= ControlTextPadding;
                            // for some reason, non-label controls get text aligned closer to top and left borders than labels...
                            DrawString(graphics, ctrl.Text, ctrl.Font, m_brushBlack, ctrlBoundsOnPage,
                                stringFormat, printPage);
                        }
                    }

                    // Print border, if one is specified:
                    bool printBorder = false;
                    if (ctrl is UserControl && !(ctrl is Form)
                        // form contains a grid, which is printed specially
                        && ((UserControl)ctrl).BorderStyle != BorderStyle.None)
                        printBorder = true;
                    else if (ctrl is Panel && ((Panel)ctrl).BorderStyle != BorderStyle.None)
                        printBorder = true;
                    if (printBorder)
                    {
                        // `````` should really check if other classes of ctrl have a border
                        DrawRectangle(graphics, m_penBlack, ctrlBoundsOnPage, printPage);
                    }
                }
            }

            return canFitOnPage;
        }

        /// <summary>
        /// Prints a Microsoft Chart Controls chart.
        /// </summary>
        /// <param name="chart">Chart to print</param>
        /// <param name="graphics">Graphics object to print to</param>
        /// <param name="ctrlBoundsOnPage">Rectangle representing the area on the page the chart can be printed to</param>
        /// Microsoft Bug Workaround: This started as just a call to chart.Printing.PrintPaint(graphics, chartPosition). But that printed the chart 
        /// with Markers that were way too big. I found out (on 20-Nov-13) this is a known problem in the .NET framework. In MSDN forum page
        /// http://social.msdn.microsoft.com/Forums/vstudio/en-US/1067cc1f-0834-433e-8763-89f228f00c70/printed-chart-looks-different-to-that-on-screen?forum=MSWinWebChart, 
        /// the MS tech said, "Make sure that graphics page units are pixel. Unfortunately, looks like the chart borders doesn't scale well, but 
        /// you can increase the border before printing. This issue will be investigated further." Then some code was supplied that changed the
        /// graphics page units to pixel, then set the Chart and Graphics object to what they were before. I incorporated that code into 
        /// this method.
        public void PrintChart(System.Windows.Forms.DataVisualization.Charting.Chart chart, Graphics graphics, Rectangle ctrlBoundsOnPage)
        {
            using (System.IO.MemoryStream chartState = new System.IO.MemoryStream())
            {
                chart.Serializer.Save(chartState);
                System.Drawing.Drawing2D.GraphicsState transState = graphics.Save();
                try
                {
                    Rectangle chartPosition = new Rectangle(ctrlBoundsOnPage.X, ctrlBoundsOnPage.Y, chart.Width, chart.Height);
                    // Display units mean different thing depending if chart is rendered on the display or printed.     
                    // Typically pixels for video displays, and 1/100 inch for printers.     
                    if (graphics.PageUnit == GraphicsUnit.Display)
                    {
                        // Chart is always expecting to draw in pixels     
                        graphics.PageUnit = GraphicsUnit.Pixel;

                        // Scale chart size from 1/100 of an inch to expected pixels.     
                        chartPosition.Width *= (int)(graphics.DpiX / 100.0f);
                        chartPosition.Height *= (int)(graphics.DpiY / 100.0f);
                        chartPosition.X *= (int)(graphics.DpiX / 100.0f);
                        chartPosition.Y *= (int)(graphics.DpiY / 100.0f);
                        foreach (System.Windows.Forms.DataVisualization.Charting.Series series in chart.Series)
                        {
                            series.BorderWidth *= (int)(graphics.DpiX / 100.0f);
                        }
                    }

                    chart.Printing.PrintPaint(graphics, chartPosition);

                }
                finally
                {
                    graphics.Restore(transState);
                    chart.Serializer.Load(chartState);
                }
            }

        }

        public void SetLowestPrinting(int lowestYCoord)
        {
            if (lowestYCoord > LowestPrintingOnPage)
                LowestPrintingOnPage = lowestYCoord;
            // adjust m_OriginOfFormOnPage
            //``            m_OriginOfFormOnPage.Y += (LowestPrintingOnPage - ctrlBoundsOnPage.Bottom);
        }

        /** <summary>  DrawRectangle draws a rectangle on the page being printed, or just sets the
         * lowest printing point to the bottom edge of the rectangle if doing the 
         * pre-print "dry run".</summary>
         * Parameters: 
         * - graphicsOb: graphics object on which to draw it
         * - pen: Pen object with which to render the rectangle
         * - ctrlBoundsOnPage: rectangle indicating the actual printable bounds on the page
         * - printPage: true if called from the actual printing, false if called during 
         *   the "dry run" to calculate number of pages.
         */
        public void DrawRectangle(Graphics graphics, Pen pen, Rectangle ctrlBoundsOnPage, bool printPage)
        {
            if (printPage)
                graphics.DrawRectangle(pen, ctrlBoundsOnPage);
            SetLowestPrinting(ctrlBoundsOnPage.Bottom);
        }

        /** <summary>  DrawLine draws a line on the page being printed, or just sets the
         * lowest printing point to the lowest point on the line if doing the 
         * pre-print "dry run".</summary>
         * Parameters: 
         * - graphicsOb: graphics object on which to draw it
         * - pen: Pen object with which to render the rectangle
         * - x1: x coordinate of first point defining the line
         * - y1: y coordinate of first point defining the line
         * - x2: x coordinate of second point defining the line
         * - y2: y coordinate of second point defining the line
         * - printPage: true if called from the actual printing, false if called during 
         *   the "dry run" to calculate number of pages.
         */
        public void DrawLine(Graphics graphics, Pen pen,
            int x1, int y1, int x2, int y2, bool printPage)
        {
            if (printPage)
                graphics.DrawLine(pen, x1, y1, x2, y2);
            SetLowestPrinting((y1 > y2) ? y1 : y2);
        }

        /** <summary>  DrawString renders a string on the page being printed, or just sets the
         * lowest printing point to the bottom of where the string would be printed
         * if doing the pre-print "dry run".</summary>
         * Parameters of this overload: 
         * - graphicsOb: graphics object on which to draw it
         * - text: text string to render
         * - font: font in which to render the string
         * - brush: Brush object defining the color to render the string
         * - ctrlBoundsOnPage: rectangle indicating the actual printable bounds on the page
         * - printPage: true if called from the actual printing, false if called during 
         *   the "dry run" to calculate number of pages.
         */
        public void DrawString(Graphics graphics, string text, Font font, Brush brush,
            Rectangle ctrlBoundsOnPage, StringFormat format, bool printPage)
        {
            if (printPage)
                graphics.DrawString(text, font, brush, ctrlBoundsOnPage, format);
            SetLowestPrinting(ctrlBoundsOnPage.Bottom);
        }

        /** <summary>  DrawString renders a string on the page being printed, or just sets the
         * lowest printing point to the bottom of where the string would be printed
         * if doing the pre-print "dry run".</summary>
         * Parameters of this overload: 
         * - graphicsOb: graphics object on which to draw it
         * - text: text string to render
         * - font: font in which to render the string
         * - brush: Brush object defining the color to render the string
         * - basePoint: upper-left corner of the drawn text
         * - top: y-coordinate of top of drawn text
         * - bottom: y-coordinate of bottom of drawn text
         * - format: StringFormat that specifies formatting attributes, 
         *   such as line spacing and alignment, that are applied to the drawn text.
         * - printPage: true if called from the actual printing, false if called during 
         *   the "dry run" to calculate number of pages.
         */
        public void DrawString(Graphics graphics, string text, Font font, Brush brush,
            Point basePoint, int top, int bottom, StringFormat format, bool printPage)
        {
            if (printPage)
                graphics.DrawString(text, font, brush, basePoint, format);
            SetLowestPrinting(bottom);
        }

        /** <summary>  DrawCheckBox draws a check box on the page being printed, or just sets the
         * lowest printing point to the bottom of where the check box would be printed
         * if doing the pre-print "dry run".</summary>
         * Parameters of this overload: 
         * - graphicsOb: graphics object on which to draw it
         * - location: upper-left corner of the check box
         * - boxSize: length of one side of the check box square
         * - bChecked: whether to draw the box as "checked"
         * - printPage: true if called from the actual printing, false if called during 
         *   the "dry run" to calculate number of pages.
         */
        public void DrawCheckBox(Graphics graphics, Point location, int boxSize, bool bChecked, bool printPage)
        {
            if (printPage)
                DrawCheckBox(location, boxSize, graphics, bChecked);
            SetLowestPrinting(location.Y + boxSize);
        }

        /** <summary>  DrawCheckBox draws a check box on the page being printed</summary>
         * Parameters of this overload: 
         * - location: upper-left corner of the check box
         * - boxSize: length of one side of the check box square
         * - graphicsOb: graphics object on which to draw the check box
         * - bChecked: whether to draw the box as "checked"
         */
        public void DrawCheckBox(Point location, int boxSize, Graphics graphicsOb, bool bChecked)
        {
            // Draw a box -- with a checkmark inside if checkbox is checked:
            int thirdBoxSize = boxSize / 3;
            int fifthBoxSize = boxSize / 5;

            graphicsOb.DrawRectangle(m_penBlack, location.X, location.Y,
                boxSize, boxSize);
            if (bChecked)
            {
                // draw check mark:
                int padding = 1;
                graphicsOb.DrawLine(m_penBlackThick,
                    location.X + fifthBoxSize, location.Y + (2 * thirdBoxSize),
                    location.X + (2 * fifthBoxSize), location.Y + boxSize - padding);
                graphicsOb.DrawLine(m_penBlackThick,
                    location.X + (2 * fifthBoxSize), location.Y + boxSize - padding,
                    location.X + boxSize - fifthBoxSize, location.Y + padding);
                /* `` here is the X, which we used before:
                graphicsOb.DrawLine(borderPen, location.X, location.Y,
                    location.X + boxSize, location.Y + boxSize);
                graphicsOb.DrawLine(borderPen, location.X + boxSize,
                    location.Y, location.X, location.Y + boxSize);
                 * */
            }
        }

        /** <summary>  DrawRadioButton draws a radio button on the page being printed, or just sets the
         * lowest printing point to the bottom of where the radio button would be printed
         * if doing the pre-print "dry run".</summary>
         * Parameters of this overload: 
         * - graphicsOb: graphics object on which to draw it
         * - location: upper-left corner of the radio button
         * - buttonDiameter: diameter of the radio button circle
         * - bChecked: whether to draw the button as "checked"
         * - printPage: true if called from the actual printing, false if called during 
         *   the "dry run" to calculate number of pages.
         */
        public void DrawRadioButton(Graphics graphics, Point location, int buttonDiameter, bool bChecked, bool printPage)
        {
            if (printPage)
                DrawRadioButton(location, buttonDiameter, graphics, bChecked);
            SetLowestPrinting(location.Y + buttonDiameter);
        }

        /** <summary>  DrawRadioButton draws a radio button on the page being printed</summary>
         * Parameters of this overload: 
         * - location: upper-left corner of the radio button
         * - buttonDiameter: diameter of the radio button circle
         * - graphicsOb: graphics object on which to draw the radio button
         * - bChecked: whether to draw the button as "checked"
         */
        public void DrawRadioButton(Point location, float buttonDiameter, Graphics graphicsOb, bool bChecked)
        {
            // Draw a box -- with a dot inside if radio button is checked:
            graphicsOb.DrawEllipse(m_penBlack, location.X, location.Y,
                buttonDiameter, buttonDiameter);
            if (bChecked)
            {
                // draw dot in middle:
                float dotDiameter = buttonDiameter / 2;
                float dotRadius = dotDiameter / 2;
                graphicsOb.FillEllipse(m_brushBlack, location.X + dotRadius, location.Y + dotRadius, dotDiameter, dotDiameter);
            }
        }

        /** <summary>  ContentAlignmentToStringFormat converts a ContentAlignment specification
         * to the equivalent StringFormat specification by setting the specified 
         * StringFormat object's properties to the analogous values.</summary>
         * Parameters: 
         * - contentAlign: ContentAlignment value to convert
         * - strFormat: StringFormat object to write the analogous values to
         * 
         * Copied from GridPrintDocument.CellAlignmentToStringFormatAlignment - 
         * same .NET type idea, different type.
         * Might make this a general util.
         */
        static void ContentAlignmentToStringFormat(ContentAlignment contentAlign,
            StringFormat strFormat)
        /* BTW, .NET 2.0 MSDN documentation is WRONG about StringFormat -- 
         * at least when used with Graphics.DrawString! 
           StringFormat.LineAlignment is VERTICAL, StringFormat.Alignment is HORIZONTAL!!
         * */
        {
            switch (contentAlign)
            {
                case ContentAlignment.BottomCenter:
                    strFormat.LineAlignment = StringAlignment.Far;
                    strFormat.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    strFormat.LineAlignment = StringAlignment.Far;
                    strFormat.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.BottomRight:
                    strFormat.LineAlignment = StringAlignment.Far;
                    strFormat.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.MiddleCenter:
                    strFormat.LineAlignment = StringAlignment.Center;
                    strFormat.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleLeft:
                    strFormat.LineAlignment = StringAlignment.Center;
                    strFormat.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleRight:
                    strFormat.LineAlignment = StringAlignment.Center;
                    strFormat.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.TopCenter:
                    strFormat.LineAlignment = StringAlignment.Near;
                    strFormat.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopLeft:
                    strFormat.LineAlignment = StringAlignment.Near;
                    strFormat.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    strFormat.LineAlignment = StringAlignment.Near;
                    strFormat.Alignment = StringAlignment.Far;
                    break;
                default:
                    strFormat.LineAlignment = StringAlignment.Center;
                    strFormat.Alignment = StringAlignment.Near;
                    break;
            }
        }

        /// <summary>
        /// Creates a standard PrintDialog, connected to this PrintDocument, with the options our apps use.
        /// </summary>
        /// <returns>A PrintDialog.</returns>
        public PrintDialog CreatePrintDialog()
        {
            PrintDialog dlg = new PrintDialog();
            dlg = new PrintDialog();
            dlg.Document = this;
            dlg.AllowSomePages = true;
            dlg.PrinterSettings.MinimumPage = 1;
            dlg.PrinterSettings.MaximumPage = 9999;
            dlg.PrinterSettings.FromPage = 1;
            dlg.PrinterSettings.ToPage = 1;
            return dlg;
        }

        /// <summary>
        /// Creates a standard PrintPreviewDialog, connected to this PrintDocument, with the options our apps use.
        /// </summary>
        /// <returns>A PrintPreviewDialog.</returns>
        public PrintPreviewDialog CreatePrintPreviewDialog()
        {
            PrintPreviewDialog dlg;
            dlg = new PrintPreviewDialog();
            dlg.Document = this;
            dlg.UseAntiAlias = true;
            // REMOVED dlg.Icon = AppManager.AppMgr.Icon;
            return dlg;
        }

        public class FirstPrintableCtrlIndicesPerPageItem
        {
            public int TopLevelCtrlIndex;
            public int SubCtrlIndex;

            public FirstPrintableCtrlIndicesPerPageItem(int topLevelCtrlIndex, int subCtrlIndex)
            {
                TopLevelCtrlIndex = topLevelCtrlIndex;
                SubCtrlIndex = subCtrlIndex;
            }
        }
    }
}
