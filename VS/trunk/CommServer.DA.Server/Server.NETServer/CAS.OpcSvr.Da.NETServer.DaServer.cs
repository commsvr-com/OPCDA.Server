//_______________________________________________________________
//  Title   :  C# Net Server DAServer
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C) 2017, CAS LODZ POLAND.
//  TEL: +48 608 61 98 99 
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using CAS.Lib.RTLib.Processes;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace CAS.CommServer.DA.Server.NETServer
{

  /// <summary>
  /// A XML-DA server implementation that wraps a COM-DA server.
  /// </summary>
  //[CLSCompliant(false)]
  [Guid("BE77A3C7-D2B7-44E7-B943-B978C1C87E5A")]
  [ProgId("CAS.CommServer.DA.Server.NETServer.ProgId")]
  public class DaServer : OpcCom.Da.Wrapper.Server
  {
    /// <summary>
    /// Class created as COM object by CAS OPC DA Server Wrapper
    /// </summary>
    public DaServer()
    {
      new EventLogMonitor("New instance of CAS.OpcSvr.Da.NETServer.Server created", EventLogEntryType.Information, (int)Error.CAS_OpcSvr_Da_NETServer_Server, 56).WriteEntry();
      if (!File.Exists(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile))
      {
        new EventLogMonitor
        (
@"WARNING: The configuration (.config) file is missing. Usually CommServer OPC server executable (CASOpcDaWrapper.exe) is trying to open: 'C:\Program Files\CAS\CommServer\CASOpcDaWrapper.config' but this file does not exist. The file that exists in the directory is: 'C:\Program Files\CAS\CommServer\CASOpcDaWrapper.exe.config'.

EXPLANATION:This situation appears sometimes in Windows Server operating system, somehow the operating system is trying to find .config file (not .exe.config).

SOLUTION: The solution is to create of a copy of the 'CASOpcDaWrapper.exe.config' file and rename the copy 'CASOpcDaWrapper.config'.

Note: Before reconnection to OPC server make sure that CASOpcDaWrapper.exe is not appear on the task list (in Windows Task Manager). If it is exist please kill the process before reconnection.", EventLogEntryType.Error, (int)Error.CAS_OpcSvr_Da_NETServer_Server, 109).WriteEntry();
        throw new ApplicationException("Unable to find application configuration file. Examine the Windows application event log to get more.");
      }
      try
      {
        IServer = m_server = new Server(true);
      }
      catch (Exception ex)
      {
        string _message = "Cannot start a new instance of CAS.OpcSvr.Da.NETServer.Server created because of internal error: " + ex.Message + ex.StackTrace.ToString();
        new EventLogMonitor(_message, EventLogEntryType.Error, (int)Error.CAS_OpcSvr_Da_NETServer_Server, 65).WriteEntry();
        throw;
      }
    }
    /// <summary>
    /// Called when the object is unloaded by the COM wrapper process.
    /// </summary>
    public override void Unload()
    {
      base.Unload();
      m_server.Dispose();
    }

    #region Private Members
    private Server m_server = null;
    #endregion

  }
}
