//<summary>
//  Title   : Serial Settings wrapper to be used in property grid panel
//  System  : Microsoft Visual C# .NET 
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//  20081007: mzbrzezny: cleanup after unfinished add operation is added
//  20081007: mzbrzezny: protection from wrong values in AccesRights and StateTRigger (for backwards compatibility)
//  20081007: mzbrzezny: new properties:
//                       Default (suggested) name from DataProvider 
//                       Default (suggested) data type from DataProvider 
//                       Default (suggested) access rights from DataProvider 
//  20081006: mzbrzezny: station name in the construction is taken from datablock wrapper instead of dataset
//  20081006 mzbrzezny: implementation of ItemAccessRights and StateTrigger
//  20081003: mzbrzezny: cleanup, item default settings implementation
//  2006: created
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CAS.Lib.CommonBus;
using CAS.Lib.RTLib;
using CAS.NetworkConfigLib;
using CAS.Lib.RTLib.Utils;

namespace NetworkConfig.HMI
{
  /// <summary>
  /// Wrapper classes that wrap <see cref="ComunicationNet.TagsRow"/>. It contains getters and setters to support property grid.
  /// </summary>
  /// <seealso cref="TagBitRowWrapper">TagBitRowWrapper is the child object in TreeView</seealso>
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
  internal partial class TagsRowWrapper: Action<ComunicationNet.TagsRow>
  {
    #region private
    DataBlocksRowWrapper dataBlockRowWrapper;
    private IItemDefaultSettings DefaultSettings
    {
      get
      {
        return dataBlockRowWrapper.GetItemDefaultSettings( RealAddress );
      }
    }
    #endregion
    #region Constructor

    /// <summary>
    /// Creates new <see cref="TagsRowWrapper"/> based on specified tags row
    /// </summary>
    /// <param name="parent">Tags row</param>
    /// <param name="DataBlockRowWrapper">The data block row wrapper.</param>
    /// <param name="UseDefaultSettings">if set to <c>true</c> [use default settings from DataProvider is enabled].</param>
    public TagsRowWrapper( ComunicationNet.TagsRow parent, DataBlocksRowWrapper DataBlockRowWrapper, bool UseDefaultSettings )
      : base( parent )
    {
      dataBlockRowWrapper = DataBlockRowWrapper;
      if ( UseDefaultSettings && DefaultSettings != null )
      {
        Name = DataBlockRowWrapper.StationName + "/" + DefaultSettings.Name;
        DataTypeConversion = DefaultSettings.DefaultType.ToString();
        AccessRights = DefaultSettings.AccessRights.ToString();
      }
    }
    #endregion
    #region Properties for PropertyGrid

