//_______________________________________________________________
//  Title   : Segment State Machine Class
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


using CAS.CommServer.ProtocolHub.Communication.Properties;
using CAS.Lib.CommonBus;
using CAS.Lib.CommonBus.ApplicationLayer;
using CAS.Lib.CommServerConsoleInterface;
using CAS.Lib.RTLib.Processes;
using System;
using Statistics = BaseStation.Management.Statistics;

namespace CAS.CommServer.ProtocolHub.Communication.BaseStation
{
  /// <summary>
  /// Segment State Machine Class
  /// </summary>
  internal class SegmentStateMachine: WaitTimeList<SegmentStateMachine>.TODescriptor
  {
    #region private
    #region readonly
    private readonly string mySource;
    private readonly SegmentState myConnectedState;
    private readonly SegmentState myDisconnectedState;
    private readonly SegmentState myDisconnectedAfterFailureState;
    private readonly SegmentState myKeepConnectionSegmentState;
    private readonly IApplicationLayerMaster myProtocol;
    private readonly ISegmentStatistics myStatistics;
    #endregion
    #region static
    private static bool myDemoMode;
    private static System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
    private static string GetAt { get { return ", At = " + myStopwatch.Elapsed.ToString(); } }
    static SegmentStateMachine() { myStopwatch.Start(); }
    #endregion
    private uint myNumberOfInProgressReaWriteOperations;
    private abstract class SegmentState
    {
      #region private
      protected Condition myOnExitCondition = new Condition();
      protected virtual void ActivateState()
      {
        myMachine.CurrentSegmentState = this;
        string msg = String.Format( Resources.TraceStateEntered, CurrentState.ToString() + GetAt );
        CommServerComponent.Tracer.TraceVerbose( 37, myMachine.mySource, msg );
        switch ( CurrentState )
        {
          case State.Connected:
            break;
          case State.KeepConnection:
            myMachine.myStatistics.NewState = Statistics.SegmentStatistics.States.Connected;
            break;
          case State.Disconnected:
          case State.DisconnectedAfterFailure:
            myMachine.myStatistics.NewState = Statistics.SegmentStatistics.States.Disconnected;
            break;
        }
      }
      protected virtual void ChangeState( SegmentState newState )
      {
        newState.ActivateState();
        myOnExitCondition.NotifyAll();
      }
      protected readonly SegmentStateMachine myMachine;
      #endregion
      #region creator
      protected SegmentState( SegmentStateMachine machine )
      {
        myMachine = machine;
      }
      #endregion
      #region public
      public abstract State CurrentState { get; }
      public virtual void ConnectRequest()
      {
        System.Diagnostics.Debug.Assert( false, "State nmachine error" );
      }
      public virtual void DisconnectRequest() { }
      public virtual void ReadData( Pipe.PipeInterface.PipeDataBlock blockDescription )
      {
        myOnExitCondition.Wait( myMachine );
        myMachine.CurrentSegmentState.ReadData( blockDescription );
      }
      public virtual bool ReadData( out object data, IBlockDescription dataAddress, Interface pipeInterface )
      {
        this.myMachine.ClearCounter();
        myMachine.myStatistics.NewState = Statistics.SegmentStatistics.States.ReadWaitingToBeConn;
        myOnExitCondition.Wait( myMachine );
        return myMachine.CurrentSegmentState.ReadData( out data, dataAddress, pipeInterface );
      }
      public virtual bool WriteData( object data, IBlockDescription dataAddress, Interface pipeInterface )
      {
        this.myMachine.ClearCounter();
        myMachine.myStatistics.NewState = Statistics.SegmentStatistics.States.WriteWaitingToBeConn;
        myOnExitCondition.Wait( myMachine );
        return myMachine.CurrentSegmentState.WriteData( data, dataAddress, pipeInterface );
      }
      #endregion
      #region debug
      [System.Diagnostics.Conditional("DEBUG")]
      public virtual void NotifyKeepConnectTimeElapsed()
      {
        throw new Exception( "The method or operation is not implemented." );
      }
      #endregion
    }
    private abstract class ReadWriteOperations: SegmentState
    {
      #region private
      private IReadValue ReadData( IBlockDescription dataAddress, Interface pipeInterface )
      {
        IReadValue value = null;
        switch ( myMachine.myProtocol.ReadData( dataAddress, pipeInterface.address, out value, pipeInterface.Retries.Retry ) )
        {
          case AL_ReadData_Result.ALRes_DatTransferErrr:
            pipeInterface.SwitchIOffAfterFailure();
            CommServerComponent.Tracer.TraceInformation
              ( 116, myMachine.mySource, String.Format( Resources.TraceALRes_DatTransferEr, pipeInterface.InterfaceNumber, pipeInterface.address ) + GetAt );
            System.Diagnostics.Debug.Assert( value == null,
              String.Format( Resources.ReadWriteOperationsReadDataAssertion,
              dataAddress.startAddress,
              pipeInterface.address,
              dataAddress.length,
              dataAddress.dataType,
              "ALRes_DatTransferErrr" ) );
            break;
          case AL_ReadData_Result.ALRes_DisInd:
            ChangeState( myMachine.myDisconnectedAfterFailureState );
            System.Diagnostics.Debug.Assert( value == null,
              String.Format( Resources.ReadWriteOperationsReadDataAssertion,
              dataAddress.startAddress,
              pipeInterface.address,
              dataAddress.length,
              dataAddress.dataType,
              "ALRes_DisInd" ) );
            break;
          case AL_ReadData_Result.ALRes_Success:
            System.Diagnostics.Debug.Assert( value != null,
              String.Format( Resources.ReadWriteOperationsReadDataAssertion,
              dataAddress.startAddress,
              pipeInterface.address,
              dataAddress.length,
              dataAddress.dataType,
              "ALRes_Success" ) );
            break;
        }
        return value;
      }
      #endregion
      #region public
      public override void ReadData( Pipe.PipeInterface.PipeDataBlock block )
      {
        IReadValue value = ReadData( block.GetBlockDescription, block.CoupledInterface );
        if ( value == null )
          return;
        if ( !myDemoMode )
          block.UpdateAllTags( value );
        value.ReturnEmptyEnvelope();
        block.ResetCounter();
      }
      public override bool ReadData( out object data, IBlockDescription dataAddress, Interface iNterface )
      {
        data = null;
        IReadValue value = ReadData( dataAddress, iNterface );
        if ( value == null )
          return false;
        bool result = true;
        try
        {
          data = value.ReadValue( 0, null );
        }
        catch ( Exception ex )
        {
          result = false;
          data = null;
          CommServerComponent.Tracer.TraceWarning( 187, "SegmentState.ReadData",
            "An exception was thrown during data read:  " + ex.Message );
        }
        value.ReturnEmptyEnvelope();
        return result;
      }
      public override bool WriteData( object data, IBlockDescription dataAddress, Interface pipeInterface )
      {
        IWriteValue messToSend = myMachine.myProtocol.GetEmptyWriteDataBuffor( dataAddress, pipeInterface.address );
        if ( messToSend == null )
          return false;
        messToSend.WriteValue( data, 0 );
        bool result = false;
        switch ( myMachine.myProtocol.WriteData( ref messToSend, pipeInterface.Retries.Retry ) )
        {
          case AL_ReadData_Result.ALRes_DatTransferErrr:
            pipeInterface.SwitchIOffAfterFailure();
            break;
          case AL_ReadData_Result.ALRes_DisInd:
            ChangeState( myMachine.myDisconnectedAfterFailureState );
            break;
          case AL_ReadData_Result.ALRes_Success:
            result = true;
            break;
        }
        System.Diagnostics.Debug.Assert( messToSend == null, "After Write operation write message should be null" );
        return result;
      }
      #endregion
      #region creator
      public ReadWriteOperations( SegmentStateMachine machine )
        : base( machine ) { }
      #endregion
    }//ReadWriteOperations
    private abstract class ConnectRequestOperation: SegmentState
    {
      #region private
      private IAddress myAddress;
      TimeSpan myTimeReconnect;
      protected override void ActivateState()
      {
        base.ActivateState();
        this.myMachine.Cycle = myTimeReconnect;
        this.myMachine.ResetCounter();
      }
      #endregion
      #region public
      public override void ConnectRequest()
      {
        switch ( myMachine.myProtocol.ConnectReq( myAddress ) )
        {
          case TConnectReqRes.Success:
            {
              ChangeState( myMachine.myKeepConnectionSegmentState );
              break;
            }
          case TConnectReqRes.NoConnection:
            {
              myMachine.myStatistics.MarkConnFail();
              ChangeState( myMachine.myDisconnectedAfterFailureState );
              break;
            }
        }
      }
      #endregion
      #region creator
      public ConnectRequestOperation( IAddress address, SegmentStateMachine machine, TimeSpan timeReconnect )
        : base( machine )
      {
        myAddress = address;
        myTimeReconnect = timeReconnect;
      }
      #endregion
    }//ConnectRequestOperation
    private sealed class DisconnectedSegmentState: ConnectRequestOperation
    {
      #region private
      protected override void ActivateState()
      {
        base.ActivateState();
      }
      #endregion
      #region public
      public override State CurrentState
      {
        get { return State.Disconnected; }
      }
      #endregion
      #region constructors
      public DisconnectedSegmentState( IAddress address, SegmentStateMachine machine, TimeSpan timeReconnect )
        : base( address, machine, timeReconnect ) { }
      #endregion
    }//DisconnectedSegmentState
    private sealed class KeepConnectionSegmentState: ReadWriteOperations
    {
      #region private
      private readonly TimeSpan myKeepConnect;
      private bool active = false;
      private void myPendulumEvent( object sender, EventArgs e )
      {
        if ( !active )
          return;
        System.Diagnostics.Debug.Assert( myMachine.CurrentSegmentState == this );
        this.ChangeState( myMachine.myConnectedState );
      }
      protected override void ActivateState()
      {
        base.ActivateState();
        active = true;
        myMachine.myPendulum.Change( myKeepConnect, TimeSpan.Zero );
      }
      protected override void ChangeState( SegmentState newState )
      {
        base.ChangeState( newState );
        myMachine.myPendulum.Change( System.Threading.Timeout.Infinite, -1 );
        active = false;
      }
      #endregion
      #region public
      public override State CurrentState
      {
        get { return State.KeepConnection; }
      }
      public override void DisconnectRequest()
      {
        base.DisconnectRequest();
        myOnExitCondition.Wait( myMachine );
        myMachine.CurrentSegmentState.DisconnectRequest();
      }
      #endregion
      #region constructors
      public KeepConnectionSegmentState( SegmentStateMachine machine, TimeSpan keepConnect )
        : base( machine )
      {
        myKeepConnect = keepConnect;
        myMachine.myPendulumEvent += new EventHandler( myPendulumEvent );
      }
      #endregion
      #region DEBUG
#if DEBUG
      public override void NotifyKeepConnectTimeElapsed()
      {
        ChangeState( myMachine.myConnectedState );
      }
#endif
      #endregion
    }//KeepConnectionSegmentState
    private sealed class ConnectedSegmentState: ReadWriteOperations
    {
      #region private
      private TimeSpan myIdleKeepConn;
      private bool myActive = false;
      private void myPendulumEvent( object sender, EventArgs e )
      {
        if ( !myActive )
          return;
        System.Diagnostics.Debug.Assert( myMachine.CurrentSegmentState == this );
        this.DisconnectRequest();
      }
      private void RestartStopWatch()
      {
        myMachine.myPendulum.Change( myIdleKeepConn, TimeSpan.Zero );
      }
      protected override void ActivateState()
      {
        base.ActivateState();
        myActive = true;
        RestartStopWatch();
      }
      protected override void ChangeState( SegmentState newState )
      {
        base.ChangeState( newState );
        myActive = false;
        myMachine.myPendulum.Change( System.Threading.Timeout.Infinite, -1 );
      }
      #endregion
      #region public
      public override State CurrentState
      {
        get { return State.Connected; }
      }
      public override void DisconnectRequest()
      {
        myMachine.myProtocol.DisReq();
        ChangeState( myMachine.myDisconnectedState );
      }
      public override void ReadData( Pipe.PipeInterface.PipeDataBlock blockDescription )
      {
        base.ReadData( blockDescription );
        RestartStopWatch();
      }
      public override bool ReadData( out object data, IBlockDescription dataAddress, Interface pipeInterface )
      {
        bool ret = base.ReadData( out data, dataAddress, pipeInterface );
        RestartStopWatch();
        return ret;
      }
      public override bool WriteData( object data, IBlockDescription dataAddress, Interface pipeInterface )
      {
        bool ret = base.WriteData( data, dataAddress, pipeInterface );
        RestartStopWatch();
        return ret;
      }
      #endregion
      #region constructor
      public ConnectedSegmentState( SegmentStateMachine machine, TimeSpan idleKeepConn )
        : base( machine )
      {
        myIdleKeepConn = idleKeepConn < TimeSpan.Zero ? TimeSpan.MaxValue : idleKeepConn;
        myMachine.myPendulumEvent += new EventHandler( myPendulumEvent );
      }
      #endregion
    }//ConnectedSegmentState
    private sealed class DisconnectedAfterFailureSegmentState: ConnectRequestOperation
    {
      #region private
      protected override void ActivateState()
      {
        base.ActivateState();
        CommServerComponent.Tracer.Trace
          ( System.Diagnostics.TraceEventType.Information, 333, myMachine.mySource, Resources.TraceConnectionError + GetAt );
        if ( myMachine.DisconnectedAfterFailureEntered != null )
          myMachine.DisconnectedAfterFailureEntered( this, null );
      }
      #endregion
      #region public
      public override State CurrentState
      {
        get { return State.DisconnectedAfterFailure; }
      }
      #endregion
      #region constructor
      public DisconnectedAfterFailureSegmentState( IAddress address, SegmentStateMachine machine, TimeSpan timeReconnectAfterFailure )
        : base( address, machine, timeReconnectAfterFailure )
      { }
      #endregion
    }//DisconnectedAfterFailureSegmentState
    private System.Threading.Timer myPendulum;
    private event EventHandler myPendulumEvent;
    private void PendulumHandler( object state )
    {
      lock ( this )
      {
        if ( myPendulumEvent != null )
          myPendulumEvent( this, null );
      }
    }
    private SegmentState myCurrentSegmentState;
    private SegmentState CurrentSegmentState
    {
      get { return myCurrentSegmentState; }
      set { myCurrentSegmentState = value; }
    }
    #endregion
    #region public
    /// <summary>
    /// Gets the current state of the object.
    /// </summary>
    /// <value>The state of the object.</value>
    public State CurrentState
    { get { return myCurrentSegmentState.CurrentState; } }
    /// <summary>
    /// Connect request.
    /// </summary>
    public void ConnectRequest()
    {
      lock ( this )
        myCurrentSegmentState.ConnectRequest();
    }
    /// <summary>
    /// Disconnect request.
    /// </summary>
    public void DisconnectRequest()
    {
      lock ( this )
        myCurrentSegmentState.DisconnectRequest();
    }
    /// <summary>
    /// Reads the data.
    /// </summary>
    /// <param name="blockDescription">The block description.</param>
    public void ReadData( Pipe.PipeInterface.PipeDataBlock blockDescription )
    {
      lock ( this )
      {
        myNumberOfInProgressReaWriteOperations++;
        CurrentSegmentState.ReadData( blockDescription );
        myNumberOfInProgressReaWriteOperations--;
      }
    }
    /// <summary>
    /// Reads the data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="dataAddress">The data address.</param>
    /// <param name="interfaceObject">The interface object.</param>
    /// <returns></returns>
    public bool ReadData( out object data, IBlockDescription dataAddress, Interface interfaceObject )
    {
      lock ( this )
      {
        myNumberOfInProgressReaWriteOperations++;
        bool result = CurrentSegmentState.ReadData( out data, dataAddress, interfaceObject );
        myNumberOfInProgressReaWriteOperations--;
        return result;
      }
    }
    /// <summary>
    /// Writes the data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="dataAddress">The data address.</param>
    /// <param name="pipeInterface">The coupled <see cref="Interface"/> object.</param>
    /// <returns></returns>
    public bool WriteData( object data, IBlockDescription dataAddress, Interface pipeInterface )
    {
      lock ( this )
      {
        myNumberOfInProgressReaWriteOperations++;
        bool result = CurrentSegmentState.WriteData( data, dataAddress, pipeInterface );
        myNumberOfInProgressReaWriteOperations--;
        return result;
      }
    }
    /// <summary>
    ///Occurs when segment cannot establish or keep connection because of failure.
    /// </summary>
    public event EventHandler DisconnectedAfterFailureEntered;
    /// <summary>
    /// The state enumeratior.
    /// </summary>
    public enum State { KeepConnection, Connected, Disconnected, DisconnectedAfterFailure }
    /// <summary>
    /// Gets a value indicating whether it needs channel access.
    /// </summary>
    /// <value><c>true</c> if the object needs channel access; otherwise, <c>false</c>.</value>
    public bool NeedsChannelAccess
    {
      get { return myNumberOfInProgressReaWriteOperations > 0; }
    }
    /// <summary>
    /// Sets a value indicating whether demo mode.
    /// </summary>
    /// <value><c>true</c> if demo mode; otherwise, <c>false</c>.</value>
    public static bool DemoMode { set { myDemoMode = value; } }
    #endregion
    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SegmentStateMachine"/> class.
    /// </summary>
    /// <param name="protocol">The protocol.</param>
    /// <param name="parameters">The parameters.</param>
    /// <param name="demoVersion">if set to <c>true</c> demo version.</param>
    /// <param name="statistics">The statistics.</param>
    /// <param name="waitTimeList">The wait time list.</param>
    internal SegmentStateMachine
      (
      IApplicationLayerMaster protocol, SegmentParameters parameters, bool demoVersion, ISegmentStatistics statistics,
      WaitTimeList<SegmentStateMachine> waitTimeList
      )
      : base( waitTimeList, parameters.TimeReconnect )
    {
      mySource = this.GetType().FullName.ToString() + String.Format( "[{0}]", parameters.Name );
      mySource = String.Format( mySource, statistics.GetOPCPrefix );
      myDemoMode = demoVersion;
      myProtocol = new GuardedDataProvider( "Segment name = " + parameters.Name, protocol );
      myStatistics = statistics;
      myConnectedState = new ConnectedSegmentState( this, parameters.TimeIdleKeepConnection );
      myDisconnectedState = new DisconnectedSegmentState( parameters.SegmentAddress, this, parameters.TimeReconnect );
      myDisconnectedAfterFailureState =
        new DisconnectedAfterFailureSegmentState( parameters.SegmentAddress, this, parameters.TimeReconnectAfterFailure );
      myKeepConnectionSegmentState = new KeepConnectionSegmentState( this, parameters.TimeKeepConnrction );
      myCurrentSegmentState = myDisconnectedState;
      double period =
        Math.Min( parameters.TimeKeepConnrction.TotalMilliseconds, parameters.TimeIdleKeepConnection.TotalMilliseconds ) / 20;
      period = Math.Max( period, 20.0 );
      myPendulum = new System.Threading.Timer( new System.Threading.TimerCallback( PendulumHandler ) );
    }
    #endregion

    #region DEBUG
    [System.Diagnostics.Conditional("DEBUG")]
    internal virtual void NotifyKeepConnectTimeElapsed()
    {
      lock ( this )
        CurrentSegmentState.NotifyKeepConnectTimeElapsed();
    }
    #endregion
  }
}
