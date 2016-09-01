//<summary>
//  Title   : C# Net Server Subscription
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20071007: mzbrzezny  - DateTimeProvider is use instead of DateTime.Now or UtcNow
//    Maciej Zbrzezny - 12-04-2006
//    zmieniono by czas by³ Now a nie Utc Now,  dodano inicjalizacje commservera - a dokladniej modulu odpowiedzialnego ze poprzednio za obsluge okna
//    M.Postol - 2005
//    created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>
//============================================================================
// TITLE: Cache.cs
//
// CONTENTS:
// 
// A shared cache of item values stored in a data access server. 
//
// (c) Copyright 2003 The OPC Foundation
// ALL RIGHTS RESERVED.
//
// DISCLAIMER:
//  This code is provided by the OPC Foundation solely to assist in 
//  understanding and use of the appropriate OPC Specification(s) and may be 
//  used as set forth in the License Grant section of the OPC Specification.
//  This code is provided as-is and without warranty or support of any sort
//  and is subject to the Warranty and Liability Disclaimers which appear
//  in the printed OPC Specification.
//
// MODIFICATION LOG:
//
// Date       By    Notes
// ---------- ---   -----
// 2003/03/26 RSA   Initial implementation.

using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using CAS.Lib.DeviceSimulator;
using CAS.Lib.RTLib.Utils;
using Opc;
using Opc.Da;
using CAS.CommServer.ProtocolHub.Communication;

