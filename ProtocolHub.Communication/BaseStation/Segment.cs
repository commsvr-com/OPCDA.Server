#if SNIFFER
// <summary>
//  Title   : Segment description - segment is main listener thread that receives data from serial port and manages this data
//  Title   : Segment description
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
// </summary>
//  History :
//  MPostol - 18-08-06 Revision
//  Removed error with simultaneus writing and reading removed
//    MZbrzezny - 05-08-2005:
//      Created form segment from CommServer
//    <Author> - <date>:
//    <description>
//
//  Copyright (C) 2003, CAS LODZ POLAND.
//  TEL: 42' 686 25 47
//  mailto:techsupp@cas.com.pl
//  http://www.cas.com.pl
namespace BaseStation
{ 
  using System;
  using System.Collections;
  using System.Threading;
  using InterfacesRowDSC = NetworkConfig.ComunicationNet.InterfacesRow;
  using SegmentsRowDSC   = NetworkConfig.ComunicationNet.SegmentsRow;
  using Processes;
  using Management;
  using ApplicationLayer;
  /// <summary>
  /// Segment description - 
  /// segment is main listener thread that receives data from serial port and manages this data:
  /// - checks if all tags exists in database
  /// - update all tags
  /// Segment (in BUSSniffer) is differ from Segment from Commserver - only one segment could be situated at channel and protocol
  /// it has no time scan - it listens the channel constantly. 
  /// This Segment is much more similar to Client Station OPC
  /// </summary>
  internal class Segment
  {

#region PRIVATE
    private class SegmentAddress : CommunicationLayer.IAddress
    {
      string m_address;
      object CommunicationLayer.IAddress.address
      {
        get
        {
          return m_address;
        }
        set
        {
          m_address=System.Convert.ToString( value);
        }
      }
      internal SegmentAddress(string add)
      {
        m_address=add;
      }
    }
    private readonly SegmentAddress  myTelNum;
    private const ushort PendulumCycle = 20;
    private enum StateEnum {Connected, ConInProgress, DisInProgress, Disconnected};
    //private Management.Statistics.Segment myStatistics;
    private object myPort;
    //    private ConnectTimeMenager myCTM = new ConnectTimeMenager();
    private IApplicationLayerSniffer myProtocol;
    private StateEnum mySegmentState       = StateEnum.Disconnected;
    private Thread ScannerThread;
    /// <summary>
    /// !! main thread !!
    /// </summary>
    private void Scanner() 
    {
      myProtocol.ConnectReq(out myPort,myTelNum);
      mySegmentState=StateEnum.Connected;
      while(mySegmentState==StateEnum.Connected)
      {
        //wlaczamy nasluchiwanie polaczenia
        if(myProtocol.ListenReq(true) != 0) throw new System.Exception("Cannot listen (myProtocol)");
        while(true)
        {
          //rozpoczêcie czekania na po³aczenie 
          object port;
          int conind;
          conind = myProtocol.ConnectInd(out port);
          if (conind == 0)
          {
            ReadValue readvalue;
            IBlockDescription description;
#region ReadCMD loop
            while ( myProtocol.ReadData(out description,  out readvalue, myPort) )				
            {
              myProtocol.Statistic.TimeSlaveResponseDelayResetAndStart();
              DataQueue.UpdateAllTags(description, readvalue);
              myProtocol.Statistic.TimeSlaveResponseDelayStop();
              if (readvalue!=null) readvalue.ReturnEmptyEnvelope();
            } //while ( myProtocol.ReadCMD( port, out description, station, out cmd, out message) )
#endregion
          }
        }//while
      }//while(mySegmentState=StateEnum.Connected)
    }//ScannerThread
    private void Disconnect()
    {
      myProtocol.DisconnectReq(myPort);
      mySegmentState       = StateEnum.Disconnected;
    }

#endregion
#region PUBLIC
    internal void SwitchOn()
    {
      lock(this)
      {
        ScannerThread = Processes.Manager.StartProcess
          (new System.Threading.ThreadStart(Scanner), "SegmentScanner", true);
      } // lock(this)
    } // SwitchOn
    internal void SwitchOff()
    {
      lock(this)
      {
        Disconnect();
      }
    }
    internal Segment(IApplicationLayerSniffer protocol)
    {
      myProtocol          = protocol;
      myTelNum=new SegmentAddress("0");
    }
#endregion
  }
}
#endif
#if COMMSERVER
//_______________________________________________________________
//  Title   : Segment implementation.
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

using CAS.CommServer.ProtocolHub.Communication.LicenseControl;
using CAS.Lib.CommonBus.ApplicationLayer;
using CAS.Lib.CommServerConsoleInterface;
using CAS.Lib.RTLib.Processes;
using System;
using InterfacesRowDSC = CAS.NetworkConfigLib.ComunicationNet.InterfacesRow;
using SegmentsRowDSC = CAS.NetworkConfigLib.ComunicationNet.SegmentsRow;

namespace CAS.CommServer.ProtocolHub.Communication.BaseStation
{
  /// <summary>
  /// Segment implementation.
  /// </summary>
  internal class Segment : SegmentStateMachine
  {

