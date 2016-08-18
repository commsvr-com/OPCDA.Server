//<summary>
//  Title   : Serial Settings wrapper to be used in property grid panel
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20081203: mzbrzezny: creating nodes when node is detached is fixed
//    20081105: mzbrzezny: GroupsRowWrapper: additional check in CreateNewChildObject
//    PM - 19-09-2006: created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.ComponentModel;
using System.Data;
using CAS.NetworkConfigLib;

namespace NetworkConfig.HMI
{
  /// <summary>
  /// Wrapper classes that wrap <see cref="ComunicationNet.GroupsRow"/>. It contains getters and setters to support property grid.
  /// </summary>
  /// <seealso cref="DataBlocksRowWrapper">DataBlocksRowWrapper is the child object in TreeView</seealso>
  /// <remarks>
  /// Attributes for property grid:
  /// DescriptionAttribute. Sets the text for the property that is displayed in the description help pane below the properties. This is a useful way to provide help text for the active property (the property that has focus). Apply this attribute to the MaxRepeatRate property. 
  /// CategoryAttribute. Sets the category that the property is under in the grid. This is useful when you want a property grouped by a category name. If a property does not have a category specified, then it will be assigned to the Misc category. Apply this attribute to all properties. 
  /// BrowsableAttribute – Indicates whether the property is shown in the grid. This is useful when you want to hide a property from the grid. By default, a public property is always shown in the grid. Apply this attribute to the SettingsChanged property. 
  /// ReadOnlyAttribute – Indicates whether the property is read-only. This is useful when you want to keep a property from being editable in the grid. By default, a public property with get and set accessor functions is editable in the grid. Apply this attribute to the AppVersion property. 
  /// DefaultValueAttribute – Identifies the property's default value. This is useful when you want to provide a default value for a property and later determine if the property's value is different than the default. Apply this attribute to all properties. 
  /// DefaultPropertyAttribute – Identifies the default property for the class. The default property for a class gets the focus first when the class is selected in the grid. Apply this attribute to the AppSettings class. 
  /// </remarks>
  [DefaultProperty( "Name" )]
  internal partial class GroupsRowWrapper: Action<ComunicationNet.GroupsRow>
  {
    #region private
    ProtocolAndSerialRowWrapper ProtocolWrapper;
    #endregion
    #region Constructor
    /// <summary>
    /// Creates new <see cref="GroupsRowWrapper"/> based on specified groups row
    /// </summary>
    /// <param name="parent">Groups row</param>
    /// <param name="_ProtocolWrapper">link to the <see cref="ProtocolAndSerialRowWrapper"/></param>
    public GroupsRowWrapper( ComunicationNet.GroupsRow parent, ProtocolAndSerialRowWrapper _ProtocolWrapper )
      : base( parent )
    {
      ProtocolWrapper = _ProtocolWrapper;
    }
    #endregion
    #region Properties for PropertyGrid
    /// <summary>
    /// Gets or sets human readable name for the group
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Global Settings" ),
    DefaultValueAttribute( "name for the group" ),
    DescriptionAttribute( "Human readable name for the group" )
    ]
    public string Name
    {
      get
      {
        return ( m_Parent.Name );
      }
      set
      {
        m_Parent.Name = value;
      }
    }
    /// <summary>
    /// Gets coupled station identifier
    /// </summary>
    [
#if DEBUG
BrowsableAttribute( true ),
CategoryAttribute( "XXX debug" ),
#else
    BrowsableAttribute( false ),
    CategoryAttribute( "General Settings" ),
#endif
 DefaultValueAttribute( 0 ),
    DescriptionAttribute( "Coupled station identifier (to make the configuration easier, NetworkConfig displays name of the coupled station instead of its numerical identifier)" )
    ]
    public long StationID
    {
      get
      {
        return ( m_Parent.StationID );
      }
    }
    /// <summary>
    /// Gets the unique group numerical identifier
    /// </summary>
    [
#if DEBUG
BrowsableAttribute( true ),
CategoryAttribute( "XXX debug" ),
#else
    BrowsableAttribute( false ),
    CategoryAttribute( "General Settings" ),
#endif
 DefaultValueAttribute( 0 ),
DescriptionAttribute( "Unique group numerical identifier" )
]
    public long GroupID
    {
      get
      {
        return ( m_Parent.GroupID );
      }
    }
    /// <summary>
    /// Gets or sets the frequency of scanning this group
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Time Settings" ),
    DefaultValueAttribute( ulong.MaxValue ),
    DescriptionAttribute( "Frequency of scanning this group (when NORMAL scanning is enabled) [msec]" )
    ]
    public ulong TimeScan
    {
      get
      {
        return ( m_Parent.TimeScan );
      }
      set
      {
        m_Parent.TimeScan = value;
      }
    }
    /// <summary>
    /// Gets the maximal time, which can elapse between two consecutive data refreshments without changing the quality
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Time Settings" ),
    DefaultValueAttribute( ulong.MaxValue ),
    DescriptionAttribute( "Maximal time, which can elapse between two consecutive data refreshments without changing the quality. After expiration thereof the server will change the quality of all tags belonging to the group to BAD. This parameter is in force if the scan mode is NORMAL. [msec]" )
    ]
    public ulong TimeOut
    {
      get
      {
        return ( m_Parent.TimeOut );
      }
      set
      {
        m_Parent.TimeOut = value;
      }
    }
    /// <summary>
    /// Gets or sets the frequency of scanning this group
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Time Settings" ),
    DefaultValueAttribute( ulong.MaxValue ),
    DescriptionAttribute( "Frequency of scanning this group (when FAST scanning is enabled) [msec]" )
    ]
    public ulong TimeScanFast
    {
      get
      {
        return ( m_Parent.TimeScanFast );
      }
      set
      {
        m_Parent.TimeScanFast = value;
      }
    }
    /// <summary>
    /// Gets or sets maximal time, which can elapse between two consecutive data refreshments without changing the quality.
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Time Settings" ),
    DefaultValueAttribute( ulong.MaxValue ),
    DescriptionAttribute( "Maximal time, which can elapse between two consecutive data refreshments without changing the quality. After expiration thereof the server will change the quality of all tags belonging to the group to BAD. This parameter is in force if the scan mode is FAST [msec]" )
    ]
    public ulong TimeOutFast
    {
      get
      {
        return ( m_Parent.TimeOutFast );
      }
      set
      {
        m_Parent.TimeOutFast = value;
      }
    }
    #endregion properties for PropertyGrid
    #region Overrides
    #region object override
    /// <summary>
    /// Override to string method
    /// </summary>
    /// <returns>Group id, group name, time scan and time out in specified string format</returns>
    public override string ToString()
    {
      return String.Format( "Group: {0}[Ts/To: {1}/{2}]", Name, TimeScan.ToString(), TimeOut.ToString() );
    }
    #endregion
    #region Action overrides
    /// <summary>
    /// Creates new data block row
    /// </summary>
    /// <returns>Returns <see cref="DataBlocksRowWrapper"/> object</returns>
    public override IAction CreateNewChildObject()
    {
      ComunicationNet.DataBlocksDataTable dataBlockTable = ( (ComunicationNet)( m_Parent.Table.DataSet ) ).DataBlocks;
      ulong DataType = 0;
      ulong Address = 0;
      if ( ProtocolWrapper != null && ProtocolWrapper.DataProvider != null )
      {
        DataType = (ulong)ProtocolWrapper.DataProvider.GetAvailiableAddressspaces()[ 0 ].Identifier;
        Address = (ulong)ProtocolWrapper.DataProvider.GetAvailiableAddressspaces()[ 0 ].StartAddress;
      }
      ComunicationNet.DataBlocksRow dr =
        dataBlockTable.NewDataBlocksRow( m_Parent.GroupID, m_Parent.Name, DataType, Address );
      return new DataBlocksRowWrapper( dr, ProtocolWrapper );
    }
    /// <summary>
    /// Creates data block rows in treeview
    /// </summary>
    public override void CreateNodes()
    {
      base.CreateNodes();
      if ( m_Parent.RowState == DataRowState.Detached )
        return;
      foreach ( ComunicationNet.DataBlocksRow curr in m_Parent.GetDataBlocksRows() )
        if ( curr.RowState != DataRowState.Deleted )
        {
          DataBlocksRowWrapper newWrapper = new DataBlocksRowWrapper( curr, ProtocolWrapper );
          newWrapper.AddActionTreeNode( m_Node, 3, 3 );
          newWrapper.CreateNodes();
        }
    }
    /// <summary>
    /// Pastes specified <see cref="DataBlocksRowWrapper"/> under this wrapper
    /// </summary>
    /// <param name="objToPaste"><see cref="DataBlocksRowWrapper"/> to be pasted</param>
    public override void PasteChildObject( IAction objToPaste )
    {
      if ( ( objToPaste is DataBlocksRowWrapper ) )
      {
        DataBlocksRowWrapper wrapperToPaste = objToPaste as DataBlocksRowWrapper;
        ComunicationNet.DataBlocksDataTable dt = ( (ComunicationNet)( m_Parent.Table.DataSet ) ).DataBlocks;
        dt.NewDataBlocksRow( m_Parent.GroupID, wrapperToPaste.DataRow, false, m_Parent.Name );
      }
    }
    /// <summary>
    /// Checks if specified <see cref="IAction"/> interface is <see cref="DataBlocksRowWrapper"></see>
    /// </summary>
    /// <param name="objToPaste">Object to check</param>
    /// <returns>True if specified object is <see cref="DataBlocksRowWrapper"/></returns>
    public override bool CanBePastedAsChild( IAction objToPaste )
    {
      Action<ComunicationNet.DataBlocksRow> cRTP = objToPaste as Action<ComunicationNet.DataBlocksRow>;
      return cRTP != null;
    }
    /// <summary>
    /// Checks if specified <see cref="IAction"/> object can be moved 
    /// </summary>
    /// <returns>True if specified object can be moved </returns>
    public override bool CanBeMoved() { return true; }
    /// <summary>
    /// Moves specified <see cref="DataBlocksRowWrapper"/> object under this wrapper
    /// </summary>
    /// <param name="objToPaste"><see cref="DataBlocksRowWrapper"/> object to be pasted</param>
    public override void MoveChildObject( IAction objToPaste )
    {
      DataBlocksRowWrapper wrapperToPaste = objToPaste as DataBlocksRowWrapper;
      if ( wrapperToPaste == null )
        return;
      ComunicationNet.DataBlocksRow rowToPaste = wrapperToPaste.DataRow;
      rowToPaste.BeginEdit();
      rowToPaste.GroupID = m_Parent.GroupID;
      rowToPaste.EndEdit();
    }
    #endregion
    #endregion
  }
}
