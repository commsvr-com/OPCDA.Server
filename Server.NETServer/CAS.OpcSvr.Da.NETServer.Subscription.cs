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
//    zmieniono by czas by³ Now a nie Utc Now,  Pierwszy Update wysy³any do klienta jest opozniony o skonfigurowana wartosc
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
// TITLE: Subscription.cs
//
// CONTENTS:
// 
// A class that manages a subscription for a set of items.
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
// 2004/04/02 RSA   Initial implementation.

using CAS.Lib.RTLib.Utils;
using Opc;
using Opc.Da;
using System;
using System.Collections;
using System.Threading;

namespace CAS.OpcSvr.Da.NETServer
{
  /// <summary>
  /// A class that manages a subscription for a set of items.
  /// </summary>
  internal class  Subscription : Opc.Da.ISubscription
  { 
    /// <summary>
    /// Initializes the object with default values.
    /// </summary>
		//public Subscription(Cache cache, Server server, SubscriptionState state) zgodnie z kodem dostarczonym z NETApi2
    public Subscription(ICacheServer cache, Server server, SubscriptionState state) 
    {
      if (server == null) throw new ArgumentNullException("server");
      if (state == null)  throw new ArgumentNullException("state");

      m_cache  = cache;
      m_server = server;
      m_state  = new SubscriptionState();
      m_state.Active = false;

      // set the initial state.
      ModifyState((int)StateMask.All, state);
    }

    private void Update(long ticks, int interval)
    {
      lock (this)
      {
        // no update for inactive groups.
        if (!m_state.Active || !m_enabled /*|| m_dataChanged == null*/) { return; }
        //MZ: m_dataChanged == null means that we are updating only subscription that has OnDataChange handler, 
        //but what if we reading synchronously and there is no OnDataChange handler

        // reset the tick offset after subscription is activated. this ensures that the next 
        // cycle of the update thread will send an update instead of waiting until the 
        // update period has expired.
				bool callbackRequired = true;//zgodnie z kodem dostarczonym z serwerem i NETApi2
				if (m_tickOffset < 0)//zgodnie z kodem dostarczonym z serwerem i NETApi2
        {
					callbackRequired = m_tickOffset == -1;//zgodnie z kodem dostarczonym z serwerem i NETApi2
          m_tickOffset = ticks;
        }

        // do the next sample for each item.
        int changedItems = 0;

        foreach (SubscriptionItem item in m_items.Values)
        {
          changedItems += item.Update(ticks - m_tickOffset, interval, m_state.Locale, m_state.UpdateRate, m_state.Deadband);
        }
        if ( m_dataChanged != null )//we are sending update only if we have handler
        {
          // create the values to return.
          if ( callbackRequired && changedItems > 0 )//zgodnie z kodem dostarczonym z serwerem i NETApi2
          {
            ArrayList items = new ArrayList( changedItems );

            foreach ( SubscriptionItem item in m_items.Values )
            {
              item.ReadSamples( items );
            }

            m_lastUpdate = DateTimeProvider.GetCurrentTime();//zgodnie z kodem dostarczonym z serwerem i NETApi2
            m_updateQueue.Enqueue( items.ToArray( typeof( ItemValueResult ) ) );
            ThreadPool.QueueUserWorkItem( new WaitCallback( OnUpdate ) );
          }

          else if ( m_state.KeepAlive > 0 )
          {
            long delta = DateTimeProvider.GetCurrentTime().Ticks - m_lastUpdate.Ticks;//zgodnie z kodem dostarczonym z serwerem i NETApi2

            if ( delta > m_state.KeepAlive * TimeSpan.TicksPerMillisecond )
            {
              m_lastUpdate = DateTimeProvider.GetCurrentTime();//zgodnie z kodem dostarczonym z serwerem i NETApi2
              m_updateQueue.Enqueue( new ItemValueResult[ 0 ] );
              ThreadPool.QueueUserWorkItem( new WaitCallback( OnUpdate ) );
            }
          }
        }
      }
    }

    #region IDisposable Members
    /// <remarks/>
    public void Dispose()
    {
      lock (this)
      {
        m_disposed = true;
				//m_cache.DeactivateSubscription(this);//zgodnie z kodem dostarczonym z serwerem i NETApi2
        DeactivateSubscription();
        m_updateQueue.Clear();
      }
    }
    #endregion

