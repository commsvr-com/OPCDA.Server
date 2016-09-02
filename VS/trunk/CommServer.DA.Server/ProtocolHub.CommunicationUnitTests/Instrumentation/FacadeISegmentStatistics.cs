//_______________________________________________________________
//  Title   : Facade implementation of ISegmentStatistics
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

using BaseStation.Management;
using CAS.Lib.CommServerConsoleInterface;
using System;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests.Instrumentation
{
  internal class FacadeISegmentStatistics : ISegmentStatistics
  {

    internal int NumberOfMarkConnFail;
    internal Statistics.SegmentStatistics.States State;
    internal long Min;
    internal long Max;
    internal long Average;

    #region ISegmentStatistics Members
    public void MarkConnFail()
    {
      NumberOfMarkConnFail++;
    }
    public Statistics.SegmentStatistics.States NewState
    {
      set { State = value; }
    }
    public void SetOvertimeCoefficient(long min, long max, long avr)
    {
      Min = min;
      Max = max;
      Average = avr;
    }
    #endregion

    #region IInterfaceLink Members
    void IInterface2SegmentLink.AddInterface(Statistics.InterfaceStatistics interfaceStatistics)
    {
      throw new Exception("The method or operation is not implemented.");
    }
    void IInterface2SegmentLink.GetProtocolStatistics(ref uint[] counters, out bool isAnySuccess)
    {
      throw new Exception("The method or operation is not implemented.");
    }
    public string GetOPCPrefix
    {
      get { return "FacadeISegmentStatistics"; }
    }
    #endregion
  }
}
