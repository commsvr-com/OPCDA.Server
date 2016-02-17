//<summary>
//  Title   : Configuration For Commserver 
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//  20081006 mzbrzezny: implementation of ItemAccessRights and StateTrigger
//    20080905: mzbrzezny: NewInterfacesRow function do not assign port interface number 
//    2007 mpostol - created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using Opc.Da;
using CAS.Lib.RTLib;

namespace CAS.NetworkConfigLib
{
  /// <summary>
  /// DataSet representing CommServer network configuration data
  /// </summary>
  partial class ComunicationNet
  {
    #region private
    private static char m_IdentSep = '/';
    #endregion
    ///<summary>
    /// Custom helpers for <see cref="SegmentsDataTable"/> DataTable
    ///</summary>
    partial class SegmentsDataTable
    {
      /// <summary>
      /// Creates new <see cref="SegmentsRow"/> row and assigns default values
      /// </summary>
      /// <param name="pPrefix">prefix for new segments names that are created</param>
      /// <param name="pProtocolID">Foreign key for <see cref="ProtocolDataTable"/></param>
      /// <returns>New <see cref="SegmentsRow"/></returns>
      public SegmentsRow NewSegmentsRow( long pProtocolID, string pPrefix )
      {
        SegmentsRow sg = this.NewSegmentsRow();
        sg.Name = String.Format( "{1}{2}Segment{0}", sg.SegmentID.ToString(), pPrefix, m_IdentSep );
        sg.ProtocolID = pProtocolID;
        return sg;
      }
      /// <summary>
      /// Creates new <see cref="SegmentsRow"/> row and assigns default values
      /// </summary>
      /// <param name="pPrefix">prefix for new segments names that are created</param>
      /// <param name="pProtocolID">Foreign key for <see cref="ProtocolDataTable"/></param>
      /// <param name="pRowToBeCopied">New row is a shallow copy of the current rowToPaste</param>
      /// <returns>New <see cref="SegmentsRow"/>A shallow copy of the current rowToPaste</returns>
      public void NewSegmentsRow( long pProtocolID, SegmentsRow pRowToBeCopied, string pPrefix )
      {
        SegmentsRow sg = this.NewSegmentsRow( pProtocolID, pPrefix );
        sg.Address = pRowToBeCopied.Address;
        sg.TimeScan = pRowToBeCopied.TimeScan;
        sg.KeepConnect = pRowToBeCopied.KeepConnect;
        sg.PickupConn = pRowToBeCopied.PickupConn;
        sg.timeKeepConn = pRowToBeCopied.timeKeepConn;
        sg.TimeReconnect = pRowToBeCopied.TimeReconnect;
        sg.TimeIdleKeepConn = pRowToBeCopied.TimeIdleKeepConn;
        this.AddSegmentsRow( sg );
        return;
      }
    }
    ///<summary>
    /// Custom helpers for <see cref="TagBitDataTable"/> DataTable
    ///</summary>
    partial class TagBitDataTable
    {
      /// <summary>
      /// Creates new tag bit row and assigns default values
      /// </summary>
      /// <param name="cParent">Parent Tag <see cref="TagsRow"/></param>
      /// <param name="pPrefix">prefix for new tagbit names that are created</param>
      /// <returns>New <see cref="TagBitRow"/></returns>
      public TagBitRow NewTagBitRow( TagsRow cParent, string pPrefix )
      {
        TagBitRow dr = this.NewTagBitRow();
        dr.TagID = cParent.TagID;
        short idx = (short)cParent.GetTagBitRows().Length;
        dr.Name = String.Format( "{0}{1}NewTagBit{2}", pPrefix, m_IdentSep, idx );
        dr.BitNumber = idx;
        return dr;
      }
      /// <summary>
      /// Creates new tag bit row and assigns default values
      /// </summary>
      /// <param name="cParent">Parent Tag <see cref="TagsRow"/></param>
      /// <param name="pRowToBeCopied">New row is a shallow copy of the current rowToPaste</param>
      /// <returns>New <see cref="TagBitRow"/></returns>
      public void NewTagBitRow( TagsRow cParent, TagBitRow pRowToBeCopied )
      {
        TagBitRow dr = NewTagBitRow();
        dr.Name = pRowToBeCopied.Name;
        dr.BitNumber = pRowToBeCopied.BitNumber;
        dr.TagID = cParent.TagID;
        this.AddTagBitRow( dr );
        return;
      }
      /// <summary>
      /// this function is chcecking if the tag already exists
      /// </summary>
      /// <param name="pTagID">tag ID to be found</param>
      /// <param name="pName">tag name to be found</param>
      /// <returns></returns>
      public bool Contain( long pTagID, string pName )
      {
        object[] indx = new object[] { pTagID, pName };
        return this.Rows.Find( indx ) != null;
      }
    }
    /// <summary>
    /// Data Table of Groups
    /// </summary>
    partial class GroupsDataTable
    {
      /// <summary>
      /// Creates new group row and assigns default values: GroupID, StationID, Name
      /// </summary>
      /// <param name="pPrefix">prefix for new group names that are created</param>
      /// <param name="stationID">Station id</param>
      /// <returns>New <see cref="GroupsRow"/></returns>
      public GroupsRow NewGroupsRow( long stationID, string pPrefix )
      {
        GroupsRow gr = this.NewGroupsRow();
        gr.StationID = stationID;
        gr.Name = String.Format( "{1}{2}Group{0}", gr.GroupID, pPrefix, m_IdentSep );
        return gr;
      }
      /// <summary>
      /// Creates new group row and assigns default values: GroupID, StationID, Name
      /// </summary>
      /// <param name="stationID"> id of station that this grop belongs to</param>
      /// <param name="pRowToBeCopied">copy of the group to be copied</param>
      /// <param name="pShallowCopy">indicate if the copy is deep or shalow</param>
      /// <param name="pPrefix">prefix for the station</param>
      public void NewGroupsRow( long stationID, GroupsRow pRowToBeCopied, bool pShallowCopy, string pPrefix )
      {
        GroupsRow dr = NewGroupsRow( stationID, pPrefix );
        dr.TimeScan = pRowToBeCopied.TimeScan;
        dr.TimeOut = pRowToBeCopied.TimeOut;
        dr.TimeScanFast = pRowToBeCopied.TimeScanFast;
        dr.TimeOutFast = pRowToBeCopied.TimeOutFast;
        this.AddGroupsRow( dr );
        if ( !pShallowCopy )
        {
          foreach ( DataBlocksRow br in pRowToBeCopied.GetDataBlocksRows() )
            ( (ComunicationNet)this.DataSet ).DataBlocks.NewDataBlocksRow( dr.GroupID, br, false, dr.Name );
        }
        return;
      }
      /// <summary>
      /// Gets the next group ID.
      /// </summary>
      /// <returns>group identifier</returns>
      public long GetNextGroupID()
      {
        long grid = 0;
        foreach ( GroupsRow grow in this )
          if ( grow.GroupID >= grid )
            grid = grow.GroupID + 1;
        return grid;
      }
    }
    /// <summary>
    /// Data Table od Stations
    /// </summary>
    partial class StationDataTable
    {
      /// <summary>
      /// Creates new station row and assigns default StationID and Name
      /// </summary>
      /// <param name="pPrefix">The prefix.</param>
      /// <returns>New <see cref="StationRow"/></returns>
      public StationRow NewStationRow( string pPrefix )
      {
        //ulong id = 0;
        //foreach ( StationRow st in this )
        //  id = Math.Max( id, st.StationID );
        //id++;
        StationRow cr = this.NewStationRow();
        //cr.StationID = id;
        cr.Name = String.Format( "{0}{2}Station{1}", pPrefix, cr.StationID.ToString(), m_IdentSep );
        return cr;
      }
      /// <summary>
      /// Creates new station row and assigns default StationID and Name
      /// </summary>
      /// <param name="pRowToBeCopied">row to be copied</param>
      /// <param name="pShallowCopy">indicate if the copy is shallow or deep</param>
      /// <param name="pPrefix">prefix for the name</param>
      public void NewStationRow( StationRow pRowToBeCopied, bool pShallowCopy, string pPrefix )
      {
        StationRow rw = NewStationRow( pPrefix );
        this.AddStationRow( rw );
        if ( !pShallowCopy )
          foreach ( GroupsRow gr in pRowToBeCopied.GetGroupsRows() )
            ( (ComunicationNet)this.DataSet ).Groups.NewGroupsRow( rw.StationID, gr, false, rw.Name );
        return;
      }
    }
    /// <summary>
    /// Data Table of Bloclks
    /// </summary>
    partial class DataBlocksDataTable
    {
      /// <summary>
      /// Creates new data block row and assigns default values
      /// </summary>
      /// <param name="groupID">Group id</param>
      /// <param name="pPrefix">prefix for the station name</param>
      /// <param name="DataType">Type of the data.</param>
      /// <param name="Address">The address.</param>
      /// <returns>New <see cref="DataBlocksRow"/></returns>
      public DataBlocksRow NewDataBlocksRow( long groupID, string pPrefix, ulong DataType, ulong Address )
      {
        DataBlocksRow dr = this.NewDataBlocksRow();
        dr.Name = String.Format( "{1}{2}Block{0}", dr.DatBlockID.ToString(), pPrefix, m_IdentSep );
        dr.GroupID = groupID;
        dr.DataType = DataType;
        dr.Address = Address;
        return dr;
      }
      /// <summary>
      /// Creates new data block row and assigns default values
      /// </summary>
      /// <param name="groupID">Group ID</param>
      /// <param name="pRowToBeCopied">row that is copied</param>
      /// <param name="pShallowCopy">indicate if the copy is shallow or deep</param>
      /// <param name="pPrefix">prefix for the name</param>
      public void NewDataBlocksRow
        ( long groupID, DataBlocksRow pRowToBeCopied, bool pShallowCopy, string pPrefix )
      {
        DataBlocksRow dr = NewDataBlocksRow( groupID, pPrefix,
          pRowToBeCopied.DataType, pRowToBeCopied.Address );
        this.AddDataBlocksRow( dr );
        if ( !pShallowCopy )
          foreach ( TagsRow tr in pRowToBeCopied.GetTagsRows() )
            ( (ComunicationNet)this.DataSet ).Tags.NewTagsRow( dr.DatBlockID, tr, false, dr.Name );
        return;
      }
    }
    /// <summary>
    /// Data Table of Tags
    /// </summary>
    partial class TagsDataTable
    {
      /// <summary>
      /// Creates new tags row and assigns default values
      /// </summary>
      /// <param name="pDatBlockID">Data block ID</param>
      /// <param name="pPrefix">Prefix for the name</param>
      /// <returns>New <see cref="TagsRow"/></returns>
      public TagsRow NewTagsRow( int pDatBlockID, string pPrefix )
      {
        TagsRow tr = this.NewTagsRow();
        tr.Name = String.Format( "{1}{2}Tag{0}", tr.TagID.ToString(), pPrefix, m_IdentSep );
        tr.DatBlockID = pDatBlockID;
        tr.AccessRights = (sbyte)ItemAccessRights.ReadWrite;
        tr.StateTrigger = (sbyte)StateTrigger.None;
        tr.StateMask = 0;
        return tr;
      }
      /// <summary>
      /// Creates new tags row and assigns default values
      /// </summary>
      /// <param name="pDatBlockID">Data block ID</param>
      /// <param name="pPrefix">Prefix for the name</param>
      /// <param name="pRowToBeCopied">row to be copied</param>
      /// <param name="pShallowCopy">indicates if the copy is shallow or deep</param>
      public void NewTagsRow
        ( int pDatBlockID, TagsRow pRowToBeCopied, bool pShallowCopy, string pPrefix )
      {
        TagsRow dr = NewTagsRow( pDatBlockID, pPrefix );
        dr.AccessRights = pRowToBeCopied.AccessRights;
        dr.StateTrigger = pRowToBeCopied.StateTrigger;
        dr.Alarm = pRowToBeCopied.Alarm;
        dr.AlarmMask = pRowToBeCopied.AlarmMask;
        dr.StateMask = pRowToBeCopied.StateMask;

        this.AddTagsRow( dr );
        //kopiowanie Properties:
        foreach ( ItemPropertiesTableRow iptr in pRowToBeCopied.GetItemPropertiesTableRows() )
          ( (ComunicationNet)this.DataSet ).ItemPropertiesTable.NewItemPropertiesTableRow( iptr, dr.TagID );

        //kopiowanie TagBits:
        if ( !pShallowCopy )
          foreach ( TagBitRow tr in pRowToBeCopied.GetTagBitRows() )
            ( (ComunicationNet)this.DataSet ).TagBit.NewTagBitRow( dr, string.Empty );
        return;
      }
    }
    /// <summary>
    /// Row of Tags table
    /// </summary>
    public partial class TagsRow
    {
      /// <summary>
      /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
      /// </summary>
      /// <returns>
      /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
      /// </returns>
      public override string ToString()
      {
        return this.Name;
      }
      #region item properties
      private ItemPropertiesTableRow myDataTypeConversionPropertiesRow;
      private ItemPropertiesTableRow GetDataTypeItemPropertiesTableRow()
      {
        // znajdowanie odpowiedniego wiersza w tabeli properties
        if ( this.GetItemPropertiesTableRows().Length == 0 )
          return null;
        foreach ( ItemPropertiesTableRow iptr in this.GetItemPropertiesTableRows() )
        {
          if ( iptr.ID_Code == Property.DATATYPE.Code )
          {
            return iptr;
          }
        }
        return null;
      }
      /// <summary>
      /// Determines whether data type conversion is null.
      /// </summary>
      /// <returns>
      /// 	<c>true</c> if data type conversion is null; otherwise, <c>false</c>.
      /// </returns>
      public bool IsDataTypeConversionNull()
      {

        ItemPropertiesTableRow iptr = GetDataTypeItemPropertiesTableRow();
        if ( iptr != null )
        {
          if ( iptr.IsValueNull() )
            return true;
          if ( iptr.Value == null )
            return true;
          if ( iptr.Value == "" )
            return true;
          myDataTypeConversionPropertiesRow = iptr;
          return false;
        }
        return true;
      }
      /// <summary>
      /// Gets or sets the data type conversion.
      /// </summary>
      /// <value>The data type conversion.</value>
      public string DataTypeConversion
      {
        get
        {
          if ( IsDataTypeConversionNull() )
            return "N/A";
          return myDataTypeConversionPropertiesRow.Value;
        }
        set
        {
          if ( IsDataTypeConversionNull() )
          {
            ItemPropertiesTableRow newiptr = GetDataTypeItemPropertiesTableRow();
            if ( newiptr == null )
              newiptr = ( (ComunicationNet)this.Table.DataSet ).ItemPropertiesTable.NewItemPropertiesTableRow();
            newiptr.TagID = this.TagID;
            newiptr.ID_Name_Name = Property.DATATYPE.Name.Name;
            newiptr.ID_Name_Namespace = Property.DATATYPE.Name.Namespace;
            newiptr.ID_Code = Property.DATATYPE.Code;
            newiptr.Value = value;
            if ( newiptr.RowState == System.Data.DataRowState.Detached )
              ( (ComunicationNet)this.Table.DataSet ).ItemPropertiesTable.AddItemPropertiesTableRow( newiptr );
          }
          else
          {
            myDataTypeConversionPropertiesRow.Value = value;
          }
        }
      }
      #endregion item properties
    }
    /// <summary>
    /// Data  Table of Protocols
    /// </summary>
    partial class ProtocolDataTable
    {
      /// <summary>
      /// Creates new protocol row in protocol data table
      /// </summary>
      /// <param name="channelID">Chanel identfier</param>
      /// <param name="pPrefix">prefix for the name</param>
      /// <returns>New <see cref="ProtocolRow"/></returns>
      /// <remarks>
      /// Default values:
      /// - ProtocolType = 0;
      /// - Name = "Protocol ID"
      /// </remarks>
      public ProtocolRow NewProtocolRow( long channelID, string pPrefix )
      {
        ProtocolRow protocolRow = this.NewProtocolRow();
        protocolRow.ChannelID = channelID;
        protocolRow.Name = String.Format( "{1}{2}Protocol{0}", protocolRow.ProtocolID.ToString(), pPrefix, m_IdentSep );
        return protocolRow;
      }
      /// <summary>
      /// Creates new protocol row in protocol data table
      /// </summary>
      /// <param name="channelID">Channel ID</param>
      /// <param name="pRowToBeCopied">ow to be copied</param>
      /// <param name="pShallowCopy">indicates if the copy is shallow or deep</param>
      /// <param name="pPrefix">prefix for the name</param>
      public void NewProtocolRow( long channelID, ProtocolRow pRowToBeCopied, bool pShallowCopy, string pPrefix )
      {
        ProtocolRow pr = this.NewProtocolRow( channelID, pPrefix );
        pr.ChannelID = channelID;
        if ( !pRowToBeCopied.IsDPIdentifierNull() )
          pr.DPIdentifier = pRowToBeCopied.DPIdentifier;
        if ( !pRowToBeCopied.IsDPConfigNull() )
          pr.DPConfig = pRowToBeCopied.DPConfig;
        this.AddProtocolRow( pr );
        if ( !pShallowCopy )
          foreach ( SegmentsRow sr in pRowToBeCopied.GetSegmentsRows() )
            ( (ComunicationNet)this.DataSet ).Segments.NewSegmentsRow( pr.ProtocolID, sr, pr.Name );
        return;
      }
    }
    ///<summary>
    /// Data Table of Interfaces
    ///</summary>
    partial class InterfacesDataTable
    {
      /// <summary>
      /// Creates new interface row and assigns default values
      /// </summary>
      /// <param name="segmentID">Segment id</param>
      /// <param name="pPrefix">prefix of the name</param>
      /// <returns>New <see cref="InterfacesRow"/></returns>
      public InterfacesRow NewInterfacesRow( long segmentID, string pPrefix )
      {
        InterfacesRow dr = this.NewInterfacesRow();
        dr.InterfaceNum = 0;
        dr.SegmentId = segmentID;
        dr.Name = String.Format( "{0}{1}Port", pPrefix, m_IdentSep );
        return dr;
      }
    }
    /// <summary>
    /// Channels Data TAble
    /// </summary>
    partial class ChannelsDataTable
    {
      /// <summary>
      /// creates new channel row
      /// </summary>
      /// <param name="pPrefix">prefix for the name</param>
      /// <returns>new channel row</returns>
      public ChannelsRow NewChannelsRow( String pPrefix )
      {
        ComunicationNet.ChannelsRow cr = this.NewChannelsRow();
        cr.Name = String.Format( "{0}{2}Channel{1}", pPrefix, cr.ChannelID.ToString(), m_IdentSep );
        return cr;
      }
      /// <summary>
      /// creates new channel row
      /// </summary>
      /// <param name="pRowToBeCopied">row to be copied</param>
      /// <param name="pShallowCopy">indicate if the copy is shallow or deep</param>
      /// <param name="pPrefix">prefix for the name</param>
      public void NewChannelsRow( ChannelsRow pRowToBeCopied, bool pShallowCopy, String pPrefix )
      {
        ComunicationNet.ChannelsRow cr = this.NewChannelsRow( pPrefix );
        this.AddChannelsRow( cr );
        if ( !pShallowCopy )
          foreach ( ProtocolRow pr in pRowToBeCopied.GetProtocolRows() )
            ( (ComunicationNet)this.DataSet ).Protocol.NewProtocolRow( cr.ChannelID, pr, false, pPrefix );
        return;
      }
    }
    partial class ItemPropertiesTableDataTable
    {
      internal void NewItemPropertiesTableRow( ItemPropertiesTableRow iptr, long ParentTagID )
      {
        ItemPropertiesTableRow newiptr = this.NewItemPropertiesTableRow();
        newiptr.ID_Code = iptr.ID_Code;
        newiptr.ID_Name_Name = iptr.ID_Name_Name;
        newiptr.ID_Name_Namespace = iptr.ID_Name_Namespace;
        newiptr.TagID = ParentTagID;
        newiptr.Value = iptr.Value;
        this.AddItemPropertiesTableRow( newiptr );
      }
    }
  }
}