    #region ISubscription Members
    /// <remarks/>
    public event Opc.Da.DataChangedEventHandler DataChanged
    {
      add    
      {
        lock(this) 
        { 
          m_dataChanged += value; 

          foreach (SubscriptionItem item in m_items.Values)
          {
            item.ResetLastUpdate();
          }
        }
      }

      remove 
      {
        lock(this) 
        { 
          m_dataChanged -= value; 
        }
      }
    }

    /// <remarks/>
    public int GetResultFilters()
    {
      lock (this)
      {
        return m_filters;
      }
    }

    /// <remarks/>
    public void SetResultFilters(int filters)
    {
      lock (this)
      {
        m_filters = filters;
      }
    }

    /// <remarks/>
    public SubscriptionState GetState()
    {
      lock (this)
      {
        return (SubscriptionState)m_state.Clone();
      }
    }

    /// <remarks/>
    public SubscriptionState ModifyState(int masks, SubscriptionState state)
    {
      if (state == null) throw new ArgumentNullException("state");

      lock (this)
      {
        // save copy of current state.
        SubscriptionState modifiedState = (SubscriptionState)m_state.Clone();

        // update subscription defaults.
        if ((masks & (int)StateMask.Name) != 0)         { modifiedState.Name         = state.Name;         } 
        if ((masks & (int)StateMask.ClientHandle) != 0) { modifiedState.ClientHandle = state.ClientHandle; }
//TODO wrocic do tego - zmy³a dla WIZCON        if ((masks & (int)StateMask.Active) != 0)       { modifiedState.Active       = state.Active;       }
        if ((masks & (int)StateMask.Active) != 0)       
        { 
          modifiedState.Active       = state.Active;       
          if(state.Active==true) m_firstUpdateDelay=true;
        }
        if ((masks & (int)StateMask.UpdateRate) != 0)   { modifiedState.UpdateRate   = state.UpdateRate;   }
        if ((masks & (int)StateMask.KeepAlive) != 0)    { modifiedState.KeepAlive    = state.KeepAlive;    }
        if ((masks & (int)StateMask.Deadband) != 0)     { modifiedState.Deadband     = state.Deadband;     }

        if ((masks & (int)StateMask.Locale) != 0)    
        { 
          modifiedState.Locale = Opc.Server.FindBestLocale(state.Locale, m_server.GetSupportedLocales());       
        }

        // check for unsupported update rate.
        modifiedState.UpdateRate = Cache.AdjustUpdateRate(modifiedState.UpdateRate);      
				// check update rate chanegd. Ten IF zgodnie z kodem dostarczonym z serwerem i NETApi2
				if ((masks & (int)StateMask.UpdateRate) != 0)
				{
					// resynch timebase but don't send an update.
					m_tickOffset = -2;
				}
      
        // check for unsupported keepalive rate.
        if (modifiedState.KeepAlive != 0)
        {
          modifiedState.KeepAlive = Cache.AdjustUpdateRate(modifiedState.KeepAlive);
        }
        
        // check if subscription deactivated.
        if (!m_state.Active && modifiedState.Active)
        {
					//m_cache.ActivateSubscription(this);//zgodnie z kodem dostarczonym z serwerem i NETApi2
          ActivateSubscription();
          // force an update to be sent.
          m_tickOffset = -1;
        }

        // check if subscription deactivated.
        else if (m_state.Active && !modifiedState.Active)
        {
					//m_cache.DeactivateSubscription(this);//zgodnie z kodem dostarczonym z serwerem i NETApi2
          DeactivateSubscription();
          m_updateQueue.Clear();
        }

        // replace existing state.
        m_state = modifiedState;

        // return new state.
        return GetState();
      }
    }

