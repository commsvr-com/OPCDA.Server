
using CAS.CommServer.ProtocolHub.Communication.LicenseControl;
using CAS.Lib.CodeProtect.LicenseDsc;
using System;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests.Instrumentation
{
  internal class FacadeASALicense : ASALicense
  {
    internal static Action<string> m_Logger;

    protected override void TraceNoLicenseFile(string reason)
    {
      m_Logger(reason);
    }
    protected override void TraceCurrentLicence(LicenseFile license)
    {
      m_Logger(license.ToString());
    }
    protected override void TraceFailureReason(string reason)
    {
      m_Logger(reason);
    }
  }
}
