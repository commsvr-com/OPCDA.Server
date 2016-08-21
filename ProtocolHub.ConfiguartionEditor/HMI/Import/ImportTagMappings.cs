//<summary>
//  Title   : ImportTagMappings
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
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

using BaseStation;
using CAS.NetworkConfigLib;
using CAS.Windows.Forms;
using System;

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
      string file = CSVManagement.ReadFile(m_ImportTagMappingsInfo.Filename);
      file = CSVManagement.PrepareForCSVProcessing(file);
      while (file.Length > 0)
      {
        string basename = "";
        string destname = "";
        try
        {
          basename = CSVManagement.GetAndMoveNextElement(ref file);
          destname = CSVManagement.GetAndMoveNextElement(ref file);
          bool taghasbeenfound = false;
          foreach (ComunicationNet.TagsRow trow in m_database.Tags)
          {

            if (trow.Name.Equals(basename))
            {
              trow.Name = destname;
              changes_number++;
              taghasbeenfound = true;
              break;
            }
          }
          if ( !taghasbeenfound )
            AppendToLog( "Tag " + basename + " -> "+destname+" is not found" );
        }
        catch (
Exception
#if DEBUG
 ex
#endif
)
        {
          AppendToLog("problem with: base:" + basename + " dest:" + destname + " :"
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
    public ImportTagMappings( CAS.NetworkConfigLib.ComunicationNet database, System.Windows.Forms.Form parrent_form )
      : base( parrent_form )
    {
      m_database = database;
      m_ImportTagMappingsInfo = new ImportTagMappingsInfo();
      SetImportInfo(m_ImportTagMappingsInfo);
    }
    #endregion
  }
}