    /// <remarks/>
    public ItemResult[] AddItems(Item[] items)
    {
      if (items == null) throw new ArgumentNullException("items");

      lock (this)
      {
        // handle trivial case.
        if (items.Length == 0)
        {
          return new ItemResult[0];
        }

        // create item results.
        ItemResult[] results = new ItemResult[items.Length];

        for (int ii = 0; ii < items.Length; ii++)
        {
          // initialize result with item.
          results[ii] = new ItemResult((ItemIdentifier)items[ii]);

          // check that the item is valid.
          if (items[ii].ItemName == null || items[ii].ItemName.Length == 0)
          {
            results[ii].ResultID = ResultID.Da.E_INVALID_ITEM_NAME;
            continue;
          }

          // check that the item exists.
          if (!m_cache.IsValidItem(items[ii].ItemName))
          {
            results[ii].ResultID = ResultID.Da.E_UNKNOWN_ITEM_NAME;
            continue;
          }

          // check that requested datatype is valid.
          if (items[ii].ReqType == typeof(Opc.Type))
          {
            results[ii].ResultID = ResultID.Da.E_BADTYPE;
            continue;
          }

          // create a new subscription item.
          SubscriptionItem item = new SubscriptionItem(items[ii].ItemName, m_cache);

          item.Active        = (items[ii].ActiveSpecified)?items[ii].Active:true;
          item.ClientHandle  = items[ii].ClientHandle;
          item.ReqType       = items[ii].ReqType;
          item.Deadband      = (items[ii].DeadbandSpecified)?items[ii].Deadband:-1; 
          item.SamplingRate  = (items[ii].SamplingRateSpecified)?items[ii].SamplingRate:-1;
          item.BufferEnabled = (items[ii].EnableBufferingSpecified)?items[ii].EnableBuffering:false;

          // update sampling rate.
          if (item.SamplingRate != -1)
          {
            item.SamplingRate = Cache.AdjustUpdateRate(item.SamplingRate);
          }

          // assign unique server handle.
          results[ii].ServerHandle = item.ServerHandle = AssignHandle();

          // update result object.
          results[ii].Active                   = item.Active;
          results[ii].ActiveSpecified          = true;
          results[ii].ClientHandle             = item.ClientHandle;
          results[ii].ReqType                  = item.ReqType;
          results[ii].Deadband                 = (item.Deadband != -1)?item.Deadband:0;
          results[ii].DeadbandSpecified        = item.Deadband != -1;
          results[ii].SamplingRate             = (item.SamplingRate != -1)?item.SamplingRate:0;
          results[ii].SamplingRateSpecified    = item.SamplingRate != -1;
          results[ii].EnableBuffering          = item.BufferEnabled;
          results[ii].EnableBufferingSpecified = item.SamplingRate != -1;

          // save reference to new item.
          m_items[results[ii].ServerHandle] = item;
        }

        // return results.
        return results;
      }
    }

    /// <remarks/>
    public ItemResult[] ModifyItems(int masks, Item[] items)
    {
      if (items == null) throw new ArgumentNullException("items");

      lock (this)
      {
        // handle trivial case.
        if (items.Length == 0)
        {
          return new ItemResult[0];
        }

        // create item results.
        ItemResult[] results = new ItemResult[items.Length];

        for (int ii = 0; ii < items.Length; ii++)
        {
          // initialize result with item.
          results[ii] = new ItemResult(items[ii]);

          // check for invalid handle.
          if (items[ii].ServerHandle == null)
          {
            results[ii].ResultID = ResultID.Da.E_INVALIDHANDLE;
            continue;
          }

          // lookup existing item.
          SubscriptionItem item = (SubscriptionItem)m_items[items[ii].ServerHandle];

          
          // check that the item is valid.
          if (item == null) //MP (m_items.Contains(items)) w nowym NETAPi jest jednak po staremu
					//if (m_items.Contains(items))
          {
            results[ii].ResultID = ResultID.Da.E_INVALIDHANDLE;
            continue;
          }

          // update subscription item.
          System.Type reqType         = item.ReqType;
          bool        active          = item.Active;
          object      clientHandle    = item.ClientHandle;
          float       deadband        = item.Deadband;
          int         samplingRate    = item.SamplingRate;
          bool        enableBuffering = item.BufferEnabled;

          // requested data type.
          if ((masks & (int)StateMask.ReqType) != 0)
          { 
            // check that requested datatype is valid.
            if (items[ii].ReqType == Opc.Type.ILLEGAL_TYPE)
            {
              results[ii].ResultID = ResultID.Da.E_BADTYPE;
              continue;
            }

            reqType = items[ii].ReqType;
          }

          // client handle.
          if ((masks & (int)StateMask.ClientHandle) != 0)
          {
            clientHandle = items[ii].ClientHandle;
          }

          // deadband.
          if ((masks & (int)StateMask.Deadband) != 0)
          {
            if (items[ii].DeadbandSpecified)
            {
              deadband = items[ii].Deadband;

              if (deadband < 0.0 || deadband > 100.0)
              {
                results[ii].ResultID = ResultID.E_INVALIDARG;
                continue;
              }
            }
            else
            {
              deadband = -1;
            }
          }

          // sampling rate.
          if ((masks & (int)StateMask.SamplingRate) != 0)
          {
            samplingRate = (items[ii].SamplingRateSpecified)?items[ii].SamplingRate:-1;

            int requestedSamplingRate = samplingRate;

            if (samplingRate != -1)
            {
              samplingRate = Cache.AdjustUpdateRate(requestedSamplingRate); 
            }
            
            if (requestedSamplingRate != samplingRate)
            {
              results[ii].ResultID = ResultID.Da.S_UNSUPPORTEDRATE;
            }
          }

          if ((masks & (int)StateMask.EnableBuffering) != 0)
          {
            enableBuffering = (items[ii].EnableBufferingSpecified)?items[ii].EnableBuffering:false;
          }

          // active.
          if ((masks & (int)StateMask.Active) != 0)
          {
            active = (items[ii].ActiveSpecified)?items[ii].Active:item.Active;

            // ensure a callback is sent on the next update.
            if (active && !item.Active)
            {
              item.ResetLastUpdate();
            }
          }

          // save new values.
          item.ReqType       = reqType;
          item.Active        = active;
          item.ClientHandle  = clientHandle;
          item.Deadband      = deadband;
          item.SamplingRate  = samplingRate;
          item.BufferEnabled = enableBuffering;

          // update result object.
          results[ii].Active                   = item.Active;
          results[ii].ActiveSpecified          = true;
          results[ii].ClientHandle             = item.ClientHandle;
          results[ii].ReqType                  = item.ReqType;
          results[ii].Deadband                 = (item.Deadband != -1)?item.Deadband:0;
          results[ii].DeadbandSpecified        = item.Deadband != -1;
          results[ii].SamplingRate             = (item.SamplingRate != -1)?item.SamplingRate:0;
          results[ii].SamplingRateSpecified    = item.SamplingRate != -1;
          results[ii].EnableBuffering          = item.BufferEnabled;
          results[ii].EnableBufferingSpecified = item.BufferEnabled;
        }

        // return results.
        return results;
      }
    }