    /// <summary>
    /// Gets the human readable name for the tag
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "General Settings" ),
    DefaultValueAttribute( "Human readable name" ),
    DescriptionAttribute( "Human readable name for the tag" )
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
    /// Gets the station identifier that the tag is coupled with
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
DescriptionAttribute( "Station identifier that the tag is coupled  with" )
]
    public long StationID
    {
      get
      {
        return ( m_Parent.DataBlocksRow.GroupsRow.StationID );
      }
    }
    /// <summary>
    /// Gets the tag identifier
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
    DescriptionAttribute( "Unique tag identifier" )
    ]
    public long TagID
    {
      get { return ( m_Parent.TagID ); }
      set { m_Parent.TagID = value; }
    }
    /// <summary>
    /// Gets the address of the block the tag belongs to
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
    DescriptionAttribute( "Address of the block the tag belongs to. The block with selected address must be previously defined. There is one to one relation between process variables located in the physical device and tags holding current value of the equivalent variable in the OPC server. Actual position of the process variable in the continuous address space represented by the block is determined by the tag numerical identifier. Tags are sorted in the block using these identifiers, and the actual variable address is a sum of the block address and position of the tag in the block. Therefore, the block length is determined by the number of tags belonging to it, and there are no gaps between process variables, whose values are held in the equivalent tags." )
    ]
    public ulong Address
    {
      get
      {
        return ( m_Parent.DataBlocksRow.Address );
      }
    }
    /// <summary>
    /// Gets the value informs that it indicates that the corresponding tag is inherently writable. For example, a value representing physical output or an adjustable parameter such as a setpoint or alarm limit would generally be writable.
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "General Settings" ),
    DefaultValueAttribute( false ),
    TypeConverter( typeof( GenericEnumTypeConverterHelper<ItemAccessRights> ) ),
    DescriptionAttribute( "Indicates if this item is read only, write only or read/write. This is NOT related to security but rather to the nature of the underlying hardware" )
    ]
    public string AccessRights
    {
      get
      {
        try
        {
          return ( GenericEnumTypeConverterHelper<ItemAccessRights>.GetNameFromValue( m_Parent.AccessRights ) );
        }
        catch ( Exception ex )
        {
          MessageBox.Show( String.Format( Properties.Resources.tx_TagRowWrapper_AccessRights, ex.Message ) );
          m_Parent.AccessRights = (sbyte)DefaultSettings.AccessRights;
          return DefaultAccessRights;
        }
      }
      set
      {
        m_Parent.AccessRights = (sbyte)GenericEnumTypeConverterHelper<ItemAccessRights>.GetValueFromString( value );
      }
    }
    /// <summary>
    /// Gets the value informs that this tag is used to switch on the FAST scanning mode (see StateMask for additional details)
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Special functions" ),
    DefaultValueAttribute( false ),
    TypeConverter( typeof( GenericEnumTypeConverterHelper<StateTrigger> ) ),
    DescriptionAttribute( "Indicates wheter there is any state trigger associated with this item. The triggers could be low or high or none." )
    ]
    public string StateTrigger
    {
      get
      {
        try
        {
          return ( GenericEnumTypeConverterHelper<StateTrigger>.GetNameFromValue( m_Parent.StateTrigger ) );
        }
        catch ( Exception ex )
        {
          MessageBox.Show( String.Format( Properties.Resources.tx_TagRowWrapper_StateTrigger, ex.Message ) );
          m_Parent.StateTrigger = (sbyte)CAS.NetworkConfigLib.StateTrigger.None;
          return CAS.NetworkConfigLib.StateTrigger.None.ToString();
        }

      }
      set
      {
        m_Parent.StateTrigger = (sbyte)GenericEnumTypeConverterHelper<StateTrigger>.GetValueFromString( value );
      }
    }
    /// <summary>
    /// Gets the value that informs if there is an alarm linked with  this tag (the server is obligated to listen on the channel this tag belongs to and pick up incoming connection)
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Special functions" ),
    DefaultValueAttribute( false ),
    DescriptionAttribute( "This attribute informs if there is an alarm linked with  this tag (the server is obligated to listen on the channel this tag belongs to and pick up incoming connection).  If true, the remote station is able to establish connection with the server, and the server will service it." )
    ]
    public bool Alarm
    {
      get
      {
        return ( m_Parent.Alarm );
      }
      set
      {
        m_Parent.Alarm = value;
      }
    }
    /// <summary>
    /// Gets the alarm mask that defines condition when the remote station is in the alarm state, and as a result should start to establish connection to the server in an independent alarm channel (e.g. the second ISDN B channel)
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Special functions" ),
    DefaultValueAttribute( 0 ),
    DescriptionAttribute( "Alarm mask defines condition when the remote station is in the alarm state, and as a result should start to establish connection to the server in an independent alarm channel (e.g. the second ISDN B channel)" )
    ]
    public ulong AlarmMask
    {
      get
      {
        return ( m_Parent.AlarmMask );
      }
      set
      {
        m_Parent.AlarmMask = value;
      }
    }
    /// <summary>
    /// Gets the value that defines conditions when the remote station is to enter the alarm state and the scan mode should be switched  – this conditions are used to switch between the fast and normal scan modes. 
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Special functions" ),
    DefaultValueAttribute( 0 ),
    DescriptionAttribute( "Defines conditions when the remote station is to enter the alarm state and the scan mode should be switched  – this conditions are used to switch between the fast and normal scan modes. Logical operations (and, xor) are used to check the condition:  . If this condition is true, the scan mode is switched depending on the StateHighTrigger or StateLowTrigger setting." )
    ]
    public ulong StateMask
    {
      get
      {
        return ( m_Parent.StateMask );
      }
      set
      {
        m_Parent.StateMask = value;
      }
    }
    /// <summary>
    /// Gets or sets Conversion requirements in human readable format. 
    /// DataTypeConversion identifiers are converted to string format based on OPCTypes 
    /// </summary>
    [
    BrowsableAttribute( true ),
    TypeConverter( typeof( TagsRowWrapper.OPCDataTypeConverter ) ),
    CategoryAttribute( "Special functions" ),
    DefaultValueAttribute( 0 ),
    DescriptionAttribute( "Coversion requirements. Data will be available in required type (if conversion is possible)." )
    ]
    public string DataTypeConversion
    {
      get
      {
        if ( m_Parent.IsDataTypeConversionNull() )
          return "N/A";
        else
          return m_Parent.DataTypeConversion;
      }
      set
      {
        m_Parent.DataTypeConversion = value;
      }
    }
    /// <summary>
    /// Gets the real address.
    /// </summary>
    /// <value>The real address.</value>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Information" ),
    DefaultValueAttribute( 0 ),
    DescriptionAttribute( "Real Address in the address space" )
    ]
    public ulong RealAddress
    {
      get
      {
        return dataBlockRowWrapper.CaclculateTagRealAddressBasedOnTagID( this.TagID );
      }
    }
    /// <summary>
    /// Gets the name of the default.
    /// </summary>
    /// <value>The name of the default.</value>
    [
BrowsableAttribute( true ),
CategoryAttribute( "Information" ),
DefaultValueAttribute( 0 ),
DescriptionAttribute( "Default (suggested) name from DataProvider " )
]
    public string DefaultName
    {
      get
      {
        if ( DefaultSettings == null )
          return "";
        return DefaultSettings.Name;
      }
    }
    /// <summary>
    /// Gets the type of the default.
    /// </summary>
    /// <value>The type of the default.</value>
    [
BrowsableAttribute( true ),
CategoryAttribute( "Information" ),
DefaultValueAttribute( 0 ),
DescriptionAttribute( "Default (suggested) data type from DataProvider " )
]
    public string DefaultType
    {
      get
      {
        if ( DefaultSettings == null )
          return "";
        return DefaultSettings.DefaultType.ToString();
      }
    }
    [
BrowsableAttribute( true ),
CategoryAttribute( "Information" ),
DefaultValueAttribute( 0 ),
DescriptionAttribute( "Default (suggested) access rights from DataProvider " )
]
    public string DefaultAccessRights
    {
      get
      {
        if ( DefaultSettings == null )
          return "";
        return DefaultSettings.AccessRights.ToString();
      }
    }
    /// <summary>
    /// AdditionalInfo from DataProvider
    /// </summary>
    [
#if DEBUG
BrowsableAttribute( true ),
CategoryAttribute( "XXX debug" ),
#else
        BrowsableAttribute( false ),
        CategoryAttribute( "Information" ),
#endif
 DefaultValueAttribute( "Additional information" ),
DescriptionAttribute( "AdditionalInfo from DataProvider" )
]
    public string AdditionalInfo
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine( "Additional information: " );
        if ( DefaultSettings != null )
        {
          sb.AppendLine( "Default settings: " );
          sb.AppendLine( "name: " + DefaultSettings.Name );
          sb.AppendLine( "type: " + DefaultSettings.DefaultType.ToString() );
          sb.AppendLine( "availiable types:" );
          foreach ( Type t in DefaultSettings.AvailiableTypes )
            sb.Append( t.ToString() + "; " );
          sb.AppendLine();
          sb.AppendLine( "access rights: " + DefaultSettings.AccessRights.ToString() );
        }
        else
        {
          sb.Append( "Cannot obtain default settings" );
        }
        return ( sb.ToString() );
      }
    }
    #endregion //Properties for PropertyGrid
    #region Overrides
    #region Object override
    /// <summary>
    /// Overrides to string method
    /// </summary>
    /// <returns>Returns tag row name and tag identifier in specified string format</returns>
    public override string ToString() { return "Tag: " + Name; }
    #endregion
    #region Action overrides
    public override void AddUnfinishedCleanup()
    {
      //musimy wyczyscic wszystkie properties
      ComunicationNet.ItemPropertiesTableRow[] myproperties = m_Parent.GetItemPropertiesTableRows();
      foreach ( ComunicationNet.ItemPropertiesTableRow iptr in myproperties )
        iptr.Delete();
    }
    /// <summary>
    /// Creates new tagbit row
    /// </summary>
    /// <returns><see cref="TagBitRowWrapper"/> object</returns>
    public override IAction CreateNewChildObject()
    {
      ComunicationNet.TagBitDataTable dt = ( (ComunicationNet)( m_Parent.Table.DataSet ) ).TagBit;
      return new TagBitRowWrapper( dt.NewTagBitRow( m_Parent, String.Empty ) );
    }
    /// <summary>
    /// Creates tagbit row in treeview
    /// </summary>
    public override void CreateNodes()
    {
      base.CreateNodes();
      foreach ( ComunicationNet.TagBitRow curr in m_Parent.GetTagBitRows() )
        if ( curr.RowState != DataRowState.Deleted )
        {
          TagBitRowWrapper newWrapper = new TagBitRowWrapper( curr );
          newWrapper.AddActionTreeNode( m_Node, 16, 16 );
        }
    }
    /// <summary>
    /// Pastes specified <see cref="TagBitRowWrapper"/> object under this wrapper
    /// </summary>
    /// <param name="objToPaste"><see cref="TagBitRowWrapper"/> object to be pasted</param>
    public override void PasteChildObject( IAction objToPaste )
    {
      if ( ( objToPaste is TagBitRowWrapper ) )
      {
        TagBitRowWrapper wrapperToPaste = objToPaste as TagBitRowWrapper;
        ComunicationNet.TagBitRow rowToPaste = wrapperToPaste.DataRow;
        ComunicationNet.TagBitDataTable dt = ( (ComunicationNet)( m_Parent.Table.DataSet ) ).TagBit;
        dt.NewTagBitRow( m_Parent, rowToPaste );
      }
    }
    /// <summary>
    /// Checks if specified <see cref="IAction"></see> interface is <see cref="TagBitRowWrapper"></see>
    /// </summary>
    /// <param name="objToPaste">Object to check</param>
    /// <returns>True if specified object is <see cref="TagBitRowWrapper"/></returns>
    public override bool CanBePastedAsChild( IAction objToPaste )
    {
      Action<ComunicationNet.TagBitRow> cRTP = objToPaste as Action<ComunicationNet.TagBitRow>;
      if ( cRTP == null )
        return false;
      ComunicationNet.TagBitDataTable cTab = ( (ComunicationNet)m_Parent.Table.DataSet ).TagBit;
      return !cTab.Contain( TagID, cRTP.DataRow.Name );
    }
    /// <summary>
    /// Checks if specified <see cref="IAction"/> object can be moved 
    /// </summary>
    /// <returns>True if specified object can be moved </returns>
    public override bool CanBeMoved() { return true; }
    /// <summary>
    /// Moves specified <see cref="TagsRowWrapper"/> object under this wrapper
    /// </summary>
    /// <param name="objToPaste"><see cref="TagsRowWrapper"/> object to be pasted</param>
    public override void MoveChildObject( IAction objToPaste )
    {
      TagBitRowWrapper wrapperToPaste = objToPaste as TagBitRowWrapper;
      if ( wrapperToPaste == null )
        return;
      ComunicationNet.TagBitRow rowToPaste = wrapperToPaste.DataRow;
      rowToPaste.BeginEdit();
      rowToPaste.TagID = m_Parent.TagID;
      rowToPaste.EndEdit();
    }
    #endregion
    #endregion
    #region TypeConverter class

    /// <summary>
    /// This class is used in DataTypeConverter dropdown list 
    /// </summary>
    private class OPCDataTypeConverter: StringConverter
    {
      /// <summary>
      /// Gets a value indicating whether this object supports a standard set of values that can be picked from a list. 
      /// </summary>
      /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context. </param>
      /// <returns>Always returns <b>true</b> - means show a combobox </returns>
      public override bool GetStandardValuesSupported( ITypeDescriptorContext context )
      {
        //true means show a combobox
        return true;
      }
      /// <summary>
      /// Gets a value indicating whether the list of standard values returned from the GetStandardValues method is an exclusive list. 
      /// </summary>
      /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context. </param>
      /// <returns>Always returns <b>true</b> - means it limits to list</returns>
      public override bool GetStandardValuesExclusive( ITypeDescriptorContext context )
      {
        //true will limit to list. false will show the list, but allow free-form entry
        return false;
      }
      /// <summary>
      /// Gets a collection of standard values for the data type this validator is designed for. 
      /// </summary>
      /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context. </param>
      /// <returns>A <see cref="TypeConverter.StandardValuesCollection"/>  that holds a standard set of valid values </returns>
      public override
          StandardValuesCollection
          GetStandardValues( ITypeDescriptorContext context )
      {
        IItemDefaultSettings contextsettigns=( (TagsRowWrapper)context.Instance ).DefaultSettings;
        if ( contextsettigns != null )
          return new StandardValuesCollection( contextsettigns.AvailiableTypes );
        else
          return null;
      }
    }
    #endregion
  }
}
