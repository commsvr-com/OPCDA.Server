
//<summary>
//  Title   : Interlink between the Interface and Segment statistic object
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    MPostol: created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
using System;
namespace CAS.Lib.CommServerConsoleInterface
{
  /// <summary>
  /// Interlink between the Interface and Segment statistic object
  /// </summary>
  public interface IInterface2SegmentLink
  {
    /// <summary>
    /// Adds the interface.
    /// </summary>
    /// <param name="iNtrerface">The intrerface.</param>
    void AddInterface( BaseStation.Management.Statistics.InterfaceStatistics iNtrerface );
    /// <summary>
    /// Gets the protocol statistics.
    /// </summary>
    /// <param name="counters">The counters.</param>
    /// <param name="isAnySuccess">if set to <c>true</c> means any success.</param>
    void GetProtocolStatistics( ref uint[] counters, out bool isAnySuccess );
    /// <summary>
    /// Gets the get OPC prefix.
    /// </summary>
    /// <value>The get OPC prefix.</value>
    string GetOPCPrefix { get;}
  }
}