    /// <remarks/>
    public IdentifiedResult[] RemoveItems(ItemIdentifier[] items)
    {
      if (items == null) throw new ArgumentNullException("items");

      lock (this)
      {
        // handle trivial case.
        if (items.Length == 0)
        {
          return new IdentifiedResult[0];
        }

        // create item results.
        IdentifiedResult[] results = new IdentifiedResult[items.Length];

        for (int ii = 0; ii < items.Length; ii++)
        {
          // initialize result with item.
          results[ii] = new IdentifiedResult(items[ii]);

          // check for invalid handle.
          if (items[ii].ServerHandle == null)
          {
            results[ii].ResultID = ResultID.Da.E_INVALIDHANDLE;
            continue;
          }

          // lookup subscription item.
          SubscriptionItem item = (SubscriptionItem)m_items[items[ii].ServerHandle];

          if (item == null)
          {
            results[ii].ResultID = ResultID.Da.E_INVALIDHANDLE;
            continue;
          }

          // remove item.
          m_items.Remove(items[ii].ServerHandle);

          results[ii].ResultID = ResultID.S_OK;
        }

        // return results.
        return results;
      }
    }

    /// <remarks/>
    public ItemValueResult[] Read(Item[] items)
    {
      if (items == null) throw new ArgumentNullException("items");

      lock (this)
      {
        // handle trivial case.
        if (items.Length == 0)
        {
          return new ItemValueResult[0];
        }

        // create item results.
        ItemValueResult[] results = new ItemValueResult[items.Length];

        for (int ii = 0; ii < items.Length; ii++)
        {
          // initialize result with item.
          results[ii] = new ItemValueResult(items[ii]);

          // check for invalid handle.
          if (items[ii].ServerHandle == null)
          {
            results[ii].ResultID = ResultID.Da.E_INVALIDHANDLE;
            continue;
          }

          // lookup subscription item.
          SubscriptionItem item = (SubscriptionItem)m_items[items[ii].ServerHandle];

          if (item == null)
          {
            results[ii].ResultID = ResultID.Da.E_INVALIDHANDLE;
            continue;
          }

          // read the item value.
          results[ii] = item.Read(m_state.Active, m_state.Locale, items[ii].MaxAge);
          results[ii].ServerHandle = items[ii].ServerHandle;
        }

        // apply result filters.
        ApplyFilters(m_filters, results);

        // return results.
        return results;
      }
    }

