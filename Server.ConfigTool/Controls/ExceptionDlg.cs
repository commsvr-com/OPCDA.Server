using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CAS.CommServer.DA.Server.ConfigTool
{
    /// <summary>
    /// A dialog that displays an exception trace in an HTML page.
    /// </summary>
    public partial class ExceptionDlg : Form
    {
        public ExceptionDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Display the exception in the dialog.
        /// </summary>
        public void ShowDialog(string caption, Exception e)
        {
            Text = caption;
   
            StringBuilder buffer = new StringBuilder();

            buffer.Append("<html><body style='margin:0'>");
            buffer.Append("<font style='font:9pt/12pt verdana; color:black'>");

            while (e != null)
            {
                string message = e.Message;
                                
                message = message.Replace("<", "&lt;");
                message = message.Replace(">", "&gt;");
                message = message.Replace("\r\n", "<br>");

                buffer.AppendFormat("<font color='red'><b>{0}</b></font><br>", message);
                buffer.AppendFormat("{0}<p>", e.StackTrace);

                e = e.InnerException;
            }
            
            buffer.Append("</font>");
            buffer.Append("</body></html>");

            ExceptionBrowser.DocumentText = buffer.ToString();

            ShowDialog();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}