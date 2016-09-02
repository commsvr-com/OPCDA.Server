//<summary>
//  Title   : ConsoleIterface
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    MZbrzezny 2006-03-01: created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using BaseStation.Management;
using CAS.CommServer.ProtocolHub.Communication.Properties;
using CAS.Lib.RTLib.Management;
using CAS.Lib.RTLib.Processes;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using Statistics = BaseStation.Management.Statistics;

namespace CAS.CommServer.ProtocolHub.Communication.BaseStation
{
  /// <summary>
  /// Summary description for ConsoleIterface.
  /// </summary>
  public class ConsoleIterface : global::BaseStation.ConsoleIterfaceAbstract
  {
    private class StatisticAndIUpdatePair<StatT>
    {
      private StatT statT;
      private IUpdateInternalStatistics m_UpdateObject;
      internal StatisticAndIUpdatePair(StatT StatisticsObject, IUpdateInternalStatistics IUpdateInternalStatisticsObject)
      {
        statT = StatisticsObject;
        m_UpdateObject = IUpdateInternalStatisticsObject;
      }
      internal StatT StatisticsObject
      {
        get
        {
          return statT;
        }
      }
      internal IUpdateInternalStatistics IUpdateInternalStatisticsObject
      {
        get
        {
          return m_UpdateObject;
        }
      }
      internal StatT GetStatisticsObjectUpdated()
      {
        m_UpdateObject.UpdateInternal();
        return statT;
      }
    }
    private static SortedList<long, Statistics.StationStatistics.StationStatisticsInternal> stationlist;
    private static SortedList<long, Statistics.SegmentStatistics.SegmentStatisticsInternal> segmentlist;//lista wykorzystywana przy odczytywaniu calej listy
    private static SortedList<ulong, Statistics.InterfaceStatistics.InterfaceStatisticsInternal> interfacelist;//lista wykorzystywana przy odczytywaniu calej listy
    private static SortedList<long, StatisticAndIUpdatePair<Statistics.SegmentStatistics.SegmentStatisticsInternal>> segmentlistpairs; //lista wykorzystywana przy odczytywaniu konkretnego elementu
    private static SortedList<ulong, StatisticAndIUpdatePair<Statistics.InterfaceStatistics.InterfaceStatisticsInternal>> interfacelistpairs;//lista wykorzystywana przy odczytywaniu konkretnego elementu
    private static SortedList<long, Statistics.SegmentStatistics.States> listSegmentStates;
    private static SortedList<long, int> listStationStates;
    private static SortedList<ulong, Statistics.InterfaceStatistics.InterfaceState> listInterfaceStates;
    private static string ProductName = "CommServer";
    private static string ProductVersion = "3";
    /// <summary>
    /// Gets the report.
    /// </summary>
    /// <returns>html report</returns>
    public override string GetReport()
    {
      try
      {
        Diagnostic.ReportGenerator rep = new Diagnostic.ReportGenerator("CAS-Commserver_state");
        return rep.GetReportString();
      }
      catch (Exception ex)
      {
        return Resources.ExceptionDuringReportCreation + ex.ToString();
      }
    }
    /// <summary>
    /// Gets the protocol.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>selected (by index) protocol</returns>
    public override IProtocol GetProtocol(long index)
    {
      return Protocol.GetProtocolStatistics(index);
    }
    /// <summary>
    /// Gets the protocol list.
    /// </summary>
    /// <returns>list of the protocols</returns>
    public override ProtocolDsc[] GetProtocolList()
    {
      return Protocol.GetProtDescriptions;
    }
    /// <summary>
    /// Gets the station.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>selected (by index) station</returns>
    public override Statistics.StationStatistics.StationStatisticsInternal GetStation(long index)
    {
      return stationlist[index];
    }
    /// <summary>
    /// Gets the state of the station.
    /// </summary>
    /// <returns>SortedList&lt;System.Int64, System.Int32&gt;.</returns>
    public override SortedList<long, int> GetStationStates()
    {
      foreach (var element in stationlist)
      {
        int myValue = element.Value.StationState;
        if (!listStationStates.ContainsKey(element.Key))
          listStationStates.Add(element.Key, myValue);
        else
          listStationStates[element.Key] = myValue;
      }
      return listStationStates;


    }
    /// <summary>
    /// Gets the station list.
    /// </summary>
    /// <returns>station list</returns>
    public override SortedList<long, Statistics.StationStatistics.StationStatisticsInternal> GetStationList()
    {
      return stationlist;
    }
    /// <summary>
    /// Gets the interface.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>selected (by index) interface</returns>
    public override Statistics.InterfaceStatistics.InterfaceStatisticsInternal GetInterface(ulong index)
    {
      Statistics.InterfaceStatistics.InterfaceStatisticsInternal Interface =
       interfacelistpairs[index].GetStatisticsObjectUpdated();
      return Interface;
    }
    /// <summary>
    /// Gets the state of the interface.
    /// </summary>
    /// <returns>SortedList&lt;System.UInt64, Statistics.InterfaceStatistics.InterfaceState&gt;.</returns>
    public override SortedList<ulong, Statistics.InterfaceStatistics.InterfaceState> GetInterfaceStates()
    {
      foreach (var element in interfacelistpairs)
      {
        Statistics.InterfaceStatistics.InterfaceState myValue = element.Value.GetStatisticsObjectUpdated().State;
        if (!listInterfaceStates.ContainsKey(element.Key))
          listInterfaceStates.Add(element.Key, myValue);
        else
          listInterfaceStates[element.Key] = myValue;
      }
      return listInterfaceStates;
    }
    /// <summary>
    /// Gets the interface list.
    /// </summary>
    /// <returns>interface list</returns>
    public override SortedList<ulong, Statistics.InterfaceStatistics.InterfaceStatisticsInternal> GetInterfaceList()
    {
      return interfacelist;
    }
    /// <summary>
    /// Gets the segment.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>selected (by index) segment</returns>
    public override Statistics.SegmentStatistics.SegmentStatisticsInternal GetSegment(long index)
    {
      return segmentlistpairs[index].GetStatisticsObjectUpdated();
    }
    /// <summary>
    /// Gets the state of the segment.
    /// </summary>
    /// <returns>SortedList&lt;System.Int64, Statistics.SegmentStatistics.States&gt;.</returns>
    public override SortedList<long, Statistics.SegmentStatistics.States> GetSegmentStates()
    {
      foreach (var element in segmentlistpairs)
      {
        Statistics.SegmentStatistics.States myValue = element.Value.GetStatisticsObjectUpdated().CurrentState;
        if (!listSegmentStates.ContainsKey(element.Key))
          listSegmentStates.Add(element.Key, myValue);
        else
          listSegmentStates[element.Key] = myValue;
      }
      return listSegmentStates;
    }
    /// <summary>
    /// Gets the segment list.
    /// </summary>
    /// <returns>segment list</returns>
    public override SortedList<long, Statistics.SegmentStatistics.SegmentStatisticsInternal> GetSegmentList()
    {
      return segmentlist;
    }
    /// <summary>
    /// Gets the run time.
    /// </summary>
    /// <returns>run time in seconds</returns>
    public override uint GetRunTime()
    {
      return CommServerComponent.RunTime;
    }
    /// <summary>
    /// Gets the name of the product.
    /// </summary>
    /// <returns>product name</returns>
    public override string GetProductName()
    {
      return ProductName;
    }
    /// <summary>
    /// Gets the product version.
    /// </summary>
    /// <returns>product version</returns>
    public override string GetProductVersion()
    {
      return ProductVersion;
    }
    private static bool started = false;
    /// <summary>
    /// Starts the server with specified product name and version.
    /// </summary>
    /// <param name="cProductName">Name of the c product.</param>
    /// <param name="cProductVersion">The c product version.</param>
    public static void Start(string cProductName, string cProductVersion)
    {
      if (!started)
      {
        ProductName = cProductName;
        ProductVersion = cProductVersion;
        //inicjalizacja kanalu
        started = true;
        try
        {
          TcpServerChannel channel;
          //        SoapServerFormatterSinkProvider ftProvider = new SoapServerFormatterSinkProvider();
          //        ftProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
          //        ftProvider.Next=new  System.Runtime.Remoting.MetadataServices.SdlChannelSinkProvider();
          BinaryServerFormatterSinkProvider ftProvider = new BinaryServerFormatterSinkProvider();
          ftProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
          channel = new TcpServerChannel(null, AppConfigManagement.ConsoleRemotingHTTPport, ftProvider);
          //rejestracja kana³u
          ChannelServices.RegisterChannel(channel, false);
        }
        catch (Exception ex)
        {
          EventLogMonitor.WriteToEventLog
            (
              String.Format(Resources.ConsoleInterface_ChannelRegistrationError, ex.Message),
              System.Diagnostics.EventLogEntryType.Error, (int)CAS.Lib.RTLib.Processes.Error.CommServer_CommServerComponent, 267
            );
        }
        //rejestracja OPCRealtimeDataAccess
        WellKnownServiceTypeEntry remObj = new WellKnownServiceTypeEntry
          (
          typeof(BaseStation.ConsoleIterface),
          "CommServerConsole",
          WellKnownObjectMode.Singleton
          );
        RemotingConfiguration.RegisterWellKnownServiceType(remObj);
        //inicjalizacja listy protokolow :
        //inicjalizacja listy stacji :
        stationlist = new SortedList<long, Statistics.StationStatistics.StationStatisticsInternal>(Statistics.stationList.Count);
        foreach (Statistics.StationStatistics obj in Statistics.stationList)
        {
          stationlist.Add(((Statistics.StationStatistics)obj).myStat.myID, ((Statistics.StationStatistics)obj).myStat);
        }
        //inicjalizacja listy segmentow :
        segmentlistpairs = new SortedList<long, StatisticAndIUpdatePair<Statistics.SegmentStatistics.SegmentStatisticsInternal>>(Statistics.segmentList.Count);
        segmentlist = new SortedList<long, Statistics.SegmentStatistics.SegmentStatisticsInternal>(Statistics.segmentList.Count);
        foreach (Statistics.SegmentStatistics obj in Statistics.segmentList)
        {
          segmentlist.Add(obj.myStat.MyID, obj.myStat);
          segmentlistpairs.Add(
            obj.myStat.MyID, new StatisticAndIUpdatePair<Statistics.SegmentStatistics.SegmentStatisticsInternal>
              (obj.myStat, obj));
        }
        //inicjalizacja listy interfejsow :
        interfacelistpairs = new SortedList<ulong, StatisticAndIUpdatePair<Statistics.InterfaceStatistics.InterfaceStatisticsInternal>>(Statistics.interfaceList.Count);
        interfacelist = new SortedList<ulong, Statistics.InterfaceStatistics.InterfaceStatisticsInternal>(Statistics.interfaceList.Count);
        foreach (Statistics.InterfaceStatistics obj in Statistics.interfaceList)
        {
          interfacelist.Add(obj.myStat.myID_Internal, obj.myStat);
          interfacelistpairs.Add(
            obj.myStat.myID_Internal, new StatisticAndIUpdatePair<Statistics.InterfaceStatistics.InterfaceStatisticsInternal>
              (obj.myStat, obj));
        }
        listSegmentStates = new SortedList<long, Statistics.SegmentStatistics.States>(segmentlistpairs.Count);
        listStationStates = new SortedList<long, int>(stationlist.Count);
        listInterfaceStates = new SortedList<ulong, Statistics.InterfaceStatistics.InterfaceState>(interfacelistpairs.Count);
      }
    }
  }
}
