//<summary>
//  Title   : Guarded Data Provider
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20081007: mzbrzezny: when exception occurs the out IReadValue or ref IWriteValue should be returned to pool and changed to null
//    20071008: mpostol: implementation
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using CAS.Lib.CommonBus;
using CAS.Lib.CommonBus.ApplicationLayer;
using CAS.Lib.RTLib.Processes;
using CAS.CommServer.ProtocolHub.Communication.Properties;

namespace CAS.CommServer.ProtocolHub.Communication
{
  /// <summary>
  /// Guarded Data Provider
  /// </summary>
  [Watchdog( "GuardedDataProvider", 120 )]
  internal class GuardedDataProvider: ContextBoundObject, IApplicationLayerMaster
  {
    #region private
    private string m_Source = "CAS.Lib.CommServer.GuardedDataProvider";
    private IApplicationLayerMaster mDataProvider;
    private void TraceException( Exception ex, int line )
    {
      string message = String.Format( Resources.TraceDataProviderError, ex.Message, ex.Source, ex.StackTrace );
      CommServerComponent.Tracer.Trace
        ( System.Diagnostics.TraceEventType.Error, line, m_Source, message );
    }
    #endregion
    #region IApplicationLayerMaster Members
    /// <summary>
    /// Reads process data from the selected location and device resources.
    /// </summary>
    /// <param name="pBlock"><see cref="IBlockDescription"/> selecting the resource containing the data block to be read.</param>
    /// <param name="pStation">Address of the remote station connected to the common field bus. –1 if not applicable.</param>
    /// <param name="pData">The buffer <see cref="IReadValue"/> containing the requested data.</param>
    /// <param name="pRetries">Number of retries to get data.</param>
    /// <returns>Result of the operation</returns>
    public AL_ReadData_Result ReadData( IBlockDescription pBlock, int pStation, out IReadValue pData, byte pRetries )
    {
      pData = null;
      try
      {
        return mDataProvider.ReadData( pBlock, pStation, out pData, pRetries );
      }
      catch ( Exception ex )
      {
        if ( (pData != null) && ! pData.InPool )
          pData.ReturnEmptyEnvelope();
        pData = null;
        TraceException( ex, 46 );
        return AL_ReadData_Result.ALRes_DatTransferErrr;
      }
    }
    /// <summary>
    /// Gets a buffer from a pool and initiates. After filling it up with the data can be send to the data provider remote
    /// unit by the <see cref="WriteData"/>.
    /// </summary>
    /// <param name="block"><see cref="IBlockDescription"/> selecting the resource where the data block is to be written.</param>
    /// <param name="station">Address of the remote station connected to the common field bus. –1 if not applicable.</param>
    /// <returns>
    /// A buffer <see cref="IWriteValue"/> ready to be filled up with the data and written down by the <see cref="WriteData"/>
    /// to the destination – remote station.
    /// </returns>
    public IWriteValue GetEmptyWriteDataBuffor( IBlockDescription block, int station )
    {
      try
      {
        return mDataProvider.GetEmptyWriteDataBuffor( block, station );
      }
      catch ( Exception ex )
      {
        TraceException( ex, 60 );
        return null;
      }
    }
    /// <summary>
    /// Writes process data down to the selected location and device resources.
    /// </summary>
    /// <param name="data">Data to be send. Always null after return. Data buffer must be returned to the pool.</param>
    /// <param name="retries">Number of retries to wrtie data.</param>
    /// <returns>
    /// 	<see cref="AL_ReadData_Result"/> result of the operation.
    /// </returns>
    public AL_ReadData_Result WriteData( ref IWriteValue data, byte retries )
    {
      try
      {
        return mDataProvider.WriteData( ref data, retries );
      }
      catch ( Exception ex )
      {
        if ( (data != null) && ! data.InPool )
          data.ReturnEmptyEnvelope();
        data = null;
        TraceException( ex, 72 );
        return AL_ReadData_Result.ALRes_DatTransferErrr;
      }
    }
    #endregion
    #region IConnectionManagement Members
    public TConnectReqRes ConnectReq( IAddress remoteAddress )
    {
      try
      {
        return mDataProvider.ConnectReq( remoteAddress );
      }
      catch ( Exception ex )
      {
        TraceException( ex, 72 );
        return TConnectReqRes.NoConnection;
      }
    }
    public TConnIndRes ConnectInd( IAddress pRemoteAddress, int pTimeOutInMilliseconds )
    {
      try
      {
        return mDataProvider.ConnectInd( pRemoteAddress, pTimeOutInMilliseconds );
      }
      catch ( Exception ex )
      {
        TraceException( ex, 72 );
        return TConnIndRes.ConnectInd;
      }
    }
    public void DisReq()
    {
      try
      {
        mDataProvider.DisReq();
      }
      catch ( Exception ex )
      {
        TraceException( ex, 72 );
      }
    }
    public bool Connected
    {
      get
      {
        try
        {
          return mDataProvider.Connected;
        }
        catch ( Exception ex )
        {
          TraceException( ex, 72 );
          return false;
        }
      }
    }
    #endregion
    #region IDisposable Members
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      mDataProvider.Dispose();
    }
    #endregion
    #region object
    /// <summary>
    /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
    /// </returns>
    public override string ToString()
    {
      return mDataProvider.ToString();
    }
    #endregion
    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="GuardedDataProvider"/> class.
    /// </summary>
    /// <param name="watchdogName">Name of the watchdog.</param>
    /// <param name="parent">The wrapped <see cref="IApplicationLayerMaster"/> parent object.</param>
    public GuardedDataProvider( string watchdogName, IApplicationLayerMaster parent )
    {
      mDataProvider = parent;
    }
    #endregion
  }
}
