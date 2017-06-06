
using CAS.CommServer.ProtocolHub.Communication;
using CAS.Lib.CodeProtect;

namespace CAS.CommServer.DA.Server.ProductInstaller
{

  partial class InstallerCustomActions
  {

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.m_CodeProtectInstaller = new LibInstaller();
      this.m_ProtocolHubCommunicationInstaller = new CommServerInstaller();
      this.Installers.AddRange(new System.Configuration.Install.Installer[] { this.m_CodeProtectInstaller, m_ProtocolHubCommunicationInstaller });
    }
    #endregion

    private LibInstaller m_CodeProtectInstaller;
    private CommServerInstaller m_ProtocolHubCommunicationInstaller;
  }
}