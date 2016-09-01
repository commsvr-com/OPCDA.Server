//<summary>
//  Title   : Retry management and quality assesment
//  System  : Microsoft Visual C# .NET 
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20080905: mzbrzezny: class is marked as public (this was done due to changing interface to pulic (Interface class must be visible in NetworkConfig))
//    MPostol 07-10-2007: created 
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;

namespace CAS.CommServer.ProtocolHub.Communication
{
  /// <summary>
  /// Retry management and quality assesment
  /// </summary>
  public struct RetryFilter
  {
    #region private
    private const float scale = 100.0F;
    private const float coefficient = 4.0F;
    private float quality;
    private byte maxRetry;
    private byte currentRetry;
    #endregion
    #region public
    /// <summary>
    /// Marks the fail.
    /// </summary>
    public void MarkFail()
    {
      currentRetry = Convert.ToByte( currentRetry / 2.0 + 0.1);
      quality -= quality / coefficient;
    }
    /// <summary>
    /// Marks the success.
    /// </summary>
    public void MarkSuccess()
    {
      currentRetry = maxRetry;
      quality += ( scale - quality ) / coefficient;
    }
    /// <summary>
    /// Gets the retry.
    /// </summary>
    /// <value>The retry.</value>
    public byte Retry { get { return currentRetry; } }
    /// <summary>
    /// Gets the quality.
    /// </summary>
    /// <value>The quality.</value>
    public byte Quality { get { return Convert.ToByte( quality ); } }
    #endregion
    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="RetryFilter"/> struct.
    /// </summary>
    /// <param name="retry">The retry.</param>
    public RetryFilter( byte retry )
    {
      currentRetry = retry;
      maxRetry = retry;
      quality = scale;
    }
    #endregion
  }
}
