using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace CAS.CommServer.DA.Server.ConfigTool
{
    public partial class ParameterEditDlg : Form
    {
        public ParameterEditDlg()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Initializes and displays a modal dialog.
        /// </summary>
        public KeyValuePair<string,string> ShowDialog(KeyValuePair<string,string> parameter)
        {
            NameTB.Text  = parameter.Key;
            ValueTB.Text = parameter.Value;

            ShowDialog();

            if (DialogResult != DialogResult.OK)
            {
                return parameter;
            }

            return new KeyValuePair<string,string>(NameTB.Text, ValueTB.Text);
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {                      
                if (String.IsNullOrEmpty(NameTB.Text))
                {
                    throw new ApplicationException("Cannot add a parameter without a name.");
                }

                // close dialog.
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
    }
}