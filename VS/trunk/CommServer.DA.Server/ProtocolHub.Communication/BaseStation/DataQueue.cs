//_______________________________________________________________
//  Title   : OPC server handling class
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

using CAS.Lib.CommonBus.ApplicationLayer;
using CAS.Lib.RTLib;
using CAS.Lib.RTLib.Processes;
using CAS.NetworkConfigLib;
using System;
using System.Collections;
using NetConfig = CAS.NetworkConfigLib.ComunicationNet;

namespace CAS.CommServer.ProtocolHub.Communication.BaseStation
{
  /// <summary>
  /// OPC tags data queue handling class.
  /// </summary>
  internal sealed class DataQueue : HandlerWaitTimeList<DataBlock>
  {
    #region PRIVATE
    private static DataQueue myDataQueue;
    protected override void Handler(DataBlock currDatBlock) { ((DataDescription)currDatBlock).InvalidateTag(); }
    #endregion PRIVATE

    #region PUBLIC
    internal abstract class DataDescription : DataBlock
    {
      #region PRIVATE
      protected class TagDataDescription : DataBlock.Tag
      {
        #region PRIVATE
        /// <summary>
        ///  Title   : class that describe Bit Tags
        /// </summary>
        private class TagBit : CAS.Lib.DeviceSimulator.Device.TagInDevice
        {
          #region PRIVATE
          private int BitNumber;
          #endregion PRIVATE
          #region PUBLIC
          //          public override void UpdateTag(object val)
          //          {
          //            bool res = base.UpdateTag(val);
          //            return res;
          //          }
          protected override bool UpdateRemote(object data)
          {
            return false;
          }
          protected override bool ReadRemote(out object data)
          {
            data = null;
            return false;
          }
          internal int getBitNumber
          {
            get { return this.BitNumber; }
          }
          internal TagBit(string myName, int myBitNumber)
            :
            base(myName, null, Opc.Da.qualityBits.bad, ItemAccessRights.ReadOnly, typeof(bool))
          {
            this.BitNumber = myBitNumber;
          }
          #endregion PUBLIC
        }// TagBit
        /// <summary>
        ///  Title   : Class that allow to write to station
        /// </summary>
        private class DataWriteDescription : IBlockDescription
        {
          #region PRIVATE
          private int myAddress;
          private short myDataType;
          private IDataWrite writeToStation;
          #endregion PRIVATE
          #region IBlockDescription
          int IBlockDescription.length { get { return 1; } }
          int IBlockDescription.startAddress { get { return myAddress; } }
          short IBlockDescription.dataType { get { return myDataType; } }
          #endregion
          #region PUBLIC
          internal bool WriteData(object data)
          {
            return writeToStation.WriteData(data, this);
          }
          internal bool ReadData(out object data)
          {
            return writeToStation.ReadData(out data, this);
          }
          internal DataWriteDescription
            (int myAddress, short myDataType, IDataWrite writeToStation)
          {
            this.myAddress = myAddress;
            this.myDataType = myDataType;
            this.writeToStation = writeToStation;
            //this.myStation=0; //MZTD: ew. do zaimplementowania
          }
          #endregion
        }
        private DataWriteDescription myDWD = null;
        private bool writableTag = false;
        //MZ: Bit Tags:
        private System.Collections.ArrayList TagBitList;
        #endregion PRIVATE
        #region PUBLIC
        public override void UpdateTag(object val)
        {
          base.UpdateTag(val);
          //MZ: UpdateAllBitTags
          if (this.TagBitList != null)
          {
            int val_int = (int)val;
            foreach (TagBit t in this.TagBitList)
            {
              int mask = 1;
              mask = mask << (int)t.getBitNumber;
              //MP wywalilem if i przerywam po bledzie
              t.UpdateTag((mask & val_int) > 0);
              //              if ( ! res ) return false;
            }
          }
          //          return res;
        } //UpdateTag
        protected override bool UpdateRemote(object data)
        {
          if (myDWD.WriteData(data))
          {
            UpdateTag(data);
            return true;
          }
          else
          {
            MarkTagQuality(Opc.Da.qualityBits.bad);
            return false;
          }
        }
        protected override bool ReadRemote(out object data)
        {
          //MPTD brak konwersji na postac kanoniczna
          if (myDWD.ReadData(out data))
          {
            UpdateTag(data);
            return true;
          }
          else
          {
            MarkTagQuality(Opc.Da.qualityBits.bad);
            return false;
          }
        }
        /// <summary>
        /// Tag constructor
        /// </summary>
        /// <param name="myDSC">params from Tags table</param>
        /// <param name="myStation">pointer to interface that allow to change priority of the station</param>
        /// <param name="myAddress">address</param>
        /// <param name="myDataType">Data type</param>
        /// <param name="writeToStation">pointer to interface that allow to write to station</param>
        /// <param name="property_colllection">collection of properties for this tag see: <see cref="Opc.Da.ItemPropertyCollection"/></param>
        internal TagDataDescription
          (
          NetConfig.TagsRow myDSC, IStationState myStation, int myAddress,
          short myDataType, IDataWrite writeToStation, Opc.Da.ItemPropertyCollection property_colllection
          )
          : base(myDSC, myStation)
        {
          if (property_colllection != null)
            this.AddProperties(property_colllection);
          writableTag = (myDSC.AccessRights == (sbyte)ItemAccessRights.ReadWrite || myDSC.AccessRights == (sbyte)ItemAccessRights.WriteOnly);
          myDWD = new DataWriteDescription(myAddress, myDataType, writeToStation);
          ComunicationNet.TagBitRow[] tagbitsDsc = myDSC.GetTagBitRows();
          if (tagbitsDsc.Length != 0)
          {
            this.TagBitList = new ArrayList();
            foreach (ComunicationNet.TagBitRow curr in tagbitsDsc)
            {
              TagBit newTagBit = new TagBit(myDSC.Name + "_" + curr.Name, (int)curr.BitNumber);
              this.TagBitList.Add(newTagBit);
            }
          }
          else
            this.TagBitList = null;
        }
        #endregion
      }// Tag
      internal override void UpdateAllTags(IReadValue val)
      {
        base.UpdateAllTags(val);
        ResetCounter();
      }
      protected abstract TimeSpan TimeOut { get; }
      internal void ChangeTimeout()
      {
        Cycle = TimeOut;
        ResetCounter();
        NotifyNewTimeScan?.Invoke(this, EventArgs.Empty);
      }
      protected override DataBlock.Tag newTag(ComunicationNet.TagsRow currRow, IStationState myStationState, int myAddress, short myDataType, IDataWrite myWriteInt)
      {
#if COMMSERVER
        Opc.Da.ItemPropertyCollection prop_coll = global::BaseStation.ItemDescriber.ItemDescriber2OpcDA.GetItemPropertiesCollection(currRow.Name, Initialization.m_ds_dsc);
#else
        Opc.Da.ItemPropertyCollection prop_coll=null;
#endif
        return new TagDataDescription(currRow, myStationState, myAddress, myDataType, myWriteInt, prop_coll);
      }
      #endregion PRIVATE

