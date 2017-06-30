//<summary>
//  Title   : C# Net Server - Server
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20071007: mzbrzezny  - DateTimeProvider is use instead of DateTime.Now or UtcNow
//    Maciej Zbrzezny - 12-04-2006
//    zmieniono by serwer zwracal czas Now , a nie UtcNow 
//    M.Postol - 2005
//    created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
//============================================================================
// TITLE: Server.cs
//
// CONTENTS:
// 
// An in-process wrapper for a remote OPC XML-DA server (not thread safe).
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

using CAS.Lib.RTLib.Utils;
using Opc;
using Opc.Da;
using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

//using System.Security.Permissions;

namespace CAS.OpcSvr.Da.NETServer
{
  /// <summary>
  /// An implementation of a Data Access server.
  /// </summary>
  [ComVisible(false)]
  // Set the SerializationFormatter property.
  public class Server : Opc.Da.IServer
  {
    #region Public Members
    //======================================================================
    // Construction

    /// <summary>
    /// Initializes the object.
    /// </summary>d
    public Server(bool supportsCOM)
    {
      Initialize(supportsCOM);
      m_ThisInstanceCount++;
    }
    /// <summary>
    /// Initializes a server instance after it is created.
    /// </summary>
    public virtual void Initialize(bool supportsCOM)
    {
      lock (this)
      {
        // initialize the cache.
        if (TheCacheSingleton == null)
        {
          TheCacheSingleton = new Cache(supportsCOM);
          TheCacheSingleton.Initialize();
        }
        m_cache = TheCacheSingleton;
        m_cache.AddRef();

        // get the default locale.
        SetLocale(m_cache.SupportedLocales[0]);

      }
    }
    #endregion

    #region IDisposable Members
    /// <summary>
    /// This must be called explicitly by clients to ensure the COM server is released.
    /// </summary>
    public void Dispose()
    {
      m_ThisInstanceCount--;
      if (m_ThisInstanceCount != 0)
        return;
      TheCacheSingleton.Dispose();
    }
    #endregion

    #region Opc.IServer Members
    //======================================================================
    // Events

    /// <summary>
    /// An event to receive server shutdown notifications.
    /// </summary>
    public virtual event ServerShutdownEventHandler ServerShutdown;

    //======================================================================
    // Localization

    /// <summary>
    /// The locale used in any error messages or results returned to the client.
    /// </summary>
    /// <returns>The locale name in the format "[languagecode]-[country/regioncode]".</returns>
    public string GetLocale()
    {
      lock (this)
      {
        return m_locale;
      }
    }

    /// <summary>
    /// Sets the locale used in any error messages or results returned to the client.
    /// </summary>
    /// <param name="locale">The locale name in the format "[languagecode]-[country/regioncode]".</param>
    /// <returns>A locale that the server supports and is the best match for the requested locale.</returns>
    public string SetLocale(string locale)
    {
      lock (this)
      {
        m_locale = Opc.Da.Server.FindBestLocale(locale, m_cache.SupportedLocales);

        // set specific culture.
        m_culture = new CultureInfo((m_locale != null) ? m_locale : "en-US");

        if (m_culture.IsNeutralCulture)
        {
          foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
          {
            if (culture.Parent != null && culture.Parent.Name == m_culture.Name)
            {
              m_culture = culture;
              break;
            }
          }
        }

        return m_locale;
      }
    }

    /// <summary>
    /// Returns the locales supported by the server
    /// </summary>
    /// <remarks>The first element in the array must be the default locale for the server.</remarks>
    /// <returns>An array of locales with the format "[languagecode]-[country/regioncode]".</returns>
    public string[] GetSupportedLocales()
    {
      lock (this)
      {
        return (string[])Opc.Convert.Clone(m_cache.SupportedLocales);
      }
    }

