//_______________________________________________________________
//  Title   : Multichannel License unit tests.
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C) 2016, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using CAS.Lib.CodeProtect;
using CAS.Lib.CodeProtect.Properties;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CAS.CommServer.ProtocolHub.Communication.LicenseControl
{
  /// <summary>
  /// Multichannel License unit tests
  /// </summary>
  [LicenseProvider( typeof( CodeProtectLP ) )]
  [GuidAttribute( "EB5CC34C-C18B-4727-AE78-8E183BEFC4A3" )]
  internal class Multichannel: IsLicensed<Multichannel>
  {
    #region private
    private static int m_CreatedChannels = 0;
    private const string m_LogSourceTitle = "CAS.Lib.CommServer.LicenseControl.Multichannel";
    private Multichannel()
      : base( 1, null )
    {
      if ( Licensed )
      {
        try
        {
          string fmt = "Multi-channel constrain allows you to create up to {0} channels.";
          CommServerComponent.Tracer.TraceVerbose( 41, m_LogSourceTitle, String.Format( fmt, Volumen.ToString() ) );
        }
        catch ( Exception ex )
        {
          CommServerComponent.Tracer.TraceVerbose( 45, m_LogSourceTitle, "There was a problem while testing Multichannel license: " +
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
        CommServerComponent.Tracer.TraceWarning( 70, m_LogSourceTitle,
          String.Format( "Number of channel has exceeded, only {0} of channels are allowed",License.Volumen.ToString()) );
        throw new LicenseException( License.GetType(), License, Resources.Tx_LicVolumeConstrainErr );
      }
      m_CreatedChannels++;
    } 
    #endregion
  }
}
