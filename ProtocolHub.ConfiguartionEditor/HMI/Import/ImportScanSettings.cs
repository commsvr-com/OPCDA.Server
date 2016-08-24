//_______________________________________________________________
//  Title   : Name of Application
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C) 2016, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________


using CAS.Lib.ControlLibrary;
using CAS.Lib.RTLib;
using CAS.NetworkConfigLib;
using CAS.Windows.Forms;
using System;

namespace NetworkConfig.HMI.Import
{
  /// <summary>
  /// Summary description for ImportScanSettings.
  /// </summary>
  internal class ImportScanSettings : ImportFunctionRootClass
  {
    #region ImportScanSettingsInfo
    internal class ImportScanSettingsInfo : ImportFileControll.ImportInfo
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
      /// default browse filter for the dialog which is used for selecting a file
      /// </summary>
      public override string BrowseFilter
      {
        get
        {
          return "Scan Settings files (*.CSV)|*.CSV";
        }
      }
      /// <summary>
      /// default extension for the dialog which is used for selecting a file
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
      CSVManagement file = CSVManagement.ReadFile(m_ImportScanSettingsInfo.Filename);
      string value_to_parse = "";
      //przegladamy linia po lini:
      //przegl¹damy tak d³ugo plik jak jest jeszcze jakaœ zawartoœæ
      while (file.ToString().Length > 0)
      {
        try
        {
          // file format:
          //tag_name;writable(0/1);StateHighTriger(0/1);StateLowTrigger(0/1);Alarm(0/1);AlarmMask;StateMask;DataTypeConv
          //tagname:
          string tag_name = file.GetAndMove2NextElement();
          //lets read the rest of data:
          string writable_s = file.GetAndMove2NextElement();
          string StateHighTriger_s = file.GetAndMove2NextElement();
          string StateLowTriger_s = file.GetAndMove2NextElement();
          string Alarm_s = file.GetAndMove2NextElement();
          string AlarmMask_s = file.GetAndMove2NextElement();
          string StateMask_s = file.GetAndMove2NextElement();
          string DataTypeConv_s = file.GetAndMove2NextElement();
          value_to_parse = String.Format("{0};{1};{2};{3};{4};{5};{6}", tag_name, writable_s, StateHighTriger_s, Alarm_s, AlarmMask_s, StateMask_s, DataTypeConv_s);
          //writable
          bool writable = false;
          if (System.Convert.ToInt16(writable_s) > 0)
            writable = true;
          //StateHighTriger
          bool StateHighTriger = false;
          if (System.Convert.ToInt16(StateHighTriger_s) > 0)
            StateHighTriger = true;
          //StateLowTriger
          bool StateLowTriger = false;
          if (System.Convert.ToInt16(StateLowTriger_s) > 0)
            StateLowTriger = true;
          //Alarm
          bool Alarm = false;
          if (System.Convert.ToInt16(Alarm_s) > 0)
            Alarm = true;
          //AlarmMask
          uint AlarmMask = 0;
          AlarmMask = System.Convert.ToUInt16(AlarmMask_s);
          //StateMask
          uint StateMask = 0;
          StateMask = System.Convert.ToUInt16(StateMask_s);
          //DataConversion
          string DataTypeConv = "System.Object";
          bool DataTypeConvertable = true;
          if (DataTypeConv_s != null && DataTypeConv_s != "")
          {
            try
            {
              DataTypeConv = DataTypeConv_s;
            }
            catch (Exception)
            {
              DataTypeConvertable = false;
            }
          }
          else
            DataTypeConvertable = false;
          //odczytano wszyskie dane - czas wprowadzic je to bazy konfiguracji
          bool taghasbeenfound = false;
          foreach (ComunicationNet.TagsRow tagrow in m_database.Tags)
          {
            if (tagrow.Name.Equals(tag_name))
            {
              //zlokalizowalismy odpoiweidni tag wiec zmieniamy go:
              tagrow.Alarm = Alarm;
              if (StateHighTriger)
                tagrow.StateTrigger = (sbyte)StateTrigger.StateHigh;
              if (StateLowTriger)
                tagrow.StateTrigger = (sbyte)StateTrigger.StateLow;
              if (writable)
                tagrow.AccessRights = (sbyte)ItemAccessRights.ReadWrite;
              else
                tagrow.AccessRights = (sbyte)ItemAccessRights.ReadOnly;
              if (DataTypeConvertable)
                tagrow.DataTypeConversion = DataTypeConv;
              if (Alarm)
                tagrow.AlarmMask = AlarmMask;
              if (StateHighTriger || StateLowTriger)
                tagrow.StateMask = StateMask;
              changes_number++;
              taghasbeenfound = true;
            }
          }
          if (!taghasbeenfound)
            AppendToLog("Tag " + tag_name + " is not found");
        }
        catch (Exception e)
        {
          AppendToLog(e.Message + " near to:" + value_to_parse);
        }
      }
      #endregion IMPORT
      AppendToLog("Number of changed lines: " + changes_number.ToString());
    }

    #endregion

    #region creator
    public ImportScanSettings(CAS.NetworkConfigLib.ComunicationNet database, System.Windows.Forms.Form parrent_form)
      : base(parrent_form)
    {
      m_database = database;
      m_ImportScanSettingsInfo = new ImportScanSettingsInfo();
      SetImportInfo(m_ImportScanSettingsInfo);
    }
    #endregion

  }

}
