//_______________________________________________________________
//  Title   : ICacheServer
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C) 2017, CAS LODZ POLAND.
//  TEL: +48 608 61 98 99 
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using Opc;
using Opc.Da;

namespace CAS.OpcSvr.Da.NETServer
{
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
    string GetString(string name, string locale);
    /// <summary>
    /// Fetches the specified properties for an item.
    /// </summary>
    ItemPropertyCollection GetProperties(ItemIdentifier itemID, PropertyID[] propertyIDs, bool returnValues);
    /// <summary>
    /// Fetches the specified properties for an item.
    /// </summary>
    Opc.Da.ServerStatus GetStatus();
    /// <summary>
    /// Reads the value from the cache.
    /// </summary>
    ItemValueResult Read(string itemID, string locale, System.Type reqType, int maxAge);
    /// <summary>
    /// Writes a value to the device.
    /// </summary>
    IdentifiedResult Write(string itemID, string locale, ItemValue value);
    /// <summary>
    /// Reads the value of the specified property.
    /// </summary>
    object ReadProperty(string itemID, PropertyID propertyID);
    /// <summary>
    /// Returns the set of elements in the address space that meet the specified criteria.
    /// </summary>
    Opc.Da.BrowseElement[] Browse(ItemIdentifier itemID, BrowseFilters filters, ref Opc.Da.BrowsePosition position);
    /// <summary>
    /// Initializes the cache when the first server is created.
    /// </summary>
    void Initialize();
    /// <summary>
    /// Checks whether the item id is valid.
    /// </summary>
    bool IsValidItem(string itemID);
  }
}
