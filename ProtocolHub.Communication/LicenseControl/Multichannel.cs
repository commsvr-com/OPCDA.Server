//<summary>
//  Title   : Multichannel License unit tests.
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
using CAS.CommServer.ProtocolHub.Communication;

namespace CAS.CommServer.ProtocolHub.Communication.LicenseControl
{
  /// <summary>
  /// Multichannel License unit tests
  /// </summary>
  [LicenseProvider( typeof( CodeProtectLP ) )]
  [GuidAttribute( "EB5CC34C-C18B-4727-AE78-8E183BEFC4A3" )]
  public sealed class Multichannel: IsLicensed<Multichannel>
  {
    #region private
    private static int m_CreatedChannels = 0;
    private const string m_Src = "CAS.Lib.CommServer.LicenseControl.Multichannel";
    private Multichannel()
      : base( 1, null )
    {
      if ( Licensed )
      {
        try
        {
          string fmt = "Multi-channel constrain allows you to create up to {0} channels.";
          CommServerComponent.Tracer.TraceVerbose( 41, m_Src, String.Format( fmt, Volumen.ToString() ) );
        }
        catch ( Exception ex )
        {
          CommServerComponent.Tracer.TraceVerbose( 45, m_Src, "There was a problem while testing Multichannel license: " +
            ex.Message );
        }
      }
    }
    #endregion
    #region public
    /// <summary>
    /// Get access to the <see cref="Multichannel"/> instance.
    /// </summary>
    public static Multichannel License = new Multichannel();
    /// <summary>
    /// Gets the number of the created channels.
    /// </summary>
    /// <value>The created.</value>
    public int Created { get { return m_CreatedChannels; } }
    /// <summary>
    /// Increment the number of created channnels. Throw the <see cref="LicenseException"/> if reached the license constrain.
    /// </summary>
    /// <exception cref="LicenseException">Thrown if the license constrain does not allows for creating next channel.</exception>
    public static void NextChannnel()
    {
      if ( License.Volumen <= m_CreatedChannels )
      {
        CommServerComponent.Tracer.TraceWarning( 70, m_Src,
          String.Format( "Number of channel has exceeded, only {0} of channels are allowed",License.Volumen.ToString()) );
        throw new LicenseException( License.GetType(), License, Resources.Tx_LicVolumeConstrainErr );
      }
      m_CreatedChannels++;
    } 
    #endregion
  }
}
