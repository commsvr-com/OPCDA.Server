//_______________________________________________________________
//  Title   : Communication statistics management class
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

using CAS.Lib.CommServerConsoleInterface;
using CAS.Lib.CommServerConsoleInterface.Properties;
using CAS.Lib.RTLib.Management;
using CAS.Lib.RTLib.Utils;
using System;
using System.Text;
using CommunicationDSC = CAS.NetworkConfigLib.ComunicationNet;

namespace BaseStation.Management
{

  /// <summary>
  /// Communication statistics management class
  /// </summary>
  public partial class Statistics
  {

    /// <summary>
    ///  Title   : Communication statistics management class 
    /// </summary>
    [Serializable]
    public abstract class SegmentStatistics: Metronom.RefreshAble, IHtmlOutput, ISegmentStatistics, IUpdateInternalStatistics
    {

      #region PRIVATE
      private readonly DateTime startTime = DateTime.Now;
      private System.Collections.ArrayList interfaceList = new System.Collections.ArrayList();
      private ChannelStatistics myChannel;
      private CAS.Lib.RTLib.Processes.Stopwatch SW_WriteDelay = new CAS.Lib.RTLib.Processes.Stopwatch();
      private CAS.Lib.RTLib.Processes.Stopwatch SW_ReadDelay = new CAS.Lib.RTLib.Processes.Stopwatch();
      private CAS.Lib.RTLib.Processes.Stopwatch SW_ConnectTime = new CAS.Lib.RTLib.Processes.Stopwatch();
      //this region is used for updating OPC tags
      /// <summary>
      /// connect time
      /// </summary>
      private CAS.Lib.RTLib.Processes.Stopwatch connectTime = new CAS.Lib.RTLib.Processes.Stopwatch();
      /// <summary>
      /// MinMaxAvr Read Delay
      /// </summary>
      private MinMaxAvr mmaReadDelay = new MinMaxAvr( 10 );
      /// <summary>
      /// MinMaxAvr Write Delay
      /// </summary>
      private MinMaxAvr mmaWriteDelay = new MinMaxAvr( 10 );
      /// <summary>
      /// MinMaxAvr  Connect Time
      /// </summary>
      private MinMaxAvr mmaConnectTime = new MinMaxAvr( 10 );
      private void MMAStopWatch_markNewVal( long min, long max, long avr )
      {
        myStat.UpdateStringsWithStatisticsFromStopwatch( mmaWriteDelay.ToString(), mmaReadDelay.ToString(), mmaConnectTime.ToString() );
      }
              /// <summary>
        /// Gets the average sampling time in ms as string.
        /// </summary>
        /// <value>The average sampling time in ms as string.</value>
        private string AverageSamplingTimeInMsAsString
        {
          get
          {
            uint numberOfConnections = myStat.ConnMadeCount + myStat.ConnFailCount;
            if ( numberOfConnections > 0 )
            {
              return ( (long)(( DateTime.Now - startTime ).TotalMilliseconds / numberOfConnections )).ToString();
            }
            else
              return Resources.report_text_NA;
          }
        }
      #endregion