    /// <remarks/>
    public IdentifiedResult[] Write(ItemValue[] items)
    {
      if (items == null) throw new ArgumentNullException("items");

      lock (this)
      {
        // handle trivial case.
        if (items.Length == 0)
        {
          return new IdentifiedResult[0];
        }

        // create item results.
        IdentifiedResult[] results = new IdentifiedResult[items.Length];

        for (int ii = 0; ii < items.Length; ii++)
        {
          // initialize result with item.
          results[ii] = new IdentifiedResult(items[ii]);

          // check for invalid handle.
          if (items[ii].ServerHandle == null)
          {
            results[ii].ResultID = ResultID.Da.E_INVALIDHANDLE;
            continue;
          }

          // lookup subscription item.
          SubscriptionItem item = (SubscriptionItem)m_items[items[ii].ServerHandle];

          if (item == null)
          {
            results[ii].ResultID = ResultID.Da.E_INVALIDHANDLE;
            continue;
          }

          // write the item value.
          results[ii] = item.Write(m_state.Locale, items[ii]);
          results[ii].ServerHandle = items[ii].ServerHandle;
        }

        // apply result filters.
        ApplyFilters(m_filters, results);

        // return results.
        return results;
      }
    }

    /// <summary>
    /// Begins an asynchronous read operation for a set of items.
    /// </summary>
    /// <param name="items">The set of items to read (must include the server handle).</param>
    /// <param name="requestHandle">An identifier for the request assigned by the caller.</param>
    /// <param name="callback">A delegate used to receive notifications when the request completes.</param>
    /// <param name="request">An object that contains the state of the request (used to cancel the request).</param>
    /// <returns>A set of results containing any errors encountered when the server validated the items.</returns>
    public IdentifiedResult[] Read(
      Item[]                   items,
      object                   requestHandle,
      ReadCompleteEventHandler callback,
      out IRequest             request)
    { 
      if (items == null) throw new ArgumentNullException("items");

      lock (this)
      {
        request = null;

        // handle trivial case.
        if (items.Length == 0)
        {
          return new IdentifiedResult[0];
        }

        // validate the items.
        ArrayList validItems = new ArrayList();

        IdentifiedResult[] results = new IdentifiedResult[items.Length];

        for (int ii = 0; ii < items.Length; ii++)
        {
          // initialize result with item.
          results[ii] = new IdentifiedResult(items[ii]);

          // check for invalid handle.
          if (items[ii].ServerHandle == null)
          {
            results[ii].ResultID = ResultID.Da.E_INVALIDHANDLE;
            continue;
          }

          // lookup subscription item.
          SubscriptionItem item = (SubscriptionItem)m_items[items[ii].ServerHandle];

          if (item == null)
          {
            results[ii].ResultID = ResultID.Da.E_INVALIDHANDLE;
            continue;
          }

          // at least one valid item exists.
          validItems.Add(items[ii]);
        }

        if (validItems.Count > 0)
        {
          request = new Opc.Da.Request(this, requestHandle);  
          m_requests.Add(request, callback);
          ThreadPool.QueueUserWorkItem(new WaitCallback(OnRead), new object[] { request, validItems.ToArray(typeof(Item)) });
        }

        // apply result filters.
        ApplyFilters(m_filters, results);

        // return results.
        return results;
      }
    }

