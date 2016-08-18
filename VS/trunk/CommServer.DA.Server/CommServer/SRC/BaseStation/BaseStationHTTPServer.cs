//  Title   : BaseStation HTTP Server
//  Author  : Maciej Zbrzezny
//  System  : Microsoft Visual C# .NET
//  History :
//    28-08-2005: created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2003, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.com.pl

using System;
using BaseStation;
using BaseStation.Management;
namespace BaseStation
{
  /// <summary>
  /// Summary description for BaseStationHTTPServer.
  /// </summary>
  public class BaseStationHTTPServer: Utils.HTTPServer
  {
    protected override string GetStringData( string directory, string filename, System.Collections.Hashtable parameters )
    {
      ReportGenerator rep = new ReportGenerator( "CAS-Commserver_state" );
      string ret = "";
      if ( filename.Equals( "stats.html" ) )
        ret = rep.GetReportString();
      {
        ret += "<br>directory=" + directory;
        ret += "<br>filename=" + filename;
        if ( parameters != null )
          foreach ( string key in parameters.Keys )
            ret += "<br>" + key + "=" + parameters[ key ].ToString();
      }
      return ret;
    }
    public BaseStationHTTPServer( int port )
      : base( port )
    {
    }
  }
}
