//<summary>
//  Title   : C# Net Server SubscriptionItem
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    200901 :  mzbrzezny:based on the congfiguration function HasChnaged is checking whether timestamp has changed
//    Maciej Zbrzezny - 12-04-2006
//    funckja has changed sprawdza tez czy timestamp sie zmienil - usuniete
//    M.Postol - 2005
//    created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>
//============================================================================
// TITLE: DeviceItem.cs
//
// CONTENTS:
// 
// A class representing a single item within a subscription.
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
// 2004/03/26 RSA   Initial implementation.

using System;
using System.Collections;
using Opc;
using Opc.Da;

namespace CAS.CommServer.DA.Server.NETServer
{
  /// <summary>
  /// A single item within a subscription.
  /// </summary>
  internal class SubscriptionItem
  {
    /// <summary>
    /// The item id.
    /// </summary>
    public string ItemID
    {
      get { return m_itemID; }
    }

    /// <summary>
    /// The unique handle assigned by the client.
    /// </summary>
    public object ClientHandle
    {
      get { return m_clientHandle; }
      set { m_clientHandle = value; }
    }

    /// <summary>
    /// The unique handle assigned by the server.
    /// </summary>
    public object ServerHandle
    {
      get { return m_serverHandle; }
      set { m_serverHandle = value; }
    }

    /// <summary>
    /// Whether the item value is updated from the device.
    /// </summary>
    public bool Active
    {
      get { return m_active; }
      set { m_active = value; }
    }

    /// <summary>
    /// The data type to return to the client.
    /// </summary>
    public System.Type ReqType
    {
      get { return m_reqType; }
      set { m_reqType = value; }
    }

    /// <summary>
    /// The deadband to use when determine whether the value has changed.
    /// </summary>
    public float Deadband
    {
      get { return m_deadband; }
      set { m_deadband = value; }
    }

    /// <summary>
    /// Rate which the device should be read for values.
    /// </summary>
    public int SamplingRate
    {
      get { return m_samplingRate; }
      set { m_samplingRate = value; }
    }

    /// <summary>
    /// Whether sampled values should be buffered between data updates.
    /// </summary>
    public bool BufferEnabled
    {
      get { return m_bufferEnabled; }
      set { m_bufferEnabled = value; }
    }

    /// <summary>
    /// Initializes the object with its item id and device.
    /// </summary>
    //public SubscriptionItem(string itemID, Cache cache)//zgodnie z kodem dostarczonym z serwerem i NETApi2
    public SubscriptionItem( string itemID, ICacheServer cache )
    {
      if ( itemID == null )
        throw new ArgumentNullException( "itemID" );
      if ( cache == null )
        throw new ArgumentNullException( "cache" );

      m_itemID = itemID;
      m_cache = cache;

      m_euType = (euType)m_cache.ReadProperty( m_itemID, Property.EUTYPE );

      if ( m_euType == euType.analog )
      {
        m_maxValue = (double)m_cache.ReadProperty( m_itemID, Property.HIGHEU );
        m_minValue = (double)m_cache.ReadProperty( m_itemID, Property.LOWEU );
      }
    }

    /// <summary>
    /// Reads the value of the specified item.
    /// </summary>
    public Opc.Da.ItemValueResult Read( bool active, string locale, int maxAge )
    {
      // read value.
      ItemValueResult result = m_cache.Read( m_itemID, locale, m_reqType, maxAge );

      if ( result == null )
      {
        return new ItemValueResult( m_itemID, ResultID.E_FAIL );
      }

      // check if returning a cached value for inactive items.
      if ( ( !active || !m_active ) && maxAge >= Int32.MaxValue )
      {
        result.Quality = new Quality( qualityBits.badOutOfService );
      }

      return result;
    }

    /// <summary>
    /// Writes the value of the specified item.
    /// </summary>
    public Opc.IdentifiedResult Write( string locale, Opc.Da.ItemValue value )
    {
      // write value.
      IdentifiedResult result = m_cache.Write( m_itemID, locale, value );

      if ( result == null )
      {
        return new IdentifiedResult( m_itemID, ResultID.E_FAIL );
      }

      return result;
    }

    /// <summary>
    /// Checks if it is time to read the next sample from the device.
    /// </summary>
    public int Update(
      long tick,
      int interval,
      string locale,
      int updateRate,
      float deadband )
    {
      // inactive item do not update.
      if ( !m_active )
        return 0;

      bool doSample = true;
      bool doUpdate = true;

      // determine if it is time for a subscription update.
      if ( updateRate / interval > 1 )
      {
        doUpdate = ( tick % ( updateRate / interval ) == 0 );
      }

      // use the item or subscription sampling rate to determine the item update rate,
      int samplingRate = ( m_samplingRate != -1 ) ? m_samplingRate : updateRate;

      // update value only if the update rate has elapsed since last update.
      if ( samplingRate / interval > 1 )
      {
        doSample = ( tick % ( samplingRate / interval ) == 0 );
      }

      // check if there is anything to do.
      if ( !doSample && !doUpdate )
      {
        return 0;
      }

      // read next sample.
      if ( doSample )
      {
        DoSample( locale, deadband );
      }

      // return number of samples available for return.
      if ( doUpdate )
      {
        return m_samples.Count;
      }

      return 0;
    }

