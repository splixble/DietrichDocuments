using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PrintLib
{
    /** <summary>ExceptionMessageBox is a message box that is displayed when an Exception is  
     * thrown.</summary> It displays descriptive diagnostic information about the exception.
     * 
     * ExceptionMessageBox is taken originally from StudyTracker's 
     * StudyTrackerExceptionMessageBox.
     */
    public class ExceptionMessageBox : ErrorMessageBox
    {
        Exception _Exception = null;
        StackTrace _StackTrace = null;

        protected override string ErrorType { get { return "Exception"; } }

        protected override string DetailInfo
        {
            get
            {
                string info = "";
                if (_Exception.StackTrace != null && _Exception.StackTrace != "")
                    info += "Stack Trace: " + NL + _Exception.StackTrace + NL + NL;
                if (_Exception.InnerException != null)
                {
                    info += "Inner Exception: " + NL +
                        _Exception.InnerException.Message + NL + NL;
                    if (_Exception.InnerException.StackTrace != null && _Exception.InnerException.StackTrace != "")
                        info += "Stack Trace: " + NL + _Exception.InnerException.StackTrace + NL + NL;
                }
                if (_StackTrace != null)
                {
                    info += "Calling Stack Trace: " + NL;
                    for (int n = 0; n < _StackTrace.FrameCount; n++)
                    {
                        StackFrame frame = _StackTrace.GetFrame(n);
                        MethodBase method = frame.GetMethod();
                        string fileInfo = ""; // info about file name and line number, which the frame might not have
                        string fileName = frame.GetFileName();
                        if (fileName != null)
                            fileInfo = " in " + frame.GetFileName() + ":" + frame.GetFileLineNumber();
                        info += "   at " + method.ReflectedType + "." + method.Name + fileInfo + NL;
                    }
                    info += NL + NL;
                }
                return info;
            }
        }

        public ExceptionMessageBox(bool showDetail)
            : base(showDetail)
        {
        }

        /// <summary>
        /// Sets the fields and control properties of the ErrorMessageBox in preparation for the box to be displayed.
        /// </summary>
        /// <param name="exceptionThrowersMessage">Message to display on the first line of the message box, describing the overall 
        /// context in which the exception was thrown, or null.</param>
        /// <param name="ex">Exception object for the exception that was thrown.</param>
        /// <param name="stackTrace">StackTrace object representing call stack when the exception was thrown.</param>
        public void SetException(string exceptionThrowersMessage, Exception ex, StackTrace stackTrace)
        {
            SetError(exceptionThrowersMessage != null ? exceptionThrowersMessage + NL + ex.Message : ex.Message);

            _Exception = ex;
            _StackTrace = stackTrace;
        }

        /// <summary>
        /// Displays an ExceptionMessageBox as a modal dialog box, with information about an exception that was thrown.
        /// </summary>
        /// <param name="headingMessage">Message to display on the first line of the message box, describing the overall context 
        /// in which the exception was thrown.</param>
        /// <param name="ex">Exception object for the exception that was thrown.</param>
        /// <param name="stackTrace">StackTrace object representing call stack when the exception was thrown.</param>
        public static void Show(string headingMessage, Exception ex, StackTrace stackTrace)
        {
            Show(headingMessage, ex, stackTrace, true, null, null);
        }

        /// <summary>
        /// Displays an ExceptionMessageBox as a modal dialog box, with information about an exception that was thrown.
        /// </summary>
        /// <param name="headingMessage">Message to display on the first line of the message box, describing the overall context 
        /// in which the exception was thrown.</param>
        /// <param name="ex">Exception object for the exception that was thrown.</param>
        /// <param name="stackTrace">StackTrace object representing call stack when the exception was thrown.</param>
        /// <param name="showDetail">True to show the call stack and instructions for notifying the software engineer, false to 
        /// show only the main error message.</param>
        public static void Show(string headingMessage, Exception ex, StackTrace stackTrace, bool showDetail)
        {
            Show(headingMessage, ex, stackTrace, showDetail, null, null);
        }

        /// <summary>
        /// Displays an ExceptionMessageBox as a modal dialog box, with information about an exception that was thrown.
        /// </summary>
        /// <param name="headingMessage">Message to display on the first line of the message box, describing the overall context 
        /// in which the exception was thrown, or null.</param>
        /// <param name="ex">Exception object for the exception that was thrown.</param>
        /// <param name="stackTrace">StackTrace object representing call stack when the exception was thrown.</param>
        /// <param name="showDetail">True to show the call stack and instructions for notifying the software engineer, false to 
        /// show only the main error message.</param>
        /// <param name="bannerText">Text to display in the message box's banner, or null.</param>
        public static void Show(string headingMessage, Exception ex, StackTrace stackTrace, bool showDetail, string bannerText)
        {
            Show(headingMessage, ex, stackTrace, showDetail, null, null);
        }

        /// <summary>
        /// Displays an ExceptionMessageBox as a modal dialog box, with information about an exception that was thrown.
        /// </summary>
        /// <param name="headingMessage">Message to display on the first line of the message box, describing the overall context 
        /// in which the exception was thrown, or null.</param>
        /// <param name="ex">Exception object for the exception that was thrown.</param>
        /// <param name="stackTrace">StackTrace object representing call stack when the exception was thrown.</param>
        /// <param name="showDetail">True to show the call stack and instructions for notifying the software engineer, false to 
        /// show only the main error message.</param>
        /// <param name="bannerText">Text to display in the message box's banner, or null.</param>
        /// <param name="owner">Represents the top-level window that will own the modal dialog box
        public static void Show(string headingMessage, Exception ex, StackTrace stackTrace, bool showDetail, string bannerText, IWin32Window owner)
        {
            ExceptionMessageBox msgBox = new ExceptionMessageBox(showDetail);

            // Set banner text to include app name and user, per 21 CFR part 11:: 
            if (bannerText != null)
                msgBox.Text = bannerText;
            else
                msgBox.Text = "Software Error - ";
            /* removed this 12/22/14 - we don't absolutely need RequiredBannerText; and sometimes getting that RequiredBannerText crashes if it's too early in app run.
            if (AppMgr != null)
                msgBox.Text = AppMgr.RequiredBannerText(msgBox.Text);
             * */

            msgBox.SetException(headingMessage, ex, stackTrace);

            if (owner != null)
                msgBox.ShowDialog(owner);
            else
                msgBox.ShowDialog();
        }
    }
}