//<summary>
//  Title   : Importing blocks from CSV files
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//  20081006 mzbrzezny: implementation of ItemAccessRights and StateTrigger
//    MZbrzezny - 20070615 - created.
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using BaseStation;
using CAS.Lib.RTLib;
using CAS.NetworkConfigLib;
using CAS.Windows.Forms;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NetworkConfig.HMI.Import
{
  class ImportBlockCSV: ImportFunctionRootClass
  {
    #region private fields
    ImportBlockCSVInfo m_ImportBlockCSVInfo;
    CAS.NetworkConfigLib.ComunicationNet m_database;
    string file;
    int tags_added_number;
    ProgressBarWindow pbw;
    #endregion
    #region ImportBlockCSVInfo
    internal class ImportBlockCSVInfo: CAS.Lib.ControlLibrary.ImportFileControll.ImportInfo
    {
      public override string ImportName
      {
        get { return "Import Block CSV"; }
      }
      public override string InitialDirectory
      {
        get
        {
          return AppDomain.CurrentDomain.BaseDirectory;
        }
      }
      /// <summary>
      /// deafult browse filter for the dialog which is used for selecting a file
      /// </summary>
      public override string BrowseFilter
      {
        get
        {
          return "Blocks CSV files (*.CSV)|*.CSV";
        }
      }
      /// <summary>
      /// deafult extension for the dialog which is used for selecting a file
      /// </summary>
      public override string DefaultExt
      {
        get
        {
          return ".CSV";
        }
      }
      /// <summary>
      /// text that is used to show the information about this importing function
      /// </summary>
      public override string InformationText
      {
        get
        {
          return "This import tool is adding tags based on block definition i CSV file."
          + "\r\n Each line format:\r\n"
          + " StationID;TimeScan;Timeout;TimeScanFast;TimeoutFast;Address;DataType;BlockLength"
          + "\r\n It ommits first line";
        }
      }
    }
    #endregion
    #region ImportFunctionRootClass
    protected override void DoTheImport()
    {
      tags_added_number = 0;
      file = CSVManagement.ReadFile( m_ImportBlockCSVInfo.Filename );
      file = CSVManagement.PrepareForCSVProcessing( file );

      pbw = new ProgressBarWindow( new DoWorkEventHandler( MainImportJob ), 0, file.Length, 1 );
      if ( pbw.ShowDialog() != DialogResult.OK )
        AppendToLog( "Cancel was pressed" );
      AppendToLog( "Number of tags added: " + tags_added_number.ToString() );
    }

    private void MainImportJob( object sender, DoWorkEventArgs e )
    {
      BackgroundWorker worker = sender as BackgroundWorker;
      ProgressBarWindow pwb = e.Argument as ProgressBarWindow;
      long StationID = 0;
      ulong TimeScan = 0, Timeout = 0, TimeScanFast = 0, TimOutFast = 0;
      ulong Address = 0;
      byte DataType = 0;
      int length = 0;
      int original_len = file.Length;
      //przegl¹damy tak d³ugo plik jak jest jeszcze jakaœ zawartoœæ
      pwb.SetInformation( "ImportingCSV" );
      while ( file.Length > 0 && !worker.CancellationPending )
      {
        pwb.SetProgressValue( original_len - file.Length );
        try
        {
          StationID = System.Convert.ToUInt32( CSVManagement.GetAndMoveNextElement( ref file ) );
          TimeScan = System.Convert.ToUInt32( CSVManagement.GetAndMoveNextElement( ref file ) );
          Timeout = System.Convert.ToUInt32( CSVManagement.GetAndMoveNextElement( ref file ) );
          TimeScanFast = System.Convert.ToUInt32( CSVManagement.GetAndMoveNextElement( ref file ) );
          TimOutFast = System.Convert.ToUInt32( CSVManagement.GetAndMoveNextElement( ref file ) );
          Address = System.Convert.ToUInt32( CSVManagement.GetAndMoveNextElement( ref file ) );
          DataType = System.Convert.ToByte( CSVManagement.GetAndMoveNextElement( ref file ) );
          length = System.Convert.ToInt32( CSVManagement.GetAndMoveNextElement( ref file ) );
          //odczytalismy wszystkie elememty definiuj¹ce dany blok danych
          //znajdujemy odpowiednia stacje
          ComunicationNet.StationRow stationrow = null;
          try { stationrow = m_database.Station.FindByStationID( StationID ); }
          catch { throw new Exception( "station " + StationID.ToString() + "not found" ); }
          //dodajemy odpowiednia grupe:
          ComunicationNet.GroupsRow grouprow = m_database.Groups.NewGroupsRow();
          grouprow.Name = "GR_" + grouprow.GroupID.ToString() + "_st_" + stationrow.Name;
          grouprow.StationID = StationID;
          grouprow.TimeOut = Timeout;
          grouprow.TimeOutFast = TimOutFast;
          grouprow.TimeScan = TimeScan;
          grouprow.TimeScanFast = TimeScanFast;
          m_database.Groups.AddGroupsRow( grouprow );
          //dodajemy teraz blok
          ComunicationNet.DataBlocksRow DBrow = m_database.DataBlocks.NewDataBlocksRow();
          DBrow.Name = "db" + grouprow.GroupID.ToString() + "_st_" + stationrow.Name;
          DBrow.GroupID = grouprow.GroupID;
          DBrow.Address = Address;
          DBrow.DataType = DataType;
          m_database.DataBlocks.AddDataBlocksRow( DBrow );
          for ( int idx = 0; idx < length; idx++ )
          {
            ComunicationNet.TagsRow TAGrow = m_database.Tags.NewTagsRow();
            TAGrow.Name = stationrow.Name + "/" + DataType.ToString() + "/" + "add" + ( Address + (ulong)idx ).ToString();
            TAGrow.AccessRights = (sbyte)ItemAccessRights.ReadWrite;
            TAGrow.StateTrigger = (sbyte)StateTrigger.None;
            TAGrow.Alarm = false;
            TAGrow.AlarmMask = 0;
            TAGrow.StateMask = 0;
            TAGrow.DatBlockID = DBrow.DatBlockID;
            m_database.Tags.AddTagsRow( TAGrow );
            tags_added_number++;
          }
        }
        catch ( Exception ex )
        {
          AppendToLog( "Error: " + ex.Message + " at \r\n"
              + StationID.ToString() + ","
              + TimeScan.ToString() + ","
              + Timeout.ToString() + ","
              + TimeScanFast.ToString() + ","
              + TimOutFast.ToString() + ","
              + Address.ToString() + ","
              + DataType.ToString() + ","
              + length.ToString() );
        }
      }
    }
    #endregion
    #region creator
    public ImportBlockCSV( CAS.NetworkConfigLib.ComunicationNet database, System.Windows.Forms.Form parrent_form )
      :
      base( parrent_form )
    {
      m_database = database;
      m_ImportBlockCSVInfo = new ImportBlockCSVInfo();
      SetImportInfo( m_ImportBlockCSVInfo );
    }
    #endregion
  }
}