    /// <summary>
    /// Begins an asynchronous write operation for a set of items.
    /// </summary>
    /// <param name="items">The set of item values to write (must include the server handle).</param>
    /// <param name="requestHandle">An identifier for the request assigned by the caller.</param>
    /// <param name="callback">A delegate used to receive notifications when the request completes.</param>
    /// <param name="request">An object that contains the state of the request (used to cancel the request).</param>
    /// <returns>A set of results containing any errors encountered when the server validated the items.</returns>
    public IdentifiedResult[] Write(
      ItemValue[]               items,
      object                    requestHandle,
      WriteCompleteEventHandler callback,
      out IRequest              request)
    { 
      if (items == null) throw new ArgumentNullException("items");

      lock (this)
      {
        request = null;

        // handle trivial case.
        if (items.Length == 0)
        {
          return new IdentifiedResult[0];
        }

        // validate the items.
        ArrayList validItems = new ArrayList();

        IdentifiedResult[] results = new IdentifiedResult[items.Length];

        for (int ii = 0; ii < items.Length; ii++)
        {
          // initialize result with item.
          results[ii] = new IdentifiedResult(items[ii]);

          // check for invalid handle.
          if (items[ii].ServerHandle == null)
          {
            results[ii].ResultID = ResultID.Da.E_INVALIDHANDLE;
            continue;
          }

          // lookup subscription item.
          SubscriptionItem item = (SubscriptionItem)m_items[items[ii].ServerHandle];

          if (item == null)
          {
            results[ii].ResultID = ResultID.Da.E_INVALIDHANDLE;
            continue;
          }

          // at least one valid item exists.
          validItems.Add(items[ii]);
        }

        if (validItems.Count > 0)
        {
          request = new Opc.Da.Request(this, requestHandle);  
          
          m_requests.Add(request, callback);
          m_writeQueue.Enqueue(new object[] { request, validItems.ToArray(typeof(ItemValue)) });

          ThreadPool.QueueUserWorkItem(new WaitCallback(OnWrite));
        }

        // apply result filters.
        ApplyFilters(m_filters, results);

        // return results.
        return results;
      }
    }

    /// <remarks/>
    public void Refresh()
    {
      IRequest request = null;
      Refresh(null, out request);   
    }

    /// <remarks/>
    public void Refresh(
      object       requestHandle,
      out IRequest request)
    {
      lock (this)
      {
        request = null; 

        // return error is group is not active.
        if (!m_state.Active)
        {
          throw new ResultIDException(ResultID.E_FAIL);
        }

        // determine list available active items.
        ArrayList activeItems = new ArrayList();

        foreach (SubscriptionItem subscriptionItem in m_items.Values)
        {
          if (subscriptionItem.Active)
          {           
            Item item = new Item(new ItemIdentifier(subscriptionItem.ItemID));

            item.ServerHandle    = subscriptionItem.ServerHandle;
            item.MaxAge          = 0;
            item.MaxAgeSpecified = true;
                        
            activeItems.Add(item);          
          }
        }

        // no active items.
        if (activeItems.Count == 0)
        {
          throw new ResultIDException(ResultID.E_FAIL);
        }

        // create request.
        request = new Opc.Da.Request(this, requestHandle);  
        m_requests.Add(request, m_dataChanged);

        // queue update.
        ThreadPool.QueueUserWorkItem(new WaitCallback(OnRead), new object[] { request, activeItems.ToArray(typeof(Item)) });
      }
    }

    /// <remarks/>
    public void Cancel(IRequest request, Opc.Da.CancelCompleteEventHandler callback)
    {
      lock (this)
      {
        if (m_requests.Contains(request))
        {
          m_requests.Remove(request);

          if (callback != null)
          {
            ThreadPool.QueueUserWorkItem(new WaitCallback(OnCancel), new object[] {request, callback});
          }
        }
      }
    }

    /// <remarks/>
    public bool GetEnabled()
    {
      lock (this)
      {
        return m_enabled;
      }
    }

    /// <remarks/>
    public void SetEnabled(bool enabled)
    {
      lock (this)
      {
        m_enabled = enabled;
      }
    }
    #endregion
    
    #region Private Members
    /// <summary>
    /// The maximum rate at which items may be scaned.
    /// </summary>
    private const int MAX_UPDATE_RATE = 100;
//TODO dla zmylki w celu uruchomienia WIZCON'a - sprawdziæ czy potrzebne
    private bool m_firstUpdateDelay=false;
    private object m_firstUpdateDelayLock=new object();
    private bool m_disposed = false;
    private bool m_enabled = true;
    private ICacheServer m_cache = null;
    private Server m_server = null;
    private long m_tickOffset = -1;
    private SubscriptionState m_state = null;
    private DateTime m_lastUpdate = DateTime.MinValue;
    private Hashtable m_items = new Hashtable();
    private Hashtable m_requests = new Hashtable();
    private int m_filters = -1;
    private int m_nextHandle = 1;
    private event Opc.Da.DataChangedEventHandler m_dataChanged = null;
    private Queue m_updateQueue = new Queue();
    private Queue m_writeQueue = new Queue();
    private static ArrayList m_subscriptions = new ArrayList();
    static Subscription()
    {
      // start the cache thread.
      ThreadPool.QueueUserWorkItem(new WaitCallback(OnUpdateAllSub));
    }
    /// <summary>
    /// Creates a unique handle for items.
    /// </summary>
    private int AssignHandle()
    {
      return m_nextHandle++;
    }    

