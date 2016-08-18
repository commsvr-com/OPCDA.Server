//<summary>
//  Title   : Main class (runnable) for NetworkConfig
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20081008: mzbrzezny: exception that occurs sometimes during licence installation is catched and message is displayed
//    Mariusz Postol - 10-03-2007
//      revised, reformated
//      COM visibility attribute removed to AssembyInfo
//    Maciej Zbrzezny - 2006-09-19
//    created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Windows.Forms;

[assembly: CLSCompliant( true )]
namespace NetworkConfig
{
  class MainClass
  {
    [STAThread]
    static void Main()
    {
      string m_cmmdLine = Environment.CommandLine;
      if ( m_cmmdLine.ToLower().Contains( "installic" ) )
        try
        {
          CAS.Lib.CodeProtect.LibInstaller.InstalLicense( true );
        }
        catch ( CAS.Lib.CodeProtect.LicenseDsc.LicenseFileException ex )
        {
          MessageBox.Show( "Cannot installicence, error:" + ex.Message );
        }
      System.Windows.Forms.Application.Run(
          new HMI.ConfigTreeView( HMI.ConfigurationManagement.configDataBase,
          new HMI.ConfigIOHandler( HMI.ConfigurationManagement.ReadConfiguration ),
          new HMI.ConfigIOHandler( HMI.ConfigurationManagement.SaveProc ),
          new HMI.ConfigIOHandler( HMI.ConfigurationManagement.ClearConfig ),
          new Properties.Settings().ToolsMenu
          ) );
    }
  }
}
