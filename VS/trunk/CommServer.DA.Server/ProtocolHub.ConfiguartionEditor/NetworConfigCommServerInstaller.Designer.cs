using CAS.CommServer.ProtocolHub.Communication;
using CAS.Lib.CodeProtect;

namespace NetworkConfig
{
  partial class NetworConfigCommServerInstaller
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.CodeProtect_LibInstaller = new LibInstaller();
      this.CommmServerMainInstaller = new CommServerInstaller();
      // 
      // NetworConfigCommServerInstaller
      // 
      this.Installers.AddRange( new System.Configuration.Install.Installer[] {
            this.CodeProtect_LibInstaller} );

    }

    #endregion

    private LibInstaller CodeProtect_LibInstaller;
    private CommServerInstaller CommmServerMainInstaller;
  }
}