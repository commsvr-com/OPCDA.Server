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
      this.m_CodeProtectInstaller = new CAS.Lib.CodeProtect.LibInstaller();
      this.m_EventLogInstaller = new System.Diagnostics.EventLogInstaller();
      // 
      // m_EventLogInstaller
      // 
      this.m_EventLogInstaller.CategoryCount = 0;
      this.m_EventLogInstaller.CategoryResourceFile = null;
      this.m_EventLogInstaller.MessageResourceFile = null;
      this.m_EventLogInstaller.ParameterResourceFile = null;
      this.m_EventLogInstaller.Source = "CAS.RealTime.Core";
      // 
      // InstallerCustomActions
      // 
      this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.m_CodeProtectInstaller,
            this.m_EventLogInstaller});

    }
    #endregion

    private LibInstaller m_CodeProtectInstaller;
    private System.Diagnostics.EventLogInstaller m_EventLogInstaller;
  }
}