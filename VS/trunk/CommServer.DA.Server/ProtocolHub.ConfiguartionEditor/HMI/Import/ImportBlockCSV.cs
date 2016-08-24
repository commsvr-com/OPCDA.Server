//_______________________________________________________________
//  Title   : ImportBlockCSV
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

using CAS.Lib.RTLib;
using CAS.NetworkConfigLib;
using CAS.Windows.Forms;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NetworkConfig.HMI.Import
{
  class ImportBlockCSV : ImportFunctionRootClass
  {
    #region private fields
    private ImportBlockCSVInfo m_ImportBlockCSVInfo;
    private ComunicationNet m_Database;
    private CSVManagement m_CSVContainer;
    private int m_TagsAddedNumber;
    private ProgressBarWindow m_ProgressBarWindow;
    #endregion

    #region ImportBlockCSVInfo
    internal class ImportBlockCSVInfo : CAS.Lib.ControlLibrary.ImportFileControll.ImportInfo
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
      /// default browse filter for the dialog which is used for selecting a file
      /// </summary>
      public override string BrowseFilter
      {
        get
        {
          return "Blocks CSV files (*.CSV)|*.CSV";
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
          return "This import tool is adding tags based on block definition i CSV file."
          + "\r\n Each line format:\r\n"
          + " StationID;TimeScan;Timeout;TimeScanFast;TimeoutFast;Address;DataType;BlockLength"
          + "\r\n It omits first line";
        }
      }
    }
    #endregion

    #region ImportFunctionRootClass
    protected override void DoTheImport()
    {
      m_TagsAddedNumber = 0;
      m_CSVContainer = CSVManagement.ReadFile(m_ImportBlockCSVInfo.Filename);

      m_ProgressBarWindow = new ProgressBarWindow(new DoWorkEventHandler(MainImportJob), 0, m_CSVContainer.ToString().Length, 1);
      if (m_ProgressBarWindow.ShowDialog() != DialogResult.OK)
        AppendToLog("Cancel was pressed");
      AppendToLog("Number of tags added: " + m_TagsAddedNumber.ToString());
    }

    private void MainImportJob(object sender, DoWorkEventArgs e)
    {
      BackgroundWorker _BackgroundWorker = sender as BackgroundWorker;
      ProgressBarWindow _ProgressBarWindow = e.Argument as ProgressBarWindow;
      long _stationID = 0;
      ulong _timeScan = 0, _timeout = 0, _timeScanFast = 0, _timOutFast = 0;
      ulong _address = 0;
      byte _dataType = 0;
      int _length = 0;
      int _originalLen = m_CSVContainer.ToString().Length;
      _ProgressBarWindow.SetInformation("ImportingCSV");
      while (m_CSVContainer.ToString().Length > 0 && !_BackgroundWorker.CancellationPending)
      {
        _ProgressBarWindow.SetProgressValue(_originalLen - m_CSVContainer.ToString().Length);
        try
        {
          _stationID = Convert.ToUInt32(m_CSVContainer.GetAndMove2NextElement());
          _timeScan = Convert.ToUInt32(m_CSVContainer.GetAndMove2NextElement());
          _timeout = Convert.ToUInt32(m_CSVContainer.GetAndMove2NextElement());
          _timeScanFast = Convert.ToUInt32(m_CSVContainer.GetAndMove2NextElement());
          _timOutFast = Convert.ToUInt32(m_CSVContainer.GetAndMove2NextElement());
          _address = Convert.ToUInt32(m_CSVContainer.GetAndMove2NextElement());
          _dataType = Convert.ToByte(m_CSVContainer.GetAndMove2NextElement());
          _length = Convert.ToInt32(m_CSVContainer.GetAndMove2NextElement());
          //odczytalismy wszystkie elememty definiuj¹ce dany blok danych
          //znajdujemy odpowiednia stacje
          ComunicationNet.StationRow _stationRow = null;
          try { _stationRow = m_Database.Station.FindByStationID(_stationID); }
          catch { throw new Exception("station " + _stationID.ToString() + "not found"); }
          ComunicationNet.GroupsRow _GroupsRow = m_Database.Groups.NewGroupsRow();
          _GroupsRow.Name = "GR_" + _GroupsRow.GroupID.ToString() + "_st_" + _stationRow.Name;
          _GroupsRow.StationID = _stationID;
          _GroupsRow.TimeOut = _timeout;
          _GroupsRow.TimeOutFast = _timOutFast;
          _GroupsRow.TimeScan = _timeScan;
          _GroupsRow.TimeScanFast = _timeScanFast;
          m_Database.Groups.AddGroupsRow(_GroupsRow);
          ComunicationNet.DataBlocksRow _dataBlocksRow = m_Database.DataBlocks.NewDataBlocksRow();
          _dataBlocksRow.Name = "db" + _GroupsRow.GroupID.ToString() + "_st_" + _stationRow.Name;
          _dataBlocksRow.GroupID = _GroupsRow.GroupID;
          _dataBlocksRow.Address = _address;
          _dataBlocksRow.DataType = _dataType;
          m_Database.DataBlocks.AddDataBlocksRow(_dataBlocksRow);
          for (int idx = 0; idx < _length; idx++)
          {
            ComunicationNet.TagsRow _tagRow = m_Database.Tags.NewTagsRow();
            _tagRow.Name = _stationRow.Name + "/" + _dataType.ToString() + "/" + "add" + (_address + (ulong)idx).ToString();
            _tagRow.AccessRights = (sbyte)ItemAccessRights.ReadWrite;
            _tagRow.StateTrigger = (sbyte)StateTrigger.None;
            _tagRow.Alarm = false;
            _tagRow.AlarmMask = 0;
            _tagRow.StateMask = 0;
            _tagRow.DatBlockID = _dataBlocksRow.DatBlockID;
            m_Database.Tags.AddTagsRow(_tagRow);
            m_TagsAddedNumber++;
          }
        }
        catch (Exception ex)
        {
          AppendToLog("Error: " + ex.Message + " at \r\n"
              + _stationID.ToString() + ","
              + _timeScan.ToString() + ","
              + _timeout.ToString() + ","
              + _timeScanFast.ToString() + ","
              + _timOutFast.ToString() + ","
              + _address.ToString() + ","
              + _dataType.ToString() + ","
              + _length.ToString());
        }
      }
    }
    #endregion

    #region creator
    public ImportBlockCSV(ComunicationNet database, Form parentForm) : base(parentForm)
    {
      m_Database = database;
      m_ImportBlockCSVInfo = new ImportBlockCSVInfo();
      SetImportInfo(m_ImportBlockCSVInfo);
    }
    #endregion
  }
}