namespace CAS.OpcSvr.Da.NETServer
{
  /// <summary>
  /// An implementation of a Data Access server.
  /// </summary>
  [SecurityPermissionAttribute( SecurityAction.Demand, SerializationFormatter = true )]
  [ComVisible( false )]
  internal class Cache: ICacheServer, IDisposable
  {
    #region Public Members
    /// <summary>
    /// The maximum rate at which items may be scaned.
    /// </summary>
    internal const int MAX_UPDATE_RATE = 100;
    /// <summary>
    /// Returns the closest supported update rate.
    /// </summary>
    public static int AdjustUpdateRate( int updateRate )
    {
      if ( updateRate % MAX_UPDATE_RATE != 0 )
      {
        return ( updateRate / MAX_UPDATE_RATE + 1 ) * MAX_UPDATE_RATE;
      }
      return ( updateRate > 0 ) ? updateRate : MAX_UPDATE_RATE;
    }
    /// <summary>
    /// Initializes the object.
    /// </summary>
    public Cache( bool supportsCOM )
    {
      m_supportsCOM = supportsCOM;
    }
    /// <summary>
    /// Removes a reference to the cache.
    /// </summary>
    internal int Release()
    {
      lock ( this )
      {
        if ( m_disposed )
          throw new ObjectDisposedException( "Opc.Da.Cache" );
        return --m_refs;
      }
    }
    #endregion
    #region ICacheServer Members
    /// <summary>
    /// The set of locales supported by the server.
    /// </summary>
    string[] ICacheServer.SupportedLocales
    {
      get { return new string[] { "en-US", "fr-FR", "de-DE", "pl-PL" }; }
    }
    /// <summary>
    /// Initializes the cache when the first server is created.
    /// </summary>
    void ICacheServer.Initialize()
    {
      lock ( this )
      {
        if ( m_disposed )
          throw new ObjectDisposedException( "Opc.Da.Cache" );

        // create the resource manager.
        m_resourceManager = new ResourceManager( "OpcDa.Resources.Strings", Assembly.GetExecutingAssembly() );

        // initialize status.
        m_status = new ServerStatus();

        m_status.VendorInfo = ( (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute( Assembly.GetExecutingAssembly(), typeof( AssemblyDescriptionAttribute ) ) ).Description;
        m_status.ProductVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        m_status.ServerState = serverState.running;
        m_status.StatusInfo = serverState.running.ToString();
        m_status.StartTime = DateTimeProvider.GetCurrentTime();
        m_status.LastUpdateTime = DateTime.MinValue;
        m_status.CurrentTime = DateTimeProvider.GetCurrentTime();
        CommServerComponent form = new CommServerComponent(); //to spowoduje inicjalizacje danych i interfejsu
        form.Initialize( CAS.Lib.RTLib.Management.AppConfigManagement.filename );
        m_Device = new Device();
        BuildAddressSpace( m_Device, m_Device.GetIndexedAddressSpace );

        // MP Moved to CAS.OpcSvr.Da.NETServer.Subscription
        // start the cache thread.
        // ThreadPool.QueueUserWorkItem(new WaitCallback(OnUpdate));
      }
    }
    /// <summary>
    /// Returns a localized string with the specified name.
    /// </summary>
    string ICacheServer.GetString( string name, string locale )
    {
      // create a culture object.
      CultureInfo culture = null;

      try { culture = new CultureInfo( locale ); }
      catch { culture = new CultureInfo( "" ); }

      // lookup resource string.
      try { return m_resourceManager.GetString( name, culture ); }
      catch { return name; }
    }
    /// <summary>
    /// Adds a reference to the cache.
    /// </summary>
    int ICacheServer.AddRef()
    {
      lock ( this )
      {
        if ( m_disposed )
          throw new ObjectDisposedException( "Opc.Da.Cache" );
        return ++m_refs;
      }
    }
    /// <summary>
    /// Copies the current status into object passed in.
    /// </summary>
    Opc.Da.ServerStatus ICacheServer.GetStatus()
    {
      lock ( this )
      {
        if ( m_disposed )
          throw new ObjectDisposedException( "Opc.Da.Cache" );

        Opc.Da.ServerStatus status = new Opc.Da.ServerStatus();

        status.VendorInfo = m_status.VendorInfo;
        status.ProductVersion = m_status.ProductVersion;
        status.ServerState = m_status.ServerState;
        status.StatusInfo = m_status.StatusInfo;
        status.StartTime = m_status.StartTime;
        status.LastUpdateTime = m_status.LastUpdateTime;
        status.CurrentTime = DateTimeProvider.GetCurrentTime();

        return status;
      }
    }
    /// <summary>
    /// Returns the set of elements in the address space that meet the specified criteria.
    /// </summary>
    public Opc.Da.BrowseElement[] Browse(
      ItemIdentifier itemID,
      BrowseFilters filters,
      ref Opc.Da.BrowsePosition position )
    {
      lock ( this )
      {
        if ( m_disposed )
          throw new ObjectDisposedException( "Opc.Da.Cache" );

        BrowseElement element = m_addressSpace;

        // find desired element.
        string browsePath = ( itemID != null ) ? itemID.ItemName : null;

        if ( browsePath != null && browsePath.Length > 0 )
        {
          element = m_addressSpace.Find( browsePath );

          if ( element == null )
          {
            // check if browsing a property item.
            PropertyID property = Property.VALUE;

            string rootID = LookupProperty( browsePath, out property );

            if ( rootID != null )
            {
              element = m_addressSpace.Find( rootID );
            }

            if ( element == null )
            {
              throw new ResultIDException( ResultID.Da.E_UNKNOWN_ITEM_NAME );
            }

            // property items never contain children.
            return new Opc.Da.BrowseElement[ 0 ];
          }
        }

        // check if no elements exist.
        if ( element.Count == 0 )
        {
          return new Opc.Da.BrowseElement[ 0 ];
        }

        // determine start position.
        int start = 0;

        if ( position != null )
        {
          start = ( (BrowsePosition)position ).Index;
          position.Dispose();
          position = null;
        }

        // process child nodes.
        ArrayList results = new ArrayList();

        for ( int ii = start; ii < element.Count; ii++ )
        {
          BrowseElement child = element.Child( ii );

          // exclude elements without children.
          if ( filters.BrowseFilter == browseFilter.branch && child.Count == 0 )
          {
            continue;
          }

          // check if an element is an item.
          CacheItem item = (CacheItem)m_items[ child.ItemID ];

          // exclude elements which are not items.
          if ( filters.BrowseFilter == browseFilter.item && item == null )
          {
            continue;
          }

          // apply name filter (using the SQL LIKE operator).
          if ( filters.ElementNameFilter != null && filters.ElementNameFilter.Length > 0 )
          {
            if ( !Opc.Convert.Match( child.Name, filters.ElementNameFilter, true ) )
            {
              continue;
            }
          }

          // add element to list of results.
          Opc.Da.BrowseElement result = new Opc.Da.BrowseElement();

          result.Name = child.Name;
          result.ItemName = child.ItemID;
          result.ItemPath = null;
          result.IsItem = item != null;
          result.HasChildren = child.Count > 0;
          result.Properties = null;

          // add properties to results.
          if ( filters.ReturnAllProperties || filters.PropertyIDs != null )
          {
            result.Properties = GetProperties( item, ( filters.ReturnAllProperties ) ? null : filters.PropertyIDs, filters.ReturnPropertyValues );
          }

          results.Add( result );

          // check if max elements exceeded.
          if ( filters.MaxElementsReturned > 0 && results.Count >= filters.MaxElementsReturned )
          {
            if ( ii + 1 < element.Count )
            {
              position = new BrowsePosition( itemID, filters );
              ( (BrowsePosition)position ).Index = ii + 1;
            }

            break;
          }
        }
        // return results.
        return (Opc.Da.BrowseElement[])results.ToArray( typeof( Opc.Da.BrowseElement ) );
      }
    }
    /// <summary>
    /// Fetches the specified properties for an item.
    /// </summary>
    ItemPropertyCollection ICacheServer.GetProperties(
      ItemIdentifier itemID,
      PropertyID[] propertyIDs,
      bool returnValues )
    {
      if ( itemID == null )
        throw new ArgumentNullException( "itemID" );

      if ( m_disposed )
        throw new ObjectDisposedException( "Opc.Da.Cache" );

      ItemPropertyCollection properties = new ItemPropertyCollection();

      properties.ItemName = itemID.ItemName;
      properties.ItemPath = itemID.ItemPath;
      properties.ResultID = ResultID.S_OK;
      properties.DiagnosticInfo = null;

      if ( itemID.ItemName == null || itemID.ItemName.Length == 0 )
      {
        properties.ResultID = ResultID.Da.E_INVALID_ITEM_NAME;
        return properties;
      }

      // find the item.
      CacheItem item = GetItemFromItemsTableByItemName( itemID );

      if ( item == null )
      {
        properties.ResultID = ResultID.Da.E_UNKNOWN_ITEM_NAME;
        return properties;
      }

      // get the properties.
      if ( propertyIDs == null )
      {
        properties = item.GetAvailableProperties( returnValues );
      }
      else
      {
        properties = item.GetAvailableProperties( propertyIDs, returnValues );
      }

      // return result.
      return properties;

    }


    /// <summary>
    /// Checks whether the item id is valid.
    /// </summary>
    bool ICacheServer.IsValidItem( string itemID )
    {
      lock ( this )
      {
        return ( LookupItem( itemID ) != null );
      }
    }
    /// <summary>
    /// Reads the value from the cache.
    /// </summary>
    ItemValueResult ICacheServer.Read( string itemID, string locale, System.Type reqType, int maxAge )
    {
      if ( m_disposed )
        throw new ObjectDisposedException( "Opc.Da.Cache" );

      if ( itemID == null || itemID.Length == 0 )
      {
        return new ItemValueResult( itemID, ResultID.Da.E_INVALID_ITEM_NAME );
      }

      CacheItem item = LockAndLookupItem( itemID );

      if ( item == null )
      {
        return new ItemValueResult( itemID, ResultID.Da.E_UNKNOWN_ITEM_NAME );
      }

      return item.Read( locale, reqType, maxAge, m_supportsCOM );
    }
    /// <summary>
    /// Reads the value of the specified property.
    /// </summary>
    object ICacheServer.ReadProperty( string itemID, PropertyID propertyID )
    {
      if ( m_disposed )
        throw new ObjectDisposedException( "Opc.Da.Cache" );

      CacheItem item = LockAndLookupItem( itemID );

      if ( item == null )
      {
        return null;
      }

      return item.ReadProperty( propertyID );
    }

    /// <summary>
    /// Writes a value to the device.
    /// </summary>
    IdentifiedResult ICacheServer.Write( string itemID, string locale, ItemValue value )
    {
      if ( m_disposed )
        throw new ObjectDisposedException( "Opc.Da.Cache" );

      if ( itemID == null || itemID.Length == 0 )
      {
        return new IdentifiedResult( itemID, ResultID.Da.E_INVALID_ITEM_NAME );
      }

      CacheItem item = LockAndLookupItem( itemID );

      if ( item == null )
      {
        return new IdentifiedResult( itemID, ResultID.Da.E_UNKNOWN_ITEM_NAME );
      }

      return item.Write( locale, value, m_supportsCOM );
    }
    #endregion
    #region IDisposable Members
    /// <summary>
    /// Stops all threads and releases all resources.
    /// </summary>
    public void Dispose()
    {
      lock ( this )
      {
        m_disposed = true;
      }
    }
    #endregion
    #region Private Members
    ///// <summary>
    ///// Adds an item without a link to the address space.
    ///// </summary>
    //    private bool AddItem(string itemID, IDevice device)
    //    {
    //      lock (this)
    //      {
    //        // validate item id.
    //        if (itemID == null || itemID.Length == 0)
    //        {
    //          return false;
    //        }
    //
    //        // check if item already exists.
    //        if (m_items.Contains(itemID))
    //        {
    //          return false;
    //        }
    //
    //        // create new item and index by item id.
    //        m_items[itemID] = new CacheItem(itemID, device);
    //		
    //        return true;
    //      }
    //    }

    /// <summary>
    /// Removes an item from address space.
    /// </summary>
    private bool RemoveItem( string itemID )
    {
      lock ( this )
      {
        // validate item id.
        if ( itemID == null || itemID.Length == 0 )
        {
          return false;
        }

        // check if item already exists.
        if ( !m_items.Contains( itemID ) )
        {
          return false;
        }

        // remove item.
        m_items.Remove( itemID );

        return true;
      }
    }

    /// <summary>
    /// Adds an link and an item with the same id to the address space.
    /// </summary>
    private bool AddItemAndLink( string browsePath, ushort itemIndex, IDeviceIndexed device )
    {
      lock ( this )
      {
        // validate browse path.
        if ( browsePath == null || browsePath.Length == 0 )
        {
          return false;
        }

        // check if item does not exists.
        if ( m_items.Contains( browsePath ) )
        {
          return false;
        }

        // create the browse element.
        BrowseElement element = m_addressSpace.Insert( browsePath );

        if ( element == null )
        {
          return false;
        }

        // create new item and index by item id.
        m_items[ element.ItemID ] = new CacheItem( browsePath, itemIndex, device );
        return true;
      }
    }

    //    /// <summary>
    //    /// Adds an link with an item id to the address space.
    //    /// </summary>
    //    private bool AddItemAndLink(string browsePath, string itemID, IDevice device)
    //    {
    //      lock (this)
    //      {
    //        // validate browse path.
    //        if (browsePath == null || browsePath.Length == 0)
    //        {
    //          return false;
    //        }
    //
    //        // check if item does not exists.
    //        //MPTD removed ! check it
    //        if (m_items.Contains(itemID))
    //        {
    //          return false;
    //        }
    //
    //        // create the browse element.
    //        BrowseElement element = m_addressSpace.Insert(browsePath, itemID);
    //
    //        if (element == null)
    //        {
    //          return false;
    //        }
    //
    //        // create new item and index by item id.
    //        m_items[element.ItemID] = new CacheItem(itemID, device);
    //        return true;
    //      }
    //    }

    /// <summary>
    /// Removes an link and an item with the same id to the address space.
    /// </summary>
    private bool RemoveItemAndLink( string browsePath )
    {
      lock ( this )
      {
        // validate browse path.
        if ( browsePath == null || browsePath.Length == 0 )
        {
          return false;
        }

        // find the browse element.
        BrowseElement element = m_addressSpace.Find( browsePath );

        if ( element != null )
        {
          // remove item.
          m_items.Remove( element.ItemID );

          // remove element.
          element.Remove();
        }

        return true;
      }
    }

    /// <summary>
    /// Adds an link without an item to the address space.
    /// </summary>
    private bool AddLink( string browsePath )
    {
      lock ( this )
      {
        // validate browse path.
        if ( browsePath == null || browsePath.Length == 0 )
        {
          return false;
        }

        // create the browse element.
        BrowseElement element = m_addressSpace.Insert( browsePath );

        if ( element == null )
        {
          return false;
        }

        return true;
      }
    }

    /// <summary>
    /// Removes an link (but not the item) from the address space.
    /// </summary>
    private bool RemoveLink( string browsePath )
    {
      lock ( this )
      {
        // validate browse path.
        if ( browsePath == null || browsePath.Length == 0 )
        {
          return false;
        }

        // create the browse element.
        BrowseElement element = m_addressSpace.Find( browsePath );

        if ( element != null )
        {
          element.Remove();
          return true;
        }

        return false;
      }
    }

    /// <summary>
    /// Removes an link (but not the item) from the address space only if it has no children.
    /// </summary>
    private bool RemoveEmptyLink( string browsePath )
    {
      lock ( this )
      {
        // validate browse path.
        if ( browsePath == null || browsePath.Length == 0 )
        {
          return false;
        }

        // create the browse element.
        BrowseElement element = m_addressSpace.Find( browsePath );

        if ( element != null )
        {
          if ( element.Count == 0 )
          {
            element.Remove();
            return true;
          }
        }

        return false;
      }
    }
    /// <summary>
    /// Adds the address space for the device to the cache.
    /// </summary>
    private void BuildAddressSpace( IDeviceIndexed device, ItemDsc[] browsePath )
    {
      lock ( this )
        foreach ( ItemDsc item in browsePath )
          AddItemAndLink( item.itemID, item.itemIdx, device );
    }
    private int m_refs = 0;
    private bool m_disposed = false;
    private ResourceManager m_resourceManager = null;
    private static Opc.Da.ServerStatus m_status = null;
    private Hashtable m_items = new Hashtable();
    private BrowseElement m_addressSpace = new BrowseElement( null, null );
    private bool m_supportsCOM = false;

    /// <summary>
    /// Splits an item identfier into its component parts.
    /// </summary>
    private bool ParseItemID( string itemID, out string baseItemID, out PropertyID propertyID )
    {
      // set default values.
      baseItemID = itemID;
      propertyID = Property.VALUE;

      // validate item id.
      if ( itemID == null || itemID.Length == 0 )
      {
        return false;
      }

      // check for a property id qualifier.
      int index = itemID.LastIndexOf( ":" );

      if ( index == -1 )
      {
        return true;
      }

      // extract base item id.
      baseItemID = itemID.Substring( 0, index );

      // parse property id.
      try
      {
        int code = System.Convert.ToInt32( itemID.Substring( index + 1 ) );
        propertyID = new PropertyID( code );
      }
      catch
      {
        return false;
      }

      // item syntax is valid.
      return true;
    }

    /// <summary>
    /// Fetches the specified properties for an item.
    /// </summary>
    private Opc.Da.ItemProperty[] GetProperties(
      CacheItem item,
      PropertyID[] propertyIDs,
      bool returnValues )
    {
      // check for trivial case.
      if ( item == null )
      {
        return null;
      }

      // fetch properties.
      ItemPropertyCollection properties = null;

      if ( propertyIDs == null )
      {
        properties = item.GetAvailableProperties( returnValues );
      }
      else
      {
        properties = item.GetAvailableProperties( propertyIDs, returnValues );
      }

      // convert collection to array.
      return (Opc.Da.ItemProperty[])properties.ToArray( typeof( Opc.Da.ItemProperty ) );
    }
    private CacheItem GetItemFromItemsTableByItemName( ItemIdentifier itemID )
    {
      lock ( this )
      {
        CacheItem item = (CacheItem)m_items[ itemID.ItemName ];
        return item;
      }
    }
    /// <summary>
    /// Looks up the cache item for the specified item id.
    /// </summary>
    private CacheItem LookupItem( string itemID )
    {
      int index = itemID.LastIndexOf( ":" );

      if ( index == -1 )
      {
        return (CacheItem)m_items[ itemID ];
      }

      return (CacheItem)m_items[ itemID.Substring( 0, index ) ];
    }
    private CacheItem LockAndLookupItem( string itemID )
    {
      CacheItem item = null;
      lock ( this )
      {
        item = LookupItem( itemID );
      }
      return item;
    }
    /// <summary>
    /// Extracts the property id for the specified item id.
    /// </summary>
    private string LookupProperty( string itemID, out PropertyID propertyID )
    {
      propertyID = Property.VALUE;

      // look for property delimiter.
      int index = itemID.LastIndexOf( ":" );

      if ( index == -1 )
      {
        return itemID;
      }

      // lookup property id.
      try
      {
        propertyID = new PropertyID( System.Convert.ToInt32( itemID.Substring( index + 1 ) ) );
      }
      catch
      {
        return null;
      }

      // return root item id.
      return itemID.Substring( 0, index );
    }
    private IDeviceIndexed m_Device = null;
    #endregion
  }//Cache
  /// <summary>
  /// Interface between Cache and Server
  /// </summary>
  internal interface ICacheServer
  {
    /// <summary>
    /// Adds a reference to the cache.
    /// </summary>
    int AddRef();
    /// <summary>
    /// The set of locales supported by the server.
    /// </summary>
    string[] SupportedLocales { get; }
    /// <summary>
    /// Returns a localized string with the specified name.
    /// </summary>
    string GetString( string name, string locale );
    /// <summary>
    /// Fetches the specified properties for an item.
    /// </summary>
    ItemPropertyCollection GetProperties( ItemIdentifier itemID, PropertyID[] propertyIDs, bool returnValues );
    /// <summary>
    /// Fetches the specified properties for an item.
    /// </summary>
    Opc.Da.ServerStatus GetStatus();
    /// <summary>
    /// Reads the value from the cache.
    /// </summary>
    ItemValueResult Read( string itemID, string locale, System.Type reqType, int maxAge );
    /// <summary>
    /// Writes a value to the device.
    /// </summary>
    IdentifiedResult Write( string itemID, string locale, ItemValue value );
    /// <summary>
    /// Reads the value of the specified property.
    /// </summary>
    object ReadProperty( string itemID, PropertyID propertyID );
    /// <summary>
    /// Returns the set of elements in the address space that meet the specified criteria.
    /// </summary>
    Opc.Da.BrowseElement[] Browse( ItemIdentifier itemID, BrowseFilters filters, ref Opc.Da.BrowsePosition position );
    /// <summary>
    /// Initializes the cache when the first server is created.
    /// </summary>
    void Initialize();
    /// <summary>
    /// Checks whether the item id is valid.
    /// </summary>
    bool IsValidItem( string itemID );
  }
}
