//<summary>
//  Title   : Unit Tests for Segment State Machine
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    200709 mpostol - created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

#pragma warning disable 1591

#if  DEBUG
using System;
using System.Threading;
using CAS.Lib.CommonBus.ApplicationLayer;
using CAS.Lib.RTLib.Processes;
using CAS.NetworkConfigLib;
using NUnit.Framework;

namespace CAS.Lib.CommServer.Tests
{
  [SetUpFixture]
  public class SetUpOfFixture
  {
    public static CAS.NetworkConfigLib.ComunicationNet myConfig;
    [SetUp]
    public void OnceExecutedSetUp()
    {
      NetworkConfig.XMLManagement xml = 
        new NetworkConfig.XMLManagement( BaseStation.Management.AppConfigManagement.filename );
      Assert.IsNotNull( xml.configuration, "Problem with configuration file: null configuration" );
      Assert.Greater( xml.configuration.Channels.Count, 0, "Problem with configuration file: 0 channels" );
      myConfig = xml.configuration;
      int volumeConstrain = int.MaxValue;
      Station.InitStations( myConfig.Station, ref volumeConstrain );
    }
    [TearDown]
    public void OnceExecutedTearDown()
    {
    }
  }
  [TestFixture]
  public class TestSegmentStateMachine
  {
    #region private
    internal class FacadeWaitTimeList: WaitTimeList<SegmentStateMachine>
    {
      internal FacadeWaitTimeList() : base( "TestSegmentStateMachine" ) { }
    }
    private FacadeWaitTimeList myTimeList = new FacadeWaitTimeList();
    private SegmentStateMachine myMachine;
    private FacadeSegment.FacadePipeInterface.FacadePipeDataBlock myPipeDataBlock;
    private FacadeApplicationLayerMaster myMaster;
    private IBlockDescription myBlockDescription = new FacadeBlockDescription( int.MaxValue, int.MaxValue, 0 );
    private FacadeSegment.FacadePipeInterface myInterface;
    private FacadeISegmentStatistics myFacadeISegmentStatistics;
    private TimeSpan FiveSeconds = new TimeSpan( 0, 0, 0, 5, 0 );
    private SegmentParameters parameters;
    private int myNumberOfThreads = 0;
    private void MakeConnection()
    {
      myMachine.ConnectRequest();
      Assert.AreEqual( SegmentStateMachine.State.KeepConnection, myMachine.CurrentState );
      TestRead();
      Assert.AreEqual( SegmentStateMachine.State.KeepConnection, myMachine.CurrentState );
      TestWriteData();
      Assert.AreEqual( SegmentStateMachine.State.KeepConnection, myMachine.CurrentState );
    }
    private bool myMachine_DisconnectedAfterFailureEnteredExecuted = false;
    private void myMachine_DisconnectedAfterFailureEntered( object sender, EventArgs e )
    {
      myMachine_DisconnectedAfterFailureEnteredExecuted = true;
    }
    private void TestRead()
    {
      //myMachine.ReadData( myPipeDataBlock );
      object data;
      IBlockDescription dataAddress = new FacadeBlockDescription( int.MaxValue, int.MaxValue, short.MaxValue );
      myMachine.ReadData( out data, dataAddress, myInterface );
    }
    private void TestWriteData()
    {
      myMachine.WriteData( 0, myBlockDescription, myInterface );
    }
    private void AssertConnected()
    {
      Assert.AreEqual( SegmentStateMachine.State.Connected, myMachine.CurrentState );
    }
    private void AssertDisconnected()
    {
      Assert.AreEqual( SegmentStateMachine.State.Disconnected, myMachine.CurrentState );
    }
    private void AssertKeepConnection()
    {
      Assert.AreEqual( SegmentStateMachine.State.KeepConnection, myMachine.CurrentState );
    }
    private void WaitCallbackHndle( object state )
    {
      System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
      TestSegmentStateMachine myParent = (TestSegmentStateMachine)state;
      myStopwatch.Reset();
      myStopwatch.Start();
      while ( myStopwatch.Elapsed < FiveSeconds )
      {
        TestRead();
        TestWriteData();
      }
      System.Threading.Interlocked.Decrement( ref myParent.myNumberOfThreads );
    }
    private void SegmentTiming()
    {
      ComunicationNet.SegmentsRow segmentRow = SetUpOfFixture.myConfig.Segments[ 0 ];
      segmentRow.KeepConnect = false;
      segmentRow.TimeIdleKeepConn = 100;
      segmentRow.timeKeepConn = 5000;
      segmentRow.TimeReconnect = 10000;
      segmentRow.TimeScan = 10000;
      parameters = new SegmentParameters( segmentRow );
    }
    #endregion
    #region SetUp
    [SetUp]
    public void SetUp()
    {
      FacadePipe myPipe = new FacadePipe( SetUpOfFixture.myConfig.Station[ 0 ] );
      myMaster = new FacadeApplicationLayerMaster();
      myFacadeISegmentStatistics = new FacadeISegmentStatistics();

      SegmentTiming();

      myMachine = new SegmentStateMachine( myMaster, parameters, false, myFacadeISegmentStatistics, myTimeList );
      myMachine.ResetCounter();
      myMachine.DisconnectedAfterFailureEntered += new EventHandler( myMachine_DisconnectedAfterFailureEntered );
      FacadeSegment myFacadeSegment = new FacadeSegment();
      int myMaxNumberOfTags = int.MaxValue;
      BaseStation.Management.Statistics.ChannelStatistics myChanel =
        new BaseStation.Management.Statistics.ChannelStatistics( SetUpOfFixture.myConfig.Channels[ 0 ] );
      BaseStation.Management.Segment mySegment = new BaseStation.Management.Segment( SetUpOfFixture.myConfig.Segments[ 0 ], myChanel );
      myInterface = ( new FacadeSegment.FacadePipeInterface
        ( new Interface.Parameters( SetUpOfFixture.myConfig.Interfaces[ 0 ] ), myPipe, mySegment ) );
      myInterface.ResetCounter();
      FacadeSegment.FacadeDataDescription myDataDescription =
        new FacadeSegment.FacadeDataDescription( SetUpOfFixture.myConfig.DataBlocks[ 0 ], ref myMaxNumberOfTags );
      myDataDescription.ResetCounter();
      myPipeDataBlock = new FacadeSegment.FacadePipeInterface.FacadePipeDataBlock( myFacadeSegment, myDataDescription, myInterface );
      myPipeDataBlock.ResetCounter();
    }