    #region private
    private const string m_Src = "CAS.Lib.CommServer.Segment";
    /// <summary>
    /// Implementation of the Interface in the Segment.
    /// </summary>
    private sealed class SegmentInterface : Pipe.PipeInterface
    {
      #region private
      private Segment mySegment;
      private void myStateMachine_DisconnectedAfterFailureEntered(object sender, EventArgs e)
      {
        this.SwitchIOffAfterFailure();
      }
      #endregion
      #region internal
      /// <summary>
      /// Writes the data.
      /// </summary>
      /// <param name="data">The data.</param>
      /// <param name="dataAddress">The data address.</param>
      /// <returns></returns>
      internal protected override bool WriteData(object data, IBlockDescription dataAddress)
      {
        return mySegment.WriteData(data, dataAddress, this);
      }
      /// <summary>
      /// Reads the data.
      /// </summary>
      /// <param name="data">The data.</param>
      /// <param name="dataAddress">The data address.</param>
      /// <returns></returns>
      internal protected override bool ReadData(out object data, IBlockDescription dataAddress)
      {
        return mySegment.ReadData(out data, dataAddress, this);
      }
      #endregion
      #region creator
      /// <summary>
      /// Initializes a new instance of the <see cref="SegmentInterface"/> class.
      /// </summary>
      /// <param name="interfaceParameters">The interface parameters.</param>
      /// <param name="pipe">The pipe.</param>
      /// <param name="timeList">The time list.</param>
      /// <param name="segment">The segment.</param>
      /// <param name="defaultNumberOfRetries">The default number of retries.</param>
      internal SegmentInterface
        (Interface.Parameters interfaceParameters, Pipe pipe, SegmentWaitTimeList timeList, Segment segment, byte defaultNumberOfRetries)
        :
        base(interfaceParameters, pipe, timeList, segment.myStatistics, defaultNumberOfRetries)
      {
        CommServerComponent.Tracer.TraceVerbose(237, m_Src, "Creating port: " + interfaceParameters.Name);
        this.mySegment = segment;
        segment.DisconnectedAfterFailureEntered += new EventHandler(myStateMachine_DisconnectedAfterFailureEntered);
        CommServerComponent.Tracer.TraceVerbose(240, m_Src, "Port: " + interfaceParameters.Name + " has been created");
      }
      #endregion
    }
    private sealed class SegmentWaitTimeList : HandlerWaitTimeList<SegmentInterface.PipeDataBlock>
    {
      #region private
      private Segment mySegment;
      private void AddInterfaces(InterfacesRowDSC[] interfaceList, byte defRetries)
      {
        foreach (InterfacesRowDSC currRow in interfaceList)
        {
          Station currSt = Station.FindStation((uint)currRow.StationId);
          System.Diagnostics.Debug.Assert(currSt != null, "Configuration inconsistency - cannot find station");
          if (currSt != null)
            try
            {
              var si = new SegmentInterface(new Interface.Parameters(currRow), currSt, this, mySegment, defRetries);
              si.ResetCounter();
            }
            catch (System.ComponentModel.LicenseException ex)
            {
              CommServerComponent.Tracer.TraceInformation(262, m_Src, ex.Message);
            }
        }
      }
      #endregion
      #region HandlerWaitTimeList implementation
      /// <summary>
      /// Handlers the specified list item.
      /// </summary>
      /// <param name="listItem">The list item.</param>
      protected override void Handler(SegmentInterface.PipeDataBlock listItem)
      {
        mySegment.ReadData(listItem);
      }
      /// <summary>
      /// New values of the overtime coefficient. Event handler invocekd every time new values are available.
      /// </summary>
      /// <param name="min">The min.</param>
      /// <param name="max">The max.</param>
      /// <param name="average">The average.</param>
      public override void NewOvertimeCoefficient(long min, long max, long average)
      {
        mySegment.myStatistics.SetOvertimeCoefficient(min, max, average);
      }
      #endregion
      #region creator
      /// <summary>
      /// Initializes a new instance of the <see cref="SegmentWaitTimeList"/> class.
      /// </summary>
      /// <param name="parent">The parent segment <see cref="Segment"/>.</param>
      /// <param name="interfacesList">The interfaces list.</param>
      /// <param name="retries">The retries.</param>
      /// <param name="handlerThreadName">Name of the handler thread.</param>
      internal SegmentWaitTimeList(Segment parent, InterfacesRowDSC[] interfacesList, byte retries, string handlerThreadName)
        : base(false, handlerThreadName, OTALicense.License.Licensed)
      {
        mySegment = parent;
        AddInterfaces(interfacesList, retries);
      }
      #endregion
    }//SegmentWaitTimeList
    private ISegmentStatistics myStatistics;
    private SegmentWaitTimeList dataQueue;
    private void PickUpHandler() { }
    #endregion

    #region creator
    internal Segment
      (SegmentsRowDSC segmentDSC, byte defRetries, IApplicationLayerMaster protocol, SegmentParameters parameters, bool demoVersio, ISegmentStatistics statistics, Channel myQueue)
      : base(protocol, parameters, demoVersio, statistics, myQueue)
    {
      CommServerComponent.Tracer.TraceVerbose(315, m_Src, "Creating the segment: " + segmentDSC.Name);
      myStatistics = statistics;
      dataQueue = new SegmentWaitTimeList(this, segmentDSC.GetInterfacesRows(), defRetries, "Segment." + segmentDSC.Name);
      CommServerComponent.Tracer.TraceVerbose(318, m_Src, "The segment " + segmentDSC.Name + "has been created");
    }
    #endregion

  }
}
#endif
