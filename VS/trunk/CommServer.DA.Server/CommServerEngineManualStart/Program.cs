using System;
using CAS.OpcSvr.Da.NETServer;

namespace CAS.CommServer.ProtocolHub.CommunicationManualStart
{
  class Program
  {
    static void Main( string[] args )
    {
      try
      {
        CAS.Lib.CodeProtect.LibInstaller.InstalLicense();
      }
      catch ( CAS.Lib.CodeProtect.LicenseDsc.LicenseFileException ex )
      {
        Console.WriteLine( "Cannot install license, error:" + ex.Message );
      }
      Console.WriteLine( "CommServer is starting..... " );
      DaServer daserver = new DaServer();
      Console.WriteLine( "CommServer is started. Hit Enter to close application" );
      Console.ReadLine();
      Console.WriteLine( "Enter is receive, waiting for application end" );
    }
  }
}