    /// <summary>
    /// Updates a result list based on the request options and sets the handles for use by the client.
    /// </summary>
    public ItemIdentifier[] ApplyFilters(int filters, ItemIdentifier[] results)
    {
      if (results == null) { return null; }

      foreach (ItemIdentifier result in results)
      {
        if (result.ServerHandle != null)
        {
          SubscriptionItem item = (SubscriptionItem)m_items[result.ServerHandle];

          if (item != null)
          {
            result.ItemName     = ((filters & (int)ResultFilter.ItemName) != 0)?item.ItemID:null;
            result.ItemPath     = null;
            result.ServerHandle = result.ServerHandle;
            result.ClientHandle = ((filters & (int)ResultFilter.ClientHandle) != 0)?item.ClientHandle:null;
          }
        }

        if ((filters & (int)ResultFilter.ItemTime) == 0)
        {
          if (typeof(ItemValue).IsInstanceOfType(result))
          {
            ((ItemValueResult)result).Timestamp = DateTime.MinValue;
            ((ItemValueResult)result).TimestampSpecified = false;
          }
        }
      }

      return results;
    }
    
    /// <summary>
    /// Sends a data update callback.
    /// </summary>
    private void OnUpdate(object stateInfo) 
    {
//TODO zmylka dla wizkona ustalic czy faktycznie potrzebna
      lock(m_firstUpdateDelayLock)
      {
        if(m_firstUpdateDelay)
        {
          // DEBUG_INFO_MESSAGE
          //Opc.EventLogMonitor.WriteToEventLogInfo("sleep:"+BaseStation.Management.AppConfigManagement.WaitForFirstGroupUpdateSendInMiliSec.ToString());
          System.Threading.Thread.Sleep(CAS.Lib.RTLib.Management.AppConfigManagement.WaitForFirstGroupUpdateSendInMiliSec);
          m_firstUpdateDelay=false;
        }
      }

      try
      {
        do
        {
          object clientHandle = null;
          ItemValueResult[] values = null;
          DataChangedEventHandler callback = null;

          lock (this)
          {
            // check if subscription object has been disposed.
            if (m_disposed)
            {
              return;
            }

            if (m_updateQueue.Count == 0)
            {
              break;
            }
  
            callback     = m_dataChanged;
            clientHandle = m_state.ClientHandle;
            values       = (ItemValueResult[])m_updateQueue.Dequeue();
          }

          if (callback != null)
          {
            callback(clientHandle, null, values);
          }
        }
        while (true); 
      }
      catch (Exception e)
      {
        string message = e.Message;
      }
    }

