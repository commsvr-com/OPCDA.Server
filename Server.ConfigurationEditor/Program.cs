//_______________________________________________________________
//  Title   : Program - entry point for the application.
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate:  $
//  $Rev: $
//  $LastChangedBy: $
//  $URL: $
//  $Id:  $
//
//  Copyright (C) 2017, CAS LODZ POLAND.
//  TEL: +48 608 61 98 99 
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using CAS.CommServer.ProtocolHub.ConfigurationEditor.HMI;
using CAS.CommServer.ProtocolHub.ConfigurationEditor;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CAS.CommServer.DA.Server.ConfigurationEditor
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      try
      {
        AssemblyTraceEvent.Tracer.TraceMessage(TraceEventType.Verbose, 32, "Starting application CAS.CommServer.DA.Server.ConfigurationEditor");
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new ConfigTreeView(ConfigurationManagement.ProtocolHubConfiguration,
                               new ConfigIOHandler(ConfigurationManagement.ReadConfiguration),
                               new ConfigIOHandler(ConfigurationManagement.SaveProc),
                               new ConfigIOHandler(ConfigurationManagement.ClearConfig),
                               true)
                        );
        AssemblyTraceEvent.Tracer.TraceMessage(TraceEventType.Verbose, 32, "Finishing application CAS.CommServer.DA.Server.ConfigurationEditor");
      }
      catch (Exception _ex)
      {
        string _message = $"The application has been finished by the exception {_ex.Message} call the vendor for assistance";
        AssemblyTraceEvent.Tracer.TraceMessage(TraceEventType.Error, 36, _message);
        AssemblyTraceEvent.Tracer.TraceMessage(TraceEventType.Error, 36, $"Stock for the exception {_ex.StackTrace}");

        MessageBox.Show(_message, "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally { }
    }
  }
}
