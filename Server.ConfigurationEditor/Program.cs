using CAS.CommServer.ProtocolHub.ConfigurationEditor.HMI;
using CAS.CommServer.ProtocolHub.ConfigurationEditor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
      AssemblyTraceEvent.Tracer.TraceMessage(TraceEventType.Verbose, 32, "Starting application CAS.CommServer.DA.Server.ConfigurationEditor");
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new ProtocolHub.ConfigurationEditor.HMI.ConfigTreeView(ConfigurationManagement.ProtocolHubConfiguration,
                             new ConfigIOHandler(ConfigurationManagement.ReadConfiguration),
                             new ConfigIOHandler(ConfigurationManagement.SaveProc),
                             new ConfigIOHandler(ConfigurationManagement.ClearConfig),
                             false)
                      );
      AssemblyTraceEvent.Tracer.TraceMessage(TraceEventType.Verbose, 32, "Finishing application CAS.CommServer.DA.Server.ConfigurationEditor");
    }
  }
}
