//<summary>
//  Title   : Facade implementation of Pipe
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

using CAS.NetworkConfigLib;
using System.Collections;

namespace CAS.Lib.CommServer.Tests
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
      myStatistics = new BaseStation.Management.Station( currSDsc );
    }
  }
}
