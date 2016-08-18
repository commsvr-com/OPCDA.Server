//<summary>
//  Title   : Facade implementation of ISegmentStatistics
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  History :
//    200709 mpostol - created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
using System;
using System.Collections.Generic;
using System.Text;
using CAS.Lib.CommServerConsoleInterface;
using BaseStation.Management;
namespace CAS.Lib.CommServer.Tests
{
  class FacadeISegmentStatistics: ISegmentStatistics
  {
    internal int NumberOfMarkConnFail;
    internal Statistics.SegmentStatistics.States State;
    internal long Min;
    internal long Max;
    internal long Avarage;
    #region ISegmentStatistics Members
    public void MarkConnFail()
    {
      NumberOfMarkConnFail++;
    }
    public Statistics.SegmentStatistics.States NewState
    {
      set { State = value; }
    }
    public void SetOvertimeCoefficient( long min, long max, long avr )
    {
      Min = min;
      Max = max;
      Avarage = avr;
    }
    #endregion
    #region IInterfaceLink Members
    void IInterface2SegmentLink.AddInterface( Statistics.InterfaceStatistics iNtrerface )
    {
      throw new Exception( "The method or operation is not implemented." );
    }
    void IInterface2SegmentLink.GetProtocolStatistics( ref uint[] counters, out bool isAnySuccess )
    {
      throw new Exception( "The method or operation is not implemented." );
    }
    public string GetOPCPrefix
    {
      get { return "FacadeISegmentStatistics"; }
    }
    #endregion
  }
}