    /// <summary>
    /// Returns the localized text for the specified result code.
    /// </summary>
    /// <param name="locale">The locale name in the format "[languagecode]-[country/regioncode]".</param>
    /// <param name="resultID">The result code identifier.</param>
    /// <returns>A message localized for the best match for the requested locale.</returns>
    public string GetErrorText(string locale, ResultID resultID)
    {
      lock (this)
      {
        // determine the best supported locale.
        string revisedLocale = Opc.Da.Server.FindBestLocale(locale, m_cache.SupportedLocales);

        // lookup the string.
        return m_cache.GetString(resultID.Name.Name, revisedLocale);
      }
    }
    #endregion

    #region Opc.Da.IServer Members
    //======================================================================
    // Result Filters

    /// <summary>
    /// Returns the filters applied by the server to any item results returned to the client.
    /// </summary>
    /// <returns>A bit mask indicating which fields should be returned in any item results.</returns>
    public int GetResultFilters()
    {
      lock (this)
      {
        return m_filters;
      }
    }

    /// <summary>
    /// Sets the filters applied by the server to any item results returned to the client.
    /// </summary>
    /// <param name="filters">A bit mask indicating which fields should be returned in any item results.</param>
    public void SetResultFilters(int filters)
    {
      lock (this)
      {
        m_filters = filters;
      }
    }

    //======================================================================
    // GetStatus

    /// <summary>
    /// Returns the current server status.
    /// </summary>
    /// <returns>The current server status.</returns>
    public ServerStatus GetStatus()
    {
      lock (this)
      {
        Opc.Da.ServerStatus status = m_cache.GetStatus();
        status.StatusInfo = m_cache.GetString(status.StatusInfo, m_culture.Name);
        status.LastUpdateTime = DateTimeProvider.GetCurrentTime();
        status.CurrentTime = DateTimeProvider.GetCurrentTime();

        return status;
      }
    }

    //======================================================================
    // Read

    /// <summary>
    /// Reads the current values for a set of items. 
    /// </summary>
    /// <param name="items">The set of items to read.</param>
    /// <returns>The results of the read operation for each item.</returns>
    public Opc.Da.ItemValueResult[] Read(Item[] items)
    {
      if (items == null) throw new ArgumentNullException("items");

      if (items.Length == 0)
      {
        return new Opc.Da.ItemValueResult[0];
      }

      lock (this)
      {
        ArrayList results = new ArrayList(items.Length);

        foreach (Item item in items)
        {
          ItemValueResult result = m_cache.Read(item.ItemName, m_culture.Name, item.ReqType, (item.MaxAgeSpecified) ? item.MaxAge : 0);

          if (result == null)
          {
            result = new ItemValueResult(item, ResultID.E_FAIL);
          }

          result.ClientHandle = item.ClientHandle;

          results.Add(result);
        }

        return (ItemValueResult[])results.ToArray(typeof(ItemValueResult));
      }
    }

    //======================================================================
    // Write

    /// <summary>
    /// Writes the value, quality and timestamp for a set of items.
    /// </summary>
    /// <param name="items">The set of item values to write.</param>
    /// <returns>The results of the write operation for each item.</returns>
    public IdentifiedResult[] Write(ItemValue[] items)
    {
      if (items == null) throw new ArgumentNullException("items");

      if (items.Length == 0)
      {
        return new Opc.IdentifiedResult[0];
      }

      lock (this)
      {
        ArrayList results = new ArrayList(items.Length);

        foreach (ItemValue item in items)
        {
          IdentifiedResult result = m_cache.Write(item.ItemName, m_culture.Name, item);

          if (result == null)
          {
            result = new IdentifiedResult(item, ResultID.E_FAIL);
          }

          result.ClientHandle = item.ClientHandle;

          results.Add(result);
        }

        return (IdentifiedResult[])results.ToArray(typeof(IdentifiedResult));
      }
    }

    //======================================================================
    // CreateSubscription

