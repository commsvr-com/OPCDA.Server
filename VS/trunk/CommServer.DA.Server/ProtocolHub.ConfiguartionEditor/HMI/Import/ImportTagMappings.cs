//_______________________________________________________________
//  Title   : ImportTagMappings
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

using CAS.NetworkConfigLib;
using CAS.Windows.Forms;
using System;
using System.Windows.Forms;

namespace NetworkConfig.HMI.Import
{
  
  /// <summary>
  /// Summary description for ImportTagMappings.
  /// </summary>
  internal class ImportTagMappings : ImportFunctionRootClass
  {
    #region ImportTagMappingsInfo
    internal class ImportTagMappingsInfo : CAS.Lib.ControlLibrary.ImportFileControll.ImportInfo
    {
      public override string ImportName
      {
        get { return "Import Tag Mappings"; }
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
          return "CSV Tag mappings definition file (*.CSV)|*.CSV";
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
          return "This function changes the names of tak - each line format: PreviousName;NewName";
        }
      }
    }
    #endregion
    #region private
    private CAS.NetworkConfigLib.ComunicationNet m_database;
    private ImportTagMappingsInfo m_ImportTagMappingsInfo;
    #endregion
    #region ImportFunctionRootClass
    protected override void DoTheImport()
    {
      #region IMPORT
      int changes_number = 0;
      CSVManagement _csvContainer = CSVManagement.ReadFile(m_ImportTagMappingsInfo.Filename);
      while (_csvContainer.ToString().Length > 0)
      {
        string _baseName = "";
        string _destinationName = "";
        try
        {
          _baseName = _csvContainer.GetAndMove2NextElement();
          _destinationName = _csvContainer.GetAndMove2NextElement();
          bool _tagHasBeenFound = false;
          foreach (ComunicationNet.TagsRow trow in m_database.Tags)
          {

            if (trow.Name.Equals(_baseName))
            {
              trow.Name = _destinationName;
              changes_number++;
              _tagHasBeenFound = true;
              break;
            }
          }
          if ( !_tagHasBeenFound )
            AppendToLog( "Tag " + _baseName + " -> "+_destinationName+" is not found" );
        }
        catch (
Exception
#if DEBUG
 ex
#endif
)
        {
          AppendToLog("problem with: base:" + _baseName + " dest:" + _destinationName + " :"
#if DEBUG
 + ex.Message.ToString()
#endif
);
        }
      }
      #endregion IMPORT
      AppendToLog("Number of changed tags: " + changes_number.ToString());
    }
    #endregion

    #region creator
    public ImportTagMappings( ComunicationNet database, Form parentForm)
      : base( parentForm )
    {
      m_database = database;
      m_ImportTagMappingsInfo = new ImportTagMappingsInfo();
      SetImportInfo(m_ImportTagMappingsInfo);
    }
    #endregion

  }

}
