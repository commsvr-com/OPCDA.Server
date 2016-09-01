//_______________________________________________________________
//  Title   : Report Generator for CommServer
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


using CAS.CommServer.ProtocolHub.Communication.Properties;
using CAS.Lib.RTLib.Management;
using CAS.Lib.RTLib.Utils;
using System;
using System.IO;
using System.Text;
using Statistics = global::BaseStation.Management.Statistics;

namespace CAS.CommServer.ProtocolHub.Communication.Diagnostic
{
  /// <summary>
  /// Class that generate html report about CommServer state
  /// </summary>
  public class ReportGenerator: CAS.Lib.RTLib.Utils.ReportGenerator
  {
    /// <summary>
    /// Gets the string that represents the report.
    /// </summary>
    /// <returns>html report</returns>
    public override string GetReportString()
    {
      StringBuilder sb = new StringBuilder();
      //wpisujemy nag³ówek
      sb.Append( this.getHeader() );
      //najpierw robimy nag³ówek
      sb.Append( @"<tr><td width='1200'><table border='0' align='center'><tr><td><h1>CAS - CommServer</h1></td></tr></table></td><tr>" );
      sb.Append( @"<tr><td  bgcolor='#021376' width='1200' height='1'></td></tr>" );
      sb.Append( @"<tr><td width='1200'>" );
      try
      {
        //
        //Miejsce na wlasciwe dane
        sb.Append( @"<table border='0' align='center' width='1200' class='t1'>" );
        sb.Append( @"<tr><td  class='k1'>Report time </td><td  class='k2'>" + DateTimeProvider.GetCurrentTime().ToString() + "</td></tr>" );
        sb.Append( @"<tr><td  class='k1'>Run Time [s] </td><td class='k2'>" + CommServerComponent.RunTime.ToString() + "</td></tr>" );
        sb.Append(@"<tr><td  class='k1'>Configuration file </td><td class='k2'>" + CAS.Lib.RTLib.Management.AppConfigManagement.filename + "</td></tr>");
        sb.Append( @"<tr><td  class='k1'>Program Name </td><td class='k2'>" + System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName + "</td></tr>" );
        sb.Append( @"</table>" );
      }
      catch ( Exception ex )
      {
        SignalError( sb, ex );
      }
      sb.Append( @"<Table border='0' width='400'>
	          <TBody>
	          <TR>
	          <TD class='kk'><t>.......................................................................................</TD>
            </TR></TBODY></Table>
             " );
      //
      //
        bool first = true;
      //stations:
      sb.Append( @"<h2>Stations</h2>" );
      sb.Append( @"<table border='1' class='t2'>" );
      try
      {
        foreach ( IHtmlOutput obj in global::BaseStation.Management.Statistics.stationList )
        {
          if ( first )
          {
            sb.Append( obj.GetHtmlTableRowDescription() );
            first = false;
          }
          sb.Append( obj.ToHtmlTableRow() );
        }
      }
      catch ( Exception ex )
      {
        SignalError( sb, ex );
      }
      sb.Append( @"</table>" );
      //segments:
      sb.Append( @"<h2>Segments</h2><table border='1'>" );
      first = true;
      try
      {
        if (Statistics.segmentList.Count > 0 )
        {
          foreach ( IHtmlOutput obj in Statistics.segmentList )
          {
            if ( first )
            {
              sb.Append( obj.GetHtmlTableRowDescription() );
              first = false;
            }
            sb.Append( obj.ToHtmlTableRow() );
          }
        }
        else
        {
          sb.Append( "No segments are defined" );
        }
      }
      catch ( Exception ex )
      {
        SignalError( sb, ex );
      }
      sb.Append( @"</table>" );
      sb.Append( @"<h4> * Values might be equal 0 when number of samples is less than 10.</h4>" );
      sb.Append( @"<h4>** Values might be equal 0 when number of samples is less than 20.</h4>" );
      //interfaces:
      first = true;
      sb.Append( @"<h2>Interfaces</h2><table border='1'>" );
      try
      {
        if ( Statistics.interfaceList.Count > 0 )
        {
          foreach ( IHtmlOutput obj in Statistics.interfaceList )
          {
            if ( first )
            {
              sb.Append( obj.GetHtmlTableRowDescription() );
              first = false;
            }
            sb.Append( obj.ToHtmlTableRow() );
          }
        }
        else
        {
          sb.Append( "No interfaces are defined" );
        }
      }
      catch ( Exception ex )
      {
        SignalError( sb, ex );
      }
      sb.Append( @"</table>" );
      //Protocols:
      first = true;
      sb.Append( @"<h2>Protocols</h2><table border='1'>" );
      try
      {
        foreach ( ProtocolDsc curr in Protocol.GetProtDescriptions )
        {
          IHtmlOutput obj = new Diagnostic.CommseverProtocol( curr );
          if ( first )
          {
            sb.Append( obj.GetHtmlTableRowDescription() );
            first = false;
          }
          sb.Append( obj.ToHtmlTableRow() );
        }
      }
      catch ( Exception ex )
      {
        SignalError( sb, ex );
      }
      sb.Append( @"</table>" );
      //
      //stopka:
      sb.Append( this.getFooter() );
      return sb.ToString();
    }

    private static void SignalError( StringBuilder sb, Exception ex )
    {
      sb.Append( Resources.ExceptionDuringReportCreation );
      sb.Append( ": " );
      sb.Append( ex.ToString() );
    }
    /// <summary>
    /// generatin a report
    /// </summary>
    protected override void doReport()
    {
      //otwieramy
      using ( StreamWriter sw = File.CreateText( DestFilename ) )
      {
        sw.WriteLine( this.GetReportString() );
      }

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ReportGenerator"/> class.
    /// </summary>
    /// <param name="title">The title of the report.</param>
    public ReportGenerator( string title )
      : base( title )
    {
    }

  }
}