      #region PUBLIC
      internal event EventHandler NotifyNewTimeScan;
      internal abstract TimeSpan TimeScann { get; }
      internal void InvalidateTag()
      {
        //MPTD brak invalidacji tagow bitowych - trzeba to zrobiæ
        DataBlock.Tag[] tagHendlers = createdTags;
        System.Diagnostics.Debug.Assert(createdTags != null, "DataQueue.InvalidateTag: createdTags!= null");
        System.Diagnostics.Debug.Assert(tagHendlers != null, "DataQueue.InvalidateTag: tagHendlers != null");
        for (ushort idx = 0; (idx < tagHendlers.Length); idx++)
          tagHendlers[idx].MarkTagQuality(Opc.Da.qualityBits.bad);
      }
      internal DataDescription
        (
        ComunicationNet.DataBlocksRow myRow, TimeSpan timeOut, IDataWrite myWriteInt,
        IStationState myStationState, ref int cVConstrain
        )
        : base(myRow, myWriteInt, myStationState, myDataQueue, timeOut, ref cVConstrain)
      { }
      #endregion PUBLIC

    }// class DataDescription
    #endregion
    #region INIT
    internal static void finalize()
    {
      //MPTD wywo³ac ShutdownRequest z OpcCom.Server
    }
    private DataQueue()
      : base(false, "DataQueueHandler")
    { }//DataQueue
    ~DataQueue()
    { }
    static DataQueue()
    {
      myDataQueue = new DataQueue();
    }
    #endregion
  }//class DataQueue
}//namespace BaseStation