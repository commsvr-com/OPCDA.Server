//<summary>
//  Title   : Tests
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    2007 - mpostol created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http://www.cas.eu
//</summary>
using NUnit.Framework;
using CAS.Lib.CommonBus.ApplicationLayer;
namespace CAS.Lib.CommServer.Tests.GuardedDataProviderTest
{
  /// <summary>
  /// Master Test
  /// </summary>
  [TestFixture()]
  public class MasterTest
  {
    /// <summary>
    /// Goes the test.
    /// </summary>
    [Test]
    public void GoTest()
    {
      FacadeApplicationLayerMaster m_Master = new FacadeApplicationLayerMaster();
      string m_MasterToString = m_Master.ToString();
      GuardedDataProvider m_gp = new GuardedDataProvider( "GoTest", m_Master );
      string m_gpToString = m_gp.ToString();
      Assert.AreEqual( m_gpToString, m_MasterToString, "ToString error" );
      CAS.Lib.CommonBus.StringAddress m_address = new CAS.Lib.CommonBus.StringAddress("DummyAddress");
      Assert.IsFalse( m_gp.Connected, "State error" );
      m_gp.ConnectReq( m_address );
      Assert.IsTrue( m_gp.Connected, "State error" );
      FacadeBlockDescription m_block = new FacadeBlockDescription( int.MaxValue, 100, short.MaxValue );
      IReadValue m_readVal;
      switch ( m_gp.ReadData( m_block, int.MaxValue, out m_readVal, byte.MaxValue ) )
      {
        case AL_ReadData_Result.ALRes_DatTransferErrr:
        case AL_ReadData_Result.ALRes_DisInd:
          Assert.Fail( "incorectr return value from ReadData" );
          break;
        case AL_ReadData_Result.ALRes_Success:
          Assert.IsNotNull( m_readVal, "ReadData returned null" );
          m_readVal.ReturnEmptyEnvelope();
          break;
      }
      Assert.IsTrue( m_gp.Connected, "State error" );
      IWriteValue m_writeVal = m_gp.GetEmptyWriteDataBuffor( m_block, int.MaxValue );
      m_writeVal.WriteValue( int.MaxValue, 0 );
      switch ( m_gp.WriteData( ref m_writeVal, byte.MaxValue ) )
      {
        case AL_ReadData_Result.ALRes_DatTransferErrr:
        case AL_ReadData_Result.ALRes_DisInd:
          Assert.Fail( "incorectr return value from ReadData" );
          break;
        case AL_ReadData_Result.ALRes_Success:
          Assert.IsNull( m_writeVal, "WriteData has not returned the envelope to a pool" );
          break;
      }
      Assert.IsTrue( m_gp.Connected, "State error" );
      m_gp.DisReq();
      Assert.IsFalse( m_gp.Connected, "State error" );
      m_gp.Dispose();
    }
  }
}
