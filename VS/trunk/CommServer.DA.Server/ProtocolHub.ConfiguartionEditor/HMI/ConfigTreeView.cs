// <summary>
//  Title   : Config tree view for Network Config
//  Author  : Mariusz Postol
//  System  : Microsoft Visual C# .NET
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20081203: mzbrzezny: ConfigTreeView: fixed: issue with save button, undoredomanager is switched off, database.accept, rejectchanges  functionality is used when enforce constrain fails
//    20081105: mzbrzezny: ConfigTreeView: fixed: sometimes exception after cleanup after unfinished add operation is added
//    20081007: mzbrzezny: cleanup after unfinished add operation is added
//    20081006: mzbrzezny: constrains are turned off while trying to add new object. clicked object on tree is selected automaticly
//    MPostol 15-03-2007:
//      Wydzielilem plik designer, poniewa¿ znikala inicjacja zmiennej m_PNavigator, ale to nic nie dalo.
//    Tomasz Siwecki 12.2006 - 2.2007
//    implementation
//    Mariusz Schabowski - 2006-09-20
//    small changes 
//    Mariusz Postol - 2006-09-19
//    adapted to new view
//    Mariusz Postol - 2004
//    created
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: 42' 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
// </summary>

using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CAS.Lib.CodeProtect;
using CAS.Lib.CodeProtect.LicenseDsc;
using CAS.Lib.CodeProtect.Properties;
using CAS.Lib.ControlLibrary;
using CAS.Lib.RTLib.Processes;
using NetworkConfig.HMI.Exceptions;
using NetworkConfig.Properties;
using System.Diagnostics;
using CAS.Lib.CodeProtect.Controls;
using System.Resources;
using CAS.Lib.RTLib.Database;

namespace NetworkConfig.HMI
{
  /// <summary>
  /// Config tree view for Network Config
  /// </summary>
  [LicenseProvider(typeof(CodeProtectLP))]
  [GuidAttribute("577750FC-CF14-406f-B367-41CE15563265")]
  internal partial class ConfigTreeView : Form
  {
    #region private
    private static CAS.NetworkConfigLib.ComunicationNet m_configDataBase;
    private readonly bool m_DemoVer = true;
    private LicenseFile m_license = null;
    private void ShowAboutDialog()
    {
      string usr = null;
      if (m_license != null)
        usr = m_license.User.Organization + "[" + m_license.User.Email + "]";
      Assembly cMyAss = Assembly.GetEntryAssembly();
      using (AboutForm cAboutForm = new CAS.Lib.ControlLibrary.AboutForm(null, usr, cMyAss))
      {
        cAboutForm.ShowDialog(this);
      }
    }

