using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Opc.ConfigTool
{
    public partial class ComServerListDlg : Form
    {
        public ComServerListDlg()
        {
            InitializeComponent();
                        
            RegisteredServersRB.Checked = true;
            ServersCTRL.Initialize(ConfigUtils.CATID_RegisteredDotNetOpcServers);
			m_currentDirectory = Application.StartupPath;
        }

        private string m_currentDirectory; 

        public void ShowDialog(Guid catid)
        {
            if (catid == ConfigUtils.CATID_DotNetOpcServers)
            {
                DotNetServersRB.Checked = true;
            }

            else if (catid == ConfigUtils.CATID_DotNetOpcServerWrappers)
            {
                WrappersRB.Checked = true;
            }

            else if (catid == ConfigUtils.CATID_RegisteredDotNetOpcServers)
            {
                RegisteredServersRB.Checked = true;
            }
            
            ServersCTRL.Initialize(catid);
            base.ShowDialog();
        }

        private void DotNetServersRB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (DotNetServersRB.Checked)
                {
                    ServersCTRL.Initialize(ConfigUtils.CATID_DotNetOpcServers);
                }
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void WrappersRB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (WrappersRB.Checked)
                {
                    ServersCTRL.Initialize(ConfigUtils.CATID_DotNetOpcServerWrappers);
                }
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void RegisteredServersRB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RegisteredServersRB.Checked)
                {
                    ServersCTRL.Initialize(ConfigUtils.CATID_RegisteredDotNetOpcServers);
                }
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void RegisterServerMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (new RegisterServerDlg().ShowDialog(null) != null)
                {
                    if (RegisteredServersRB.Checked)
                    {
                        ServersCTRL.Initialize(ConfigUtils.CATID_RegisteredDotNetOpcServers);
                    }
                }
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void ExportMI_Click(object sender, EventArgs e)
        {
            try
            {
				Cursor = Cursors.WaitCursor;
			
				// select file.
				OpenFileDialog dialog = new OpenFileDialog();

				dialog.CheckFileExists  = false;
				dialog.CheckPathExists  = true;
				dialog.DefaultExt       = ".xml";
				dialog.Filter           = "Registration Files (*.xml)|*.xml|All Files (*.*)|*.*";
				dialog.ValidateNames    = true;
				dialog.Title            = "Export Registered Servers";
				dialog.RestoreDirectory = true;
				dialog.AddExtension     = true;
				dialog.FileName         = "";
				dialog.InitialDirectory = m_currentDirectory;

				if (dialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}	
		
                RegisteredDotNetOpcServer.Export(dialog.FileName);

				m_currentDirectory = new FileInfo(dialog.FileName).DirectoryName;
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
            finally
            {
				Cursor = Cursors.Default;
            }
        }

        private void ExitMI_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
    }
}