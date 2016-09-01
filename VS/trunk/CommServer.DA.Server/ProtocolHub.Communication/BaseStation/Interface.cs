//_______________________________________________________________
//  Title   : Abstract class describing interface functionality of the station pipe
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

using CAS.Lib.CommServerConsoleInterface;
using CAS.Lib.RTLib.Processes;
using System;
using InterfacesRow = CAS.NetworkConfigLib.ComunicationNet.InterfacesRow;

namespace CAS.Lib.CommServer
{
  /// <summary>
  /// Abstract class describing interface functionality of the station pipe
  /// </summary>
  public abstract class Interface : WaitTimeList<Interface>.TODescriptor
  {
    #region private
    private readonly BaseStation.Management.Interface myStatistics;
    private readonly Parameters myParameters;
    #endregion

    #region PUBLIC
    /// <summary>
    /// Parameters of the interface
    /// </summary>
    public struct Parameters
    {
      internal readonly TimeSpan InactivityTime;
      internal readonly TimeSpan InactivityAfterFailureTime;
      internal string Name;
      internal ushort Address;
      internal byte InterfaceNumber;
      /// <summary>
      /// Gets the interface number max value.
      /// </summary>
      /// <value>The interface number max value.</value>
      static public byte InterfaceNumberMaxValue { get { return 1; } }
      /// <summary>
      /// Initializes a new instance of the <see cref="Parameters"/> struct.
      /// </summary>
      /// <param name="interfacesRow">The interfaces row.</param>
      public Parameters(InterfacesRow interfacesRow)
      {
        InactivityTime = TimeSpan.FromMilliseconds(interfacesRow.InactTime);
        InactivityAfterFailureTime = TimeSpan.FromMilliseconds(interfacesRow.InactTimeAFailure);
        Name = interfacesRow.Name;
        Address = (ushort)Math.Min(interfacesRow.Address, ushort.MaxValue);
        InterfaceNumber = (byte)Math.Min(interfacesRow.InterfaceNum, byte.MaxValue);
        if (InterfaceNumber > InterfaceNumberMaxValue)
          throw new ArgumentOutOfRangeException("InterfaceNumber > InterfaceNumberMaxValue");
      }
    }
    internal ushort address { get { return myParameters.Address; } }
    /// <summary>
    /// Gets the interface number.
    /// </summary>
    /// <value>The interface number.</value>
    internal ushort InterfaceNumber
    {
      get { return (ushort)myStatistics.myID_InterfaceNum; }
    }
    /// <summary>
    /// Switches the Interface off after communication failure.
    /// </summary>
    internal virtual void SwitchIOffAfterFailure()
    {
      myStatistics.CurrentInterfaceState = BaseStation.Management.Statistics.InterfaceStatistics.InterfaceState.Fail;
      Cycle = myParameters.InactivityAfterFailureTime;
      ResetCounter();
    }
    /// <summary>
    /// Switches the interface off.
    /// </summary>
    internal virtual void SwitchIOff()
    {
      myStatistics.CurrentInterfaceState = BaseStation.Management.Statistics.InterfaceStatistics.InterfaceState.Standby;
      Cycle = myParameters.InactivityTime;
      ResetCounter();
    }
    /// <summary>
    /// Switches the interface on.
    /// </summary>
    internal virtual void SwitchIOn()
    {
      myStatistics.CurrentInterfaceState = BaseStation.Management.Statistics.InterfaceStatistics.InterfaceState.Active;
      Remove();
    }
    /// <summary>
    /// Marks the end of RW operation.
    /// </summary>
    internal virtual void MarkEndOfRWOperation()
    {
      myStatistics.MarkRWOperationResult();
    }
    /// <summary>
    /// Gets the retries.
    /// </summary>
    /// <value>The get retries.</value>
    internal protected abstract RetryFilter Retries { get; }
    /// <summary>
    /// Writes the data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="dataAddress">The data address.</param>
    /// <returns></returns>
    internal protected abstract bool WriteData(object data, CAS.Lib.CommonBus.ApplicationLayer.IBlockDescription dataAddress);
    /// <summary>
    /// Reads the data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="dataAddress">The data address.</param>
    /// <returns></returns>
    internal protected abstract bool ReadData(out object data, CAS.Lib.CommonBus.ApplicationLayer.IBlockDescription dataAddress);
#if COMMSERVER
    /// <summary>
    /// Initializes a new instance of the <see cref="Interface"/> class.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <param name="myWTimeList">wait time list</param>
    /// <param name="segmentStatistic">The segment statistic.</param>
    /// <param name="stationStatistic">The station statistic.</param>
    internal Interface
      (
      Parameters parameters, WaitTimeList<Interface> myWTimeList, IInterface2SegmentLink segmentStatistic,
      BaseStation.Management.Station stationStatistic
      )
#endif
#if SNIFFER
    internal Interface
      (
      InterfaceDsc interfaceDSC, WaitTimeList myWTimeList, Management.Statistics.Segment statSegment, 
      Management.Station statStation
      )
#endif
      : base(myWTimeList, parameters.InactivityTime)
    {
      myParameters = parameters;
      myStatistics = new BaseStation.Management.Interface(parameters.Name, parameters.InterfaceNumber, segmentStatistic, stationStatistic);
    }//Interface
    #endregion
  }
}
