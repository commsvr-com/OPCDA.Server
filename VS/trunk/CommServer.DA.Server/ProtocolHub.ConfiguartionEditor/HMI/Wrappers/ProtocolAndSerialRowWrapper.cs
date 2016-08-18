//<summary>
//  Title   : NetworkConfig.Wrappers
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//  20081003: mzbrzezny: AddressSpaceDescriptor implementation
//   Tomek Siwecki - 26.12.2006 - Integrated porotocol and serial row wrappers 
//    
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using CAS.Lib.CommonBus;
using CAS.Lib.CommonBus.Components;
using CAS.Lib.ControlLibrary;
using CAS.NetworkConfigLib;

namespace NetworkConfig.HMI
{
  internal interface IProtocolUI
  {
    CommonBusControl GetCommonBusControl { get; }
    IDataProviderID GetCurrentDP { get; }
  }
  /// <summary>
  /// Wrapper classes that wrap <see cref="ComunicationNet.ProtocolRow"/> and <see cref="ComunicationNet.SerialSetingsRow"/>. It contains getters and setters to support property grid.
  /// </summary>
  /// <seealso cref="SegmentsRowWrapper">SegmentsRowWrapper is the child object in TreeView</seealso>
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
  internal partial class ProtocolAndSerialRowWrapper: Action<ComunicationNet.ProtocolRow>, IProtocolUI
  {
    #region private
    private IDataProviderID m_DataProvider = null;
    private SortedList<short, IAddressSpaceDescriptor> m_DataProviderAddressSpaces = null;
    #endregion
    #region Constructor
    /// <summary>
    /// Creats new <see cref="ProtocolAndSerialRowWrapper"/> based on specified Protocol nad Serial rows
    /// </summary>
    /// <param name="protocol">Protocol row</param>
    public ProtocolAndSerialRowWrapper( ComunicationNet.ProtocolRow protocol )
      : base( protocol )
    {
      if ( !protocol.IsDPIdentifierNull() )
      {
        CAS.Lib.CommonBus.Management.PluginCollection cPC = new CAS.Lib.CommonBus.Management.PluginCollection( m_CommonBusControl );
        try
        {
          m_DataProvider = cPC[ protocol.DPIdentifier ];
          m_DataProvider.SetSettings( protocol.DPConfig );
        }
        catch ( Exception )
        {
          MessageBox.Show( "One of the Data Provider cannot be loaded: \n\r " + protocol.DPConfig );
        }
      }
    }
    #endregion
    #region public
    internal SortedList<short, IAddressSpaceDescriptor> DataProviderAddressSpaces
    {
      get
      {
        if ( m_DataProviderAddressSpaces == null && m_DataProvider != null )
        {
          m_DataProviderAddressSpaces = new SortedList<short, IAddressSpaceDescriptor>();
          foreach ( IAddressSpaceDescriptor asd in m_DataProvider.GetAvailiableAddressspaces() )
          {
            m_DataProviderAddressSpaces.Add( asd.Identifier, asd );
          }
        }
        return m_DataProviderAddressSpaces;
      }
    }
    #endregion
    #region Properties for PropertyGrid
    [
    DisplayName( "Data Provider" ),
    BrowsableAttribute( true ),
    CategoryAttribute( "Protocol: Global Settings" ),
    DescriptionAttribute( "Data provider and underlying communication layer settings." ),
    EditorAttribute( typeof( ProtocolAndSerialRowWrapper.ProtocolUI ), typeof( System.Drawing.Design.UITypeEditor ) ),
    TypeConverterAttribute( typeof( ExpandableObjectConverter ) )
    ]
    public IDataProviderID DataProvider
    {
      get { return m_DataProvider; }
      set
      {
        m_DataProvider = value;
        m_Parent.DPIdentifier = m_DataProvider.GetDataProviderDescription.Identifier;
        m_Parent.DPConfig = m_DataProvider.GetSettings();
      }
    }
    #region Protocol settings
    /// <summary>
    /// Gets protocol unique numerical identifier
    /// </summary>
    [
    BrowsableAttribute( false ),
    CategoryAttribute( "XXX debug" ),
    DefaultValueAttribute( 0 ),
    DescriptionAttribute( "Protocol unique numerical identifier" )
    ]
    public long ProtocolID
    {
      get
      {
        return ( m_Parent.ProtocolID );
      }
    }
    /// <summary>
    /// Gets or sets human readable protocol name
    /// </summary>
    [
    BrowsableAttribute( true ),
    CategoryAttribute( "Protocol: Global Settings" ),
    DescriptionAttribute( "Human readable protocol name." )
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
    /// Gets channel unique numerical identifier (name of the channel)
    /// </summary>
    [
#if DEBUG
BrowsableAttribute( true ),
CategoryAttribute( "XXX debug" ),
#else
    BrowsableAttribute( false ),
    CategoryAttribute( "General Settings" ),
#endif
 DescriptionAttribute( "Channel unique numerical identifier (name of the channel)" )
    ]
    public long ChannelID
    {
      get
      {
        return ( m_Parent.ChannelID );
      }
    }
    ///// <summary>
    ///// Gets or sets maximal number of retries getting the response.
    ///// </summary>
    //[
    //BrowsableAttribute( true ),
    //CategoryAttribute( "Protocol: Time Settings" ),
    //DefaultValueAttribute( 3 ),
    //DescriptionAttribute( "Maximal number of retries getting the response. After passing this limit the base station finds that communication with the device using current interface coupled with this protocol is broken and activates next interface in the pipe if available. The failed interface is marked inactive and will remain in this state until defined InactTimeAFailure timeout expiration." )
    //]
    //public long MaxNumberOfRetries
    //{
    //  get
    //  {
    //    return ( m_Parent1.MaxNumberOfRetries );
    //  }
    //  set { m_Parent1.MaxNumberOfRetries = value; }
    //}
    #endregion
    #endregion properties for PropertyGrid
    #region Overrides
    #region object override
    /// <summary>
    /// Override to string method 
    /// </summary>
    /// <returns>Protocol identifier, protocol name and serial number in specified string format </returns>
    public override string ToString()
    {
      return String.Format( "Protocol: {0}[{1}]", Name, m_DataProvider == null ? "Not set" : m_DataProvider.Title );
    }
    #endregion
    #region Action Overrides
    /// <summary>
    /// Creates new segment row
    /// </summary>
    /// <returns><see cref="SegmentsRowWrapper"/> object</returns>
    public override IAction CreateNewChildObject()
    {
      ComunicationNet.SegmentsDataTable dt = ( (ComunicationNet)( m_Parent.Table.DataSet ) ).Segments;
      return new SegmentsRowWrapper( dt.NewSegmentsRow( m_Parent.ProtocolID, Name ), this );
    }
    /// <summary>
    /// Creates segments rows in treeview
    /// </summary>
    public override void CreateNodes()
    {
      base.CreateNodes();
      foreach ( ComunicationNet.SegmentsRow curr in m_Parent.GetSegmentsRows() )
        if ( curr.RowState != DataRowState.Deleted )
        {
          SegmentsRowWrapper newWrapper = new SegmentsRowWrapper( curr, this );
          newWrapper.AddActionTreeNode( m_Node, 17, 17 );
          newWrapper.CreateNodes();
        }
    }
    public override void PasteChildObject( IAction objToPaste )
    {
      if ( objToPaste is SegmentsRowWrapper )
      {
        SegmentsRowWrapper wrapperToPaste = objToPaste as SegmentsRowWrapper;
        ComunicationNet.SegmentsDataTable st = ( (ComunicationNet)( m_Parent.Table.DataSet ) ).Segments;
        st.NewSegmentsRow( m_Parent.ProtocolID, wrapperToPaste.DataRow, Name );
      }
    }
    /// <summary>
    /// Checks if specified <see cref="IAction"></see> interface is <see cref="SegmentsRowWrapper"></see>
    /// </summary>
    /// <param name="objToPaste">Object to check</param>
    /// <returns>True if specified object is <see cref="SegmentsRowWrapper"/></returns>
    public override bool CanBePastedAsChild( IAction objToPaste ) { return objToPaste is SegmentsRowWrapper; }
    /// <summary>
    /// Checks if specified <see cref="IAction"/> object can be moved 
    /// </summary>
    /// <returns>True if specified object can be moved </returns>
    public override bool CanBeMoved() { return true; }
    /// <summary>
    /// Moves specified <see cref="SegmentsRowWrapper"/> object under this wrapper
    /// </summary>
    /// <param name="objToPaste"><see cref="SegmentsRowWrapper"/> object to be pasted</param>
    public override void MoveChildObject( IAction objToPaste )
    {
      SegmentsRowWrapper wrapperToPaste = objToPaste as SegmentsRowWrapper;
      if ( wrapperToPaste == null )
        return;
      ComunicationNet.SegmentsRow rowToPaste = wrapperToPaste.DataRow;
      rowToPaste.BeginEdit();
      rowToPaste.ProtocolID = m_Parent.ProtocolID;
      rowToPaste.EndEdit();
    }
    /// <summary>
    /// Inform the object that some values have been changed.
    /// </summary>
    public override void HasChanged()
    {
      m_Parent.DPConfig = m_DataProvider.GetSettings();
      m_Parent.DPIdentifier = m_DataProvider.GetDataProviderDescription.Identifier;
      base.HasChanged();
    }
    #endregion
    #endregion
    #region TypeConverter & UITypeEditor classes
    private class ProtocolUI: UITypeEditor
    {
      private IDataProviderID DPSelectConfig( IProtocolUI m_Wrraper, IWindowsFormsEditorService cProvider )
      {
        IDataProviderID cDataProviderID;
        IDataProviderID cPreviousDP = m_Wrraper.GetCurrentDP;
        using ( OKCancelForm okcan = new OKCancelForm( "Data Provider Configuration" ) )
        {
          AvailableDPTree c_AvailableDPTree;
          if ( cPreviousDP == null )
            c_AvailableDPTree = new AvailableDPTree( m_Wrraper.GetCommonBusControl, okcan );
          else
          {
            string cSettings = cPreviousDP.GetSettings();
            Guid cPreviousDPIdent = cPreviousDP.GetDataProviderDescription.Identifier;
            c_AvailableDPTree = new AvailableDPTree( m_Wrraper.GetCommonBusControl, okcan, cSettings, cPreviousDPIdent );
          }
          okcan.SetUserControl = c_AvailableDPTree;
          using ( c_AvailableDPTree )
          {
            cProvider.ShowDialog( okcan );
            if ( okcan.DialogResult != DialogResult.OK )
              return cPreviousDP;
            cDataProviderID = c_AvailableDPTree.GetSelectedDPID;
          }
        }
        using ( AddObject<IDataProviderID> c_DPSettings = new AddObject<IDataProviderID>() )
        {
          c_DPSettings.Object = cDataProviderID;
          cProvider.ShowDialog( c_DPSettings );
          if ( c_DPSettings.DialogResult != DialogResult.OK )
            return cPreviousDP;
        }
        return cDataProviderID;
      }
      public override object EditValue( ITypeDescriptorContext context, IServiceProvider provider, object value )
      {
        IWindowsFormsEditorService cProvider = provider.GetService( typeof( IWindowsFormsEditorService ) ) as IWindowsFormsEditorService;
        return DPSelectConfig( context.Instance as IProtocolUI, cProvider );
      }
      public override UITypeEditorEditStyle GetEditStyle( ITypeDescriptorContext context )
      {
        return UITypeEditorEditStyle.Modal;
      }
    }
    #endregion
    #region IProtocolUI Members
    CommonBusControl IProtocolUI.GetCommonBusControl
    {
      get { return m_CommonBusControl; }
    }
    IDataProviderID IProtocolUI.GetCurrentDP
    {
      get { return m_DataProvider; }
    }
    #endregion
  }
}