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
using CAS.Lib.RTLib.Management;
using System;

namespace BaseStation.Management
{
  /// <summary>
  /// Communication statistics management class
  /// </summary>
  public partial class Statistics
  {
    /// <summary>
    /// Provides statistic information to management level - human machine interface (HMI)
    /// </summary>
    [Serializable]
    public abstract class InterfaceStatistics : Metronom.RefreshAble, IHtmlOutput, IUpdateInternalStatistics
    {

      #region PRIVATE
      private IInterface2SegmentLink mySegment;
      private StationStatistics myStation;
      private CAS.Lib.RTLib.Processes.Stopwatch failTime = new CAS.Lib.RTLib.Processes.Stopwatch();
      private CAS.Lib.RTLib.Processes.Stopwatch activeTime = new CAS.Lib.RTLib.Processes.Stopwatch();
      private CAS.Lib.RTLib.Processes.Stopwatch standbyTime = new CAS.Lib.RTLib.Processes.Stopwatch();

      private void GeatProtocolStatistics(out bool isAnySuccess)
      {
        mySegment.GetProtocolStatistics(ref myStat.currCount, out isAnySuccess);
        for (ushort idx = 0; idx < myStat.currCount.Length; idx++)
        {
          myStat.packetsCount[idx] += myStat.currCount[idx];
        }
      }//GeatProtocolStatistics
      #endregion

      #region PUBLIC
      /// <summary>
      /// provides statistic information to management level - human machine interface (HMI)(internal statistics - that are send through remoting)
      /// </summary>
      [Serializable]
      public class InterfaceStatisticsInternal
      {
        #region private
        private static ulong InternalIDCounter = 0;
        /// <summary>
        /// Interface statistics
        /// </summary>
        protected InterfaceState myState = InterfaceState.Standby;
        private ulong activeTime = 0, failTime = 0, standbyTime = 0;
        internal uint[] packetsCount = new uint[] { 0, 0, 0, 0, 0, 0 };
        internal uint[] currCount = new uint[] { 0, 0, 0, 0, 0, 0 };
        #endregion
        #region public
        /// <summary>
        /// Name
        /// </summary>
        public readonly string myName;
        /// <summary>
        /// ID InterfaceNum
        /// </summary>
        public readonly ulong myID_InterfaceNum;
        /// <summary>
        /// ID Internal
        /// </summary>
        public readonly ulong myID_Internal;
        #endregion
        #region public method
        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceStatisticsInternal"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The id.</param>
        internal InterfaceStatisticsInternal(string name, ulong id)
        {
          myName = name;
          myID_InterfaceNum = id;
          myID_Internal = InternalIDCounter++;
        }
        /// <summary>
        /// Gets the get state as  string.
        /// </summary>
        /// <value>The get state as string.</value>
        public string GetState2String
        {
          get
          {
            return myState.ToString();
          }
        }//CurrentInterfaceState
        /// <summary>
        /// Gets a value indicating whether this <see cref="InterfaceStatisticsInternal"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active
        {
          get
          {
            return (myState == InterfaceState.Active);
          }
        }
        /// <summary>
        /// Gets  state.
        /// </summary>
        /// <value> state.</value>
        public InterfaceState MyState
        {
          get
          {
            return myState;
          }
        }
        /// <summary>
        /// Gets the active time.
        /// </summary>
        /// <value>The active time.</value>
        public ulong ActiveTime
        {
          get { return activeTime; }
        }
        /// <summary>
        /// Gets the fail time.
        /// </summary>
        /// <value>The fail time.</value>
        public ulong FailTime
        {
          get { return failTime; }
        }
        /// <summary>
        /// Gets the standby time.
        /// </summary>
        /// <value>The standby time.</value>
        public ulong StandbyTime
        {
          get { return standbyTime; }
        }
        /// <summary>
        /// Gets the state .
        /// </summary>
        /// <value>The state .</value>
        public InterfaceState State
        {
          get
          {
            return myState;
          }
          internal set
          {
            myState = value;
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
          return myName + "(" + myID_InterfaceNum.ToString() + ")";
        }
        internal void UpdateTimers(ulong ActiveTime, ulong FailTime, ulong StandbyTime)
        {
          activeTime = ActiveTime;
          failTime = FailTime;
          standbyTime = StandbyTime;
        }
        #endregion public method
      };
      /// <summary>
      /// access to statistic internal
      /// </summary>
      public InterfaceStatisticsInternal myStat;
      /// <summary>
      /// Gets  name.
      /// </summary>
      /// <value>name.</value>
      private string myName
      {
        get
        {
          return myStat.myName;
        }
      }
      /// <summary>
      /// Gets my ID interface num.
      /// </summary>
      /// <value>My ID interface num.</value>
      public ulong myID_InterfaceNum
      {
        get
        {
          return myStat.myID_InterfaceNum;
        }
      }
      /// <summary>
      /// states of the interfaces
      /// </summary>
      public enum InterfaceState
      {
        /// <summary>
        /// Active
        /// </summary>
        Active,
        /// <summary>
        /// Fail
        /// </summary>
        Fail,
        /// <summary>
        /// Standby
        /// </summary>
        Standby
      }

