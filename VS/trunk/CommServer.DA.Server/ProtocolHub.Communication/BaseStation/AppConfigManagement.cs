//_______________________________________________________________
//  Title   : Reads application configuration file
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

using CAS.Lib.RTLib.Management;
using CAS.Lib.RTLib.Utils;
using System.IO;

namespace CAS.CommServer.ProtocolHub.Communication
{
  /// <summary>
  /// Reads application configuration file
  /// </summary>
  internal class AppConfigManagement 
  {
    internal static readonly int RemotingHTTPport = 5000;
    internal static readonly int ConsoleRemotingHTTPport = 5757;
    internal static readonly string ItemDsc_configfile;
#if SNIFFER
    internal static readonly uint   TagDefaultTimeout= 1000;
#endif
    static AppConfigManagement()
    {
      ItemDsc_configfile = ApplicationConfiguration.GetAppSetting("ItemDsc_configfile", "");
      FileInfo fi = RelativeFilePathsCalculator.GetAbsolutePathToFileInApplicationDataFolder( ItemDsc_configfile );
      if ( ItemDsc_configfile == null || ItemDsc_configfile.Length < 3 )
      {
        FileInfo fiIfNull = RelativeFilePathsCalculator.GetAbsolutePathToFileInApplicationDataFolder( "\\item_dsc.xml" );
        ItemDsc_configfile = fiIfNull.FullName;
      }
      else
        ItemDsc_configfile = fi.FullName;
      RemotingHTTPport = ApplicationConfiguration.GetAppSetting( "RemotingHTTPport", RemotingHTTPport );
      ConsoleRemotingHTTPport = ApplicationConfiguration.GetAppSetting("ConsoleRemotingHTTPport", ConsoleRemotingHTTPport);
    }
#if SNIFFER
      //GroupScanRate
      try {TagDefaultTimeout = System.UInt16.Parse(System.Configuration.ConfigurationSettings.AppSettings["StdTimeout"]); } 
      catch (Exception ex)
      {
        new Processes.EventLogMonitor(ex.Message+" - Problem with StdTimeout parameter", System.Diagnostics.EventLogEntryType.Error, 
          (int)Processes.Error.RTLib_AppConfigManagement, 26).WriteEntry();
      }
#endif
  }
}
