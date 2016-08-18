//<summary>
//  Title   : Configuration Management
//  System  : Microsoft Visual C# .NET 
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20081203: mzbrzezny: undoredomanager is switched off
//    20081022: mzbrzezny: modification to clear function
//    Tomek Siwecki - 2007-2-19
//    Refomated and some code changes
//    Mariusz Postol - 2006-10-19
//      reformated
//    Maciej Zbrzezny - 2006-09-19
//    created
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using CAS.NetworkConfigLib;
using CAS.Lib.RTLib.Database;

namespace NetworkConfig.HMI
{
  internal delegate void ConfigIOHandler( Form form );
  internal class ConfigurationManagement
  {
    #region Fields
    private static ConfigurationManagement me;
    private static string configFileName;
    #endregion

    #region Properties
    public static string ConfigFileName
    {
      get { return configFileName; }
      set { configFileName = value; }
    }
    #endregion

    #region Constructors
    private ConfigurationManagement() { }
    static ConfigurationManagement()
    {
      me = new ConfigurationManagement();
      // 
      // configDataBase
      // 
      configDataBase = new ComunicationNet();
      configDataBase.DataSetName = "ComunicationNet";
      configDataBase.Locale = new CultureInfo( "en-US" );
      configDataBase.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    }
    #endregion


    #region Methods

    #region public
    internal static ComunicationNet configDataBase;
    internal static void ClearConfig( Form form )
    {
      if ( MessageBox.Show( form, "Clear all datagrids???", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
        ClearConfig_perform( form );
    }
    internal static void SaveDemoProc( Form form )
    {
      MessageBox.Show( Properties.Resources.tx_DemoWriteErr, Properties.Resources.tx_licenseCap, MessageBoxButtons.OK, MessageBoxIcon.Hand );
    }
    internal static void SaveProc( Form form )
    {
      SaveFileDialog saveXMLFileDialog = new SaveFileDialog();
      saveXMLFileDialog.OverwritePrompt = true;
      saveXMLFileDialog.Filter = "XML files|*.xml";
      saveXMLFileDialog.DefaultExt = "xml";
      saveXMLFileDialog.InitialDirectory = CAS.Lib.CodeProtect.InstallContextNames.ApplicationDataPath;
      if ( !string.IsNullOrEmpty( ConfigurationManagement.ConfigFileName ) )
        saveXMLFileDialog.FileName = ConfigurationManagement.ConfigFileName;
      switch ( saveXMLFileDialog.ShowDialog() )
      {
        case DialogResult.OK:
          try
          {
            XML2DataSetIO.writeXMLFile( configDataBase, saveXMLFileDialog.FileName );
            UpdateFormName( form, saveXMLFileDialog.FileName );
            ConfigFileName = saveXMLFileDialog.FileName;
            configDataBase.Channels.AcceptChanges();
            configDataBase.Protocol.AcceptChanges();
            configDataBase.SerialSetings.AcceptChanges();
            configDataBase.Station.AcceptChanges();
            configDataBase.Segments.AcceptChanges();
            configDataBase.Interfaces.AcceptChanges();
            configDataBase.Groups.AcceptChanges();
            configDataBase.Tags.AcceptChanges();
            configDataBase.TagBit.AcceptChanges();
            configDataBase.DataBlocks.AcceptChanges();
          }
          catch ( Exception e )
          {
            MessageBox.Show( "Error", "I cant save file to this location because: " + e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error );
          }
          break;
        default:
          break;
      }
    }
    internal static void ReadConfiguration( Form form )
    {
      OpenFileDialog openFileDialogXMLFile = new OpenFileDialog();
      if ( ( configDataBase.Channels.GetChanges() != null )
        || ( configDataBase.Protocol.GetChanges() != null )
        || ( configDataBase.SerialSetings.GetChanges() != null )
        || ( configDataBase.Segments.GetChanges() != null )
        || ( configDataBase.Station.GetChanges() != null )
        || ( configDataBase.Interfaces.GetChanges() != null )
        || ( configDataBase.Groups.GetChanges() != null )
        || ( configDataBase.Tags.GetChanges() != null )
        || ( configDataBase.TagBit.GetChanges() != null )
        || ( configDataBase.DataBlocks.GetChanges() != null ) )
      {
        if ( MessageBox.Show( form, "Save current data?", "Data changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
          SaveProc( form );
      }
      openFileDialogXMLFile.InitialDirectory = CAS.Lib.CodeProtect.InstallContextNames.ApplicationDataPath;
      openFileDialogXMLFile.Filter = "XML files|*.xml";
      openFileDialogXMLFile.DefaultExt = ".XML";
      switch ( openFileDialogXMLFile.ShowDialog() )
      {
        case DialogResult.OK:
          ClearConfig_perform( form );
#if UNDOREDO
          RTLib.DataBase.UndoRedo.UndoRedoMenager.SuspendLogging();
#endif
          try
          {
            XML2DataSetIO.readXMLFile( configDataBase, openFileDialogXMLFile.FileName );
            //int idx = 0;
            //foreach ( ComunicationNet.DataBlocksRow cr in configDataBase.DataBlocks )
            //{
            //  //cr.DatBlockID = idx++;
            //  foreach ( ComunicationNet.TagsRow ctg in cr.GetTagsRows() )
            //    ctg.DatBlockID = cr.DatBlockID;
            //}
          }
          catch ( Exception e )
          {
            MessageBox.Show( "Error", "I cant load file from this location because: " + e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error );
          }
          //((Button)sender).Enabled = false;
          ConfigFileName = openFileDialogXMLFile.FileName;
          UpdateFormName( form, openFileDialogXMLFile.FileName );
          configDataBase.Channels.AcceptChanges();
          configDataBase.Protocol.AcceptChanges();
          configDataBase.SerialSetings.AcceptChanges();
          configDataBase.Station.AcceptChanges();
          configDataBase.Segments.AcceptChanges();
          configDataBase.Interfaces.AcceptChanges();
          configDataBase.Groups.AcceptChanges();
          configDataBase.Tags.AcceptChanges();
          configDataBase.TagBit.AcceptChanges();
          configDataBase.DataBlocks.AcceptChanges();
#if UNDOREDO
          RTLib.DataBase.UndoRedo.UndoRedoMenager.ClearLog();
#endif
          break;
        default:
          break;
      }
    }

    #endregion //public
    #region private
    private static void UpdateFormName( Form form, string filename )
    {
      form.Text = "Network configuration ";
      if ( !string.IsNullOrEmpty( filename ) )
        form.Text += filename;
    }
    /// <summary>
    /// Clears curent configuration dataset
    /// </summary>
    private static void ClearConfig_perform( Form form )
    {
      configDataBase.Clear();
      UpdateFormName( form, null );
      ConfigFileName = null;
    }
    #endregion

    #endregion
  }
}
