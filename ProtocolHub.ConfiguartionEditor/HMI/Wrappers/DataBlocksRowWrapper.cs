//<summary>
//  Title   : Serial Settings wrapper to be used in property grid panel
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//  20081105: mzbrzezny: DataBlocksRowWrapper: Fixed exception in get: AddressSpaceDescriptor (there is message in catch)
//  20081003: mzbrzezny: AddressSpaceDescriptor implementation
//  20081003: mzbrzezny: cleanup, itemdefaultsettings implementation, sorted dictionary for tags, real address calculation
//    Tomek Siwecki - from 12-2006 to 2-2007 - implementation
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using CAS.Lib.CommonBus;
using CAS.NetworkConfigLib;
using NetworkConfig.HMI.Editors;
using NetworkConfig.Properties;
using CAS.CommServer.CommonBus;

namespace NetworkConfig.HMI
{
  /// <summary>
  /// Wrapper classes that wrap <see cref="ComunicationNet.DataBlocksRow" />. It contains getters and setters to support property grid.
  /// </summary>
  /// <seealso cref="TagsRowWrapper">TagsRowWrapper is the child object in TreeView</seealso>
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
  internal partial class DataBlocksRowWrapper: Action<ComunicationNet.DataBlocksRow>
  {
    #region private
    ProtocolAndSerialRowWrapper ProtocolWrapper;
    #endregion
    #region Constructor
    /// <summary>
    /// Creates new <see cref="DataBlocksRowWrapper"/> object based on specified DataBlok row
    /// </summary>
    /// <param name="parent">DataBlok row</param>
    /// <param name="_ProtocolWrapper">link to the <see cref="ProtocolAndSerialRowWrapper"/></param>
    public DataBlocksRowWrapper( ComunicationNet.DataBlocksRow parent, ProtocolAndSerialRowWrapper _ProtocolWrapper )
      : base( parent )
    {
      ProtocolWrapper = _ProtocolWrapper;
    }
    #endregion
    #region public
    internal SortedList<short, IAddressSpaceDescriptor> GetAvailiableAddressSpaces()
    {
      if ( ProtocolWrapper == null )
        return null;
      return ProtocolWrapper.DataProviderAddressSpaces;
    }
    #endregion
    #region Properties for PropertyGrid
    /// <summary>
    /// Gets or sets the human readable name for the datablock
    /// </summary>
    [
      BrowsableAttribute( true ),
      CategoryAttribute( "Global Settings" ),
      DefaultValueAttribute( "name of the datablock" ),
      DescriptionAttribute( "Human readable name for the datablock" )
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
    /// Gets station name
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
    DescriptionAttribute(
    "Name of coupled station; only one station can be selected for the data block (to make the configuration easier, NetworkConfig displays name of the coupled station instead of its numerical identifier"
    ),
DisplayNameAttribute( "Station name" )
]
    public string StationName
    {
      get
      {
        return ( m_Parent.GroupsRow.StationRow.Name );
      }
    }
    /// <summary>
    /// Identifier of the coupled station
    /// </summary>
    /// <remarks></remarks>
    [
#if DEBUG
BrowsableAttribute( true ),
CategoryAttribute( "XXX debug" ),
#else
        BrowsableAttribute( false ),
        CategoryAttribute( "General Settings" ),
#endif
 DefaultValueAttribute( 0 ),
  DescriptionAttribute(
    "Identifier of the coupled station; only one station can be selected for the data block (to make the configuration easier, NetworkConfig displays name of the coupled station instead of its numerical identifier)"
    )
]
    public long StationId
    {
      get
      {
        return ( m_Parent.GroupsRow.StationID );
      }
    }
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Information" ),
    DefaultValueAttribute( 0 ),
    DescriptionAttribute(
        "Minimal start address in the selected address sapce"
        )
    ]
    public long AddressStart
    {
      get
      {
        return AddressSpaceDescriptor.StartAddress;
      }
    }
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Information" ),
    DefaultValueAttribute( 0 ),
    DescriptionAttribute(
        "Maximal start address in the selected address sapce"
        )
    ]
    public long AddressEnd
    {
      get
      {
        return AddressSpaceDescriptor.EndAddress;
      }
    }
    /// <summary>
    /// Gets or sets The first address of the address space which contains all process variables.
    /// </summary>
    [
      BrowsableAttribute( true ),
      CategoryAttribute( "Global Settings" ),
      DefaultValueAttribute( 0 ),
      DescriptionAttribute(
        "The first address of the address space which contains all process variables (coils, inputs, registers,  timers, etc.)"
        )
    ]
    public ulong Address
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
    /// Gets the name of of the coupled group.
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
 DescriptionAttribute(
"Identifier of the coupled group (to make the configuration easier, NetworkConfig displays name of the coupled group instead of its numerical identifier). The datablock can belong only to one group."
)
]
    public string GroupName
    {
      get
      {
        return ( m_Parent.GroupsRow.Name );
      }
    }
    /// <summary>
    /// Gets the identifier of the coupled group
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
  DescriptionAttribute(
    "Identifier of the coupled group (to make the configuration easier, NetworkConfig displays name of the coupled group instead of its numerical identifier). The datablock can belong only to one group."
    )
]
    public string GroupId
    {
      get
      {
        return ( m_Parent.GroupsRow.Name );
      }
    }
    /// <summary>
    /// Data type in human readable string format
    /// </summary>
    [
    BrowsableAttribute( true ),
    TypeConverter( typeof( DataBlocksRowWrapper.DataTypeConverter ) ),
    CategoryAttribute( "Global Settings" ),
    DefaultValueAttribute( 0 ),
    DescriptionAttribute( "Data type (interpretation depends on the protocol or the device in use)." ),
    DisplayNameAttribute( "Data type" )
    ]
    public string DataType
    {
      get { return WrappersHelpers.GetName( GetAvailiableAddressSpaces(), Convert.ToInt16( m_Parent.DataType ) ); }
      set { m_Parent.DataType = Convert.ToUInt32( WrappersHelpers.GetID( GetAvailiableAddressSpaces(), value ) ); }
    }
    [
    DisplayName( "Tags" ),
    BrowsableAttribute( true ),
    CategoryAttribute( "Global Settings" ),
    DefaultValueAttribute( 0 ),
    DescriptionAttribute( "Collection of tags makes up a block. Each tag represents one process variable usually stored in a controller register. First tag has address set up in the block definition. All tags fills up consecutive address space – are adjacent." ),
    EditorAttribute( typeof( DataBlocksRowWrapper.TagsCollectionEditor ), typeof( System.Drawing.Design.UITypeEditor ) )
    ]
    public string Tags
    {
      get { return "(Tags collection)"; }
      set
      {
        this.CreateNodes();
        m_Node.Expand();
      }
    }
    #endregion //Properties for PropertyGrid
    #region Override
    #region Object override
    /// <summary>
    /// Returns data block name and address
    /// </summary>
    /// <returns></returns>
    public override string ToString() { return String.Format( "Block: {0}[{1}:{2}]", Name, this.DataType, this.Address ); }
    #endregion
    #region Action overrides
    /// <summary>
    /// Creates new tags row
    /// </summary>
    /// <returns>Returns <see cref="TagsRowWrapper"/> object</returns>
    public override IAction CreateNewChildObject()
    {
      ComunicationNet.TagsDataTable tagBitTable = ( (ComunicationNet)( m_Parent.Table.DataSet ) ).Tags;
      return new TagsRowWrapper( tagBitTable.NewTagsRow( m_Parent.DatBlockID, m_Parent.Name ), this, true );
    }
    /// <summary>
    /// Creates protocol tags rows in treeview
    /// </summary>
    public override void CreateNodes()
    {
      base.CreateNodes();
      SortedList cSl = new SortedList();
      foreach ( ComunicationNet.TagsRow curr in m_Parent.GetTagsRows() )
        if ( curr.RowState != DataRowState.Deleted )
          cSl.Add( curr.TagID, new TagsRowWrapper( curr, this, false ) );
      foreach ( TagsRowWrapper newWrapper in cSl.Values )
      {
        newWrapper.AddActionTreeNode( m_Node, 15, 15 );
        newWrapper.CreateNodes();
      }
    }
    /// <summary>
    /// Pastes specified <see cref="TagsRowWrapper"/> under this wrapper
    /// </summary>
    /// <param name="objToPaste"><see cref="TagsRowWrapper"/> to be pasted</param>
    public override void PasteChildObject( IAction objToPaste )
    {
      if ( ( objToPaste is TagsRowWrapper ) )
      {
        TagsRowWrapper wrapperToPaste = objToPaste as TagsRowWrapper;
        ComunicationNet.TagsRow rowToPaste = wrapperToPaste.DataRow;
        ComunicationNet.TagsDataTable dt = ( (ComunicationNet)( m_Parent.Table.DataSet ) ).Tags;
        dt.NewTagsRow( m_Parent.DatBlockID, rowToPaste, false, m_Parent.Name );
      }
    }
    /// <summary>
    /// Checks if specified <see cref="IAction"/> interface is <see cref="TagsRowWrapper"></see>
    /// </summary>
    /// <param name="objToPaste">Object to check</param>
    /// <returns>True if specified object is <see cref="TagsRowWrapper"/></returns>
    public override bool CanBePastedAsChild( IAction objToPaste )
    {
      if ( objToPaste is TagsRowWrapper )
        return true;
      else
        return false;
    }
    /// <summary>
    /// Checks if specified <see cref="IAction"/> object can be moved 
    /// </summary>
    /// <returns>True if specified object can be moved </returns>
    public override bool CanBeMoved() { return true; }
    /// <summary>
    /// Moves specified <see cref="TagBitRowWrapper"/> object under this wrapper
    /// </summary>
    /// <param name="objToPaste"><see cref="TagBitRowWrapper"/> object to be pasted</param>
    public override void MoveChildObject( IAction objToPaste )
    {
      TagsRowWrapper wrapperToPaste = objToPaste as TagsRowWrapper;
      if ( wrapperToPaste == null )
        return;
      ComunicationNet.TagsRow cRTM = wrapperToPaste.DataRow;
      cRTM.BeginEdit();
      cRTM.DatBlockID = m_Parent.DatBlockID;
      cRTM.EndEdit();
    }
    #endregion
    #endregion
    #region TypeConverter class
    /// <summary>
    /// This class is used in DataTypeConverter dropdown list 
    /// </summary>
    private class DataTypeConverter: StringConverter
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
        return true;
      }
      /// <summary>
      /// Gets a collection of standard values for the data type this validator is designed for. 
      /// </summary>
      /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context. </param>
      /// <returns>A <see cref="TypeConverter.StandardValuesCollection"/>  that holds a standard set of valid values </returns>
      public override StandardValuesCollection GetStandardValues( ITypeDescriptorContext context )
      {
        return new StandardValuesCollection( WrappersHelpers.GetNames( ( (DataBlocksRowWrapper)context.Instance ).GetAvailiableAddressSpaces() ) );
      }
    }
    #endregion
    #region TypeConverter & UITypeEditor classes
    private class TagsCollectionEditor: UITypeEditor
    {
      private string DPSelectConfig( DataBlocksRowWrapper m_Wrraper, IWindowsFormsEditorService cProvider )
      {
        using ( TagsCollection cTagsDilog = new TagsCollection( m_Wrraper.m_Parent, m_Wrraper ) )
        {
          cProvider.ShowDialog( cTagsDilog );
          if ( cTagsDilog.DialogResult != DialogResult.OK )
            return "canceled";
        }
        return "ok";
      }
      public override object EditValue( ITypeDescriptorContext context, IServiceProvider provider, object value )
      {
        IWindowsFormsEditorService cProvider = provider.GetService( typeof( IWindowsFormsEditorService ) ) as IWindowsFormsEditorService;
        return DPSelectConfig( context.Instance as DataBlocksRowWrapper, cProvider );
      }
      public override UITypeEditorEditStyle GetEditStyle( ITypeDescriptorContext context )
      {
        return UITypeEditorEditStyle.Modal;
      }
    }
    #endregion

    internal IItemDefaultSettings GetItemDefaultSettings( ulong RealAddress )
    {
      if ( ProtocolWrapper==null || ProtocolWrapper.DataProvider == null )
        return null;
      return ProtocolWrapper.DataProvider.GetItemDefaultSettings( (short)m_Parent.DataType, RealAddress );
    }
    internal SortedDictionary<long, TagsRowWrapper> SortedDictionaryOfTagsRowWrapper
    {
      get
      {
        SortedDictionary<long, TagsRowWrapper> cSL = new SortedDictionary<long, TagsRowWrapper>();
        foreach ( ComunicationNet.TagsRow cr in m_Parent.GetTagsRows() )
          cSL.Add( cr.TagID, new TagsRowWrapper( cr, this, false ) );
        return cSL;
      }
    }
    internal ulong CaclculateTagRealAddressBasedOnTagID( long TagIdentifier )
    {
      ulong realaddress = this.Address;
      foreach ( TagsRowWrapper trw in SortedDictionaryOfTagsRowWrapper.Values )
      {
        if ( TagIdentifier == trw.TagID )
          break;
        realaddress++;
      }
      return realaddress;
    }
    internal IAddressSpaceDescriptor AddressSpaceDescriptor
    {
      get
      {
        short? id = WrappersHelpers.GetID( ProtocolWrapper.DataProviderAddressSpaces, this.DataType );
        if ( id.HasValue && ProtocolWrapper.DataProviderAddressSpaces != null )
        {
          try
          {
            return ProtocolWrapper.DataProviderAddressSpaces[ (short)id ];
          }
          catch
          {
            MessageBox.Show( Resources.Tx_DataBlocksRowWrapper_AddressSpaceDescriptor );
          }
        }
        string name = "Unknown";
        if ( id.HasValue )
          name += id.ToString();
        return new AddressSpaceDescriptor( name, (short)( id.HasValue ? id : 0 ), 0, 0 );
      }
    }
  }
}