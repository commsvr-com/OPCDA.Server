//<summary>
//  Title   : ImportTagsForSimulation
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//  20081006 mzbrzezny: implementation of ItemAccessRights and StateTrigger
//    mzbrzezny - 2007-08-03:
//    created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.ComponentModel;
using CAS.Lib.RTLib;
using CAS.NetworkConfigLib;

namespace NetworkConfig.HMI.Import
{
  /// <summary>
  /// Summary description for ImportTagsForSimulation.
  /// </summary>
  internal class ImportTagsForSimulation: ImportFunctionRootClass
  {
    #region ImportTagsForSimulationInfo
    internal class ImportTagsForSimulationInfo: CAS.Lib.ControlLibrary.ImportFileControll.ImportInfo
    {
      #region private
      uint m_stationID = 0;
      uint m_address = 0;
      uint m_datatype = 0;
      #endregion
      public override string ImportName
      {
        get { return "Import Tags for simulation"; }
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
          return "Tags for simulation TXT file (*.TXT)|*.TXT";
        }
      }
      /// <summary>
      /// deafult extension for the dialog which is used for selecting a file
      /// </summary>
      public override string DefaultExt
      {
        get
        {
          return ".TXT";
        }
      }
      /// <summary>
      /// text that is used to show the information about this importing function
      /// </summary>
      public override string InformationText
      {
        get
        {
          return "This function immport tags from file - each line in file is one tag name";
        }
      }
      [
      BrowsableAttribute( true ),
      CategoryAttribute( "Settings" ),
      DescriptionAttribute( "ID of the station" )
      ]
      public uint StationID
      {
        get
        {
          return m_stationID;
        }
        set
        {
          m_stationID = value;
        }
      }
      [
      BrowsableAttribute( true ),
      CategoryAttribute( "Settings" ),
      DescriptionAttribute( "Address of this DataBlock" )
      ]
      public uint Address
      {
        get
        {
          return m_address;
        }
        set
        {
          m_address = value;
        }
      }
      [
      BrowsableAttribute( true ),
      CategoryAttribute( "Settings" ),
      DescriptionAttribute( "Data type of this DataBlock" )
      ]
      public uint DataType
      {
        get
        {
          return m_datatype;
        }
        set
        {
          m_datatype = value;
        }
      }

    }
    #endregion
    #region private
    private CAS.NetworkConfigLib.ComunicationNet m_database;
    private ImportTagsForSimulationInfo m_ImportTagsForSimulationInfo;
    #endregion
    #region ImportFunctionRootClass
    protected override void DoTheImport()
    {
      #region IMPORT
      //robimy jakies importowanie:
      long stationid = m_ImportTagsForSimulationInfo.StationID;
      ulong address = m_ImportTagsForSimulationInfo.Address;
      ulong datatype = m_ImportTagsForSimulationInfo.DataType;
      string filename = m_ImportTagsForSimulationInfo.Filename;
      int changes_number = 0;
      //dodajemy grupe:
      ComunicationNet.GroupsRow grow = HMI.ConfigurationManagement.configDataBase.Groups.NewGroupsRow();
      //grow.GroupID = groupid;
      grow.Name = "SimulationGR_" + grow.GroupID.ToString();
      grow.StationID = stationid;
      grow.TimeScan = 1000;
      grow.TimeScanFast = 1000;
      grow.TimeOut = 10000;
      grow.TimeOutFast = 10000;
      HMI.ConfigurationManagement.configDataBase.Groups.AddGroupsRow( grow );
      //dodajemy datablock
      ComunicationNet.DataBlocksRow drow = HMI.ConfigurationManagement.configDataBase.DataBlocks.NewDataBlocksRow();
      drow.GroupID = grow.GroupID;
      //drow.SatationID = stationid;
      drow.Name = "SimulationDB_" + grow.GroupID.ToString();
      drow.Address = address;
      drow.DataType = datatype;
      HMI.ConfigurationManagement.configDataBase.DataBlocks.AddDataBlocksRow( drow );
      //teraz otworzymy plik i dodamy wszystkie tagi:
      System.IO.StreamReader plik = new System.IO.StreamReader( filename, System.Text.Encoding.Default );
      string plikzawartosc = plik.ReadToEnd();
      plik.Close();
      string Tagname = "-- unknowname --";
      while ( plikzawartosc.Length > 0 )
      {
        try
        {
          int pos = plikzawartosc.IndexOf( "\r\n" );
          if ( pos < 0 )
            pos = plikzawartosc.Length;
          Tagname = plikzawartosc.Substring( 0, pos );
          plikzawartosc = plikzawartosc.Remove( 0, pos + 2 );
          //dodajemy taga:
          ComunicationNet.TagsRow trow = HMI.ConfigurationManagement.configDataBase.Tags.NewTagsRow();
          trow.Name = Tagname;
          trow.AccessRights = (sbyte)ItemAccessRights.ReadWrite;
          trow.DatBlockID = drow.DatBlockID;
          trow.AlarmMask = 0;
          trow.StateMask = 0;
          trow.StateTrigger = (sbyte)StateTrigger.None;
          trow.Alarm = false;
          HMI.ConfigurationManagement.configDataBase.Tags.AddTagsRow( trow );
          changes_number++;
        }
        catch (
Exception
#if DEBUG
 ex
#endif
 )
        {
          AppendToLog( "problem with tag:" + Tagname + " :"
#if DEBUG
 + ex.Message.ToString()
#endif
 );
        }
      }
      #endregion IMPORT
      AppendToLog( "Number of changed tags: " + changes_number.ToString() );
    }

    #endregion
    #region creator
    public ImportTagsForSimulation( CAS.NetworkConfigLib.ComunicationNet database, System.Windows.Forms.Form parrent_form )
      : base( parrent_form )
    {
      m_database = database;
      m_ImportTagsForSimulationInfo = new ImportTagsForSimulationInfo();
      SetImportInfo( m_ImportTagsForSimulationInfo );
    }
    #endregion
  }
}