      #region HMI
      /// <summary>
      /// delegate  that is used when interface state is changed
      /// </summary>
      public delegate void InterfaceStateChanged(InterfaceState currState);
      /// <summary>
      /// Occurs when [update interface state].
      /// </summary>
      public event InterfaceStateChanged UpdateInterfaceState;
      /// <summary>
      /// Gets the active time.
      /// </summary>
      /// <value>The active time.</value>
      public ulong ActiveTime
      {
        get
        {
          ((IUpdateInternalStatistics)this).UpdateInternal();
          return myStat.ActiveTime;
        }
      }
      /// <summary>
      /// Gets the fail time.
      /// </summary>
      /// <value>The fail time.</value>
      public ulong FailTime
      {
        get
        {
          ((IUpdateInternalStatistics)this).UpdateInternal();
          return myStat.FailTime;
        }
      }
      /// <summary>
      /// Gets the standby time.
      /// </summary>
      /// <value>The standby time.</value>
      public ulong StandbyTime
      {
        get
        {
          ((IUpdateInternalStatistics)this).UpdateInternal();
          return myStat.StandbyTime;
        }
      }
      /// <summary>
      /// Gets the state .
      /// </summary>
      /// <value>The state .</value>
      public InterfaceState GetState { get { return myStat.MyState; } }
      /// <summary>
      /// Gets the get state as string.
      /// </summary>
      /// <value>The get state as string.</value>
      public string GetState2String
      {
        get
        {
          return myStat.GetState2String;
        }
      }//CurrentInterfaceState
      /// <summary>
      /// Gets a value indicating whether this <see cref="InterfaceStatistics"/> is active.
      /// </summary>
      /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
      public bool Active
      {
        get
        {
          return myStat.Active;
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
        return myName + "_" + myID_InterfaceNum.ToString();
      }
      /// <summary>
      /// Gets the NAME.
      /// </summary>
      /// <value>The NAME.</value>
      public string NAME
      {
        get
        {
          return myName;
        }
      }
      #endregion

      #region PARENT
      /// <summary>
      /// Marks the ReadWrite operation result.
      /// </summary>
      public void MarkRWOperationResult()
      {
        //        MPTD zaiplementowaæ - tu jest circular reference
        //        lock(this)
        //        {
        //          bool isAnySuccess;
        //          GeatProtocolStatistics(out isAnySuccess);
        ////          if ( (myState == InterfaceState.Fail) && isAnySuccess )
        ////          {
        ////            ulong voidV = activeTime.Start;
        ////            voidV       = failTime.Stop;
        ////            myState     = InterfaceState.Active;
        ////          }
        //        } 
      }
      /// <summary>
      /// Sets the state of the current interface.
      /// </summary>
      /// <value>The state of the current interface.</value>
      public InterfaceState CurrentInterfaceStateUpdate
      {
        set
        {
          lock (this)
          {
            ulong voidV;
            switch (value)
            {
              case InterfaceState.Active:
                //if (myState == InterfaceState.Standby )
                voidV = standbyTime.Stop;
                voidV = failTime.Stop;
                voidV = activeTime.Start;
                break;
              case InterfaceState.Fail:
                System.Diagnostics.Debug.Assert(myStat.State == InterfaceState.Active);
                voidV = activeTime.Stop;
                voidV = failTime.Start;
                break;
              case InterfaceState.Standby:
                System.Diagnostics.Debug.Assert(myStat.State == InterfaceState.Active);
                voidV = activeTime.Stop;
                voidV = standbyTime.Start;
                myStat.State = InterfaceState.Standby;
                break;
            }
            myStat.State = value;
          }
        }
      }//CurrentInterfaceState

      /// <summary>
      /// Sets the state of the current interface.
      /// </summary>
      /// <value>The state of the current interface.</value>
      public InterfaceState CurrentInterfaceState
      {
        set
        {
          CurrentInterfaceStateUpdate = value;
          lock (this)
          {
            switch (value)
            {
              case InterfaceState.Active:
                //if (myState == InterfaceState.Standby )
                myStation.StationState = true;
                break;
              case InterfaceState.Fail:
                myStation.StationState = false;
                break;
              case InterfaceState.Standby:
                myStation.StationState = false;
                break;
            }
            if (UpdateInterfaceState != null)
              UpdateInterfaceState(value);
          }
        }
      }//CurrentInterfaceState
      /// <summary>
      /// Initializes a new instance of the <see cref="InterfaceStatistics"/> class.
      /// </summary>
      /// <param name="name">The name.</param>
      /// <param name="interfaceNumber">The interface number.</param>
      /// <param name="seg">The seg.</param>
      /// <param name="stt">The STT.</param>
      public InterfaceStatistics(string name, ulong interfaceNumber, IInterface2SegmentLink seg, StationStatistics stt)
      {
        mySegment = seg;
        myStation = stt;
        interfaceList.Add(this);
        stt.AddInterface(this);
        seg.AddInterface(this);
        myStat = new InterfaceStatisticsInternal(name, interfaceNumber);
      }
      #endregion
      #endregion

      #region IHtmlOutput Members
      /// <summary>
      /// variable resposible for row color changing
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
          if (changecolor)
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
        ret += "<td class='k3' >" + "Name" + "</td>";
        ret += "<td class='k3' >" + "State" + "</td>";
        ret += "<td class='k3' >" + "Active Time" + "</td>";
        ret += "<td class='k3' >" + "Fail Time" + "</td>";
        ret += "<td class='k3' >" + "Standby Time" + "</td>";
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
        ret += "<td class='" + RowClassAct + "'>&nbsp;" + this.ToString() + "</td>";
        ret += "<td class='" + RowClassAct + "'>&nbsp;" + this.GetState + "</td>";
        ret += "<td class='" + RowClassAct + "'>&nbsp;" + this.ActiveTime.ToString() + "</td>";
        ret += "<td class='" + RowClassAct + "'>&nbsp;" + this.FailTime.ToString() + "</td>";
        ret += "<td class='" + RowClassAct + "'>&nbsp;" + this.StandbyTime.ToString() + "</td>";
        ret += "</tr>";
        return ret;
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
        myStat.UpdateTimers(CAS.Lib.RTLib.Processes.Stopwatch.ConvertTo_s(activeTime.Read),
          CAS.Lib.RTLib.Processes.Stopwatch.ConvertTo_s(failTime.Read),
          CAS.Lib.RTLib.Processes.Stopwatch.ConvertTo_s(standbyTime.Read));
      }

      #endregion

    }
  }
}