      #region PUBLIC
      /// <summary>
      /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
      /// </summary>
      /// <returns>
      /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
      /// </returns>
      public override string ToString() { return myName; }
      /// <summary>
      /// States of the segment
      /// </summary>
      public enum States
      {
        /// <summary>
        /// disconnected 
        /// </summary>
        Disconnected,
        /// <summary>
        /// Connected
        /// </summary>
        Connected,
        /// <summary>
        /// Write Waiting To Be Connected
        /// </summary>
        WriteWaitingToBeConn,
        /// <summary>
        /// Read Waiting To Be Connected
        /// </summary>
        ReadWaitingToBeConn
      }
      /// <summary>
      /// Resets this instance. (Resets stopwatch: connect time, read delay, write delay)
      /// </summary>
      public void Reset()
      {
        ulong voidI;
        voidI = SW_ConnectTime.Reset;
        voidI = SW_ReadDelay.Reset;
        voidI = SW_WriteDelay.Reset;
      }
      /// <summary>
      /// Sets the new state.
      /// </summary>
      /// <value>The new state.</value>
      public States NewStateUpdateStatistics
      {
        set
        {
          ulong voidV;
          switch ( value )
          {
            case States.Disconnected:
              voidV = connectTime.Stop;
              mmaConnectTime.Add = (long)CAS.Lib.RTLib.Processes.Stopwatch.ConvertTo_ms( SW_ConnectTime.Reset );
              break;
            case States.Connected:
              myStat.ConnMadeCount++;
              voidV = SW_ReadDelay.Stop;
              voidV = SW_WriteDelay.Stop;
              mmaReadDelay.Add = (long)CAS.Lib.RTLib.Processes.Stopwatch.ConvertTo_ms( SW_ReadDelay.Reset );
              mmaWriteDelay.Add = (long)CAS.Lib.RTLib.Processes.Stopwatch.ConvertTo_ms( SW_WriteDelay.Reset );
              voidV = connectTime.Start;
              SW_ConnectTime.StartReset();
              break;
            case States.ReadWaitingToBeConn:
              voidV = SW_ReadDelay.Start;
              break;
            case States.WriteWaitingToBeConn:
              voidV = SW_WriteDelay.Start;
              break;
          };
          myStat.CurrentState = value;
        }
      }//NewState

