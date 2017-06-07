//<summary>
//  Title   : C# Net Server DAServer
//  System  : Microsoft Visual C# .NET 
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    Mariusz Postol - 18-08-2006
//      This method ChannelServices.RegisterChannel(IChannel chnl) is now obsolete. Use 
//      System.Runtime.Remoting.ChannelServices.RegisterChannel(IChannel chnl, bool ensureSecurity) 
//      
//    Maciej Zbrzezny - 12-04-2006
//      wylaczono rejestrace zdalnego kanalu z kanalu 
//
//    M.Postol - 2005
//    created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http://www.cas.eu
//</summary>
//============================================================================
// TITLE: Server.cs
//
// CONTENTS:
// 
// A server that implements the COM-DA interfaces. 
//
// (c) Copyright 2003 The OPC Foundation
// ALL RIGHTS RESERVED.
//
// DISCLAIMER:
//  This code is provided by the OPC Foundation solely to assist in 
//  understanding and use of the appropriate OPC Specification(s) and may be 
//  used as set forth in the License Grant section of the OPC Specification.
//  This code is provided as-is and without warranty or support of any sort
//  and is subject to the Warranty and Liability Disclaimers which appear
//  in the printed OPC Specification.
//
// MODIFICATION LOG:
//
// Date       By    Notes
// ---------- ---   -----
// 2003/03/26 RSA   Initial implementation.

using System;
using System.Runtime.InteropServices;

namespace CAS.OpcSvr.Da.NETServer
{
  /// <summary>
  /// A XML-DA server implementation that wraps a COM-DA server.
  /// </summary>
  //[CLSCompliant(false)]
#if COMMSERVER
  [Guid("8CA689CF-E5A2-3A80-BA00-6F08269C4644")]
  public class DaServer : OpcCom.Da.Wrapper.Server
#elif SNIFFER
  public class DaServerBUSSniffer : OpcCom.Da.Wrapper.Server
#endif
  {
#if COMMSERVER
    /// <summary>
    /// Class created as COM object by CAS OPC DA Server Wrapper
    /// </summary>
    public DaServer()
#elif SNIFFER
    public DaServerBUSSniffer() 
#endif
    {
      // Register a channel.
#if COMMSERVER
      new CAS.Lib.RTLib.Processes.EventLogMonitor
        (
        "New instance of CAS.OpcSvr.Da.NETServer.Server created",
        System.Diagnostics.EventLogEntryType.Information, (int)CAS.Lib.RTLib.Processes.Error.CAS_OpcSvr_Da_NETServer_Server, 56).WriteEntry();
#elif SNIFFER
      new Processes.EventLogMonitor
        (
        "New instance of CAS.OpcSvr.Da.NETServer.DaServerBUSSniffer created", 
        System.Diagnostics.EventLogEntryType.Information, (int)Processes.Error.CAS_OpcSvr_Da_NETServer_DaServerBUSSniffer, 56).WriteEntry();
      RgisterRemotingCannel();
#endif
      if ( new System.IO.FileInfo( AppDomain.CurrentDomain.SetupInformation.ConfigurationFile ).Exists == false )
      {
        new CAS.Lib.RTLib.Processes.EventLogMonitor
        (
@"WARNING: The configuration (.config) file is missing. Usually CommServer OPC server executable (CASOpcDaWrapper.exe) is trying to open: 'C:\Program Files\CAS\CommServer\CASOpcDaWrapper.config' but this file does not exist. The file that exists in the directory is: 'C:\Program Files\CAS\CommServer\CASOpcDaWrapper.exe.config'.

EXPLANATION:This situation appears someties in Windows Server operating system, somehow the operating system is trying to find .config file (not .exe.config).

SOLUTION: The solution is to create of a copy of the 'CASOpcDaWrapper.exe.config' file and rename the copy 'CASOpcDaWrapper.config'.

Note: Before reconnection to OPC server make sure that CASOpcDaWrapper.exe is not appear on the task list (in Windows Task Manager). If it is exist please kill the process before reconnection.
",        System.Diagnostics.EventLogEntryType.Warning,
          ( int)CAS.Lib.RTLib.Processes.Error.CAS_OpcSvr_Da_NETServer_Server, 109 ).WriteEntry();
      }
      try { IServer = m_server = new Server(true); }
      catch (Exception ex)
      {
        new CAS.Lib.RTLib.Processes.EventLogMonitor
          (
          "Cannot start a new instance of CAS.OpcSvr.Da.NETServer.Server created because of internal error: " + ex.Message + ex.StackTrace.ToString(),
          System.Diagnostics.EventLogEntryType.Error,
          (int)CAS.Lib.RTLib.Processes.Error.CAS_OpcSvr_Da_NETServer_Server, 65).WriteEntry();
        throw ex;
      }
    }
    #region Private Members
#if COMMSERVER
    static DaServer()
#elif SNIFFER
      static DaServerBUSSniffer()
#endif
    {
      //TODO po wprowadzeniu zmian do BUSSniffera wywaliæ ca³kiem rejestracjê
      //RgisterRemotingCannel();
    }
    private Server m_server = null;
#if SNIFFER
    private static bool channelRegistered = false;
    private static void RgisterRemotingCannel()
    {
      if (!channelRegistered)
      {
        try
        {
          TcpChannel channel = new TcpChannel();
          ChannelServices.RegisterChannel(channel, false);
          channelRegistered = true;
          // Register the JobServerImpl type as a WKO.
          //  WellKnownClientTypeEntry remotetype =
          //  new WellKnownClientTypeEntry(typeof(Device), "tcp://localhost:5000/CAS_OpcSvr_Da_NETServer_URI");
          WellKnownClientTypeEntry remotetype = 
            new WellKnownClientTypeEntry(typeof(Device), "tcp://localhost:5555/CAS_OpcSvr_Da_NETServer_URI");
          RemotingConfiguration.RegisterWellKnownClientType(remotetype);
          new Processes.EventLogMonitor
            (
            "New TcpChannel created with name: " + channel.ChannelName + ", and registered remote type: " + remotetype.ToString(),
            System.Diagnostics.EventLogEntryType.Information, (int)Processes.Error.CAS_OpcSvr_Da_NETServer_Server, 93
            ).WriteEntry();
        }
        catch (Exception ex)
        {
          new Processes.EventLogMonitor
            (ex.Message, System.Diagnostics.EventLogEntryType.Error, (int)Processes.Error.CAS_OpcSvr_Da_NETServer_Server, 98).WriteEntry();
          throw ex;
        }
      }
    }
#endif
    #endregion
  }
}
