//<summary>
//  Title   : ImportScanSettings
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//  20081006 mzbrzezny: implementation of ItemAccessRights and StateTrigger
//    mzbrzezny - 2007-08-10:
//    modified to use CSV management class
//    mzbrzezny - 2007-08-03:
//    created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http://www.cas.eu
//</summary>

using System;
using BaseStation;
using CAS.Lib.RTLib;
using CAS.NetworkConfigLib;

namespace NetworkConfig.HMI.Import
{
  /// <summary>
  /// Summary description for ImportScanSettings.
  /// </summary>
  internal class ImportScanSettings: ImportFunctionRootClass
  {
    #region ImportScanSettingsInfo
    internal class ImportScanSettingsInfo: CAS.Lib.ControlLibrary.ImportFileControll.ImportInfo
    {
      public override string ImportName
      {
        get { return "Import Scan Settings"; }
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
          return "Scan Settings files (*.CSV)|*.CSV";
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
          return "This function imports scan settings. \r\n" +
            " File format: tag_name;writable(0/1);StateHighTriger(0/1);StateLowTrigger(0/1);Alarm(0/1);AlarmMask;StateMask;DataTypeConv";
        }
      }
    }
    #endregion
    #region private
    private CAS.NetworkConfigLib.ComunicationNet m_database;
    private ImportScanSettingsInfo m_ImportScanSettingsInfo;
    #endregion
    #region ImportFunctionRootClass
    protected override void DoTheImport()
    {
      #region IMPORT
      int changes_number = 0;
      //odczytanie pliku:
      string file = CSVManagement.ReadFile( m_ImportScanSettingsInfo.Filename );
      file = CSVManagement.PrepareForCSVProcessing( file );
      string value_to_parse = "";
      //przegladamy linia po lini:
      //przegl¹damy tak d³ugo plik jak jest jeszcze jakaœ zawartoœæ
      while ( file.Length > 0 )
      {
        try
        {
          // file format:
          //tag_name;writable(0/1);StateHighTriger(0/1);StateLowTrigger(0/1);Alarm(0/1);AlarmMask;StateMask;DataTypeConv
          //tagname:
          string tag_name = CSVManagement.GetAndMoveNextElement( ref file );
          //lets read the rest of data:
          string writable_s = CSVManagement.GetAndMoveNextElement( ref file );
          string StateHighTriger_s = CSVManagement.GetAndMoveNextElement( ref file );
          string StateLowTriger_s = CSVManagement.GetAndMoveNextElement( ref file );
          string Alarm_s = CSVManagement.GetAndMoveNextElement( ref file );
          string AlarmMask_s = CSVManagement.GetAndMoveNextElement( ref file );
          string StateMask_s = CSVManagement.GetAndMoveNextElement( ref file );
          string DataTypeConv_s = CSVManagement.GetAndMoveNextElement( ref file );
          value_to_parse = String.Format( "{0};{1};{2};{3};{4};{5};{6}", tag_name, writable_s, StateHighTriger_s, Alarm_s, AlarmMask_s, StateMask_s, DataTypeConv_s );
          //writable
          bool writable = false;
          if ( System.Convert.ToInt16( writable_s ) > 0 )
            writable = true;
          //StateHighTriger
          bool StateHighTriger = false;
          if ( System.Convert.ToInt16( StateHighTriger_s ) > 0 )
            StateHighTriger = true;
          //StateLowTriger
          bool StateLowTriger = false;
          if ( System.Convert.ToInt16( StateLowTriger_s ) > 0 )
            StateLowTriger = true;
          //Alarm
          bool Alarm = false;
          if ( System.Convert.ToInt16( Alarm_s ) > 0 )
            Alarm = true;
          //AlarmMask
          uint AlarmMask = 0;
          AlarmMask = System.Convert.ToUInt16( AlarmMask_s );
          //StateMask
          uint StateMask = 0;
          StateMask = System.Convert.ToUInt16( StateMask_s );
          //DataConversion
          string DataTypeConv = "System.Object";
          bool DataTypeConvertable = true;
          if ( DataTypeConv_s != null && DataTypeConv_s != "" )
          {
            try
            {
              DataTypeConv = DataTypeConv_s;
            }
            catch ( Exception )
            {
              DataTypeConvertable = false;
            }
          }
          else
            DataTypeConvertable = false;
          //odczytano wszyskie dane - czas wprowadzic je to bazy konfiguracji
          bool taghasbeenfound = false;
          foreach ( ComunicationNet.TagsRow tagrow in m_database.Tags )
          {
            if ( tagrow.Name.Equals( tag_name ) )
            {
              //zlokalizowalismy odpoiweidni tag wiec zmieniamy go:
              tagrow.Alarm = Alarm;
              if ( StateHighTriger )
                tagrow.StateTrigger = (sbyte)StateTrigger.StateHigh;
              if ( StateLowTriger )
                tagrow.StateTrigger = (sbyte)StateTrigger.StateLow;
              if ( writable )
                tagrow.AccessRights = (sbyte)ItemAccessRights.ReadWrite;
              else
                tagrow.AccessRights = (sbyte)ItemAccessRights.ReadOnly;
              if ( DataTypeConvertable )
                tagrow.DataTypeConversion = DataTypeConv;
              if ( Alarm )
                tagrow.AlarmMask = AlarmMask;
              if ( StateHighTriger || StateLowTriger )
                tagrow.StateMask = StateMask;
              changes_number++;
              taghasbeenfound = true;
            }
          }
          if ( !taghasbeenfound )
            AppendToLog( "Tag " + tag_name + " is not found" );
        }
        catch ( Exception e )
        {
          AppendToLog( e.Message + " near to:" + value_to_parse );
        }
      }
      #endregion IMPORT
      AppendToLog( "Number of changed lines: " + changes_number.ToString() );
    }

    #endregion
    #region creator
    public ImportScanSettings( CAS.NetworkConfigLib.ComunicationNet database, System.Windows.Forms.Form parrent_form )
      : base( parrent_form )
    {
      m_database = database;
      m_ImportScanSettingsInfo = new ImportScanSettingsInfo();
      SetImportInfo( m_ImportScanSettingsInfo );
    }
    #endregion
  }
}
