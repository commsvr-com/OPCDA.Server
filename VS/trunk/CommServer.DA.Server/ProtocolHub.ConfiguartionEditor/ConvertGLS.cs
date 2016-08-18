using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace NetworkConfig
{
    using Config = NetworkConfig.ComunicationNet;
  /// <summary>
  /// Summary description for ConvertGLS.
  /// </summary>
  public class ConvertGLS : System.Windows.Forms.Form
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    private void removeGroups(Config configDataBase)
    {
      foreach (Config.StationRow st in configDataBase.Station)
      {
        foreach (Config.GroupsRow gr in st.GetGroupsRows() )
          configDataBase.Groups.RemoveGroupsRow(gr);
      }
    }
    public ConvertGLS(NetworkConfig.ImportBLS newBLSColl, Config configDataBase)
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
      removeGroups(configDataBase);
    }
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if(components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      // 
      // ConvertGLS
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(352, 278);
      this.Name = "ConvertGLS";
      this.Text = "ConvertGLS";

    }
    #endregion
  }
}
