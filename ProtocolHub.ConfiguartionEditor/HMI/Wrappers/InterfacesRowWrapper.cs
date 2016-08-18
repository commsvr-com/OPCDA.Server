//<summary>
//  Title   : Serial Settings wrapper to be used in property grid panel
//  System  : Microsoft Visual C# .NET 
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20080905: mzbrzezny:InterfaceNum is hidden from GUI (visible only in DEBUG)
//    2006: created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.ComponentModel;
using CAS.NetworkConfigLib;
using NetworkConfig.HMI.Exceptions;

namespace NetworkConfig.HMI
{
  /// <summary>
  /// Wrapper classes that wrap <see cref="ComunicationNet.InterfacesRow"/>. It contains getters and setters to support property grid.
  /// </summary>
  /// <seealso cref="StationRowWrapper">StationRowWrapper is the child object in TreeView</seealso>
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
  internal partial class InterfacesRowWrapper: Action<ComunicationNet.InterfacesRow>
  {
    #region private
    ProtocolAndSerialRowWrapper ProtocolWrapper;
    #endregion
    #region Constructor
    /// <summary>
    /// Creates new <see cref="InterfacesRowWrapper"/> based on secified Interface row
    /// </summary>
    /// <param name="parent">Interface row</param>
    /// <param name="_ProtocolWrapper">link to the <see cref="ProtocolAndSerialRowWrapper"/></param>
    public InterfacesRowWrapper( ComunicationNet.InterfacesRow parent, ProtocolAndSerialRowWrapper _ProtocolWrapper )
      : base( parent )
    {
      ProtocolWrapper = _ProtocolWrapper;
    }
    #endregion
    #region Properties for PropertyGrid

    /// <summary>
    /// Gets or sets the name of the interface
    /// </summary>
    [
      BrowsableAttribute( true ),
      CategoryAttribute( "Global Settings" ),
      DefaultValueAttribute( "Name of the interaface" ),
      DescriptionAttribute( "Name of the interaface" )
    ]
    public string Name
    {
      get { return ( m_Parent.Name ); }
      set { m_Parent.Name = value; }
    }

