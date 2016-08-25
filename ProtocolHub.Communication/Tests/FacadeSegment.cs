//<summary>
//  Title   : Facade Segment for Unit tests
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    200709 mpostol - created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http://www.cas.eu
//</summary>

using System;
using CAS.Lib.RTLib.Processes;
using CAS.NetworkConfigLib;

namespace CAS.Lib.CommServer.Tests
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
      /// Facade immplementation of BaseStation.Pipe.PipeInterface.PipeDataBlock
      /// </summary>
      internal class FacadePipeDataBlock: Pipe.PipeInterface.PipeDataBlock
      {
        internal FacadePipeDataBlock( WaitTimeList<PipeDataBlock> waitTimeList, FacadeDataDescription dataDescription, FacadePipeInterface pipeInterface )
          : base( waitTimeList, dataDescription, pipeInterface )
        { }
      }
      internal protected override bool WriteData( object data, CAS.Lib.CommonBus.ApplicationLayer.IBlockDescription dataAdres )
      {
        throw new Exception( "The method or operation is not implemented." );
      }
      internal protected override bool ReadData( out object data, CAS.Lib.CommonBus.ApplicationLayer.IBlockDescription dataAdres )
      {
        throw new Exception( "The method or operation is not implemented." );
      }
      internal FacadePipeInterface
        ( Interface.Parameters interfaceDSC, FacadePipe pipe, BaseStation.Management.Segment segment )
        :
        base( interfaceDSC, pipe, null, segment, 10 ) { }
    }//FacadePipeInterface
    internal FacadeSegment() : base( "NUnitTestFacadeSegment" ) { }
  }
}
