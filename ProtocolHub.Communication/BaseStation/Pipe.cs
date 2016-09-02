//_______________________________________________________________
//  Title   : Pipe description class
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
using System.Collections;

namespace CAS.CommServer.ProtocolHub.Communication.BaseStation
{
  /// <summary>
  /// Pipe description class.
  /// </summary>
  public abstract class Pipe : IDataWrite
  {

    #region private
    private const string m_Src = "CAS.Lib.CommServer.Pipe";
    private byte myInterfaceCount = 0;
    /// <summary>
    ///  Title   : Pipe description class  
    /// </summary>
    private class PipeHandlerWaitTimeList : HandlerWaitTimeList<Interface>
    {
      protected override void Handler(Interface myDsc) { myDsc.SwitchIOn(); }
      internal PipeHandlerWaitTimeList()
        : base(false, "_IntInact")
      { }
    }
    private const short InterfaceMaxNum = 2;
    private PipeInterface[] interfaces = new PipeInterface[InterfaceMaxNum];
    private short currInterfaceNum = -1;
    private static PipeHandlerWaitTimeList myInterfaceWTList;
    private void SwitchOnNext(short interfaceNum)
    {
      ushort runIdx = 0; //zabezpiecza, gdy pusta lista dostêpnych interfejsów
      short idx = interfaceNum;
      do
        idx = (short)((++idx) % InterfaceMaxNum);
      while
        ((idx != interfaceNum) && (runIdx++ < InterfaceMaxNum) &&
        ((interfaces[idx] == null) || !interfaces[idx].IsReady()));
      if ((idx != interfaceNum) && (runIdx < InterfaceMaxNum))
        interfaces[idx].SwitchIOn();
    }
    /// <summary>
    /// Gets the get data description list.
    /// </summary>
    /// <value>The get data description list.</value>
    protected abstract IEnumerable GetDataDescriptionList { get; }
    /// <summary>
    /// Statistics
    /// </summary>
    protected Diagnostic.Station myStatistics;
    #endregion

    #region IDataWrite
    bool IDataWrite.WriteData(object data, IBlockDescription addresss)
    {
      if (currInterfaceNum < 0)
        return false;
      bool result = interfaces[currInterfaceNum].WriteData(data, addresss);
      if (result)
        interfaces[currInterfaceNum].Retries.MarkSuccess();
      else
        interfaces[currInterfaceNum].Retries.MarkFail();
      return result;
    }
    bool IDataWrite.ReadData(out object data, IBlockDescription addresss)
    {
      data = null;
      if (currInterfaceNum < 0)
        return false;
      bool result = interfaces[currInterfaceNum].ReadData(out data, addresss);
      if (result)
        interfaces[currInterfaceNum].Retries.MarkSuccess();
      else
        interfaces[currInterfaceNum].Retries.MarkFail();
      return result;
    }
    #endregion