    /// <summary>
    /// Copies all samples since the last update into a buffer.
    /// </summary>
    public bool ReadSamples( ArrayList samples )
    {
      if ( m_samples.Count == 0 )
      {
        return false;
      }

      // save latest returned value.
      m_latestValue = (ItemValueResult)( (ItemValueResult)m_samples[ m_samples.Count - 1 ] ).Clone();

      // copy samples into buffer.
      foreach ( ItemValueResult sample in m_samples )
      {
        sample.ClientHandle = ClientHandle;
        sample.ServerHandle = ServerHandle;
        samples.Add( sample );
      }

      bool overflow = m_overflow;

      // clear the buffer.
      m_samples.Clear();
      m_overflow = false;

      // return flag indicated whether an overflow occurred.
      return overflow;
    }


    /// <summary>
    /// Clears the last value sent to the client.
    /// </summary>
    public void ResetLastUpdate()
    {
      m_latestValue = null;
    }

    #region Private Members

    //private Cache m_cache = null;//zgodnie z kodem dostarczonym z serwerem i NETApi2
    private ICacheServer m_cache = null;
    private string m_itemID = null;
    private euType m_euType = euType.noEnum;
    private double m_maxValue = 0;
    private double m_minValue = 0;
    private object m_clientHandle = -1;
    private object m_serverHandle = -1;
    private bool m_active = true;
    private System.Type m_reqType = null;
    private float m_deadband = 0;
    private int m_samplingRate = -1;
    private bool m_bufferEnabled = false;
    private ItemValueResult m_latestValue = null;
    private ArrayList m_samples = new ArrayList( MAX_BUFFER_LENGTH );
    private bool m_overflow = false;
    private const int MAX_BUFFER_LENGTH = 10;

    /// <summary>
    /// Reads the next sample from the device.
    /// </summary>
    private void DoSample( string locale, float deadband )
    {
      if ( !m_active )
        return;

      // read latest value from device.
      ItemValueResult currentValue = m_cache.Read( m_itemID, locale, m_reqType, 0 );

      // check if value has changed.
      bool changed = true;

      // always return for error.
      if ( m_latestValue != null )
      {
        // compare value to last returned value if sampling has not started yet.
        if ( m_samples.Count == 0 )
        {
          changed = HasChanged( currentValue, m_latestValue, deadband );
        }

          // compare to previous sample if sampling has started.
        else if ( m_bufferEnabled )
        {
          changed = HasChanged( currentValue, (ItemValueResult)m_samples[ m_samples.Count - 1 ], 0 );
        }
      }

      // add changed value to the buffer.
      if ( changed )
      {
        // clear buffer.
        if ( !m_bufferEnabled )
        {
          m_samples.Clear();
        }

        // check if buffer length is exceeded.
        if ( m_samples.Count >= MAX_BUFFER_LENGTH )
        {
          if ( currentValue.ResultID == ResultID.S_OK )
          {
            currentValue.ResultID = ResultID.Da.S_DATAQUEUEOVERFLOW;
          }

          m_samples.RemoveAt( 0 );
          m_overflow = true;
        }

        // add new sample to buffer.
        m_samples.Add( currentValue );
      }
    }

    /// <summary>
    /// Determines whether the value has changed.
    /// </summary>
    private bool HasChanged( ItemValueResult newValue, ItemValueResult oldValue, float deadband )
    {
      if (CAS.Lib.RTLib.Management.AppConfigManagement.UseTimeStampToCheckForUpdate)
      {
         // check if the timestamp has changed.
         if (newValue.Timestamp != oldValue.Timestamp)
         {
             return true;
         }
      }

      // check if the error code has changed.
      if ( newValue.ResultID != oldValue.ResultID )
      {
        return true;
      }

      // check if the quality has changed.
      if ( newValue.Quality != oldValue.Quality )
      {
        return true;
      }

      // check for trivial case.
      if ( newValue.Value == null || oldValue.Value == null )
      {
        return newValue.Value != oldValue.Value;
      }

      // check if the value is the same.
      if ( Opc.Convert.Compare( newValue.Value, oldValue.Value ) )
      {
        return false;
      }

      // check deadband if required.
      double percentage = ( ( m_deadband != -1 ) ? m_deadband : deadband ) / 100;

      if ( m_euType != euType.analog || percentage == 0 )
      {
        return true;
      }

      // check for trival case.
      double range = m_maxValue - m_minValue;

      if ( range == 0 )
      {
        return true;
      }

      // handle array values.
      if ( newValue.Value.GetType().IsArray && oldValue.Value.GetType().IsArray )
      {
        Array newArray = (Array)newValue.Value;
        Array oldArray = (Array)oldValue.Value;

        if ( newArray.Length != oldArray.Length )
        {
          return true;
        }

        // deadband is exceeded whenever a single element exceeds the deadband.
        for ( int ii = 0; ii < newArray.Length; ii++ )
        {
          try
          {
            double newElement = System.Convert.ToDouble( newArray.GetValue( ii ) );
            double oldElement = System.Convert.ToDouble( oldArray.GetValue( ii ) );

            if ( Math.Abs( ( newElement - oldElement ) / range ) > percentage )
            {
              return true;
            }
          }
          catch
          {
            return true;
          }
        }
      }

        // handle scalar values.
      else
      {
        try
        {
          double newElement = System.Convert.ToDouble( newValue.Value );
          double oldElement = System.Convert.ToDouble( oldValue.Value );

          if ( Math.Abs( ( newElement - oldElement ) / range ) > percentage )
          {
            return true;
          }
        }
        catch
        {
          return true;
        }
      }

      // deadband not exceeded.
      return false;
    }
    #endregion
  }
}
