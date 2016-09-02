//_______________________________________________________________
//  Title   : Facade implementation of Pipe
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
using CAS.NetworkConfigLib;
using System.Collections;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests.Instrumentation
{

  /// <summary>
  /// Facade implementation of Pipe
  /// </summary>
  class FacadePipe: Pipe
  {
    private ArrayList myDataDescriptionsList = new ArrayList();
    protected override IEnumerable GetDataDescriptionList
    {
      get { return myDataDescriptionsList; }
    }
    internal FacadePipe( ComunicationNet.StationRow currSDsc ) 
    {
      myStatistics = new CAS.CommServer.ProtocolHub.Communication.Diagnostic.Station( currSDsc );
    }
  }
}
