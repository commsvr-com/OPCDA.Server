
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
using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using CAS.NetworkConfigLib;
using NetworkConfig.HMI.Exceptions;
namespace NetworkConfig.HMI
{
  /// <summary>
  /// Wrapper classes that wrap <see cref="ComunicationNet.TagBitRow"/>. It contains getters and setters to support property grid.
  /// </summary>
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
  internal partial class TagBitRowWrapper: Action<ComunicationNet.TagBitRow>
  {
    #region Constructor
    /// <summary>
    /// Creates new <see cref="TagBitRowWrapper"/> based on specified TagBit row
    /// </summary>
    /// <param name="parent">TagBit row</param>
    public TagBitRowWrapper( ComunicationNet.TagBitRow parent )
      : base( parent )
    { }
    #endregion
    #region Properties for PropertyGrid

    /// <summary>
    /// Gets the station identifier that the TagBit is coupled  with
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
      DescriptionAttribute( "Station identifier that the TagBit is coupled  with" )
    ]
    public long StationID
    {
      get
      {
        return ( m_Parent.TagsRow.DataBlocksRow.GroupsRow.StationID );
      }
    }

    /// <summary>
    /// Gets the station name that the TagBit is coupled  with
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
      DescriptionAttribute( "Station name that the TagBit is coupled  with" )
    ]
    public string StationName
    {
      get
      {
        return ( m_Parent.TagsRow.DataBlocksRow.GroupsRow.StationRow.Name );
      }
    }

    /// <summary>
    /// Gets the tag identifier that the TagBit is coupled with
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
      DescriptionAttribute( "Tag identifier that the TagBit is coupled with" )
    ]
    public long TagID
    {
      get
      {
        return ( m_Parent.TagID );
      }
    }
    /// <summary>
    /// Gets the tag name that the TagBit is coupled with
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
      DescriptionAttribute( "Tag name that the TagBit is coupled with" )
    ]
    public string TagName
    {
      get
      {
        return ( m_Parent.TagsRow.Name );
      }
    }

    /// <summary>
    /// Address that the TagBit is coupled with"
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
      DescriptionAttribute( "Address that the TagBit is coupled with" )
  ]
    public ulong Address
    {
      get
      {
        return ( m_Parent.TagsRow.DataBlocksRow.Address );
      }
    }

    /// <summary>
    /// Gets the value that indicates the specified bit in the register that should be used to calculate the value
    /// </summary>
    [
      BrowsableAttribute( true ),
      CategoryAttribute( "General Settings" ),
      DefaultValueAttribute( 0 ),
      DescriptionAttribute( "Indicates the specified bit in the register that should be used to calculate the value" )
    ]
    public short BitNumber
    {
      get
      {
        return ( m_Parent.BitNumber );
      }
      set
      {
        m_Parent.BitNumber = value;
      }
    }
    /// <summary>
    /// Gets the human readable name
    /// </summary>
    [
      BrowsableAttribute( true ),
      CategoryAttribute( "General Settings" ),
      DefaultValueAttribute( "Set a human readable name" ),
      DescriptionAttribute( "Human readable name, that will be used as the OPC tag name" )
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
    #endregion Properties for PropertyGrid
    #region Overrides
    #region object override
    /// <summary>
    /// Override to string method
    /// </summary>
    /// <returns>Tagbit name, bit number, tag identifier in specified string format</returns>
    public override string ToString() { return string.Format( "{0}: BIT[{1}]", Name, BitNumber.ToString() ); }
    #endregion
    #region Action override
    /// <summary>
    /// Always returns <b>false</b> because its the last object in the treview hierarchy
    /// </summary>
    public override bool CanCreateChild
    {
      get
      {
        return false;
      }
    }
    /// <summary>
    /// Its the last node in treview hierarchy, so it allways throws <see cref="CreateChildObjectException"/>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="CreateChildObjectException">Always throws</exception>
    public override IAction CreateNewChildObject()
    {
      throw new CreateChildObjectException( "You cant add child object for Tag bit" );
    }
    /// <summary>
    /// Its the last node in treeview, so it allways throws <see cref="CreateChildObjectException"/>
    /// </summary>
    /// <param name="objToPaste"></param>
    /// <exception cref="CreateChildObjectException">Always throws</exception>
    public override void PasteChildObject( IAction objToPaste )
    {
      throw new CreateChildObjectException( "You cant add child object for Tag bit" );
    }
    /// <summary>
    /// Always returns <b>false</b> because its the last node in treview hierarchy
    /// </summary>
    /// <param name="objToPaste">Object to past</param>
    /// <returns>Always <b>false</b></returns>
    public override bool CanBePastedAsChild( IAction objToPaste ) { return false; }
    /// <summary>
    /// Checks if specified <see cref="IAction"/> object can be moved 
    /// </summary>
    /// <returns>True if specified object can be moved </returns>
    public override bool CanBeMoved() { return true; }
    /// <summary>
    /// Its the last node in treview hierarchy, so it allways throws <see cref="CreateChildObjectException"/>
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