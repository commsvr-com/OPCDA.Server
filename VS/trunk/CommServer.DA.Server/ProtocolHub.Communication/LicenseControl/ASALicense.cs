//<summary>
//  Title   : Adaptive Scanning Algorithm license access helper
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

namespace CAS.Lib.CommServer.LicenseControl
{
  /// <summary>
  /// Adaptive Scanning Algorithm license access helper
  /// </summary>
  [LicenseProvider( typeof( CodeProtectLP ) )]
  [GuidAttribute( "F3C086DE-30EC-426d-B507-8114074A9840" )]
  public sealed class ASALicense: IsLicensed<ASALicense>
  {
    #region private
    private const string m_Src = "CAS.Lib.CommServer.LicenseControl.ASALicense";
    private ASALicense()
      : base( 0, TimeSpan.Zero )
    {
      if ( Licensed )
      {
        string fmt = "Adaptive Scanning Algorithm has been activated";
        CommServerComponent.Tracer.TraceVerbose( 132, m_Src, fmt );
      }
    }
    #endregion
    #region public
    /// <summary>
    /// Get access to the <see cref="ASALicense"/> instance.
    /// </summary>
    public static ASALicense License = new ASALicense();
    #endregion

  }
}
