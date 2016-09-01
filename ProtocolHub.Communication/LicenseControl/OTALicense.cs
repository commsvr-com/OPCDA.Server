//<summary>
//  Title   : Optimal Transfer Algorithm License access helper class
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using CAS.Lib.CodeProtect;

namespace CAS.CommServer.ProtocolHub.Communication.LicenseControl
{
  /// <summary>
  ///  Optimal Transfer Algorithm License access helper class
  /// </summary>
  [LicenseProvider( typeof( CodeProtectLP ) )]
  [GuidAttribute( "75232B03-E9B9-41e8-9A99-A75A0D0CC21C" )]
  public sealed class OTALicense: IsLicensed<OTALicense>
  {
    private const string m_Src = "CAS.Lib.CommServer.LicenseControl.OTALicense";
    private OTALicense()
      : base( null, null )
    {
      if ( Licensed )
        CommServerComponent.Tracer.TraceVerbose( 58, m_Src, "Optimal Scanning Algorithm has been activated" );
    }
    /// <summary>
    /// Get access to the <see cref="OTALicense"/> instance.
    /// </summary>
    public static OTALicense License = new OTALicense();
  }
}