    private void UpdateTagNumberInfo()
    {
      this.toolStripStatusLabel_tagamount.Text = "No. of Tags: " + m_configDataBase.Tags.Count.ToString();
    }
    private bool m_UAPackage = false;
    #region ConfigIOHandlers
    ConfigIOHandler fileread;
    ConfigIOHandler filesave;
    ConfigIOHandler fileclear;
    #endregion
#if UNDOREDO
    #region UndoRedo fields
    private bool columnErrorOnChange;
    private bool rowErrorOnChange;
    private bool revertFieldValue;
    private RTLib.DataBase.UndoRedo.UndoRedoMenager tlog;
    private DataTable dt;
    private int undoRow = 0;
    #endregion
#endif
    #endregion
    #region Constructor
    internal ConfigTreeView
      (CAS.NetworkConfigLib.ComunicationNet configDataBase, ConfigIOHandler _fileread, ConfigIOHandler _filesave,
        ConfigIOHandler _fileclear, bool AdvanceMenu
      )
    {
      License lic = null;
      LicenseManager.IsValid(this.GetType(), this, out lic);
      m_license = lic as LicenseFile;
      if (m_license == null)
        MessageBox.Show( CAS.Lib.CodeProtect.Properties.Resources.Tx_LicNoFileErr, CAS.Lib.CodeProtect.Properties.Resources.Tx_LicCap, MessageBoxButtons.OK, MessageBoxIcon.Hand );
      else
      {
        if (m_license.FailureReason != String.Empty)
          ScrollableMessageBox.Instance.Show( m_license.FailureReason, CAS.Lib.CodeProtect.Properties.Resources.Tx_LicCap, MessageBoxButtons.OK, MessageBoxIcon.Hand );
        else
          m_DemoVer = false;
      }
      if (m_DemoVer)
      {
        MessageBox.Show
          ( CAS.Lib.CodeProtect.Properties.Resources.Tx_LicDemoModeInfo, CAS.Lib.CodeProtect.Properties.Resources.Tx_LicCap, MessageBoxButtons.OK, MessageBoxIcon.Information );
        filesave = new ConfigIOHandler(ConfigurationManagement.SaveDemoProc);
      }
      else
      {
        m_UAPackage = m_license.Product.ShortName.ToLower().Contains( "ua" );
        filesave = _filesave;
      }
      m_configDataBase = configDataBase;
      fileread = _fileread;
      fileclear = _fileclear;
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
      //navigation and search toolstrip initialisation
      this.backForwardTreViewToolStrip1.TreeView = m_PNavigator.cn_TreeView;
      this.searchTreeViewToolStrip1.TreeView = m_PNavigator.cn_TreeView;
      //other
      TreeBuilder.CreateTree(m_configDataBase, m_PNavigator.cn_TreeView);
      IntTreeViewCheckBoxes();
#if UNDOREDO
      tlog = new RTLib.DataBase.UndoRedo.UndoRedoMenager();
      tlog.SourceDataSet = (DataSet)m_configDataBase;
      tlog.RedoUndoOperationAdding += new RTLib.DataBase.UndoRedo.UndoRedoMenager.OperationDlgt( OnRedoUndoOperationAdding );
      tlog.RedoUndoOperationAdded += new RTLib.DataBase.UndoRedo.UndoRedoMenager.OperationDlgt( OnRedoUndoOperationAdded );
      foreach ( DataTable dt in m_configDataBase.Tables )
      {
        dt.ColumnChanging += new DataColumnChangeEventHandler( OnColumnChanging );
        dt.RowChanging += new DataRowChangeEventHandler( OnRowChanging );
      }
#else
      undoCtrlZToolStripMenuItem.Enabled = false;
      redoCtrlYToolStripMenuItem.Enabled = false;
#endif
      foreach (DataTable dt in m_configDataBase.Tables)
      {
        dt.RowChanged += new DataRowChangeEventHandler(configDataBaseDataTable_Changed);
      }
      m_PNavigator.cn_TreeView.AfterSelect += new TreeViewEventHandler(cn_TreeView_AfterSelect);
      m_PNavigator.cn_TreeView.KeyDown += new KeyEventHandler(cn_TreeView_KeyDown);
      m_PNavigator.cn_TreeView.MouseDown += new MouseEventHandler(cn_TreeView_MouseDown);
      tsbUndo.Enabled = false;
      tsbRedo.Enabled = false;
      saveToolStripButton.Enabled = false;
      saveToolStripMenuItem.Enabled = false;
      ActionBase.SetCommonBusControl = cm_commonBusControl;
      if (!AdvanceMenu)
      {
        toolsToolStripMenuItem_Tools.Enabled = false;
        toolsToolStripMenuItem_Tools.Visible = false;
      }
    }
    void configDataBaseDataTable_Changed(object sender, DataRowChangeEventArgs e)
    {
      if ( !saveToolStripButton.Enabled )
      {
        saveToolStripMenuItem.Enabled = true;
        saveToolStripButton.Enabled = true;
      }
    }
    void cn_TreeView_MouseDown(object sender, MouseEventArgs e)
    {
      m_PNavigator.cn_TreeView.SelectedNode = m_PNavigator.cn_TreeView.GetNodeAt(e.X, e.Y);
    }
    #endregion
    #region Methods
    #region TreeView type changing
    private void StationView()
    {
      TreeBuilder.CreateTree(m_configDataBase, m_PNavigator.cn_TreeView, TreeBuilderTreeViewType.Stations);
      channelsAndStationsToolStripMenuItem1.Checked = false;
      channelsToolStripMenuItem.Checked = false;
      stationsToolStripMenuItem.Checked = true;
      stationToolStripMenuItem.Checked = true;
      channelToolStripMenuItem.Checked = false;
      channelsAndStationsToolStripMenuItem.Checked = false;

    }
    private void ChannelView()
    {
      TreeBuilder.CreateTree(m_configDataBase, m_PNavigator.cn_TreeView, TreeBuilderTreeViewType.Channels);
      channelsAndStationsToolStripMenuItem1.Checked = false;
      channelsToolStripMenuItem.Checked = true;
      stationsToolStripMenuItem.Checked = false;
      channelToolStripMenuItem.Checked = true;
      stationToolStripMenuItem.Checked = false;
      channelsAndStationsToolStripMenuItem.Checked = false;
    }
    private void ChannelAndStationView()
    {
      TreeBuilder.CreateTree(m_configDataBase, m_PNavigator.cn_TreeView, TreeBuilderTreeViewType.StationsAndChannels);
      channelsAndStationsToolStripMenuItem1.Checked = true;
      channelsToolStripMenuItem.Checked = false;
      stationsToolStripMenuItem.Checked = false;
      channelToolStripMenuItem.Checked = false;
      stationToolStripMenuItem.Checked = false;
      channelsAndStationsToolStripMenuItem.Checked = true;
    }
    private void IntTreeViewCheckBoxes()
    {
      switch (TreeBuilder.TreeViewType)
      {
        case TreeBuilderTreeViewType.StationsAndChannels:
          channelsAndStationsToolStripMenuItem1.Checked = true;
          channelsToolStripMenuItem.Checked = false;
          stationsToolStripMenuItem.Checked = false;
          channelToolStripMenuItem.Checked = false;
          stationToolStripMenuItem.Checked = false;
          channelsAndStationsToolStripMenuItem.Checked = true;
          break;
        case TreeBuilderTreeViewType.Stations:
          channelsAndStationsToolStripMenuItem1.Checked = false;
          channelsToolStripMenuItem.Checked = false;
          stationsToolStripMenuItem.Checked = true;
          stationToolStripMenuItem.Checked = true;
          channelToolStripMenuItem.Checked = false;
          channelsAndStationsToolStripMenuItem.Checked = false;
          break;
        case TreeBuilderTreeViewType.Channels:
          channelsAndStationsToolStripMenuItem1.Checked = false;
          channelsToolStripMenuItem.Checked = true;
          stationsToolStripMenuItem.Checked = false;
          channelToolStripMenuItem.Checked = true;
          stationToolStripMenuItem.Checked = false;
          channelsAndStationsToolStripMenuItem.Checked = false;
          break;
      }
    }
    #endregion
    #region Open/Save
    private void Open()
    {
      fileread(this);
      m_PNavigator.Refresh(m_configDataBase);
      saveToolStripButton.Enabled = false;
      saveToolStripMenuItem.Enabled = false;
      tsbRedo.Enabled = false;
      tsbUndo.Enabled = false;
      UpdateTagNumberInfo();
    }
    private void save()
    {
      if (m_DemoVer)
      {
        MessageBox.Show
          (Properties.Resources.tx_DemoWriteErr, Properties.Resources.tx_licenseCap, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      if (ConfigurationManagement.ConfigFileName == null)
        filesave(this);
      else
        try
        {
          XML2DataSetIO.writeXMLFile(m_configDataBase, ConfigurationManagement.ConfigFileName);
        }
        catch (Exception e)
        {
          MessageBox.Show
            (Properties.Resources.tx_SaveErr + e.Message, Properties.Resources.tx_IOErrCap, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion
    #region Add/Delate
    private void add()
    {
      TreeNode tn = m_PNavigator.cn_TreeView.SelectedNode;
      if (tn == null)
        return;
      IAction currAct = (IAction)m_PNavigator.cn_TreeView.SelectedNode.Tag;
      if (!currAct.CanCreateChild)
        return;
      if (currAct is SegmentsRowWrapper)
      {
        IAction newAct = currAct.CreateNewChildObject();
        AddInterfaceAndStation fm_AddInterface = new AddInterfaceAndStation(m_configDataBase, newAct);
        if (fm_AddInterface.ShowDialog(this) != DialogResult.OK)
          return;
      }
      else
      {
#if UNDOREDO
        RTLib.DataBase.UndoRedo.UndoRedoMenager.BeginTransaction();
#endif
        try
        {
          if (!m_configDataBase.EnforceConstraints)
            MessageBox.Show(Properties.Resources.tx_configDataBaseEnforceConstraints_false, Properties.Resources.tx_configtreeview_error_messagebox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
          m_configDataBase.AcceptChanges();
          m_configDataBase.EnforceConstraints = false;
#if UNDOREDO
          RTLib.DataBase.UndoRedo.UndoRedoMenager.BeginTransaction();
#endif
          IAction newAct = currAct.CreateNewChildObject();
          AddObject<IAction> fm_AddObj = new AddObject<IAction>();
          fm_AddObj.Text = "Add new object for: " + currAct;
          fm_AddObj.Object = newAct;
          bool ok;
          do
          {
            ok = true;
            if (fm_AddObj.ShowDialog(this) != DialogResult.OK)
            {
              //deleteLastTransaction();
              newAct.AddUnfinishedCleanup();
              break;
            }
            try { newAct.AddObjectToTable(); }
            catch (Exception ex)
            {
              MessageBox.Show
                ("I cannot add new object to the configuration becase: " + ex.Message,
                Properties.Resources.tx_configtreeview_error_messagebox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
              ok = false;
            }
          }
          while (!ok);
#if UNDOREDO
          RTLib.DataBase.UndoRedo.UndoRedoMenager.EndTransaction();
#endif
          try { m_configDataBase.EnforceConstraints = true; }
          catch (Exception ex)
          {
            MessageBox.Show
              ("I cannot add new object to the configuration becase: " + ex.Message,
              Properties.Resources.tx_configtreeview_error_messagebox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            m_configDataBase.RejectChanges();
            m_configDataBase.EnforceConstraints = true;
          }
        }
        catch (CreateChildObjectException ex)
        {
          MessageBox.Show(ex.Message,
            Properties.Resources.tx_configtreeview_error_messagebox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
          MessageBox.Show("I cannot add new object to the configuration becase: " + ex.Message,
            Properties.Resources.tx_configtreeview_error_messagebox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
#if UNDOREDO
          RTLib.DataBase.UndoRedo.UndoRedoMenager.EndTransaction();
#endif
          m_configDataBase.EnforceConstraints = true;
        }
      }
      ((IAction)m_PNavigator.cn_TreeView.SelectedNode.Tag).CreateNodes();
      //m_PNavigator.cn_TreeView.SelectedNode = 
      tn.Expand();
      UpdateTagNumberInfo();
    }
    #endregion
    #region UndoRedo
#if UNDOREDO
    private void deleteLastTransaction()
    {
      undoRow = tlog.Delete( undoRow );
      if ( undoRow < 0 )
      {
        tsbUndo.Enabled = false;
      }
    }
    private void Undo()
    {
      if ( undoRow > 0 )
      {
        undoRow = tlog.Revert( undoRow );
        if ( undoRow < 0 )
          tsbUndo.Enabled = false;
        tsbRedo.Enabled = true;
        m_PNavigator.Refresh( m_configDataBase );
      }
    }
    private void Redo()
    {
      int idx = undoRow + 1;
      if ( idx < tlog.Log.Count )
      {
        undoRow = tlog.Apply( undoRow + 1 );
        if ( undoRow >= tlog.Log.Count - 1 )
        {
          tsbRedo.Enabled = false;
        }
        tsbUndo.Enabled = true;
        m_PNavigator.Refresh( m_configDataBase );
      }
    }
#endif
    #endregion
    #endregion
    #region EventHandlers
    #region Keyboard shortcuts
    void cn_TreeView_KeyDown(object sender, KeyEventArgs e)
    {
      // if Ctrl+C
      switch (e.KeyData)
      {
        case (Keys.C | Keys.Control):
          m_PNavigator.Copy();
          break;
        case (Keys.V | Keys.Control):
          m_PNavigator.Paste();
          break;
        case (Keys.X | Keys.Control):
          m_PNavigator.Cut();
          break;
        case (Keys.Delete):
          m_PNavigator.Delate();
          break;
#if UNDOREDO
       case ( Keys.Z | Keys.Control ):
          Undo();
          break;
        case ( Keys.Y | Keys.Control ):
          Redo();
          break;
#endif
        case (Keys.S | Keys.Control):
          bwSave.RunWorkerAsync();
          break;
        case (Keys.O | Keys.Control):
          Open();
          saveToolStripButton.Enabled = false;
          saveToolStripMenuItem.Enabled = false;
          break;
        default:
          base.OnKeyDown(e);
          break;
      }
    }
    #endregion
    #region Hidding buttons
    void cn_TreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (m_PNavigator.cn_TreeView.SelectedNode == null)
      {
        pasteToolStripMenuItem.Enabled = false;
        copyToolStripMenuItem.Enabled = false;
        deleteToolStripMenuItem.Enabled = false;
        cutToolStripMenuItem.Enabled = false;
        return;
      }
      IAction cActn = m_PNavigator.cn_TreeView.SelectedNode.Tag as IAction;
      bool flagCanBePasted;
      flagCanBePasted = m_PNavigator.CanBePasted();
      //Paste buttons
      if (flagCanBePasted)
      {
        pasteToolStripButton.Enabled = true;
        pasteToolStripMenuItem.Enabled = true;
        pasteToolStripMenuItem.Text = String.Format("Paste: {0}", m_PNavigator.GetPasteString);
      }
      else
      {
        pasteToolStripButton.Enabled = false;
        pasteToolStripMenuItem.Text = "Paste";
        pasteToolStripMenuItem.Enabled = false;
      }
      //Copy toolstrip button
      if (cActn.CanBeCopied)
      {
        copyToolStripButton.Enabled = true;
        copyToolStripMenuItem.Enabled = true;
      }
      else
      {
        copyToolStripButton.Enabled = false;
        copyToolStripMenuItem.Text = "Copy";
        copyToolStripMenuItem.Enabled = false;
      }
      //Cut toolstrip button
      if (cActn.CanBeMoved())
      {
        cutToolStripButton.Enabled = true;
        cutToolStripMenuItem.Enabled = true;
      }
      else
      {
        cutToolStripButton.Enabled = false;
        cutToolStripMenuItem.Enabled = false;
      }
      //Delete toolstrip button
      if (cActn.CanBeDeleted)
      {
        deleteToolStripMenuItem.Enabled = true;
      }
      else
        deleteToolStripMenuItem.Enabled = false;
    }


    #endregion
#if UNDOREDO
    #region UndoRedo handlers
    void OnColumnChanging( object sender, DataColumnChangeEventArgs e )
    {
      saveToolStripButton.Enabled = true;
      saveToolStripMenuItem1.Enabled = true;
      saveToolStripMenuItem.Enabled = true;
      if ( columnErrorOnChange )
      {
        e.Row.SetColumnError( e.Column, "Column Error" );

        if ( revertFieldValue )
        {
          e.ProposedValue = e.Row[ e.Column ];
        }
      }
      else
      {
        e.Row.SetColumnError( e.Column, null );
      }
    }

    void OnRowChanging( object sender, DataRowChangeEventArgs e )
    {
      saveToolStripButton.Enabled = true;
      saveToolStripMenuItem1.Enabled = true;
      saveToolStripMenuItem.Enabled = true;
      if ( rowErrorOnChange )
      {
        e.Row.RowError = "Row Error";
      }
      else
      {
        e.Row.RowError = null;
      }
    }

    void OnRedoUndoOperationAdding( object sender, RTLib.DataBase.UndoRedo.UndoRedoEventArgs e )
    {
      saveToolStripButton.Enabled = true;
      saveToolStripMenuItem1.Enabled = true;
      saveToolStripMenuItem.Enabled = true;
      tsbRedo.Enabled = false;

      if ( undoRow < tlog.Log.Count - 1 )
      {
        tlog.Log.RemoveRange( undoRow + 1, tlog.Log.Count - ( undoRow + 1 ) );
        tlog.AcceptChanges();
      }
    }

    void OnRedoUndoOperationAdded( object sender, RTLib.DataBase.UndoRedo.UndoRedoEventArgs e )
    {
      undoRow = tlog.Log.Count - 1;
      tsbUndo.Enabled = true;
    }


    void OnAcceptChanges( object sender, EventArgs e )
    {
      tlog.AcceptChanges();
    }

    void OnRejectChanges( object sender, EventArgs e )
    {
      while ( undoRow < tlog.Log.Count - 1 )
      {
        tsbRedo_Click( sender, e );
      }

      tlog.RejectChanges();
      undoRow = tlog.Log.Count - 1;
    }

    void OnClear( object sender, EventArgs e )
    {
      dt.Clear();
    }

    void OnCollect( object sender, EventArgs e )
    {
      tlog.CollectUncommittedRows();
    }
    #endregion
#endif
    #region Ather control eventhandler
    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Open();
    }
    private void openToolStripButton_Click(object sender, EventArgs e)
    {
      Open();
    }
    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      filesave(this);
      saveToolStripButton.Enabled = false;
      saveToolStripMenuItem.Enabled = false;
    }
    private void saveToolStripMenuItem_Click( object sender, EventArgs e )
    {
      save();
      saveToolStripButton.Enabled = false;
      saveToolStripMenuItem.Enabled = false;
    } 
    private void saveToolStripButton_Click(object sender, EventArgs e)
    {
      save();
      saveToolStripButton.Enabled = false;
      saveToolStripMenuItem.Enabled = false;
    }
    private void clearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      fileclear(this);
      m_PNavigator.Refresh(m_configDataBase);
    }
    private void newToolStripButton_Click(object sender, EventArgs e)
    {
      fileclear(this);
      m_PNavigator.Refresh(m_configDataBase);
    }
    private void stationToolStripMenuItem_Click(object sender, EventArgs e)
    {
      StationView();
    }
    private void cannelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ChannelView();
    }
    private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      m_PNavigator.Refresh(m_configDataBase);
      UpdateTagNumberInfo();
    }
    private void toolStripButton_refresh_Click(object sender, EventArgs e)
    {
      m_PNavigator.Refresh(m_configDataBase);
      UpdateTagNumberInfo();
    }
    private void addToolStripMenuItem_Click(object sender, EventArgs e)
    {
      add();
    }
    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }
    private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_PNavigator.Delate();
    }
    private void tsbUndo_Click(object sender, EventArgs e)
    {
#if UNDOREDO
      Undo();
#else
      MessageBox.Show("This functionality is not yet implemented");
#endif
    }
    private void tsbRedo_Click(object sender, EventArgs e)
    {
#if UNDOREDO
      Redo();
#else
      MessageBox.Show("This functionality is not yet implemented");
#endif
    }
    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_PNavigator.Copy();
    }
    private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_PNavigator.Paste();
    }
    private void cutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_PNavigator.Cut();
    }
    private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TreeNode tn = m_PNavigator.cn_TreeView.SelectedNode;
      if (tn != null)
        tn.ExpandAll();
    }
    private void channelsAndStationsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ChannelAndStationView();
    }
    private void cutToolStripButton_Click(object sender, EventArgs e)
    {
      m_PNavigator.Cut();
    }
    private void copyToolStripButton_Click(object sender, EventArgs e)
    {
      m_PNavigator.Copy();
    }
    private void pasteToolStripButton_Click(object sender, EventArgs e)
    {
      m_PNavigator.Paste();
    }
    private void bwSave_DoWork(object sender, DoWorkEventArgs e)
    {
      if (!m_DemoVer)
        save();
    }
    private void bwSave_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      toolStripProgressBar1.ProgressBar.Value = e.ProgressPercentage;
    }
    private void bwSave_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      m_configDataBase.Channels.AcceptChanges();
      m_configDataBase.Protocol.AcceptChanges();
      m_configDataBase.SerialSetings.AcceptChanges();
      m_configDataBase.Station.AcceptChanges();
      m_configDataBase.Segments.AcceptChanges();
      m_configDataBase.Interfaces.AcceptChanges();
      m_configDataBase.Groups.AcceptChanges();
      m_configDataBase.Tags.AcceptChanges();
      m_configDataBase.TagBit.AcceptChanges();
      m_configDataBase.DataBlocks.AcceptChanges();
    }
    private void channelsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ChannelView();
    }
    private void channelsAndStationsToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      ChannelAndStationView();
    }
    private void stationsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      StationView();
    }
    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ShowAboutDialog();
    }
    private void undoCtrlZToolStripMenuItem_Click(object sender, EventArgs e)
    {
#if UNDOREDO
      Undo();
#else
      MessageBox.Show("This functionality is not yet implemented");
#endif
    }
    private void redoCtrlYToolStripMenuItem_Click(object sender, EventArgs e)
    {
#if UNDOREDO
      Redo();
#else
      MessageBox.Show("This functionality is not yet implemented");
#endif
    }
    private void cutCtrlXToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_PNavigator.Cut();
    }
    private void copyCtrlCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_PNavigator.Copy();
    }
    private void pasteCtrlVToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_PNavigator.Paste();
    }
    private void deleteDelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_PNavigator.Delate();
    }
    private void m_helpToolStripButton_Click(object sender, EventArgs e)
    {
      if (m_UAPackage)
        System.Diagnostics.Process.Start(Settings.Default.HelpDocumentation);
      else
        Help.ShowHelp( this, Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "help\\CommServerOPCHelp.chm" ), HelpNavigator.TableOfContents );
    }
    #endregion

    private void advanceToolStripMenuItem_Click(object sender, EventArgs e)
    {
      new AdvancedFormNetworkConfig(m_configDataBase, fileread, filesave, fileclear).ShowDialog(this);
    }

    private void sBLSToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      new NetworkConfig.HMI.Import.ImportBLS(m_configDataBase, this).Import();
    }

    private void tagbloksToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      new NetworkConfig.HMI.Import.ImportBlockCSV(m_configDataBase, this).Import();
    }

    private void tagBitToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      new NetworkConfig.HMI.Import.ImportTagBits(m_configDataBase, this).Import();
    }

    private void tagToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      new NetworkConfig.HMI.Import.ImportTagMappings(m_configDataBase, this).Import();
    }

    private void scanSettingsToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      new NetworkConfig.HMI.Import.ImportScanSettings(m_configDataBase, this).Import();
    }

    private void tagsForSimulationToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      new NetworkConfig.HMI.Import.ImportTagsForSimulation(m_configDataBase, this).Import();
    }

    private void xBUSMeasureToolStripMenuItem_Click(object sender, EventArgs e)
    {
      StartAppAsync("CAS.DPDiagnostics.exe", "XBUS measurement (Data Provider Diagnostic tool)");
    }
    private void dCOMConfiguratorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //dcomcnfg
      StartAppAsync("dcomcnfg", "DCOM configuration console");
    }
    private static void StartAppAsync(string appname, string longappname)
    {
      RunMethodAsynchronously runasync = new RunMethodAsynchronously(delegate(object[] o)
      {
        try
        {
          System.Diagnostics.Process.Start(appname);
        }
        catch (Exception ex)
        {
          MessageBox.Show("Cannot start the " + longappname + " :" + ex.Message, appname, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        };
      }
      );
      runasync.RunAsync();
    }

    private void licenseInformationToolStripMenuItem_Click( object sender, EventArgs e )
    {
      string usr = null;
      if ( m_license != null )
        usr = m_license.User.Organization + "[" + m_license.User.Email + "]";
      Assembly cMyAss = Assembly.GetEntryAssembly();
      using ( LicenseForm cAboutForm = new CAS.Lib.ControlLibrary.LicenseForm( null, usr, cMyAss ) )
      {
        using ( Licences cLicDial = new Licences() )
        {
          cAboutForm.SetAdditionalControl = cLicDial;
          cAboutForm.ShowDialog( this );
        }
      }
    }

    private void oToolStripMenuItem_Click( object sender, EventArgs e )
    {
      string path = CAS.Lib.CodeProtect.InstallContextNames.ApplicationDataPath + "\\log";
      try
      {
        using ( Process process = Process.Start( @path ) ) { }
      }
      catch ( Win32Exception )
      {
        MessageBox.Show( "No Log folder exists under this link: " + path + " You can create this folder yourself.", "No Log folder !", MessageBoxButtons.OK, MessageBoxIcon.Error );
        return;
      }
      catch ( Exception )
      {
        MessageBox.Show( "An error during opening a log folder occurs and the log folder cannot be open", "Problem with log folder !", MessageBoxButtons.OK, MessageBoxIcon.Error );
        return;
      }
    }

    private void enterTheUnlockCodeToolStripMenuItem_Click( object sender, EventArgs e )
    {
      using ( UlockKeyDialog dialog = new UlockKeyDialog() )
      {
        dialog.ShowDialog();
      }
    }

    private ResourceManager Getter()
    {
      return CAS.Lib.CommServer.Properties.Resources.ResourceManager;
    }

    #region Thinks i dont use (commented)
    //TS: Do czego to niby mialo sluzyc ????????????????
    // Updates all child tree nodes recursively.
    //private void CheckAllChildNodes( TreeNode treeNode, bool nodeChecked )
    //{
    //  foreach ( TreeNode node in treeNode.Nodes )
    //  {
    //    node.Checked = nodeChecked;
    //    if ( node.Nodes.Count > 0 )
    //    {
    //      // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
    //      this.CheckAllChildNodes( node, nodeChecked );
    //    }
    //  }
    //}
    //// NOTE   This code can be added to the BeforeCheck event handler instead of the AfterCheck event.
    //// After a tree node's Checked property is changed, all its child nodes are updated to the same value.
    //private void CheckChanged( object sender, System.Windows.Forms.TreeViewEventArgs e )
    //{
    //  // The code only executes if the user caused the checked state to change.
    //  if ( e.Action != TreeViewAction.Unknown )
    //  {
    //    if ( e.Node.Nodes.Count > 0 )
    //    {
    //      //Calls the CheckAllChildNodes method, passing in the current 
    //      //Checked value of the TreeNode whose checked state changed.
    //      this.CheckAllChildNodes( e.Node, e.Node.Checked );
    //    }
    //  }
    //}
    #endregion
    #endregion
  }
}
