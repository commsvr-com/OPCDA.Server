//_______________________________________________________________
//  Title   : ComServerListDlg
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate:  $
//  $Rev: $
//  $LastChangedBy: $
//  $URL: $
//  $Id:  $
//
//  Copyright (C) 2017, CAS LODZ POLAND.
//  TEL: +48 608 61 98 99 
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using CAS.CommServer.DA.Server.ConfigTool.ServersModel;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CAS.CommServer.DA.Server.ConfigTool
{
  /// <summary>
  /// Class ComServerListDlg = dialog providing basic information about server
  /// </summary>
  /// <seealso cref="System.Windows.Forms.Form" />
  public partial class ComServerListDlg : Form
  {

    #region constructor
    public ComServerListDlg()
    {
      InitializeComponent();
      RegisteredServersRB.Checked = true;
      ServersCTRL.Initialize(CommonDefinitions.CATID_RegisteredDotNetOpcServers);
      m_currentDirectory = Application.StartupPath;
    }
    #endregion

    #region API
    public void ShowDialog(Guid CATID)
    {
      if (CATID == CommonDefinitions.CATID_DotNetOpcServers)
        DotNetServersRB.Checked = true;
      else if (CATID == CommonDefinitions.CATID_DotNetOpcServerWrappers)
        WrappersRB.Checked = true;
      else if (CATID == CommonDefinitions.CATID_RegisteredDotNetOpcServers)
        RegisteredServersRB.Checked = true;
      ServersCTRL.Initialize(CATID);
      base.ShowDialog();
    }
    #endregion

    #region private
    //var
    private string m_currentDirectory;
    //event handles
    private void DotNetServersRB_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        if (DotNetServersRB.Checked)
          ServersCTRL.Initialize(CommonDefinitions.CATID_DotNetOpcServers);
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
          ServersCTRL.Initialize(CommonDefinitions.CATID_DotNetOpcServerWrappers);
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
          ServersCTRL.Initialize(CommonDefinitions.CATID_RegisteredDotNetOpcServers);
      }
      catch (Exception exception)
      {
        GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
      }
    }
    //menu handles
    private void RegisterServerMI_Click(object sender, EventArgs e)
    {
      try
      {
        if (new RegisterServerDlg().ShowDialog(null) != null)
          if (RegisteredServersRB.Checked)
            ServersCTRL.Initialize(CommonDefinitions.CATID_RegisteredDotNetOpcServers);
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

        dialog.CheckFileExists = false;
        dialog.CheckPathExists = true;
        dialog.DefaultExt = ".xml";
        dialog.Filter = "Registration Files (*.xml)|*.xml|All Files (*.*)|*.*";
        dialog.ValidateNames = true;
        dialog.Title = "Export Registered Servers";
        dialog.RestoreDirectory = true;
        dialog.AddExtension = true;
        dialog.FileName = "";
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
    #endregion

  }
}