      /// <summary>
      /// Communication statistics management class  (internal statistics - that are send through remoting)
      /// </summary>
      [Serializable]
      public class SegmentStatisticsInternal
      {
        #region PRIVATE
        private string mmaWriteDelayString = "", mmaReadDelayString = "", mmaConnectTimeString = "";
        private readonly long myID;
        private readonly string myName;
        private ulong connecttimeinseconds = 0;
        private uint[] packetsCount = new uint[] { 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// overtime coefficient minimum
        /// </summary>
        private uint OvertimeCoefficientMin = 0;
        /// <summary>
        /// overtime coefficient maximum
        /// </summary>
        private uint OvertimeCoefficientMax = 0;
        /// <summary>
        /// overtime coefficient average
        /// </summary>
        private uint OvertimeCoefficientAvr = 0;
        /// <summary>
        /// the state
        /// </summary>
        private States m_State = States.Disconnected;
        #endregion private
        #region PUBLIC
        /// <summary>
        /// The connection fail count.
        /// </summary>
        public uint ConnFailCount {get;private set;}
        /// <summary>
        /// the count of connection made
        /// </summary>
        public uint ConnMadeCount { get; internal set; } 
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether [keep connect].
        /// </summary>
        /// <value><c>true</c> if [keep connect]; otherwise, <c>false</c>.</value>
        public bool KeepConnect { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether [pickup connect].
        /// </summary>
        /// <value><c>true</c> if [pickup connect]; otherwise, <c>false</c>.</value>
        public bool PickupConnect { get; private set; }
        /// <summary>
        /// Gets or sets the time idle keep conn.
        /// </summary>
        /// <value>The time idle keep conn.</value>
        public long TimeIdleKeepConn { get; private set; }
        /// <summary>
        /// Gets or sets the time keep conn.
        /// </summary>
        /// <value>The time keep conn.</value>
        public long TimeKeepConn { get; private set; }
        /// <summary>
        /// Gets or sets the time reconnect.
        /// </summary>
        /// <value>The time reconnect.</value>
        public long TimeReconnect { get; private set; }
        /// <summary>
        /// Gets or sets the time scan.
        /// </summary>
        /// <value>The time scan.</value>
        public long TimeScan { get; private set; }
        /// <summary>
        /// Gets the addtional information.
        /// </summary>
        /// <value>The addtional information.</value>
        public string AddtionalInformation
        {
          get
          {
            StringBuilder sb = new StringBuilder();
            if ( ConnMadeCount == 0 )
              sb.AppendLine( Resources.report_test_segment_warning_no_connection_made );
            return sb.ToString();
          }
        }
        internal SegmentStatisticsInternal(
          string name,
          long id,
          string address,
          bool keepConnect,
          bool pickupConnect,
          long timeIdleKeepConn,
          long timeKeepConn,
          long timeReconnect,
          long timeScan )
        {
          myID = id;
          myName = name;
          KeepConnect = keepConnect;
          PickupConnect = pickupConnect;
          TimeIdleKeepConn = timeIdleKeepConn;
          TimeKeepConn = timeKeepConn;
          TimeReconnect = timeReconnect;
          TimeScan = timeScan;
          Address = address;
          ConnFailCount = 0;
          ConnMadeCount = 0;
        }
        /// <summary>
        /// Gets  ID.
        /// </summary>
        /// <value> ID.</value>
        public long MyID
        {
          get
          {
            return myID;
          }
        }
        /// <summary>
        /// Gets  name.
        /// </summary>
        /// <value> name.</value>
        public string MyName
        {
          get
          {
            return myName;
          }
        }
        /// <summary>
        /// Gets or sets the <see cref="System.UInt32"/> at the specified index.
        /// </summary>
        /// <value></value>
        public uint this[ int index ]
        {
          get
          {
            return packetsCount[ index ];
          }
          set
          {
            packetsCount[ index ] = value;
          }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
          return myName + "(" + myID.ToString() + ")";
        }
        /// <summary>
        /// Gets the connect time.
        /// </summary>
        /// <value>The connect time.</value>
        public ulong ConnectTime
        {
          get { return connecttimeinseconds; }
          internal set { connecttimeinseconds = value; }
        }
        /// <summary>
        /// Gets the write delay.
        /// </summary>
        /// <value>The write delay.</value>
        public string WriteDelay
        {
          get { return mmaWriteDelayString; }
        }
        /// <summary>
        /// Gets the read delay.
        /// </summary>
        /// <value>The read delay.</value>
        public string ReadDelay
        {
          get { return mmaReadDelayString; }
        }
        /// <summary>
        /// Gets the  segment connection time Minimum/Maximum/Average.
        /// </summary>
        /// <value>The segment connection time Minimum/Maximum/Average.</value>
        public string GetSegmentConnectionTimeMinimumMximumAverage
        {
          get { return mmaConnectTimeString; }
        }
        /// <summary>
        /// Gets the state .
        /// </summary>
        /// <value>The state .</value>
        public string CurrentStateAsString
        {
          get { return m_State.ToString(); }
        }//CurrentState
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state .</value>
        public States CurrentState
        {
          get { return m_State; }
          internal set { m_State = value; }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="SegmentStatisticsInternal"/> is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        public bool Connected
        {
          get { return ( m_State == States.Connected ); }
        }
        /// <summary>
        /// Gets the count of connection made as string.
        /// </summary>
        /// <value>the count of connection made as string.</value>
        public string GetMadeCount
        {
          get { return this.ConnMadeCount.ToString(); }
        }
        /// <summary>
        /// Gets the get fail count as string
        /// </summary>
        /// <value>The get fail count.</value>
        public string GetFailCount
        {
          get { return this.ConnFailCount.ToString(); }
        }
        /// <summary>
        /// Gets the get overtime coefficient as string
        /// </summary>
        /// <value>The get overtime coefficient as string.</value>
        public string GetOvertimeCoefficient
        {
          get
          {
            return String.Format( Resources.report_test_mn_av_mx, OvertimeCoefficientMin.ToString(), OvertimeCoefficientAvr.ToString(), OvertimeCoefficientMax.ToString() );
          }
        }
        /// <summary>
        /// Marks the connection fail.
        /// </summary>
        public void MarkConnFail()
        {
          lock ( this )
            ConnFailCount++;
        }
        /// <summary>
        /// Sets the overtime coefficient.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <param name="avr">The avr.</param>
        public void SetOvertimeCoefficient( long min, long max, long avr )
        {
          lock ( this )
          {
            OvertimeCoefficientMin = (ushort)min;
            OvertimeCoefficientMax = (ushort)max;
            OvertimeCoefficientAvr = (ushort)avr;
          }
        }
        internal void UpdateStringsWithStatisticsFromStopwatch( string MinMaxAverageWriteDelayString,
          string MinMaxAverageReadDelayString, string MinMaxAverageConnectTimeString )
        {
          mmaWriteDelayString = MinMaxAverageWriteDelayString;
          mmaReadDelayString = MinMaxAverageReadDelayString;
          mmaConnectTimeString = MinMaxAverageConnectTimeString;
        }
        #region MinMaxAvgReaders
        /// <summary>
        /// Gets the overtime coefficient  minimum.
        /// </summary>
        /// <value>The overtime coefficient_ min.</value>
        public long OvertimeCoefficient_Min
        {
          get
          {
            return OvertimeCoefficientMin;
          }
        }
        /// <summary>
        /// Gets the overtime coefficient average.
        /// </summary>
        /// <value>The overtime coefficient_ avr.</value>
        public long OvertimeCoefficient_Avr
        {
          get
          {
            return OvertimeCoefficientAvr;
          }
        }
        /// <summary>
        /// Gets the overtime coefficient_ max.
        /// </summary>
        /// <value>The overtime coefficient maximum.</value>
        public long OvertimeCoefficient_Max
        {
          get
          {
            return OvertimeCoefficientMax;
          }
        }
        #endregion MinMaxAvgReaders
        #endregion
      };//statistics internal

      /// <summary>
      /// Statistics
      /// </summary>
      public SegmentStatisticsInternal myStat;
      /// <summary>
      /// Gets  name.
      /// </summary>
      /// <value>name.</value>
      public string myName
      {
        get
        {
          return myStat.MyName;
        }
      }

      #region InterfaceLink
      /// <summary>
      /// Gets the protocol statistics.
      /// </summary>
      /// <param name="counters">The counters.</param>
      /// <param name="isAnySuccess">if set to <c>true</c> if any success.</param>
      void IInterface2SegmentLink.GetProtocolStatistics( ref uint[] counters, out bool isAnySuccess )
      {
        lock ( this )
        {
          myChannel.GeatProtocolStatistics( ref counters, out isAnySuccess );
          for ( ushort idx = 0; idx < counters.Length; idx++ )
          {
            myStat[ idx ] += counters[ idx ];
          }
        }
      }//GeatProtocolStatistics
      /// <summary>
      /// Adds the interface.
      /// </summary>
      /// <param name="intr">The intr.</param>
      void IInterface2SegmentLink.AddInterface( InterfaceStatistics intr )
      {
        interfaceList.Add( intr );
      }
      /// <summary>
      /// Gets the get OPC prefix.
      /// </summary>
      /// <value>The get OPC prefix.</value>
      public abstract string GetOPCPrefix { get; }
      #endregion
      #region HMI
      /// <summary>
      /// Dalegate that is used when state is changed
      /// </summary>
      public delegate void StateChanged( States currState );
      /// <summary>
      /// Occurs when [mark new state].
      /// </summary>
      public event StateChanged MarkNewState;
      /// <summary>
      /// Gets the connect time.
      /// </summary>
      /// <value>The connect time.</value>
      public ulong ConnectTime
      {
        get
        {
          myStat.ConnectTime = CAS.Lib.RTLib.Processes.Stopwatch.ConvertTo_s( connectTime.Read );
          return myStat.ConnectTime;
        }
      }
      /// <summary>
      /// Gets the write delay.
      /// </summary>
      /// <value>The write delay.</value>
      private string WriteDelay
      {
        get { return mmaWriteDelay.ToString(); }
      }
      /// <summary>
      /// Gets the read delay.
      /// </summary>
      /// <value>The read delay.</value>
      private string ReadDelay
      {
        get { return mmaReadDelay.ToString(); }
      }
      /// <summary>
      /// Gets the get segment connection minimum/maximum/average.
      /// </summary>
      /// <value>The get segment connection minimum/maximum/average.</value>
      private string GetSegmentConnMMA
      {
        get { return mmaConnectTime.ToString(); }
      }
      /// <summary>
      /// Gets the state .
      /// </summary>
      /// <value>The state .</value>
      public string CurrentStateAsString
      {
        get { return myStat.CurrentStateAsString; }
      }//CurrentState
      /// <summary>
      /// Gets the state .
      /// </summary>
      /// <value>The state .</value>
      public States GetCurrentState { get { return myStat.CurrentState; } }
      /// <summary>
      /// Gets a value indicating whether this <see cref="SegmentStatistics"/> is connected.
      /// </summary>
      /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
      public bool Connected
      {
        get { return myStat.Connected; }
      }
      /// <summary>
      /// Gets the get made (successfull connections) count.
      /// </summary>
      /// <value>The get made(successfull connections)  count.</value>
      public string GetMadeCount
      {
        get { return myStat.GetMadeCount; }
      }
      /// <summary>
      /// Gets the get fail count.
      /// </summary>
      /// <value>The get fail count.</value>
      public string GetFailCount
      {
        get { return myStat.GetFailCount; }
      }
      /// <summary>
      /// Gets the get overtime coefficient.
      /// </summary>
      /// <value>The get overtime coefficient.</value>
      public string GetOvertimeCoefficient
      {
        get
        {
          return myStat.GetOvertimeCoefficient;
        }
      }
      #region MinMaxAvgReaders
      //WriteDelay
      /// <summary>
      /// Gets the write delay minimum.
      /// </summary>
      /// <value>The write delay min.</value>
      public long WriteDelayMin
      {
        get
        {
          return mmaWriteDelay.Min;
        }
      }
      /// <summary>
      /// Gets the write delay average.
      /// </summary>
      /// <value>The write delay avr.</value>
      public long WriteDelayAvr
      {
        get
        {
          return mmaWriteDelay.Avr;
        }
      }
      /// <summary>
      /// Gets the write delay maximum.
      /// </summary>
      /// <value>The write delay max.</value>
      public long WriteDelayMax
      {
        get
        {
          return mmaWriteDelay.Max;
        }
      }
      //ReadDelay
      /// <summary>
      /// Gets the read delay minimum.
      /// </summary>
      /// <value>The read delay min.</value>
      public long ReadDelayMin
      {
        get
        {
          return mmaReadDelay.Min;
        }
      }
      /// <summary>
      /// Gets the read delay average.
      /// </summary>
      /// <value>The read delay avr.</value>
      public long ReadDelayAvr
      {
        get
        {
          return mmaReadDelay.Avr;
        }
      }
      /// <summary>
      /// Gets the read delay maximum.
      /// </summary>
      /// <value>The read delay max.</value>
      public long ReadDelayMax
      {
        get
        {
          return mmaReadDelay.Max;
        }
      }
      //ConnectTime
      /// <summary>
      /// Gets the connect time minimum.
      /// </summary>
      /// <value>The connect time min.</value>
      public long ConnectTimeMin
      {
        get
        {
          return mmaConnectTime.Min;
        }
      }
      /// <summary>
      /// Gets the connect time average.
      /// </summary>
      /// <value>The connect time avr.</value>
      public long ConnectTimeAvr
      {
        get
        {
          return mmaConnectTime.Avr;
        }
      }
      /// <summary>
      /// Gets the connect time maximum.
      /// </summary>
      /// <value>The connect time max.</value>
      public long ConnectTimeMax
      {
        get
        {
          return mmaConnectTime.Max;
        }
      }
      #endregion MinMaxAvgReaders

      #endregion
      #region ISegmentStatistics Members
      States ISegmentStatistics.NewState
      {
        set
        {
          NewStateUpdateStatistics = value;
          if ( MarkNewState != null )
            MarkNewState( value );
        }
      }//NewState
      void ISegmentStatistics.MarkConnFail()
      {
        myStat.MarkConnFail();
      }
      void ISegmentStatistics.SetOvertimeCoefficient( long min, long max, long avr )
      {
        myStat.SetOvertimeCoefficient( min, max, avr );
      }
      #endregion
      #region creator
      /// <summary>
      /// Initializes a new instance of the <see cref="SegmentStatistics"/> class.
      /// </summary>
      /// <param name="currDsc">The segment row descriptor.</param>
      /// <param name="chn">The channel statistics</param>
      public SegmentStatistics( CommunicationDSC.SegmentsRow currDsc, ChannelStatistics chn )
      {
        myStat = new SegmentStatisticsInternal(
          currDsc.Name,
          currDsc.SegmentID,
          currDsc.Address,
          currDsc.KeepConnect,
          currDsc.PickupConn,
          currDsc.TimeIdleKeepConn,
          currDsc.timeKeepConn,
          currDsc.TimeReconnect,
          currDsc.TimeScan );
        Reset();
        segmentList.Add( this );
        myChannel = chn;
        myChannel.AddSegment( this );
        mmaConnectTime.markNewVal += new MinMaxAvr.newVal( MMAStopWatch_markNewVal );
        mmaReadDelay.markNewVal += new MinMaxAvr.newVal( MMAStopWatch_markNewVal );
        mmaWriteDelay.markNewVal += new MinMaxAvr.newVal( MMAStopWatch_markNewVal );
        MMAStopWatch_markNewVal( 0, 0, 0 );
      }

      #endregion
      #endregion PUBLIC

      #region IHtmlOutput Members
      /// <summary>
      /// variable resposible for row color changing
      /// </summary>
      static bool changecolor = true;
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
      private static string GetHtmlCell( string rowclass, string cellValue )
      {
        return GetHtmlCell( rowclass, cellValue, 1 );
      }
      private static string GetHtmlCell( string rowclass, string cellValue, int rowspan )
      {
        StringBuilder sb = new StringBuilder();
        sb.Append( "<td class='" );
        sb.Append( rowclass );
        sb.Append( "' " );
        if ( rowspan > 1 )
        {
          sb.Append( "rowspan='" );
          sb.Append( rowspan );
          sb.Append( "' " );
        }
        sb.Append( ">" );
        if ( !string.IsNullOrEmpty( cellValue ) )
          sb.Append( cellValue );
        else
          sb.Append( "&nbsp;" );
        sb.Append( "</td>" );
        return sb.ToString();
      }
      /// <summary>
      /// this function is for retreiving description fot this object in the HTML
      /// </summary>
      /// <returns>string with HTML output</returns>
      public string GetHtmlTableRowDescription()
      {
        StringBuilder ret = new StringBuilder();
        ret.Append( "<tr>" );
        ret.Append( GetHtmlCell( "k3", "Name", 2 ) );
        ret.Append( GetHtmlCell( "k3", "Segment state" ) );
        ret.Append( GetHtmlCell( "k3", "Number of successful connections" ) );
        ret.Append( GetHtmlCell( "k3", "Sampling time [ms] <br/>(from config)" ) );
        ret.Append( GetHtmlCell( "k3", "KeepConnect / PickupConnect <br/>(from config)" ) );
        ret.Append( GetHtmlCell( "k3", "Time keep connect [ms] <br/>(from config)" ) );
        ret.Append( GetHtmlCell( "k3", "Connect time [ms] *" ) );
        ret.Append( GetHtmlCell( "k3", "Write delay [ms] *" ) );
        ret.Append( GetHtmlCell( "k3", "Additional information", 2 ) );
        ret.Append( "</tr>" );
        ret.Append( "<tr>" );
        ret.Append( GetHtmlCell( "k3", "Total connect time [s]" ) );
        ret.Append( GetHtmlCell( "k3", "Number of connections failed" ) );
        ret.Append( GetHtmlCell( "k3", "Average sampling time [ms] <br/>(from real)" ) );
        ret.Append( GetHtmlCell( "k3", "Time reconnect [ms] <br/>(from config)" ) );
        ret.Append( GetHtmlCell( "k3", "Time idle keep connect [ms] <br/>(from config)" ) );
        ret.Append( GetHtmlCell( "k3", "Data overtime (Min/Avr/Max) [%] **" ) );
        ret.Append( GetHtmlCell( "k3", "Read delay [ms] *" ) );
        ret.Append( "</tr>" );
        return ret.ToString();
      }
      /// <summary>
      /// this function is for retreiving data that represens that object
      /// </summary>
      /// <returns>string with HTML data</returns>
      public string ToHtmlTableRow()
      {
        StringBuilder ret = new StringBuilder();
        string RowClassAct = RowClass;
        ret.Append( "<tr>" );
        ret.Append( GetHtmlCell( RowClassAct, String.Format( "{0}<br/>(address: '{1}')", this.myName, this.myStat.Address ), 2 ) );
        ret.Append( GetHtmlCell( RowClassAct, this.CurrentStateAsString ) );
        ret.Append( GetHtmlCell( RowClassAct, this.GetMadeCount.ToString() ) );
        ret.Append( GetHtmlCell( RowClassAct, this.myStat.TimeScan.ToString() ) );
        ret.Append( GetHtmlCell( RowClassAct, this.myStat.KeepConnect.ToString() + " / " + this.myStat.PickupConnect.ToString() ) );
        ret.Append( GetHtmlCell( RowClassAct, this.myStat.TimeKeepConn.ToString() ) );
        ret.Append( GetHtmlCell( RowClassAct, this.GetSegmentConnMMA.ToString() ) );
        ret.Append( GetHtmlCell( RowClassAct, this.WriteDelay.ToString() ) );
        ret.Append( GetHtmlCell( RowClassAct, this.myStat.AddtionalInformation, 2 ) );
        ret.Append( "</tr>" );
        ret.Append( "<tr>" );
        ret.Append( GetHtmlCell( RowClassAct, this.ConnectTime.ToString() ) );
        ret.Append( GetHtmlCell( RowClassAct, this.GetFailCount.ToString() ) );
        ret.Append( GetHtmlCell( RowClassAct, this.AverageSamplingTimeInMsAsString ) );
        ret.Append( GetHtmlCell( RowClassAct, this.myStat.TimeReconnect.ToString() ) );
        ret.Append( GetHtmlCell( RowClassAct, this.myStat.TimeIdleKeepConn.ToString() ) );
        ret.Append( GetHtmlCell( RowClassAct, this.GetOvertimeCoefficient ) );
        ret.Append( GetHtmlCell( RowClassAct, this.ReadDelay.ToString() ) );
        ret.Append( "</tr>" );
        return ret.ToString();
      }
      /// <summary>
      /// this function is for retreiving data that represens that object (data + description)
      /// </summary>
      /// <returns>string with HTML data</returns>
      public string ToHtml()
      {
        return "<table border='1' class='t2'>" + GetHtmlTableRowDescription() + ToHtmlTableRow() + "</table>";
      }
      #endregion

      #region IUpdateInternalStatistics Members

      void IUpdateInternalStatistics.UpdateInternal()
      {
        myStat.ConnectTime = CAS.Lib.RTLib.Processes.Stopwatch.ConvertTo_s( connectTime.Read );
      }

      #endregion

    }//Segment

  }
}