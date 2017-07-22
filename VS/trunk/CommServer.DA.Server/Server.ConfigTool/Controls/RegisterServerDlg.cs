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
    public partial class RegisterServerDlg : Form
    {
        public RegisterServerDlg()
        {
            InitializeComponent();

			m_currentDirectory = Application.StartupPath;
        }
        
        private string m_currentDirectory;
        private RegisteredDotNetOpcServer m_registeredServer;

        /// <summary>
        /// Initializes and displays a modal dialog.
        /// </summary>
        public RegisteredDotNetOpcServer ShowDialog(RegisteredDotNetOpcServer registeredServer)
        {
            Initialize();

            m_registeredServer = registeredServer;

            if (registeredServer != null)
            {
                // select the .NET server.
                bool found = false;

                for (int ii = 0; ii < DotNetServerCB.Items.Count; ii++)
                {
                    DotNetOpcServer server = DotNetServerCB.Items[ii] as DotNetOpcServer;

                    if (server.Clsid == registeredServer.ServerClsid)
                    {
                        DotNetServerCB.SelectedIndex = ii;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    DotNetServerCB.SelectedIndex = DotNetServerCB.Items.Add(new DotNetOpcServer(registeredServer.ServerClsid));
                }
                
                // select the wrapper process.
                found = false;

                for (int ii = 0; ii < WrapperCB.Items.Count; ii++)
                {
                    DotNetOpcServerWrapper wrapper = WrapperCB.Items[ii] as DotNetOpcServerWrapper;

                    if (wrapper.Clsid == registeredServer.WrapperClsid)
                    {
                        WrapperCB.SelectedIndex = ii;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    WrapperCB.SelectedIndex = WrapperCB.Items.Add(new DotNetOpcServerWrapper(registeredServer.WrapperClsid));
                }
                
                // set the remaining parameters.
                ClsidTB.Text       = registeredServer.Clsid.ToString();
                ProgIdTB.Text      = registeredServer.ProgId;
                DescriptionTB.Text = registeredServer.Description;

                ParametersCTRL.Initialize(registeredServer);
            }

            if (DotNetServerCB.SelectedIndex == -1 && DotNetServerCB.Items.Count > 0)
            {
                DotNetServerCB.SelectedIndex = 0;
            }

            if (WrapperCB.SelectedIndex == -1 && WrapperCB.Items.Count > 0)
            {
                WrapperCB.SelectedIndex = 0;
            }

            ShowDialog();

            if (DialogResult != DialogResult.OK)
            {
                return null;
            }

            return m_registeredServer;
        }

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        private void Initialize()
        {
            DotNetServerCB.Items.Clear();

            List<DotNetOpcServer> servers  = DotNetOpcServer.EnumServers();

            foreach (DotNetOpcServer server in servers)
            {
                DotNetServerCB.Items.Add(server);
            }

            DotNetServerCB.SelectedIndex = -1;
            
            WrapperCB.Items.Clear();
            
            List<DotNetOpcServerWrapper> wrappers  = DotNetOpcServerWrapper.EnumWrappers();

            foreach (DotNetOpcServerWrapper wrapper in wrappers)
            {
                WrapperCB.Items.Add(wrapper);
            }

            ParametersCTRL.Initialize(null);
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                RegisteredDotNetOpcServer server = new RegisteredDotNetOpcServer();

                // set the .NET server.
                if (DotNetServerCB.SelectedIndex == -1)
                {
                    throw new ApplicationException("No .NET OPC server selected.");
                }

                server.ServerClsid = ((DotNetOpcServer)DotNetServerCB.SelectedItem).Clsid;
                              
                // set the wrapper.  
                if (WrapperCB.SelectedIndex == -1)
                {
                    throw new ApplicationException("No wrapper process selected.");
                }

                server.WrapperClsid = ((DotNetOpcServerWrapper)WrapperCB.SelectedItem).Clsid;
                               
                // set the clsid.     
                if (String.IsNullOrEmpty(ClsidTB.Text))
                {
                    server.Clsid = Guid.NewGuid();
                }
                else
                {
                    server.Clsid = new Guid(ClsidTB.Text);
                }

                // set the prog id.                        
                if (String.IsNullOrEmpty(ProgIdTB.Text))
                {
                    server.ProgId = ((DotNetOpcServer)DotNetServerCB.SelectedItem).ProgId + ".Wrapped";
                }
                else
                {
                    server.ProgId = ProgIdTB.Text;
                }

                // save the decription.
                server.Description = DescriptionTB.Text;
                                 
                // save the parameters.
                Dictionary<string,string> parameters = ParametersCTRL.GetParameters();

                if (parameters != null)
                {
                    foreach (KeyValuePair<string,string> entry in parameters)
                    {
                        server.Parameters[entry.Key] = entry.Value;
                    }
                }
                
                // remove existing registration.
                if (m_registeredServer != null)
                {
                    m_registeredServer.Unregister();
                }

                // update registry.
                server.Register();
                
                m_registeredServer = server;

                // close dialog.
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void DotNetServerCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DotNetOpcServer server = DotNetServerCB.SelectedItem as DotNetOpcServer;
                
                if (server == null)
                {
                    return;
                }
                
                if (String.IsNullOrEmpty(ProgIdTB.Text))
                {
                    ProgIdTB.Text = server.ProgId + ".Wrapped";
                }
                
                if (String.IsNullOrEmpty(ClsidTB.Text))
                {
                    ClsidTB.Text = Guid.NewGuid().ToString();
                }

                // display the supported specifications.
                StringBuilder specifications = new StringBuilder();

                foreach (Specifications value in Enum.GetValues(typeof(Specifications)))
                {
                    if ((server.Specifications & value) != 0)
                    {
                        if (specifications.Length > 0)
                        {
                            specifications.Append(", ");
                        }

                        specifications.Append(value);
                    }
                }

                if (specifications.Length == 0)
                {
                    specifications.Append(Specifications.None);
                }
                
                SpecificationsTB.Text = specifications.ToString();
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void NewClsidBTN_Click(object sender, EventArgs e)
        {            
            try
            {
                ClsidTB.Text = Guid.NewGuid().ToString();
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void BrowseBTN_Click(object sender, EventArgs e)
        {            
			try
			{
				Cursor = Cursors.WaitCursor;
			
				// select file.
				OpenFileDialog dialog = new OpenFileDialog();

				dialog.CheckFileExists  = true;
				dialog.CheckPathExists  = true;
				dialog.DefaultExt       = ".dll";
				dialog.Filter           = ".NET Assemblies (*.dll)|*.dll|All Files (*.*)|*.*";
				dialog.ValidateNames    = true;
				dialog.Title            = "Load .NET Assembly";
				dialog.RestoreDirectory = true;
				dialog.AddExtension     = true;
				dialog.FileName         = "";
				dialog.InitialDirectory = m_currentDirectory;

				if (dialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}			
                
                DotNetOpcServer.RegisterAssembly(dialog.FileName);
                Initialize();

                if (DotNetServerCB.SelectedIndex == -1 && DotNetServerCB.Items.Count > 0)
                {
                    DotNetServerCB.SelectedIndex = 0;
                }

                if (WrapperCB.SelectedIndex == -1 && WrapperCB.Items.Count > 0)
                {
                    WrapperCB.SelectedIndex = 0;
                }

				m_currentDirectory = new FileInfo(dialog.FileName).DirectoryName;
			}
			finally
			{		
				Cursor = Cursors.Default;		
			}	
        }
    }
}