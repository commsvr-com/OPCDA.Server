//<summary>
//  Title   : Serial Settings wrapper to be used in property grid panel
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    PM - 19-09-2006: created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using CAS.NetworkConfigLib;
using NetworkConfig.HMI.Exceptions;
using CAS.Lib.CommonBus;
using System.Data;
namespace NetworkConfig.HMI
{
  /// <summary>
  /// Wrapper classes that wrap <see cref="ComunicationNet.SegmentsRow"/>. It contains getters and setters to support property grid.
  /// </summary>
  /// <seealso cref="InterfacesRowWrapper">InterfacesRowWrapper is the child object in TreeView</seealso>
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
  internal partial class SegmentsRowWrapper: Action<ComunicationNet.SegmentsRow>
  {
    #region private
    ProtocolAndSerialRowWrapper ProtocolWrapper;
    #endregion
    #region creator
    /// <summary>
    /// Creates new <see cref="SegmentsRowWrapper"/> object based on specified Segment row
    /// </summary>
    /// <param name="parent">Segment row</param>
    /// <param name="_ProtocolWrapper">link to the <see cref="ProtocolAndSerialRowWrapper"/></param>
    public SegmentsRowWrapper(ComunicationNet.SegmentsRow parent, ProtocolAndSerialRowWrapper _ProtocolWrapper)
      : base( parent )
    {
      ProtocolWrapper = _ProtocolWrapper;
    }
    #endregion
    #region Properties for PropertyGrid
    /// <summary>
    /// Gets or sets human readable name of the segment
    /// </summary>
    [
      BrowsableAttribute( true ),
      CategoryAttribute( "Global Settings" ),
      DefaultValueAttribute( "Name of the segment" ),
      DescriptionAttribute( "Human readable name of the segment" )
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
    /// Gets or sets segment unique numerical identifier
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
      DescriptionAttribute( "Segment unique numerical identifier" )
    ]
    public long SegmentID
    {
      get
      {
        return ( m_Parent.SegmentID );
      }
    }
    /// <summary>
    /// Coupled channel identifier (channel name)
    /// </summary>
    [
#if DEBUG
BrowsableAttribute( true ),
CategoryAttribute( "XXX debug" ),
#else
     BrowsableAttribute( false ),
     CategoryAttribute( "General Settings" ),
#endif
 DescriptionAttribute( "Coupled channel identifier (channel name)" )
]
    public string ChannelID
    {
      get
      {
        return m_Parent.ProtocolRow.ChannelsRow.Name;
      }
    }
    /// <summary>
    /// Gets coupled protocol identifier
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
DescriptionAttribute( "Coupled protocol identifier" )
]
    public string ProtocolID
    {
      get
      {
        return ( m_Parent.ProtocolRow.Name );
      }
    }
    /// <summary>
    /// Gets or sets adress
    /// </summary>
    [
      BrowsableAttribute( true ),
      CategoryAttribute( "Global Settings" ),
      DefaultValueAttribute( null ),
      DescriptionAttribute( "IP, phone number, ... etc" )
    ]
    public string Address
    {
      get
      {
        return ( m_Parent.Address );
      }
      set
      {
        m_Parent.Address = value;
      }
    }
    /// <summary>
    /// Gets or sets Period of time between connections [msec]
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Time Settings" ),
    DefaultValueAttribute( long.MaxValue ),
    DescriptionAttribute( "Period of time between connections [msec]" ),
    DisplayNameAttribute( "Sampling time" )
    ]
    public long TimeScan
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
    /// Gets or sets KeepConnect
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Time Settings" ),
    DefaultValueAttribute( true ),
    DescriptionAttribute(
        "This parameter defines if the connection should be kept in spite of there is no data to transmit. The connection is kept as long as another segment becomes ready to be connected (important in case when keeping up the connection costs nothing and other segments are inactive"
        )
    ]
    public bool KeepConnect
    {
      get
      {
        return ( m_Parent.KeepConnect );
      }
      set
      {
        m_Parent.KeepConnect = value;
      }
    }
    /// <summary>
    /// Gets or sets the value that remote station is able to establish connection - for example in case of alarm
    /// </summary>
    [
      BrowsableAttribute( true ),
      CategoryAttribute( "Time Settings" ),
      DefaultValueAttribute( false ),
      DescriptionAttribute( "If true, remote station is able to establish connection - for example in case of alarm" )
    ]
    public bool PickupConn
    {
      get
      {
        return ( m_Parent.PickupConn );
      }
      set
      {
        m_Parent.PickupConn = value;
      }
    }
    /// <summary>
    /// Gest or Sets timeKeepConn
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Time Settings" ),
    DefaultValueAttribute( 10000 ),
    DescriptionAttribute( "How long the segment should be in the data transmission mode. [msec]" ),
    DisplayNameAttribute( "Keep connection time" )
    ]
    public long timeKeepConn
    {
      get
      {
        return ( m_Parent.timeKeepConn );
      }
      set
      {
        m_Parent.timeKeepConn = value;
      }
    }
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Time Settings" ),
    DefaultValueAttribute( 60000 ),
    DescriptionAttribute(
        "If the connection cannot be established this setting defines the time when the next retry takes place. [msec]" )
    ]
    public long TimeReconnect
    {
      get
      {
        return ( m_Parent.TimeReconnect );
      }
      set
      {
        m_Parent.TimeReconnect = value;
      }
    }
    /// <summary>
    /// Gest or sets TimeIdleKeepConn
    /// </summary>
    [
      BrowsableAttribute( true ),
      CategoryAttribute( "Time Settings" ),
      DefaultValueAttribute( long.MaxValue ),
      DescriptionAttribute(
        "This parameter defines how long the segment should be in data transfer mode, when there is no data to transfer. [msec]"
        )
    ]
    public long TimeIdleKeepConn
    {
      get
      {
        return ( m_Parent.TimeIdleKeepConn );
      }
      set
      {
        m_Parent.TimeIdleKeepConn = value;
      }
    }
    #endregion //properties for PropertyGrid
    #region Override
    #region Object override
    /// <summary>
    /// Overrides to string method
    /// </summary>
    /// <returns>Returns segment identifer and segment name in specified string format</returns>
    public override string ToString() { return "Segment: " + Name; }
    #endregion
    #region Action overrides
    /// <summary>
    /// Creates new interface row
    /// </summary>
    /// <returns><see cref="InterfacesRowWrapper"/> object</returns>
    public override IAction CreateNewChildObject()
    {
      ComunicationNet.InterfacesDataTable dt = ( (ComunicationNet)( m_Parent.Table.DataSet ) ).Interfaces;
      ComunicationNet.InterfacesRow dr = dt.NewInterfacesRow( m_Parent.SegmentID, m_Parent.Name );
      return new InterfacesRowWrapper( dr,this.ProtocolWrapper );
    }
    /// <summary>
    /// Creates interfaces rows in treeview
    /// </summary>
    public override void CreateNodes()
    {
      base.CreateNodes();
      foreach ( ComunicationNet.InterfacesRow curr in m_Parent.GetInterfacesRows() )
        if ( curr.RowState != DataRowState.Deleted )
        {
          InterfacesRowWrapper newWrapper = new InterfacesRowWrapper( curr,ProtocolWrapper );
          newWrapper.AddActionTreeNode( m_Node, 9, 9 );
          newWrapper.CreateNodes();
        }
    }
    /// <summary>
    /// Cannot copyt interface - allways throws <see cref="CreateChildObjectException"/>
    /// </summary>
    /// <param name="objToPaste">Object to paste</param>
    /// <exception cref="CreateChildObjectException">Always throws</exception>
    public override void PasteChildObject( IAction objToPaste )
    {
      throw new CreateChildObjectException( "You cant add child object for Tag bit" );
    }
    /// <summary>
    /// Always return <b>false</b>
    /// </summary>
    /// <param name="objToPaste">Object to paste</param>
    /// <returns><b>false</b></returns>
    public override bool CanBePastedAsChild( IAction objToPaste ) { return objToPaste is InterfacesRowWrapper; }
    /// <summary>
    /// Checks if specified <see cref="IAction"/> object can be moved 
    /// </summary>
    /// <returns>True if specified object can be moved </returns>
    public override bool CanBeMoved() { return true; }
    /// <summary>
    /// Cannot copyt interface - it allways throws <see cref="CreateChildObjectException"/>
    /// </summary>
    /// <param name="pObjToMove"></param>
    /// <exception cref="CreateChildObjectException">Always throws</exception>
    public override void MoveChildObject( IAction pObjToMove )
    {
      Action<ComunicationNet.InterfacesRow> cRowToMove = pObjToMove as Action<ComunicationNet.InterfacesRow>;
      if ( cRowToMove == null )
        return;
      cRowToMove.DataRow.BeginEdit();
      cRowToMove.DataRow.SegmentId = m_Parent.SegmentID;
      cRowToMove.DataRow.EndEdit();
    }
    #endregion
    #endregion
  }
}