    /// <summary>
    /// Gets the copuled segment name
    /// </summary>
    [
#if DEBUG
BrowsableAttribute( true ),
CategoryAttribute( "XXX debug" ),
#else
    BrowsableAttribute( false ),
    CategoryAttribute( "General Settings" ),
#endif
 DescriptionAttribute( "Coupled segment name " )
    ]
    public string SegmentName
    {
      get { return ( m_Parent.SegmentsRow.Name ); }
    }
    /// <summary>
    /// Gets the coupled segment identifier
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
     DescriptionAttribute( "Coupled segment identifier " )
   ]
    public long SegmentId
    {
      get { return ( m_Parent.SegmentId ); }
    }
    [
#if DEBUG
BrowsableAttribute( true ),
CategoryAttribute( "XXX debug" ),
#else
    BrowsableAttribute( false ),
    CategoryAttribute( "General Settings" ),
#endif
 DescriptionAttribute( "Coupled channel name " )
]
    public string ChannelName
    {
      get
      {
        return ( m_Parent.SegmentsRow.ProtocolRow.ChannelsRow.Name );
      }
    }
    /// <summary>
    /// Gets or sets the coupled station identifier
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
DescriptionAttribute( "Coupled station identifier" )
]
    public long StationId
    {
      get
      {
        try
        {
          return ( m_Parent.StationId );
        }
        catch ( Exception ex )
        {
          if ( m_Parent.RowState == System.Data.DataRowState.Detached )
            return -1; // stacja nie zostala jeszce wybrana, 
          //ale ten interfejs jeszcze nie jest podlaczony 
          //do bazy danych konfiguracji, wiec nie ma problemu
          else
            throw ex;
        }
      }
      set
      {
        m_Parent.StationId = value;
      }
    }
    /// <summary>
    /// Gets or sets the protocol address of the station used for the interface
    /// </summary>
    [
      BrowsableAttribute( true ),
      CategoryAttribute( "Global Settings" ),
      DefaultValueAttribute( 0 ),
      DescriptionAttribute( "Defines protocol address of the station used for the interface" )
    ]
    public long Address
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
    /// Gets or sets the value that defines period of time for each interface in which the interface is in the inactive mode.
    /// </summary>
    [
      BrowsableAttribute( true ),
      CategoryAttribute( "Global Settings" ),
      DefaultValueAttribute( 0 ),
      DescriptionAttribute(
        "Defines period of time for each interface in which the interface is in the inactive mode. After expiration, this interface switches off the active interface and changes its state to activate itself to check if it is still available. The switched off interface will preempt the communication after expiration of its InactTime. This solution guarantees that only one interface is active at any time and keeps this state as long as another interface becomes active. If there is only one interface it becomes active forever and no additional action is taken."
        )
    ]
    public long InactTime
    {
      get
      {
        return ( m_Parent.InactTime );
      }
      set
      {
        m_Parent.InactTime = value;
      }
    }
    /// <summary>
    /// Gets or sets the InactTimeAFailuer (meaning of this parameter is exactly the same as InactTime, except it is used for scheduling after a communication failure on this interface)
    /// </summary>
    [
      BrowsableAttribute( true ),
      CategoryAttribute( "Global Settings" ),
      DefaultValueAttribute( 0 ),
      DescriptionAttribute(
        "Meaning of this parameter is exactly the same as InactTime, except it is used for scheduling after a communication failure on this interface"
        )
    ]
    public long InactTimeAFailure
    {
      get
      {
        return ( m_Parent.InactTimeAFailure );
      }
      set
      {
        m_Parent.InactTimeAFailure = value;
      }
    }
    /// <summary>
    /// Gets or set the identifier of the physical interface
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
DescriptionAttribute( "Unique identifier of the physical interface. It must be 0 or 1!" )
]
    public ulong InterfaceNum
    {
      get
      {
        return ( m_Parent.InterfaceNum );
      }
      set
      {
        m_Parent.InterfaceNum = value;
      }
    }
    #endregion Properties for PropertyGrid
    #region Overrides
    #region Object override
    /// <summary>
    /// Creates and returns a string representing the current <see cref="ComunicationNet.InterfacesRow"/>.
    /// </summary>
    /// <returns>String in format "Port: Name"</returns>
    public override string ToString() { return "Port: " + Name; }
    #endregion
    #region Action override
    /// <summary>
    /// It returns <b>false</b> becuse the interface cannot be copied
    /// </summary>
    public override bool CanBeCopied
    {
      get
      {
        return false;
      }
    }
    /// <summary>
    /// Returns <b>false</b> because under InterfaceRowWrapper cannot be created child object. Interface cannot exists without station
    /// </summary>
    public override bool CanCreateChild
    {
      get
      {
        return false;
      }
    }
    /// <summary>
    /// Creates new station row
    /// </summary>
    /// <returns><see cref="StationRowWrapper"/> object</returns>
    public override IAction CreateNewChildObject()
    {
      ComunicationNet.StationDataTable stationTable = ( (ComunicationNet)( m_Parent.Table.DataSet ) ).Station;
      ComunicationNet.StationRow cr = stationTable.NewStationRow( String.Empty );
      return new StationRowWrapper( cr, ProtocolWrapper );
    }
    /// <summary>
    /// Creates station rows in treeview
    /// </summary>
    public override void CreateNodes()
    {
      base.CreateNodes();
      StationRowWrapper newWrapper = new StationRowWrapper( m_Parent.StationRow, ProtocolWrapper );
      newWrapper.AddActionTreeNode( m_Node, 13, 13 );
      newWrapper.CreateNodes();
    }
    /// <summary>
    /// We cannot create station uder iterface - allways throws <see cref="CreateChildObjectException"/>
    /// </summary>
    /// <param name="objToPaste"></param>
    /// <exception cref="CreateChildObjectException">Always throws</exception>
    public override void PasteChildObject( IAction objToPaste )
    {
      throw new CreateChildObjectException( "You cant add child object for Tag bit" );
    }
    /// <summary>
    /// It alaways retusrns <b>false</b> because child object cannot be created under this wrapper
    /// </summary>
    /// <param name="objToPaste"></param>
    /// <returns></returns>
    public override bool CanBePastedAsChild( IAction objToPaste )
    {
      return false;
    }
    /// <summary>
    /// Checks if specified <see cref="IAction"/> object can be moved 
    /// </summary>
    /// <returns>True if specified object can be moved </returns>
    public override bool CanBeMoved() { return true; }
    /// <summary>
    /// Tt allways throws <see cref="CreateChildObjectException"/>
    /// </summary>
    /// <param name="objToPaste"></param>
    /// <exception cref="CreateChildObjectException">Always throws</exception>
    public override void MoveChildObject( IAction objToPaste )
    {
      throw new CreateChildObjectException( "You cant add child object for Tag bit" );
    }
    #endregion
    #endregion
  }
}