    /// <summary>
    /// Creates a new subscription.
    /// </summary>
    /// <param name="state">The initial state of the subscription.</param>
    /// <returns>The new subscription object.</returns>
    public ISubscription CreateSubscription(Opc.Da.SubscriptionState state)
    {
      if (state == null) throw new ArgumentNullException("state");

      lock (this)
      {
        Opc.Da.SubscriptionState copy = (Opc.Da.SubscriptionState)state.Clone();

        // assign unique handle.
        copy.ServerHandle = m_nextHandle++;

        // assign name.
        if (copy.Name == null || copy.Name.Length == 0)
        {
          copy.Name = String.Format("Subscription{0,2:00}", copy.ServerHandle);
        }

        // create subscription.
        Subscription subscription = new Subscription(m_cache, this, copy);

        // save reference.
        m_subscriptions[copy.ServerHandle] = subscription;

        // return new subscription.
        return subscription;
      }
    }

    //======================================================================
    // CancelSubscription

    /// <summary>
    /// Creates a new instance of the appropriate subcription object.
    /// </summary>
    /// <param name="subscription">The remote subscription object.</param>
    public void CancelSubscription(ISubscription subscription)
    {
      if (subscription == null) throw new ArgumentNullException("subscription");

      lock (this)
      {
        foreach (ISubscription target in m_subscriptions.Values)
        {
          if (target == subscription)
          {
            m_subscriptions.Remove(target);
            subscription.Dispose();
            break;
          }
        }
      }
    }

    //======================================================================
    // Browse

    /// <summary>
    /// Fetches the children of a branch that meet the filter criteria.
    /// </summary>
    /// <param name="itemID">The identifier of branch which is the target of the search.</param>
    /// <param name="filters">The filters to use to limit the set of child elements returned.</param>
    /// <param name="position">An object used to continue a browse that could not be completed.</param>
    /// <returns>The set of elements found.</returns>
    public Opc.Da.BrowseElement[] Browse(
      ItemIdentifier itemID,
      BrowseFilters filters,
      out Opc.Da.BrowsePosition position)
    {
      position = null;

      lock (this)
      {
        return m_cache.Browse(itemID, filters, ref position);
      }
    }

    //======================================================================
    // BrowseNext

    /// <summary>
    /// Continues a browse operation with previously specified search criteria.
    /// </summary>
    /// <param name="position">An object containing the browse operation state information.</param>
    /// <returns>The set of elements found.</returns>
    public Opc.Da.BrowseElement[] BrowseNext(ref Opc.Da.BrowsePosition position)
    {
      if (position == null) throw new ArgumentNullException("position");

      lock (this)
      {
        return m_cache.Browse(position.ItemID, position.Filters, ref position);
      }
    }

    //======================================================================
    // GetProperties

    /// <summary>
    /// Returns the item properties for a set of items.
    /// </summary>
    /// <param name="itemIDs">A list of item identifiers.</param>
    /// <param name="propertyIDs">A list of properties to fetch for each item.</param>
    /// <param name="returnValues">Whether the property values should be returned with the properties.</param>
    /// <returns>A list of properties for each item.</returns>
    public ItemPropertyCollection[] GetProperties(
      ItemIdentifier[] itemIDs,
      PropertyID[] propertyIDs,
      bool returnValues)
    {
      if (itemIDs == null) throw new ArgumentNullException("itemIDs");

      lock (this)
      {
        ArrayList items = new ArrayList();

        foreach (ItemIdentifier itemID in itemIDs)
        {
          ItemPropertyCollection properties = m_cache.GetProperties(itemID, propertyIDs, returnValues);

          if (properties != null)
          {
            items.Add(properties);
          }
        }

        return (ItemPropertyCollection[])items.ToArray(typeof(ItemPropertyCollection));
      }
    }
    #endregion

    #region Private Members
    private int m_nextHandle = 1;
    private ICacheServer m_cache = null;
    private string m_locale = null;
    private CultureInfo m_culture = null;
    private int m_filters = -1;
    private Hashtable m_subscriptions = new Hashtable();

    private static Cache TheCacheSingleton = null;
    private static int m_ThisInstanceCount = 0;

    #endregion
  }
}
