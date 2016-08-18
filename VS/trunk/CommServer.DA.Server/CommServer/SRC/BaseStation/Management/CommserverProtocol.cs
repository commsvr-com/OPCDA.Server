//<summary>
//  Title   : class that provides communication statistics  for protocol
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    MPostol - 20-04-2005 : created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using CAS.Lib.RTLib.Management;
using System;

namespace BaseStation.Management
{
  /// <summary>
  /// Summary description for Protocol.
  /// </summary>
  public class CommseverProtocol: IHtmlOutput
  {
    private ProtocolDsc myProtocolStats;
    /// <summary>
    /// Initializes a new instance of the <see cref="CommseverProtocol"/> class.
    /// </summary>
    /// <param name="_myProtocolStats">The  protocol statistics (description).</param>
    public CommseverProtocol( ProtocolDsc _myProtocolStats )
    {
      myProtocolStats = _myProtocolStats;
    }
    #region IHtmlOutput Members
    /// <summary>
    /// variable responsible for row color changing
    /// </summary>
    private static bool changecolor = true;
    /// <summary>
    /// returns row color class
    /// </summary>
    private string RowClass
    {
      get
      {
        changecolor = !changecolor;
        if ( changecolor )
          return "k41";
        return "k4";
      }
    }
    /// <summary>
    /// this function is for retreiving description fot this object in the HTML
    /// </summary>
    /// <returns>string with HTML output</returns>
    public string GetHtmlTableRowDescription()
    {
      string ret = "";
      ret += "<tr>";
      ret += "<td class='k3' rowspan='2'>" + "Description" + "</td>";
      ret += "<td class='k3' rowspan='2'>" + "ProtPar" + "</td>";
      ret += "<td class='k3'>" + "RX+" + "</td>";
      ret += "<td class='k3'>" + "TX+" + "</td>";
      ret += "<td class='k3'>" + "RX Frames Count" + "</td>";
      ret += "<td class='k3'>" + "CRC Errors Cnt-" + "</td>";
      ret += "<td class='k3'>" + "Time Out Cnt" + "</td>";
      ret += "<td class='k3'>" + "Protocol Rx Synch Error Cnt" + "</td>";
      ret += "<td class='k3'>" + "Max Response Time" + "</td>";
      ret += "</tr>";
      ret += "<tr>";
      ret += "<td class='k3'>" + "RX-" + "</td>";
      ret += "<td class='k3'>" + "TX-" + "</td>";
      ret += "<td class='k3'>" + "TX Frames Count" + "</td>";
      ret += "<td class='k3'>" + "Incomplete Cnt" + "</td>";
      ret += "<td class='k3'>" + "Protocol Invalid Cnt" + "</td>";
      ret += "<td class='k3'>" + "Protocol NAK Cnt" + "</td>";
      ret += "<td class='k3'>" + "Max Char Gap Time" + "</td>";
      ret += "</tr>";
      return ret;
    }
    /// <summary>
    /// this function is for retreiving data that represens that object
    /// </summary>
    /// <returns>string with HTML data</returns>
    public string ToHtmlTableRow()
    {
      string RowClassAct = RowClass;
      string ret = "";
      ret += "<tr>";
      ret += "<td class='" + RowClassAct + "' rowspan='2'>&nbsp;" + myProtocolStats.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "' rowspan='2'>&nbsp;" + myProtocolStats.m_protocolPar + "</td>";
      IProtocol m_IProtocol = Protocol.GetProtocolStatistics( myProtocolStats.m_ID );
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetRXDBSucc.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetTXDBSucc.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetStRxFrameCounter.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetStRxCRCErrorCounter.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetStRxNoResponseCounter.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetStRxSynchError.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetTimeMaxResponseDelay + "</td>";
      ret += "</tr>";
      ret += "<tr>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetRXDBFail.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetTXDBFail.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetStTxFrameCounter.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetStRxFragmentedCounter.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetStRxInvalid.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetStRxNAKCounter.ToString() + "</td>";
      ret += "<td class='" + RowClassAct + "'>&nbsp;" + m_IProtocol.GetTimeCharGap + "</td>";
      ret += "</tr>";
      return ret;
    }
    /// <summary>
    /// prepare HTML report
    /// </summary>
    /// <returns>string with HTML Report</returns>
    public string ToHtml()
    {
      return "<table border='1' class='t2'>" + GetHtmlTableRowDescription() + ToHtmlTableRow() + "</table>";
    }
    #endregion
  }
}