    /// <summary>
    /// Periodically updates the cache for all active subscriptions.
    /// </summary>
    private static void OnUpdateAllSub(object stateInfo) 
    {
      int  delay = 0;
      long ticks = 0;

      do
      {
        // sleep until next update.
        Thread.Sleep((delay > 0 && delay < MAX_UPDATE_RATE)?MAX_UPDATE_RATE - delay:MAX_UPDATE_RATE);

        delay = Environment.TickCount;

        // build list of subscriptions to update.
        ArrayList subscriptions = new ArrayList();

        lock ( typeof(Subscription) )
        {         
          //// check if object has been disposed.
          //if (m_disposed)( break; }
          foreach (Subscription subscription in m_subscriptions)
          {
            subscriptions.Add(subscription);
          }
        }
        
        // update each subscription
        foreach (Subscription subscription in subscriptions)
        {
          try
          {
            subscription.Update(ticks, MAX_UPDATE_RATE);
          }
          catch (Exception e)
          {
            string message = e.Message;
          }
        }

        delay = Environment.TickCount - delay;

        // increment tick count.
        ticks++;
      }
      while (true);
    }
    /// <summary>
    /// Adds a subscription to the list of active subscriptions.
    /// </summary>
    void ActivateSubscription()
    {
      lock ( typeof(Subscription) )
      {
        // ensure duplicates are not added.
        for (int ii = 0; ii < m_subscriptions.Count; ii++)
        {
          if (this == m_subscriptions[ii])
          {
            return;
          }
        }
        m_subscriptions.Add(this);
      }
    }
    /// <summary>
    /// Removes a subscription to the list of active subscriptions.
    /// </summary>
    void DeactivateSubscription()
    {
      lock (typeof(Subscription))
      {
        for (int ii = 0; ii < m_subscriptions.Count; ii++)
        {
          if (this == m_subscriptions[ii])
          {
            m_subscriptions.RemoveAt(ii);
            return;
          }
        }
      }
    }
    /// <summary>
    /// Completes an asynchronous read request.
    /// </summary>
    private void OnRead(object stateInfo) 
    {
      try
      {
        // unmarshal arguments.
        Opc.Da.Request request = null;
        Item[] items = null;

        if (typeof(object[]).IsInstanceOfType(stateInfo))
        {
          request  = (Opc.Da.Request)((object[])stateInfo)[0];
          items    = (Item[])((object[])stateInfo)[1];
        }

        // check if request is still valid and read values.
        object clientHandle = null;
        ItemValueResult[] results = null;
        Delegate callback = null;

        lock (this)
        {
          // check if subscription object has been disposed.
          if (m_disposed)
          {
            return;
          }

          clientHandle = m_state.ClientHandle;
          callback     = (Delegate)m_requests[request];

          if (callback != null)
          {
            m_requests.Remove(request);
            results = Read(items);
          }
        }

        // invoke read complete callback.
        if (typeof(ReadCompleteEventHandler).IsInstanceOfType(callback))
        {
          ((ReadCompleteEventHandler)callback)(request.Handle, results);
        }

        // invoke refresh complete callback.
        else if (typeof(DataChangedEventHandler).IsInstanceOfType(callback))
        {
          ((DataChangedEventHandler)callback)(clientHandle, request.Handle, results);
        }
        else
          new CAS.Lib.RTLib.Processes.EventLogMonitor
            (
            "Internal error cannot find request callback delegate", 
            System.Diagnostics.EventLogEntryType.Error,
            (int)CAS.Lib.RTLib.Processes.Error.CAS_OpcSvr_Da_NETServer_Subscription, 
            1098
            ).WriteEntry();
      }
      catch (Exception e)
      {
        new CAS.Lib.RTLib.Processes.EventLogMonitor
          (
          e.Message
#if DEBUG
          +" "+e.StackTrace
#endif
          , System.Diagnostics.EventLogEntryType.Error,
          (int)CAS.Lib.RTLib.Processes.Error.CAS_OpcSvr_Da_NETServer_Subscription, 
          1107
          ).WriteEntry();
      }
    }

    /// <summary>
    /// Completes an asynchronous write request.
    /// </summary>
    private void OnWrite(object stateInfo) 
    {
      try
      {
        do
        {
          object requestHandle = null;
          IdentifiedResult[] results = null;
          WriteCompleteEventHandler callback = null;

          lock (this)
          {
            // check if subscription object has been disposed.
            if (m_disposed)
            {
              return;
            }

            // check if write queue has emptied.
            if (m_writeQueue.Count == 0)
            {
              break;
            }

            // get operation parameters.
            object[] parameters = (object[])m_writeQueue.Dequeue();
  
            Opc.Da.Request request  = (Opc.Da.Request)parameters[0];
            ItemValue[]    items    = (ItemValue[])parameters[1];

            requestHandle = request.Handle;
            callback      = (WriteCompleteEventHandler)m_requests[request];

            // do write request if not cancelled.
            if (callback != null)
            {
              m_requests.Remove(request);
              results = Write(items);
            }
          }

          // invoke callback.
          if (callback != null)
          {
            callback(requestHandle, results);
          }
        }
        while (true); 
      }
      catch (Exception e)
      {
        string message = e.Message;
      }
    }

    /// <summary>
    /// Completes an asynchronous request cancel
    /// </summary>
    private void OnCancel(object stateInfo) 
    {
      try
      {
        lock (this)
        {
          // check if subscription object has been disposed.
          if (m_disposed)
          {
            return;
          }
        }

        // unmarshal arguments.
        Opc.Da.Request request = null;
        CancelCompleteEventHandler callback = null;

        if (typeof(object[]).IsInstanceOfType(stateInfo))
        {
          request  = (Opc.Da.Request)((object[])stateInfo)[0];
          callback = (CancelCompleteEventHandler)((object[])stateInfo)[1];
        }

        // invoke callback.
        callback(request.Handle);
      }
      catch (Exception e)
      {
        string message = e.Message;
      }
    }

    #endregion
  }
}
