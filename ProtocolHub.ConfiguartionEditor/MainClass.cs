//_______________________________________________________________
//  Title   : NetworkConfig - entry point class
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


using CAS.Lib.CodeProtect;
using CAS.Lib.CodeProtect.LicenseDsc;
using System;
using System.Windows.Forms;

[assembly: CLSCompliant(true)]
namespace NetworkConfig
{
  class MainClass
  {
    [STAThread]
    static void Main()
    {
      string m_CommandLine = Environment.CommandLine;
      if (m_CommandLine.ToLower().Contains("installic"))
        try
        {
          LibInstaller.InstalLicense(false);
        }
        catch (LicenseFileException ex)
        {
          MessageBox.Show("Cannot install license, error:" + ex.Message);
        }
      Application.Run(
          new HMI.ConfigTreeView(HMI.ConfigurationManagement.configDataBase,
          new HMI.ConfigIOHandler(HMI.ConfigurationManagement.ReadConfiguration),
          new HMI.ConfigIOHandler(HMI.ConfigurationManagement.SaveProc),
          new HMI.ConfigIOHandler(HMI.ConfigurationManagement.ClearConfig),
          new Properties.Settings().ToolsMenu
          ));
    }
  }
}
