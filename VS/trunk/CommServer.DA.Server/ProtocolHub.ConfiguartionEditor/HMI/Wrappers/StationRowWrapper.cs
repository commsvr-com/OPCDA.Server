//<summary>
//  Title   : Serial Settings wrapper to be used in property grid panel
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    PM - 18-09-2006: created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using CAS.NetworkConfigLib;
using System.Data;
namespace NetworkConfig.HMI
{
  /// <summary>
  /// Wrapper classes that wrap <see cref="ComunicationNet.StationRow"/>. It contains getters and setters to support property grid.
  /// </summary>
  /// <seealso cref="GroupsRowWrapper">GroupsRowWrapper is the child object in TreeView</seealso>
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
  internal partial class StationRowWrapper: Action<ComunicationNet.StationRow>
  {
    #region private
    ProtocolAndSerialRowWrapper ProtocolWrapper;
    #endregion
    #region Constructor
    /// <summary>
    /// Creates new <see cref="StationRowWrapper"/>
    /// </summary>
    /// <param name="parent">Station row</param>
    /// <param name="_ProtocolWrapper">link to the <see cref="ProtocolAndSerialRowWrapper"/></param>
    public StationRowWrapper( ComunicationNet.StationRow parent, ProtocolAndSerialRowWrapper _ProtocolWrapper )
      : base( parent )
    {
      ProtocolWrapper = _ProtocolWrapper;
    }
    #endregion
    #region Properties for PropertyGrid

    /// <summary>
    /// Gets or sets human readable station name
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "General Settings" ),
    DefaultValueAttribute( "readable station name" ),
    DescriptionAttribute( "Human readable station name" )
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
    /// Gets or sets human readable station name
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "General Settings" ),
    DescriptionAttribute( "Station port for this station" )
    ]
    public string StationPort
    {
      get
      {
        if ( ProtocolWrapper != null )
        {
          if ( ProtocolWrapper.DataProvider != null )
            return ProtocolWrapper.DataProvider.ToString();
          else
            return "Please select Data Provider first";
        }
        else
          return "Please change to channel view to obtain information what is the port for this station";
      }
    }
    /// <summary>
    /// Gets station unique numerical identifier
    /// </summary>
    [
#if DEBUG
BrowsableAttribute( true ),
CategoryAttribute( "XXX debug" ),
#else
    BrowsableAttribute( false ),
    CategoryAttribute( "General Settings" ),
#endif
 DescriptionAttribute( "Station unique numerical identifier" )
    ]
    public long StationID
    {
      get
      {
        return ( m_Parent.StationID );
      }
    }
    #endregion //properties for PropertyGrid
    #region Overrides
    #region Object override
    public override string ToString() { return "Station: " + Name; }
    #endregion
    #region Action overrides
    /// <summary>
    /// Creates new groups row
    /// </summary>
    /// <returns><see cref="GroupsRowWrapper"/> object</returns>
    public override IAction CreateNewChildObject()
    {
      ComunicationNet.GroupsDataTable groupsTable = ( (ComunicationNet)( m_Parent.Table.DataSet ) ).Groups;
      ComunicationNet.GroupsRow gr = groupsTable.NewGroupsRow( m_Parent.StationID, Name );
      return new GroupsRowWrapper( gr, ProtocolWrapper );
    }
    /// <summary>
    /// Creates groups rows in treeview
    /// </summary>
    public override void CreateNodes()
    {
      base.CreateNodes();
      foreach ( ComunicationNet.GroupsRow cRow in m_Parent.GetGroupsRows() )
        if ( cRow.RowState != DataRowState.Deleted )
        {
          GroupsRowWrapper newWrapper = new GroupsRowWrapper( cRow, ProtocolWrapper );
          newWrapper.AddActionTreeNode( m_Node, 5, 5 );
          newWrapper.CreateNodes();
        }
    }
    /// <summary>
    /// Pastes specified <see cref="GroupsRowWrapper"/> object under this wrapper
    /// </summary>
    /// <param name="objToPaste"><see cref="GroupsRowWrapper"/> object to be pasted</param>
    public override void PasteChildObject( IAction objToPaste )
    {
      if ( objToPaste is GroupsRowWrapper )
      {
        GroupsRowWrapper wrapperToPaste = objToPaste as GroupsRowWrapper;
        ComunicationNet.GroupsDataTable gt = ( (ComunicationNet)( m_Parent.Table.DataSet ) ).Groups;
        gt.NewGroupsRow( m_Parent.StationID, wrapperToPaste.DataRow, false, Name );
      }
    }
    /// <summary>
    /// Checks if specified <see cref="IAction"></see> interface is <see cref="GroupsRowWrapper"></see>
    /// </summary>
    /// <param name="objToPaste">Object to check</param>
    /// <returns>True if specified object is <see cref="GroupsRowWrapper"/></returns>
    public override bool CanBePastedAsChild( IAction objToPaste )
    {
      return objToPaste is GroupsRowWrapper;
    }
    /// <summary>
    /// Checks if specified <see cref="IAction"/> object can be moved 
    /// </summary>
    /// <returns>True if specified object can be moved </returns>
    public override bool CanBeMoved() { return false; }
    /// <summary>
    /// Moves specified <see cref="GroupsRowWrapper"/> object under this wrapper
    /// </summary>
    /// <param name="objToPaste"><see cref="GroupsRowWrapper"/> object to be pasted</param>
    public override void MoveChildObject( IAction objToPaste )
    {
      GroupsRowWrapper wrapperToPaste = objToPaste as GroupsRowWrapper;
      if ( wrapperToPaste == null )
        return;
      ComunicationNet.GroupsRow rowToPaste = wrapperToPaste.DataRow;
      rowToPaste.BeginEdit();
      rowToPaste.StationID = m_Parent.StationID;
      rowToPaste.EndEdit();
    }
    #endregion
    #endregion
  }
}