using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net.Mail;
using System.Windows.Forms;

namespace PrintLib
{
    /// <summary>
    /// A message box to inform the user of an application error, and enables the user to alert the software developer of the error. 
    /// </summary>
    public partial class ErrorMessageBox : Form
    {
        protected string _ErrorMessage = null;

        protected virtual string DetailInfo { get { return _DetailInfo; } }
        string _DetailInfo;

        protected virtual string ErrorType { get { return "Error"; } }

        // AppMgr is set by the AppMgr's constructor...
        // static public AppManager AppMgr = null;

        /// <summary>
        /// If true, shows the call stack and instructions for notifying the software engineer; if false, just shows the top-level error message 
        /// </summary>
        bool _ShowingDetail = true;
        public bool ShowingDetail { get { return _ShowingDetail; } }

        public ErrorMessageBox()
        {
            InitializeComponent();

            BackColor = DNColor.OperationForms;

            // Make it show up at center of screen: 
            StartPosition = FormStartPosition.Manual;
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - Height / 2);

            // if (AppMgr == null) // in case we're in Designer mode, rather than running app
            //    return;
            // AppManager.AppMgr.TimeTracer.Write("ErrorMessageBox ctor: " + new System.Diagnostics.StackTrace(true).ToString());
        }

        public ErrorMessageBox(bool showDetail)
            : this()
        {
            _ShowingDetail = showDetail;
        }

        protected override void OnLoad(EventArgs e)
        {
            lblHeading.Text = _ErrorMessage;
            tbTechnicalInfo.Text = GetTechnicalInfo(false);

            Redisplay();
            base.OnLoad(e);
        }

        protected string NL { get { return "\t" + Environment.NewLine; } } // for cleaner-looking string concatenations
        /* NOTE: The tab character ("\t") was added on 9/29/14 to work around a bug in Microsoft Outlook: it displays the body of this and other plain-text email messages 
         * with some linebreaks missing, and with a notification above the email reading "Extra line breaks in this message were removed." God knows what the morons at
         * Microsoft deem to be an "extra" line break that customers would want removed by default, but the workaround, I found in a Google search, was to add a tab
         * character to the end of the line, just before the line break.
         */

        /// <summary>
        /// Shows the controls as they should appear depending on the value of _ShowingDetail.
        /// </summary>
        void Redisplay()
        {
            lblHeadingEnterDescription.Visible = _ShowingDetail;
            lblHeadingTechInfo.Visible = _ShowingDetail;
            lblHeadingToNotify.Visible = _ShowingDetail;
            tbTechnicalInfo.Visible = _ShowingDetail;
            tbDetailsToEmail.Visible = _ShowingDetail;
            if (_ShowingDetail)
            {
                Height = 539;
                Width = 642;
                btnLeft.Text = "Notify";
            }
            else
            {
                Height = 136;
                Width = 440;
                btnLeft.Text = "Show Details";
            }
        }

        protected void SetError(string errorMessage)
        {
            _ErrorMessage = errorMessage;
        }

        protected string GetTechnicalInfo(bool includeUserProvidedInfo)
        {
            string techInfo = "";
            /* REMOVED
            if (AppMgr != null && AppMgr.AppName != null)
                techInfo += "Application: " + AppMgr.AppName + " v. " + AppMgr.GetVersionString(true, true) + NL;
            */
            techInfo += ErrorType + " message: " + NL + _ErrorMessage + NL + NL;
            if (includeUserProvidedInfo)
                techInfo += "User-provided information: " + NL + tbDetailsToEmail.Text + NL + NL;

            techInfo += DetailInfo;
            return techInfo;
        }

        ///<summary> btnLeft_Click handles a mouse click on the Notify/Show Detail button.</summary>
        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (_ShowingDetail)
            {
                // EmailErrorInfo(); // in detail mode this is the "Notify" button
                DialogResult = DialogResult.OK;
            }
            else
            {
                // in non-detail mode this is the Show Detail button:
                _ShowingDetail = true;
                Redisplay();
            }
        }

        /* REMOVED
        void EmailErrorInfo()
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(new MailAddress(AppManager.AppMgr.GlobalConfigTable[0].SoftwareEngineerEmail, AppManager.AppMgr.CompanyName + " Software Engineering"));
            msg.From = AppMgr.UserInfo.GetEmailAddress();
            string subject = AppManager.AppMgr.CompanyName + " Software " + ErrorType + ": " + _ErrorMessage;
            msg.Subject = subject.Replace("\r\n", " "); // replace chars that are not allowed in subject, so it doesn't crash

            msg.IsBodyHtml = false;
            msg.Body = GetTechnicalInfo(true);

            SmtpClient mailClient = new SmtpClient(AppManager.AppMgr.GlobalConfigTable[0].SMTPMailClient);
            mailClient.Send(msg);
        }
        */

        /// <summary>
        /// Displays an ErrorMessageBox as a modal dialog box, with information about an exception that was thrown.
        /// </summary>
        /// <param name="errorMessage">Message to display on the first line of the message box, describing the overall context 
        /// in which the exception was thrown, or null.</param>
        /// <param name="detailText">Text describing the error in detail, which the user may not need to know but the developer would.</param>
        /// <param name="showDetail">True to show detail text and instructions for notifying the software engineer, false to 
        /// show only the main error message.</param>
        public static void Show(string errorMessage, string detailText, bool showDetail)
        {
            // AppManager.AppMgr.TimeTracer.Write("ErrorMessageBox.Show: " + errorMessage + "\n" + detailText);
            ErrorMessageBox msgBox = new ErrorMessageBox(showDetail);

            msgBox.Text = "Software Error - ";

            msgBox.SetError(errorMessage);
            msgBox._DetailInfo = detailText;
            msgBox.ShowDialog();
        }
    }
}