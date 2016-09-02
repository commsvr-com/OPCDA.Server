//_______________________________________________________________
//  Title   : Name of Application
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

#pragma warning disable 1591

using CAS.CommServer.ProtocolHub.Communication;
using CAS.CommServer.ProtocolHub.Communication.BaseStation;
using CAS.CommServer.ProtocolHub.CommunicationUnitTests.Instrumentation;
using CAS.Lib.CommonBus.ApplicationLayer;
using CAS.Lib.RTLib.Processes;
using CAS.NetworkConfigLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using DiagnosticSegment = CAS.CommServer.ProtocolHub.Communication.Diagnostic.Segment;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests
{
  [TestClass]
  [DeploymentItem("DefaultConfig.xml")]
  public class TestSegmentStateMachine
  {

    [ClassInitialize]
    public static void OnceExecutedSetUp(TestContext context)
    {
      Assert.IsFalse(m_OnceExecutedSetUpFinished);
      FileInfo _configuration = new FileInfo("DefaultConfig.xml");
      Assert.IsTrue(_configuration.Exists);
      m_ConfigurationMain.ReadConfiguration(_configuration.FullName);
      Assert.IsNotNull(myConfig, "Problem with configuration file: null configuration");
      Assert.IsTrue(myConfig.Channels.Count > 0, "Problem with configuration file: 0 channels");
      int volumeConstrain = int.MaxValue;
      Station.InitStations(myConfig.Station, ref volumeConstrain);
      SetUp();
      m_OnceExecutedSetUpFinished = true;
    }
    [ClassCleanup]
    public static void OnceExecutedTearDown()
    {
      Assert.IsTrue(m_OnceExecutedSetUpFinished);
      myMaster.CheckConsistency();
      myConfig.Dispose();
    }
    private static void SetUp()
    {
      FacadePipe myPipe = new FacadePipe(myConfig.Station[0]);
      myMaster = new FacadeApplicationLayerMaster();
      myFacadeISegmentStatistics = new FacadeISegmentStatistics();

      SegmentTiming();

      myMachine = new SegmentStateMachine(myMaster, parameters, false, myFacadeISegmentStatistics, myTimeList);
      myMachine.ResetCounter();
      myMachine.DisconnectedAfterFailureEntered += new EventHandler(myMachine_DisconnectedAfterFailureEntered);
      FacadeSegment myFacadeSegment = new FacadeSegment();
      int myMaxNumberOfTags = int.MaxValue;
      BaseStation.Management.Statistics.ChannelStatistics myChanel = new BaseStation.Management.Statistics.ChannelStatistics(myConfig.Channels[0]);
      DiagnosticSegment mySegment = new DiagnosticSegment(myConfig.Segments[0], myChanel);
      myInterface = (new FacadeSegment.FacadePipeInterface(new Interface.Parameters(myConfig.Interfaces[0]), myPipe, mySegment));
      myInterface.ResetCounter();
      FacadeSegment.FacadeDataDescription myDataDescription = new FacadeSegment.FacadeDataDescription(myConfig.DataBlocks[0], ref myMaxNumberOfTags);
      myDataDescription.ResetCounter();
      myPipeDataBlock = new FacadeSegment.FacadePipeInterface.FacadePipeDataBlock(myFacadeSegment, myDataDescription, myInterface);
      myPipeDataBlock.ResetCounter();
    }

    #region private
    private class FacadeWaitTimeList : WaitTimeList<SegmentStateMachine>
    {
      internal FacadeWaitTimeList() : base("TestSegmentStateMachine") { }
    }
    private static bool m_OnceExecutedSetUpFinished = false;
    private static CommServerConfigurationMain m_ConfigurationMain = new CommServerConfigurationMain();
    private static ComunicationNet myConfig { get { return m_ConfigurationMain.Configuartion; } }
    private static FacadeWaitTimeList myTimeList = new FacadeWaitTimeList();
    private static SegmentStateMachine myMachine;
    private static FacadeSegment.FacadePipeInterface.FacadePipeDataBlock myPipeDataBlock;
    private static FacadeApplicationLayerMaster myMaster;
    private static FacadeSegment.FacadePipeInterface myInterface;
    private static FacadeISegmentStatistics myFacadeISegmentStatistics;
    private static SegmentParameters parameters;
    private static bool myMachine_DisconnectedAfterFailureEnteredExecuted = false;

    private IBlockDescription myBlockDescription = new FacadeBlockDescription(int.MaxValue, int.MaxValue, 0);
    private TimeSpan FiveSeconds = new TimeSpan(0, 0, 0, 5, 0);
    private int myNumberOfThreads = 0;
    private void MakeConnection()
    {
      myMachine.ConnectRequest();
      Assert.AreEqual(SegmentStateMachine.State.KeepConnection, myMachine.CurrentState);
      TestRead();
      Assert.AreEqual(SegmentStateMachine.State.KeepConnection, myMachine.CurrentState);
      TestWriteData();
      Assert.AreEqual(SegmentStateMachine.State.KeepConnection, myMachine.CurrentState);
    }
    private static void myMachine_DisconnectedAfterFailureEntered(object sender, EventArgs e)
    {
      myMachine_DisconnectedAfterFailureEnteredExecuted = true;
    }
    private void TestRead()
    {
      //myMachine.ReadData( myPipeDataBlock );
      object data;
      IBlockDescription dataAddress = new FacadeBlockDescription(int.MaxValue, int.MaxValue, short.MaxValue);
      myMachine.ReadData(out data, dataAddress, myInterface);
    }
    private void TestWriteData()
    {
      myMachine.WriteData(0, myBlockDescription, myInterface);
    }
    private void AssertConnected()
    {
      Assert.AreEqual(SegmentStateMachine.State.Connected, myMachine.CurrentState);
    }
    private void AssertDisconnected()
    {
      Assert.AreEqual(SegmentStateMachine.State.Disconnected, myMachine.CurrentState);
    }
    private void AssertKeepConnection()
    {
      Assert.AreEqual(SegmentStateMachine.State.KeepConnection, myMachine.CurrentState);
    }
    private void WaitCallbackHandle(object state)
    {
      System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
      TestSegmentStateMachine myParent = (TestSegmentStateMachine)state;
      myStopwatch.Reset();
      myStopwatch.Start();
      while (myStopwatch.Elapsed < FiveSeconds)
      {
        TestRead();
        TestWriteData();
      }
      System.Threading.Interlocked.Decrement(ref myParent.myNumberOfThreads);
    }
    private static void SegmentTiming()
    {
      ComunicationNet.SegmentsRow segmentRow = myConfig.Segments[0];
      segmentRow.KeepConnect = false;
      segmentRow.TimeIdleKeepConn = 100;
      segmentRow.timeKeepConn = 5000;
      segmentRow.TimeReconnect = 10000;
      segmentRow.TimeScan = 10000;
      parameters = new SegmentParameters(segmentRow);
    }
    #endregion

    [TestMethod]
    public void TestSuccess()
    {
      try
      {
        string KeepConnectExpectedTimeTemplate = "Expected KeepConnect time      ={0}";
        string KeepConnectActualTimeTemplate = "KeepConnect time               ={0}";
        string ConnectExpectedTimeTemplate = "Expected connect time          ={0}";
        string CoccectAcctualTimeTemplate = "Connect time                   ={0}";
        string IdleKeepConnectExpectedTimeTemplate = "Expected IdleKeepConnect time  ={0}";
        string IdleKeepConnectActualTimeTemplate = "IdleKeepConnect time           ={0}";

        MakeConnection();
        AssertKeepConnection();

        System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
        myStopwatch.Start();
        TimeSpan maxValue = parameters.TimeKeepConnrction + new TimeSpan(0, 0, 0, 0, 100);
        Console.WriteLine(KeepConnectExpectedTimeTemplate, parameters.TimeKeepConnrction.ToString());
        while (myMachine.CurrentState == SegmentStateMachine.State.KeepConnection)
        {
          Assert.IsTrue(myStopwatch.Elapsed < maxValue, "Timing error - too log keep connect state");
          Assert.IsFalse(myMachine.NeedsChannelAccess, "inconsistency of the myMachine.NeedsChannelAccess");
          TestRead();
          TestWriteData();
          Thread.Sleep(1);
        }
        AssertConnected();
        Assert.IsTrue(myStopwatch.Elapsed >= parameters.TimeKeepConnrction, $"Timing error - too short keep connect state time {myStopwatch.Elapsed } expected {parameters.TimeKeepConnrction}");
        Console.WriteLine(KeepConnectActualTimeTemplate, myStopwatch.Elapsed.ToString());

        myStopwatch.Reset();
        myStopwatch.Start();

        Console.WriteLine(ConnectExpectedTimeTemplate, FiveSeconds.ToString());
        while (myStopwatch.Elapsed < FiveSeconds)
        {
          TestRead();
          TestWriteData();
          Thread.Sleep(1);
          AssertConnected();
        }
        Console.WriteLine(CoccectAcctualTimeTemplate, myStopwatch.Elapsed.ToString());

        myStopwatch.Reset();
        myStopwatch.Start();
        Console.WriteLine(IdleKeepConnectExpectedTimeTemplate, parameters.TimeIdleKeepConnection.ToString());
        maxValue = parameters.TimeIdleKeepConnection + new TimeSpan(0, 0, 0, 0, 100);
        while (myMachine.CurrentState == SegmentStateMachine.State.Connected)
        {
          Assert.IsTrue(myStopwatch.Elapsed < maxValue, "Timing error - to log idle keep connect state");
          Thread.Sleep(1);
        }
        Assert.IsTrue(myStopwatch.Elapsed >= parameters.TimeIdleKeepConnection, "Timing error - to short idle keep connect state");
        AssertDisconnected();
        Console.WriteLine(IdleKeepConnectActualTimeTemplate, myStopwatch.Elapsed.ToString());

        MakeConnection();
        myMachine.NotifyKeepConnectTimeElapsed();
      }
      finally
      {
        myMachine.DisconnectRequest();
        AssertDisconnected();
        Debug.WriteLine("Test failed - machine has been disconnected.");
      }
    }

    [TestMethod]
    public void TestAsynchronousDisconnect()
    {
      Assert.Inconclusive();
      int workerThreads;
      int completionPortThreads;
      System.Threading.ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
      Console.WriteLine("number of available threads worker= {0}; CompletionPort= {1}", workerThreads, completionPortThreads);
      Assert.IsFalse(myMachine.NeedsChannelAccess, "inconsistency of the myMachine.NeedsChannelAccess");
      myNumberOfThreads = workerThreads;
      for (int index = 0; index < myNumberOfThreads; index++)
        System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(WaitCallbackHandle), this);
      //
      System.Threading.ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
      Console.WriteLine("number of available threads worker= {0}; CompletionPort= {1}", workerThreads, completionPortThreads);
      Thread.Sleep(1000);
      System.Threading.ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
      Console.WriteLine("number of available threads worker= {0}; CompletionPort= {1}", workerThreads, completionPortThreads);
      //
      while (!System.Threading.Interlocked.Equals(myNumberOfThreads, 0))
      {
        MakeConnection();
        AssertKeepConnection();
        myMachine.DisconnectRequest();
        AssertDisconnected();
      }
      Console.WriteLine(myMaster.ToString());
      AssertDisconnected();
      Assert.IsFalse(myMachine.NeedsChannelAccess, "inconsistency of the myMachine.NeedsChannelAccess");
      System.Threading.ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
      Console.WriteLine("number of available threads worker= {0}; CompletionPort= {1}", workerThreads, completionPortThreads);
    }

    [TestMethod]
    public void TestRWFailure()
    {
      myMachine.ConnectRequest();
      Assert.AreEqual(SegmentStateMachine.State.KeepConnection, myMachine.CurrentState);

      myMaster.MakeError();
      TestRead();
      Assert.AreEqual(SegmentStateMachine.State.KeepConnection, myMachine.CurrentState);
      myMaster.MakeError();
      TestWriteData();
      Assert.AreEqual(SegmentStateMachine.State.KeepConnection, myMachine.CurrentState);

      myMachine.NotifyKeepConnectTimeElapsed();
      Assert.AreEqual(SegmentStateMachine.State.Connected, myMachine.CurrentState);

      myMaster.MakeError();
      TestRead();
      Assert.AreEqual(SegmentStateMachine.State.Connected, myMachine.CurrentState);
      myMaster.MakeError();
      TestWriteData();
      Assert.AreEqual(SegmentStateMachine.State.Connected, myMachine.CurrentState);
      myMachine.DisconnectRequest();
      Assert.AreEqual(SegmentStateMachine.State.Disconnected, myMachine.CurrentState);
    }

    [TestMethod]
    public void TestConnectionAbort()
    {
      myMaster.BreakConnection();
      myMachine.ConnectRequest();
      Assert.AreEqual(SegmentStateMachine.State.DisconnectedAfterFailure, myMachine.CurrentState);

      MakeConnection();

      myMaster.BreakConnection();
      TestRead();
      Assert.AreEqual(SegmentStateMachine.State.DisconnectedAfterFailure, myMachine.CurrentState);
      Assert.IsTrue(myMachine_DisconnectedAfterFailureEnteredExecuted, "Disconnected After Failure Entered Executed not executed");
      myMachine_DisconnectedAfterFailureEnteredExecuted = false;

      MakeConnection();

      myMaster.BreakConnection();
      TestWriteData();
      Assert.AreEqual(SegmentStateMachine.State.DisconnectedAfterFailure, myMachine.CurrentState);
      Assert.IsTrue(myMachine_DisconnectedAfterFailureEnteredExecuted, "Disconnected After Failure Entered Executed not executed");
      myMachine_DisconnectedAfterFailureEnteredExecuted = false;
      Assert.IsFalse(myMachine.NeedsChannelAccess, "inconsistency of the myMachine.NeedsChannelAccess");
    }

  }
}
