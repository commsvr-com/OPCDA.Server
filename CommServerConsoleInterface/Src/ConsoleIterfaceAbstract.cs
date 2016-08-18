//<summary>
//  Title   : ConsoleIterfaceAbstract
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

using System;
using System.Collections.Generic;
using BaseStation.Management;
using CAS.Lib.RTLib.Management;

namespace BaseStation
{
  /// <summary>
  /// Summary description for ConsoleIterfaceAbstract.
  /// </summary>
  public abstract class ConsoleIterfaceAbstract: MarshalByRefObject
  {
    /// <summary>
    /// Gets the run time in seconds.
    /// </summary>
    /// <returns>run time in seconds</returns>
    public abstract uint GetRunTime();
    /// <summary>
    /// Gets the name of the product.
    /// </summary>
    /// <returns>product name</returns>
    public abstract string GetProductName();
    /// <summary>
    /// Gets the product version.
    /// </summary>
    /// <returns>product version</returns>
    public abstract string GetProductVersion();
    /// <summary>
    /// Gets the report.
    /// </summary>
    /// <returns>html report</returns>
    public abstract string GetReport();
    /// <summary>
    /// Gets the protocol list.
    /// </summary>
    /// <returns>list of the protocols</returns>
    public abstract ProtocolDsc[] GetProtocolList();
    /// <summary>
    /// Gets the protocol.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>selected (by index) protocol</returns>
    public abstract IProtocol GetProtocol( long index );
    /// <summary>
    /// Gets the station list.
    /// </summary>
    /// <returns>station list</returns>
    public abstract SortedList<long, Statistics.StationStatistics.StationStatisticsInternal> GetStationList();
    /// <summary>
    /// Gets the station.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>selected (by index) station</returns>
    public abstract Statistics.StationStatistics.StationStatisticsInternal GetStation( long index );
    /// <summary>
    /// Gets the state of the station.
    /// </summary>
    /// <returns>SortedList&lt;System.Int64, System.Int32&gt;.</returns>
    public abstract SortedList<long, int> GetStationStates();
    /// <summary>
    /// Gets the segment list.
    /// </summary>
    /// <returns>segment list</returns>
    public abstract SortedList<long, Statistics.SegmentStatistics.SegmentStatisticsInternal> GetSegmentList();
    /// <summary>
    /// Gets the segment.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>selected (by index) segment </returns>
    public abstract Statistics.SegmentStatistics.SegmentStatisticsInternal GetSegment( long index );
    /// <summary>
    /// Gets the state of the segment.
    /// </summary>
    /// <returns>SortedList&lt;System.Int64, Statistics.SegmentStatistics.States&gt;.</returns>
    public abstract SortedList<long, Statistics.SegmentStatistics.States> GetSegmentStates();
    /// <summary>
    /// Gets the interface list.
    /// </summary>
    /// <returns>interface list</returns>
    public abstract SortedList<ulong, Statistics.InterfaceStatistics.InterfaceStatisticsInternal> GetInterfaceList();
    /// <summary>
    /// Gets the interface.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>selected (by index) interface</returns>
    public abstract Statistics.InterfaceStatistics.InterfaceStatisticsInternal GetInterface( ulong index );
    /// <summary>
    /// Gets the state of the interface.
    /// </summary>
    public abstract SortedList<ulong, Statistics.InterfaceStatistics.InterfaceState> GetInterfaceStates();
  }
}
