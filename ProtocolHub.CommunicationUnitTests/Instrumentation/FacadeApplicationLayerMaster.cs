//_______________________________________________________________
//  Title   : Name of Application
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


using CAS.Lib.CommonBus;
using CAS.Lib.CommonBus.ApplicationLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests.Instrumentation
{
  /// <summary>
  /// Facade of  IApplicationLayerMaster
  /// </summary>
  internal class FacadeApplicationLayerMaster: IApplicationLayerMaster
  {
    #region private
    private bool myConnected = false;
    private bool myMakeError = false;
    private bool myBreakConnection = false;
    private bool myInsideMarker = false;
    private uint myNumberOfReadOperations = 0;
    private uint myNumberOfWriteOperations = 0;
    private uint myNumberOfFacadeIReadValue = 0;
    private uint myNumberOfFacadeWriteValue = 0;
    private class FacadeIReadValue: IReadValue
    {
      #region private
      private FacadeApplicationLayerMaster myParent;
      private bool myInPool = true;
      #endregion
      #region IReadValue Members
      public bool IsInBlock( uint station, ushort address, short type )
      {
        throw new Exception( "The method or operation is not implemented." );
      }
      public object ReadValue( int regAddress, Type pCanonicalType )
      {
        return 0;
      }
      #endregion
      #region IBlockDescription Members
      public int startAddress
      {
        get { throw new Exception( "The method or operation is not implemented." ); }
      }
      public int length
      {
        get { throw new Exception( "The method or operation is not implemented." ); }
      }
      public short dataType
      {
        get { throw new Exception( "The method or operation is not implemented." ); }
      }
      #endregion
      #region IEnvelope Members
      public void ReturnEmptyEnvelope()
      {
        myInPool = false;
        myParent.myNumberOfFacadeIReadValue--;
      }
      public bool InPool
      {
        get
        {
          return myInPool;
        }
        set
        {
          throw new Exception( "The method or operation is not implemented." );
        }
      }
      #endregion
      #region creators
      public FacadeIReadValue( FacadeApplicationLayerMaster parent )
      {
        myParent = parent;
        myParent.myNumberOfFacadeIReadValue++;
      }
      #endregion
    }
    private class FacadeWriteValue: IWriteValue
    {
      #region private
      private object[] buffer = new object[ 100 ];
      private bool myInPool = true;
      private FacadeApplicationLayerMaster myParent;
      #endregion
      #region IWriteValue Members
      public void WriteValue( object pValue, int pRegAddress )
      {
        buffer[ pRegAddress ] = pValue;
      }
      #endregion
      #region IBlockDescription Members
      public int startAddress
      {
        get { throw new Exception( "The method or operation is not implemented." ); }
      }
      public int length
      {
        get { throw new Exception( "The method or operation is not implemented." ); }
      }
      public short dataType
      {
        get { throw new Exception( "The method or operation is not implemented." ); }
      }
      #endregion
      #region IEnvelope Members
      public void ReturnEmptyEnvelope()
      {
        myInPool = false;
        myParent.myNumberOfFacadeWriteValue--;
      }
      public bool InPool
      {
        get
        {
          return myInPool;
        }
        set
        {
          throw new Exception( "The method or operation is not implemented." );
        }
      }
      #endregion
      #region creators
      public FacadeWriteValue( FacadeApplicationLayerMaster parent )
      {
        myParent = parent;
        myParent.myNumberOfFacadeWriteValue++;
      }
      #endregion
    }
    private void MonitorEnter()
    {
      Assert.IsFalse( myInsideMarker, "Monitor - the instance is not multi-threads safe" );
      myInsideMarker = true;
    }
    private void MonitorExit()
    {
      Assert.IsTrue( myInsideMarker, "Monitor - the instance is not multi-threads safe" );
      myInsideMarker = false;
    }
    #endregion
    #region IApplicationLayerMaster Members
    public AL_ReadData_Result ReadData( IBlockDescription pBlock, int pStation, out IReadValue pData, byte pRetries )
    {
      Assert.IsTrue( myConnected, "Must be connected while ReadData" );
      MonitorEnter();
      pData = null;
      myNumberOfReadOperations++;
      if ( myBreakConnection )
      {
        myBreakConnection = false;
        myMakeError = false;
        myConnected = false;
        MonitorExit();
        return AL_ReadData_Result.ALRes_DisInd;
      }
      if ( !myMakeError )
      {
        pData = new FacadeIReadValue( this );
        MonitorExit();
        return AL_ReadData_Result.ALRes_Success;
      }
      MonitorExit();
      return AL_ReadData_Result.ALRes_DatTransferErrr;
    }
    public IWriteValue GetEmptyWriteDataBuffor( IBlockDescription block, int station )
    {
      Assert.IsTrue( myConnected, "Must be connected while GetEmptyWriteDataBuffor" );
      MonitorEnter();
      MonitorExit();
      Assert.IsTrue( myNumberOfFacadeWriteValue == 0, "It seems a buffer is not returned to the pool before next write" );
      return new FacadeWriteValue( this );
    }
    public AL_ReadData_Result WriteData( ref IWriteValue data, byte retries )
    {
      Assert.IsTrue( myConnected, "Must be connected while WriteData" );
      MonitorEnter();
      data.ReturnEmptyEnvelope();
      data = null;
      myNumberOfWriteOperations++;
      if ( myBreakConnection )
      {
        myBreakConnection = false;
        myMakeError = false;
        myConnected = false;
        MonitorExit();
        return AL_ReadData_Result.ALRes_DisInd;
      }
      if ( !myMakeError )
      {
        MonitorExit();
        return AL_ReadData_Result.ALRes_Success;
      }
      myMakeError = false;
      MonitorExit();
      return AL_ReadData_Result.ALRes_DatTransferErrr;
    }
    #endregion
    #region IConnectionManagement Members
    public TConnectReqRes ConnectReq( CAS.Lib.CommonBus.IAddress remoteAddress )
    {
      Assert.IsTrue( !myConnected, "Must be disconnected while ConnectReq" );
      MonitorEnter();
      if ( !myBreakConnection )
      {
        myConnected = true;
        MonitorExit();
        return TConnectReqRes.Success;
      }
      myBreakConnection = false;
      MonitorExit();
      return TConnectReqRes.NoConnection;
    }
    public TConnIndRes ConnectInd( CAS.Lib.CommonBus.IAddress pRemoteAddress, int pTimeOutInMilliseconds )
    {
      MonitorEnter();
      MonitorExit();
      return TConnIndRes.NoConnection;
    }
    public void DisReq()
    {
      Assert.IsTrue( myConnected, "Must be connected while DisReq" );
      MonitorEnter();
      myConnected = false;
      MonitorExit();
    }
    public bool Connected { get { return myConnected; } }
    #endregion
    #region IDisposable Members
    public void Dispose() { }
    #endregion
    #region object
    public override string ToString()
    {
      return String.Format( " Number of operations: writes: {0}, reads={1}", myNumberOfWriteOperations, myNumberOfReadOperations );
    }
    #endregion
    #region public
    internal void CheckConsistency()
    {
      Assert.IsTrue( myNumberOfFacadeIReadValue == 0, "Not all read envelopes returned to pool" );
      Assert.IsTrue( myNumberOfFacadeWriteValue == 0, "Not all write envelopes returned to pool" );
    }
    internal void MakeError() { myMakeError = true; }
    internal void BreakConnection() { myBreakConnection = true; }
    #endregion
  }
}
