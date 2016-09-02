//_______________________________________________________________
//  Title   : FacadeSegment
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

using CAS.CommServer.ProtocolHub.Communication.BaseStation;
using CAS.Lib.CommonBus.ApplicationLayer;
using CAS.Lib.RTLib.Processes;
using CAS.NetworkConfigLib;
using System;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests.Instrumentation
{
  internal class FacadeSegment: WaitTimeList<Pipe.PipeInterface.PipeDataBlock>
  {
    internal class FacadeDataDescription: DataQueue.DataDescription
    {
      protected override TimeSpan TimeOut
      {
        get { throw new Exception( "The method or operation is not implemented." ); }
      }
      internal override TimeSpan TimeScann
      {
        get { return new TimeSpan( 0, 0, 0, 0, 1000 ); }
      }
      internal FacadeDataDescription( ComunicationNet.DataBlocksRow BlocksRow, ref int constraint )
        :
        base( BlocksRow, TimeSpan.MaxValue, null, null, ref constraint ) { }
    }
    internal class FacadePipeInterface: Pipe.PipeInterface
    {
      /// <summary>
      /// Facade implementation of BaseStation.Pipe.PipeInterface.PipeDataBlock
      /// </summary>
      internal class FacadePipeDataBlock: Pipe.PipeInterface.PipeDataBlock
      {
        internal FacadePipeDataBlock( WaitTimeList<PipeDataBlock> waitTimeList, FacadeDataDescription dataDescription, FacadePipeInterface pipeInterface )
          : base( waitTimeList, dataDescription, pipeInterface )
        { }
      }
      internal protected override bool WriteData( object data, IBlockDescription dataAddress )
      {
        throw new Exception( "The method or operation is not implemented." );
      }
      internal protected override bool ReadData( out object data, IBlockDescription dataAddress)
      {
        throw new Exception( "The method or operation is not implemented." );
      }
      internal FacadePipeInterface( Interface.Parameters interfaceDSC, FacadePipe pipe, CAS.CommServer.ProtocolHub.Communication.Diagnostic.Segment segment )
        :
        base( interfaceDSC, pipe, null, segment, 10 ) { }
    }//FacadePipeInterface
    internal FacadeSegment() : base( "NUnitTestFacadeSegment" ) { }
  }
}
