//<summary>
//  Title   : Class that is base for the importing classes
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    MZbrzezny - 20070615 - created.
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using CAS.Lib.ControlLibrary;
using System;

namespace NetworkConfig.HMI.Import
{
  abstract class ImportFunctionRootClass
  {
    #region private members
    private OKCancelForm m_okcancelform;
    private ImportFileControll m_ImportFileControll;
    private ImportFileControll.ImportInfo m_importinfo = null;
    private string m_import_log = "";
    private System.Windows.Forms.Form m_parrent_form = null;
    #endregion
    #region protected function
    protected abstract void DoTheImport();
    protected void SetImportInfo(ImportFileControll.ImportInfo _importinfo)
    {
      m_importinfo = _importinfo;
    }
    protected void AppendToLog(string texttoappend)
    {
      m_import_log += " " + texttoappend + "\r\n";
    }
    #endregion
    #region creator
    internal ImportFunctionRootClass( ImportFileControll.ImportInfo _importinfo, System.Windows.Forms.Form parrent_form )
    {
      m_importinfo = _importinfo;
      m_parrent_form = parrent_form;
    }
    protected ImportFunctionRootClass( System.Windows.Forms.Form parrent_form )
    {
      m_parrent_form = parrent_form;
    }
    #endregion
    #region Public
    public void Import()
    {
      m_import_log = "";
      if (m_importinfo != null)
      {
        m_okcancelform = new OKCancelForm( m_importinfo.ImportName );
        m_ImportFileControll = new ImportFileControll(m_importinfo, m_okcancelform);
        m_okcancelform.SetUserControl = m_ImportFileControll;
        //ImportFileForm form = new ImportFileForm( m_importinfo );
        if (m_okcancelform.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
          try
          {
            DoTheImport();
          }
          catch ( Exception ex )
          {
            AppendToLog( "problem during import: " + ex.Message );
          }
          if (m_import_log.Length > 0)
          {
            LogMessageWindow logform = new LogMessageWindow(m_import_log);
            logform.ShowDialog();
          }
        }
      }
      else
        throw new Exception("please initialise (SetImportInfo) class: ImportFunctionRootClass first");
    }
    public string GetImportLog()
    {
      return m_import_log;
    }
    #endregion

  }
}