    [NUnit.Framework.TearDown]
    public void TearDown()
    {
      Console.WriteLine( "TearDown enterwd" );
      myMaster.CheckConsistency();
    }
    #endregion
    #region Test TestSuccess
    [Test]
    public void TestSuccess()
    {
      string KeepConnectExpectedTimeTemplate = "Expected KeepConnect time      ={0}";
      string KeepConnectAcctualTimeTemplate = "KeepConnect time               ={0}";
      string ConnectExpectedTimeTemplate = "Expected connect time          ={0}";
      string CoccectAcctualTimeTemplate = "Connect time                   ={0}";
      string IdleKeepConnectExpectedTimeTemplate = "Expected IdleKeepConnect time  ={0}";
      string IdleKeepConnectAcctualTimeTemplate = "IdleKeepConnect time           ={0}";

      MakeConnection();
      AssertKeepConnection();

      System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
      myStopwatch.Start();
      TimeSpan maxValue = parameters.TimeKeepConnrction + new TimeSpan( 0, 0, 0, 0, 100 );
      Console.WriteLine( KeepConnectExpectedTimeTemplate, parameters.TimeKeepConnrction.ToString() );
      while ( myMachine.CurrentState == SegmentStateMachine.State.KeepConnection )
      {
        Assert.Less( myStopwatch.Elapsed, maxValue, "Timing error - too log keep connect state" );
        Assert.IsFalse( myMachine.NeedsChannelAccess, "inconsistency of the myMachine.NeedsChannelAccess" );
        TestRead();
        TestWriteData();
        Thread.Sleep( 1 );
      }
      AssertConnected();
      Assert.GreaterOrEqual( myStopwatch.Elapsed, parameters.TimeKeepConnrction, "Timing error - too short keep connect state" );
      Console.WriteLine( KeepConnectAcctualTimeTemplate, myStopwatch.Elapsed.ToString() );

      myStopwatch.Reset();
      myStopwatch.Start();

      Console.WriteLine( ConnectExpectedTimeTemplate, FiveSeconds.ToString() );
      while ( myStopwatch.Elapsed < FiveSeconds )
      {
        TestRead();
        TestWriteData();
        Thread.Sleep( 1 );
        AssertConnected();
      }
      Console.WriteLine( CoccectAcctualTimeTemplate, myStopwatch.Elapsed.ToString() );

      myStopwatch.Reset();
      myStopwatch.Start();
      Console.WriteLine( IdleKeepConnectExpectedTimeTemplate, parameters.TimeIdleKeepConnection.ToString() );
      maxValue = parameters.TimeIdleKeepConnection + new TimeSpan( 0, 0, 0, 0, 100 );
      while ( myMachine.CurrentState == SegmentStateMachine.State.Connected )
      {
        Assert.Less( myStopwatch.Elapsed, maxValue, "Timing error - to log idle keep connect state" );
        Thread.Sleep( 1 );
      }
      Assert.GreaterOrEqual( myStopwatch.Elapsed, parameters.TimeIdleKeepConnection, "Timing error - to short idle keep connect state" );
      AssertDisconnected();
      Console.WriteLine( IdleKeepConnectAcctualTimeTemplate, myStopwatch.Elapsed.ToString() );

      MakeConnection();
      myMachine.NotifyKeepConnectTimeElapsed();
      myMachine.DisconnectRequest();
      AssertDisconnected();
    }
    #endregion
    #region TestAsynchronousDisconnect
    [Test]
    public void TestAsynchronousDisconnect()
    {
      int workerThreads;
      int completionPortThreads;
      System.Threading.ThreadPool.GetAvailableThreads( out workerThreads, out completionPortThreads );
      Console.WriteLine( "number of available threads worker= {0}; CompletionPort= {1}", workerThreads, completionPortThreads );
      Assert.IsFalse( myMachine.NeedsChannelAccess, "inconsistency of the myMachine.NeedsChannelAccess" );
      myNumberOfThreads = workerThreads;
      for ( int index = 0; index < myNumberOfThreads; index++ )
        System.Threading.ThreadPool.QueueUserWorkItem( new System.Threading.WaitCallback( WaitCallbackHndle ), this );
      //
      System.Threading.ThreadPool.GetAvailableThreads( out workerThreads, out completionPortThreads );
      Console.WriteLine( "number of available threads worker= {0}; CompletionPort= {1}", workerThreads, completionPortThreads );
      Thread.Sleep( 1000 );
      System.Threading.ThreadPool.GetAvailableThreads( out workerThreads, out completionPortThreads );
      Console.WriteLine( "number of available threads worker= {0}; CompletionPort= {1}", workerThreads, completionPortThreads );
      //
      while ( !System.Threading.Interlocked.Equals( myNumberOfThreads, 0 ) )
      {
        MakeConnection();
        AssertKeepConnection();
        myMachine.DisconnectRequest();
        AssertDisconnected();
      }
      Console.WriteLine( myMaster.ToString() );
      AssertDisconnected();
      Assert.IsFalse( myMachine.NeedsChannelAccess, "inconsistency of the myMachine.NeedsChannelAccess" );
      System.Threading.ThreadPool.GetAvailableThreads( out workerThreads, out completionPortThreads );
      Console.WriteLine( "number of available threads worker= {0}; CompletionPort= {1}", workerThreads, completionPortThreads );
    }
    #endregion
    #region Test TestRWFailure
    [Test]
    public void TestRWFailure()
    {
      myMachine.ConnectRequest();
      Assert.AreEqual( SegmentStateMachine.State.KeepConnection, myMachine.CurrentState );

      myMaster.MakeError();
      TestRead();
      Assert.AreEqual( SegmentStateMachine.State.KeepConnection, myMachine.CurrentState );
      myMaster.MakeError();
      TestWriteData();
      Assert.AreEqual( SegmentStateMachine.State.KeepConnection, myMachine.CurrentState );

      myMachine.NotifyKeepConnectTimeElapsed();
      Assert.AreEqual( SegmentStateMachine.State.Connected, myMachine.CurrentState );

      myMaster.MakeError();
      TestRead();
      Assert.AreEqual( SegmentStateMachine.State.Connected, myMachine.CurrentState );
      myMaster.MakeError();
      TestWriteData();
      Assert.AreEqual( SegmentStateMachine.State.Connected, myMachine.CurrentState );
      myMachine.DisconnectRequest();
      Assert.AreEqual( SegmentStateMachine.State.Disconnected, myMachine.CurrentState );
    }
    #endregion
    #region Test TestConnectionAbort
    [Test]
    public void TestConnectionAbort()
    {
      myMaster.BreakConnection();
      myMachine.ConnectRequest();
      Assert.AreEqual( SegmentStateMachine.State.DisconnectedAfterFailure, myMachine.CurrentState );

      MakeConnection();

      myMaster.BreakConnection();
      TestRead();
      Assert.AreEqual( SegmentStateMachine.State.DisconnectedAfterFailure, myMachine.CurrentState );
      Assert.IsTrue( myMachine_DisconnectedAfterFailureEnteredExecuted, "Disconnected After Failure Entered Executed not executed" );
      myMachine_DisconnectedAfterFailureEnteredExecuted = false;

      MakeConnection();

      myMaster.BreakConnection();
      TestWriteData();
      Assert.AreEqual( SegmentStateMachine.State.DisconnectedAfterFailure, myMachine.CurrentState );
      Assert.IsTrue( myMachine_DisconnectedAfterFailureEnteredExecuted, "Disconnected After Failure Entered Executed not executed" );
      myMachine_DisconnectedAfterFailureEnteredExecuted = false;
      Assert.IsFalse( myMachine.NeedsChannelAccess, "inconsistency of the myMachine.NeedsChannelAccess" );
    }
    #endregion
  }
}
#endif //DEBUG