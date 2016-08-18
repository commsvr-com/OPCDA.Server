//<summary>
//  Title   : ImportTagBits
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

using System;
using CAS.NetworkConfigLib;
using BaseStation;

namespace NetworkConfig.HMI.Import
{
  /// <summary>
  /// Summary description for ImportTagBits.
  /// </summary>
  internal class ImportTagBits : ImportFunctionRootClass
  {
    #region ImportTagBitsInfo
    internal class ImportTagBitsInfo : CAS.Lib.ControlLibrary.ImportFileControll.ImportInfo
    {
      public override string ImportName
      {
        get { return "Import Tag Bits information"; }
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
          return "CSV Tag bits definition file (*.CSV)|*.CSV";
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
          return "This function immport tabits from file - each line format: BaseTagName;Bitnumber;Name";
        }
      }
    }
    #endregion
    #region private
    private CAS.NetworkConfigLib.ComunicationNet m_database;
    private ImportTagBitsInfo m_ImportTagBitsInfo;
    private int m_numberofTagBitsadded = 0;
    #endregion
    #region ImportFunctionRootClass
    protected override void DoTheImport()
    {
      m_numberofTagBitsadded = 0;
      string sourcefile = "";
      //wlasciwy import 
      try
      {
        sourcefile = BaseStation.CSVManagement.ReadFile( this.m_ImportTagBitsInfo.Filename );
        sourcefile = BaseStation.CSVManagement.PrepareForCSVProcessing( sourcefile );
      }
      catch (Exception ex)
      {
        AppendToLog( "problem with file " + this.m_ImportTagBitsInfo.Filename + " :" + ex.Message );
        return;
      }
      //przed chwila pozbylismy sie pierwszej lini i wszystkich znakow konca lini teraz:
      string BaseTagName="";
      string Bitnumber="";
      string Name="";
      while (sourcefile.Length > 0)
      {
        try
        {
          //format: BaseTagName;Bitnumber;Name
          //odczytujemy BaseTagName:
          BaseTagName = CSVManagement.GetAndMoveNextElement(ref sourcefile);
          //odczytujemy BaseTagName:
          Bitnumber = CSVManagement.GetAndMoveNextElement(ref sourcefile);
          //odczytujemy BaseTagName:
          Name = CSVManagement.GetAndMoveNextElement(ref sourcefile);
          //odnajdujemy odpowiendniego taga bazowego w bazie
          foreach (ComunicationNet.TagsRow trow in m_database.Tags)
          {
            if (trow.Name.Equals(BaseTagName))
            {
              //znalezlismy odpowiedniego base taga - dodajmy tagbita
              ComunicationNet.TagBitRow tagbitrow = m_database.TagBit.NewTagBitRow(trow, String.Empty);
              tagbitrow.Name = Name;
              tagbitrow.BitNumber = System.Convert.ToInt16(Bitnumber);
              m_database.TagBit.AddTagBitRow(tagbitrow);
              m_numberofTagBitsadded++;
            }
          }
        }
        catch (
Exception
#if DEBUG
 ex
#endif
)
        {
          AppendToLog("problem with: BaseTagName:" + BaseTagName + " Bitnumber:" + Bitnumber + " :"
#if DEBUG
 + ex.Message.ToString()
#endif
);
        }
      }//while (sourcefile)
      AppendToLog("Number of TagBits added: " + m_numberofTagBitsadded.ToString());
    }

    #endregion
    #region creator
    public ImportTagBits( CAS.NetworkConfigLib.ComunicationNet database, System.Windows.Forms.Form parrent_form )
      : base( parrent_form )
    {
      m_database = database;
      m_ImportTagBitsInfo = new ImportTagBitsInfo();
      SetImportInfo(m_ImportTagBitsInfo);
    }
    #endregion
  }
}
