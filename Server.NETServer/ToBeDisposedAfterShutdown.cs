//_______________________________________________________________
//  Title   : ToBeDisposedAfterShutdown
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C) 2017, CAS LODZ POLAND.
//  TEL: +48 608 61 98 99 
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using Opc;
using System;

namespace CAS.OpcSvr.Da.NETServer
{
  /// <summary>
  /// Class ToBeDisposedAfterShutdown - provides <see cref="ServerShutdownEventHandler"/> to be used to dispose the object
  /// </summary>
  internal class ToBeDisposedAfterShutdown
  {

    /// <summary>
    /// Creates <see cref="ToBeDisposedAfterShutdown"/> and returns the server shutdown event handler to be used to dispose <paramref name="toBeDisposed"/>.
    /// </summary>
    /// <param name="toBeDisposed">To be disposed.</param>
    /// <returns>ServerShutdownEventHandler.</returns>
    internal static ServerShutdownEventHandler GetServerShutdownEventHandler(IDisposable toBeDisposed)
    {
      ToBeDisposedAfterShutdown _this = new ToBeDisposedAfterShutdown((toBeDisposed));
      return _this.ShutdowntEventHandler;
    }

    #region private
    private ToBeDisposedAfterShutdown(IDisposable toBeDisposed)
    {
      m_ToBeDisposed = toBeDisposed;
    }
    private void ShutdowntEventHandler(string reason)
    {
      m_ToBeDisposed.Dispose();
    }
    private IDisposable m_ToBeDisposed;
    #endregion

  }
}
