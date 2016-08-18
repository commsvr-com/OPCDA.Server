//<summary>
//  Title   : Redundancy license access helper class.
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
using CAS.Lib.CodeProtect.Properties;

namespace CAS.Lib.CommServer.LicenseControl
{
  /// <summary>
  /// Redundancy license access helper class.
  /// </summary>
  [LicenseProvider( typeof( CodeProtectLP ) )]
  [GuidAttribute( "DB304C5F-C8FE-442b-BB73-EDBDEF541FDF" )]
  public sealed class Redundancy: IsLicensed<Redundancy>
  {
    #region private
    private const string m_Src = "CAS.Lib.CommServer.LicenseControl.Redundancy";
    private Redundancy()
      : base( 1, null )
    {
      if ( Licensed )
      {
        string fmt = "Redundancy has been activated. You can use up to {0} ports per station.";
        CommServerComponent.Tracer.TraceVerbose( 132, m_Src, String.Format( fmt, Volumen ) );
      }
    }
    #endregion
    #region public
    /// <summary>
    /// Get access to the <see cref="Redundancy"/> instance.
    /// </summary>
    public static Redundancy License = new Redundancy();
    /// <summary>
    /// Checks if allowed to create port.
    /// </summary>
    /// <param name="number">The port number.</param>
    /// <param name="name">The name of the port.</param>
    /// <exception cref="LicenseException">If not allowe.</exception>
    public static void CheckIfAllowed( byte number, string name )
    {
      if ( License.Volumen <= number )
      {
        string fmt = "Port {0} cannot be created and redundancy activated because {1}.";
        throw new LicenseException
          ( License.GetType(), License, string.Format( fmt, name, Resources.Tx_LicVolumeConstrainErr ) );
      }
    }
    #endregion
  }
}
