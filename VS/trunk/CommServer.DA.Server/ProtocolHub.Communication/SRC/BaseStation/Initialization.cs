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

using BaseStation.ItemDescriber;
using CAS.CommServer.ProtocolHub.Communication.SRC.BaseStation.NetworkConfig;
using CAS.Lib.CommServer;
using System;

namespace BaseStation
{
  /// <summary>
  /// COMMUNICATION LIBRARY - Data Base of Process Values
  /// </summary>
  internal static class Initialization
  {
#if COMMSERVER
    internal static ItemDecriberDataSet m_ds_dsc;
    private static void InitItemDescriber()
    {
      XMLManagement xml_desc = new XMLManagement();
      m_ds_dsc = new ItemDecriberDataSet();
      xml_desc.readXMLFile(m_ds_dsc, AppConfigManagement.ItemDsc_configfile);
    }
#endif
    /// <summary>
    /// Initialize the communication server
    /// </summary>
    /// <param name="parent">Parent control hosting some common resources</param>
    /// <param name="cDemoVer">true if there is no valid license, false otherwise</param>
    /// <param name="cVConstrain">Number of item I can instantiate according of the license. �1 if unlimited. Valid if cDemoVer is false.</param>
    /// <param name="ConfigurationFileName">Name of the configuration file.</param>
    internal static void InitServer(CommServerComponent parent, bool cDemoVer, ref int cVConstrain, string ConfigurationFileName)
    {
      ProtocolHubConfiguration xml = new ProtocolHubConfiguration(ConfigurationFileName, true);
#if COMMSERVER
      try
      {
        InitItemDescriber();
      }
      catch (Exception ex)
      {
        CommServerComponent.Tracer.TraceWarning(98, typeof(Initialization).FullName + ".Initialization", ex.Message);
      }
#endif
      //CAS.OpcSvr.Da.NETServer.Initialization.InitComponent();
      Station.InitStations(xml.configuration.Station, ref cVConstrain);
      Channel.InitializeChannels(xml.configuration.Channels, parent, cDemoVer);
#if COMMSERVER
      Station.SwitchOnDataScanning();
#endif
    }
    internal static void Finalise()
    {
      DataQueue.finalize();
    }
  }
}
