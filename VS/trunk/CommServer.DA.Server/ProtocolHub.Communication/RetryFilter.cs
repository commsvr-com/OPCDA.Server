//_______________________________________________________________
//  Title   : Retry management and quality assessment
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

using System;

namespace CAS.CommServer.ProtocolHub.Communication
{
  /// <summary>
  /// Retry management and quality assessment
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
