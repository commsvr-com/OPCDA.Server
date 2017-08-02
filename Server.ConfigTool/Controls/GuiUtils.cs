using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace CAS.CommServer.DA.Server.ConfigTool
{
    /// <summary>
    /// A class that provide various common utility functions and shared resources.
    /// </summary>
    public partial class GuiUtils : UserControl
    {
        public GuiUtils()
        {
            InitializeComponent();
        }

		/// <summary>
		/// Displays the details of an exception.
		/// </summary>
		public static void HandleException(string caption, MethodBase method, Exception e)
		{
            if (String.IsNullOrEmpty(caption))
            {
                caption = method.Name;
            }

			new ExceptionDlg().ShowDialog(caption, e);
		}
    }
}