    #region public
    /// <summary>
    /// Pipe description class.
    /// </summary>
    internal abstract class PipeInterface : Interface
    {
      #region private
      private RetryFilter myRetries;
      private enum State { Ioff, Ion, Ihold };
      readonly private Pipe myPipe;
      private State myInterfaceState = State.Ioff;
      private State interfaceState
      {
        get { return myInterfaceState; }
        set
        {
          myInterfaceState = value;
        }
      }
      // lista wskaŸników do kolejki TO segmentu 
      ArrayList myPipeDataBlockList = new ArrayList();
      private void DataBlocksScanningOff()
      {
        foreach (PipeDataBlock curr in myPipeDataBlockList)
          curr.Remove();
      }
      #endregion
      #region PUBLIC
      internal protected override RetryFilter Retries { get { return myRetries; } }
      /// <summary>
      ///  Title   : Pipe description class 
      /// </summary>
      internal class PipeDataBlock : WaitTimeList<PipeDataBlock>.TODescriptor
      {
        #region PRIVATE
        readonly private DataQueue.DataDescription myData;
        readonly private PipeInterface myInterface;
        /// <summary>
        /// Event handler - marks new scannin time of datablock
        /// </summary>
        /// <param name="sender">no applicable</param>
        /// <param name="e">no applicable</param>
        private void NotifyNewTimeScan(object sender, EventArgs e)
        {
          Cycle = myData.TimeScann;
        }
        #endregion
        #region PUBLIC
        internal IBlockDescription GetBlockDescription
        {
          get { return myData; }
        }
        internal void UpdateAllTags(IReadValue val)
        {
          myInterface.myRetries.MarkSuccess();
          myData.UpdateAllTags(val);
        }
        internal ushort InterfaceAddr
        {
          get { return myInterface.address; }
        }
        internal byte GetRetries { get { return myInterface.Retries.Retry; } }
        internal PipeInterface CoupledInterface { get { return myInterface; } }
        internal void MarkEndOfRWOperation()
        {
          myInterface.MarkEndOfRWOperation();
        }
        internal PipeDataBlock
          (WaitTimeList<PipeDataBlock> myTOQueue, DataQueue.DataDescription myData, PipeInterface myInterface)
          :
          base(myTOQueue, myData.TimeScann)
        {
          this.myData = myData;
          this.myData.NotifyNewTimeScan += new EventHandler(NotifyNewTimeScan);
          this.myInterface = myInterface;
        }
        #endregion
      }
      /// <summary>
      /// Switches the Interface off after communication failure.
      /// </summary>
      internal override void SwitchIOffAfterFailure()
      {
        lock (myPipe)
        {
          if (interfaceState != State.Ion)
            return;
          myRetries.MarkFail();
          myPipe.currInterfaceNum = -1;
          base.SwitchIOffAfterFailure();
          DataBlocksScanningOff();
          interfaceState = State.Ihold;
          myPipe.SwitchOnNext((short)InterfaceNumber);
        }
      }
      internal override void SwitchIOff()
      {
        lock (myPipe)
        {
          if (interfaceState != State.Ion)
            return;
          myPipe.currInterfaceNum = -1;
          base.SwitchIOff();
          interfaceState = State.Ioff;
          DataBlocksScanningOff();
        }
      }
      internal override void SwitchIOn()
      {
        lock (myPipe)
        {
          System.Diagnostics.Debug.Assert(interfaceState != State.Ion);
          if (myPipe.currInterfaceNum >= 0)
            myPipe.interfaces[myPipe.currInterfaceNum].SwitchIOff();
          myPipe.currInterfaceNum = (short)InterfaceNumber;
          interfaceState = State.Ion;
          base.SwitchIOn();
          foreach (PipeDataBlock curr in myPipeDataBlockList)
            curr.ResetCounter();
        }
      }
      internal bool IsReady()
      {
        return (interfaceState == State.Ioff);
      }
      internal PipeInterface
        (
        Interface.Parameters interfaceParameters,
        Pipe myPipe,
        WaitTimeList<PipeDataBlock> segmentScanningWTL,
        IInterface2SegmentLink statSegment,
        byte defRetries
        )
        : base(interfaceParameters, myInterfaceWTList, statSegment, myPipe.myStatistics)
      {
        Redundancy.CheckIfAllowed(myPipe.myInterfaceCount, interfaceParameters.Name);
        myPipe.myInterfaceCount++;
        this.myPipe = myPipe;
        this.myRetries = new RetryFilter(defRetries);
        myPipe.interfaces[InterfaceNumber] = this;
        foreach (DataQueue.DataDescription currDD in myPipe.GetDataDescriptionList)
          // jesli blok nie ma zdefiniowanych tag'ow nie ma sensu go skanowaæ
          if (((IBlockDescription)currDD).length > 0)
          {
            PipeDataBlock block = new PipeDataBlock(segmentScanningWTL, currDD, this);
            myPipeDataBlockList.Add((block));
            block.ResetCounter();
          }
      } //internal PipeInterface
      #endregion
    } //class PipeInterface
    /// <summary>
    /// It allows to switch on (true)/off (false) pipe. 
    /// </summary>
    internal bool SwitchPipe
    {
      set
      {
        lock (this)
        {
          if ((value) && (currInterfaceNum < 0))
            SwitchOnNext(currInterfaceNum);
          else if ((!value) && (currInterfaceNum >= 0))
            interfaces[currInterfaceNum].SwitchIOff();
        }
      }
    }
    //TODO nie u¿ywana, ale moze sie przydac prze zarzadzaniu 
    //    internal void ExchangeInterfaces()
    //    {
    //      SwitchOnNext(currInterfaceNum);
    //    } //END SwitchCommPort;
    static Pipe()
    {
      myInterfaceWTList = new PipeHandlerWaitTimeList();
    }
    #endregion

  } //public abstract class Pipe
} //Pipe

