//<summary>
//  Title   : C# Net Server CacheItem
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20071007: mzbrzezny  - DateTimeProvider is use instead of DateTime.Now or UtcNow
//    Mariusz Postol - 22-08-2005
//      System.Xml.XmlConvert.ToDateTime(string. Use XmlConvert.ToDateTime() that takes in XmlDateTimeSerializationMode
//    Maciej Zbrzezny - 12-04-2006
//    zmieniono by czas by³ Now a nie Utc Now,  
//    M.Postol - 2005
//    created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>
//============================================================================
// TITLE: CacheItem.cs
//
// CONTENTS:
// 
// A class which maintains a cache for a single item.
//
// (c) Copyright 2004 The OPC Foundation
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
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Xml;
using CAS.Lib.DeviceSimulator;
using CAS.Lib.RTLib.Utils;
using Opc;
using Opc.Da;

namespace CAS.OpcSvr.Da.NETServer
{
  /// <summary>
  /// A class which maintains a cache for a single item.
  /// </summary>
  internal class CacheItem
  {
    /// <summary>
    /// internal helper class (thread safe)
    /// </summary>
    private class CacheItemSettings
    {
      #region Private Members
      private string m_itemID = null;
      private ushort m_ItemIndex;
      private IDeviceIndexed m_device = null;
      private ItemValueResult m_value = null;
      private System.Type m_datatype = null;
      private euType m_euType = euType.noEnum;
      private string[] m_euInfo = null;
      #endregion
      internal CacheItemSettings( string itemID, ushort ItemIndex, IDeviceIndexed Device )
      {
        lock ( this )
        {
          m_itemID = itemID;
          m_ItemIndex = ItemIndex;
          m_device = Device;

          m_datatype = (System.Type)CacheItem.ReadProperty( Property.DATATYPE, m_device, m_ItemIndex );
          m_euType = (euType)CacheItem.ReadProperty( Property.EUTYPE, m_device, m_ItemIndex );

          if ( m_euType == euType.enumerated )
          {
            m_euInfo = (string[])CacheItem.ReadProperty( Property.EUINFO, m_device, m_ItemIndex );
          }
        }
      }
      internal IDeviceIndexed Device
      {
        get
        {
          lock ( this )
            return m_device;
        }
      }
      internal ushort ItemIndex
      {
        get
        {
          lock ( this )
            return m_ItemIndex;
        }
      }
      internal string ItemID
      {
        get
        {
          lock ( this )
            return m_itemID;
        }
      }
      internal string[] EuInfo
      {
        get
        {
          lock ( this )
            return m_euInfo;
        }
      }
      internal euType EuType
      {
        get
        {
          lock ( this )
            return m_euType;
        }
      }
      internal System.Type DataType
      {
        get
        {
          lock ( this )
            return m_datatype;
        }
      }
      internal ItemValueResult ReadNewValue( int maxAge, DateTime target )
      {
        lock ( this )
        {
          if ( maxAge == 0 || m_value == null || target > m_value.Timestamp )
          {
            m_value = m_device.Read( m_ItemIndex, Property.VALUE );
            m_value.Timestamp = DateTimeProvider.GetCurrentTime();
          }
          ItemValueResult result = new ItemValueResult( (ItemIdentifier)m_value );
          result.Value = m_value.Value;
          result.ResultID = m_value.ResultID;
          result.DiagnosticInfo = m_value.DiagnosticInfo;
          result.Quality = m_value.Quality;
          result.QualitySpecified = true;
          result.Timestamp = m_value.Timestamp;
          result.TimestampSpecified = true;
          return result;
        }
      }

    }
    /// <summary>
    /// Initializes the object with its item id and device.
    /// </summary>
    internal CacheItem( string itemID, ushort itemIndex, IDeviceIndexed device )
    {
      if ( itemID == null )
        throw new ArgumentNullException( "itemID" );
      if ( device == null )
        throw new ArgumentNullException( "device" );

      m_settings = new CacheItemSettings( itemID, itemIndex, device );

    }
    /// <summary>
    /// Returns all available properties for the item.
    /// </summary>
    internal Opc.Da.ItemPropertyCollection GetAvailableProperties( bool returnValues )
    {
      return m_settings.Device.GetAvailableProperties( m_settings.ItemIndex, returnValues );
    }
    /// <summary>
    /// Returns the specified properties for the item.
    /// </summary>
    internal Opc.Da.ItemPropertyCollection GetAvailableProperties( PropertyID[] propertyIDs, bool returnValues )
    {
      return m_settings.Device.GetAvailableProperties( m_settings.ItemIndex, propertyIDs, returnValues );
    }
    /// <summary>
    /// Reads the value of the specified item property.
    /// </summary>
    internal Opc.Da.ItemValueResult Read( PropertyID propertyID )
    {
      return m_settings.Device.Read( m_settings.ItemIndex, propertyID );
    }
    /// <summary>
    /// Converts a value to the specified type using the specified locale.
    /// </summary>
    private static object ChangeType( object source, System.Type type, string locale, bool supportsCOM )
    {
      CultureInfo culture = Thread.CurrentThread.CurrentCulture;
      // override the current thread culture to ensure conversions happen correctly.
      try
      {
        Thread.CurrentThread.CurrentCulture = new CultureInfo( locale );
      }
      catch
      {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;
      }
      try
      {
        return ChangeType( source, type, supportsCOM );
      }
      // restore the current thread culture after conversion.
      finally
      {
        Thread.CurrentThread.CurrentCulture = culture;
      }
    }
    /// <summary>
    /// Converts a value to the specified type using either .NET or COM conversion rules.
    /// </summary>
    private static object ChangeType( object source, System.Type type, bool supportsCOM )
    {
      if ( source == null || type == null )
        return source;
      // check for an invalid req type.
      if ( type == Opc.Type.ILLEGAL_TYPE )
      {
        throw new ResultIDException( ResultID.Da.E_BADTYPE );
      }

      // check for no conversion.
      if ( type.IsInstanceOfType( source ) )
      {
        return Opc.Convert.Clone( source );
      }

      // convert the data.
      if ( supportsCOM )
      {
        return ChangeTypeForCOM( source, type );
      }
      else
      {
        return ChangeType( source, type );
      }
    }
    /// <summary>
    /// Converts a value to the specified type using .NET conversion rules.
    /// </summary>
    private static object ChangeType( object source, System.Type type )
    {
      try
      {
        // check for array conversion.
        if ( source.GetType().IsArray && type.IsArray )
        {
          ArrayList array = new ArrayList();
          foreach ( object element in (Array)source )
          {
            try
            {
              array.Add( ChangeType( element, type.GetElementType() ) );
            }
            catch
            {
              throw new ResultIDException( ResultID.Da.E_BADTYPE );
            }
          }
          return array.ToArray( type.GetElementType() );
        }
        else if ( !source.GetType().IsArray && !type.IsArray )
        {
          if ( type == typeof( SByte ) ) { return System.Convert.ToSByte( source ); }
          if ( type == typeof( Byte ) ) { return System.Convert.ToByte( source ); }
          if ( type == typeof( Int16 ) ) { return System.Convert.ToInt16( source ); }
          if ( type == typeof( UInt16 ) ) { return System.Convert.ToUInt16( source ); }
          if ( type == typeof( Int32 ) ) { return System.Convert.ToInt32( source ); }
          if ( type == typeof( UInt32 ) ) { return System.Convert.ToUInt32( source ); }
          if ( type == typeof( Int64 ) ) { return System.Convert.ToInt64( source ); }
          if ( type == typeof( UInt64 ) ) { return System.Convert.ToUInt64( source ); }
          if ( type == typeof( Int64 ) ) { return System.Convert.ToInt64( source ); }
          if ( type == typeof( Decimal ) ) { return System.Convert.ToDecimal( source ); }
          if ( type == typeof( String ) ) { return System.Convert.ToString( source ); }

          if ( type == typeof( Single ) )
          {
            Single output = System.Convert.ToSingle( source );

            if ( Single.IsInfinity( output ) || Single.IsNaN( output ) )
            {
              throw new ResultIDException( ResultID.Da.E_RANGE );
            }

            return output;
          }

          if ( type == typeof( Double ) )
          {
            Double output = System.Convert.ToDouble( source );

            if ( Double.IsInfinity( output ) || Double.IsNaN( output ) )
            {
              throw new ResultIDException( ResultID.Da.E_RANGE );
            }

            return output;
          }

          if ( type == typeof( DateTime ) )
          {
            // check for conversions to date time from string.
            if ( typeof( string ).IsInstanceOfType( source ) )
            {
              string dateTime = ( (string)source ).Trim();

              // check for XML schema date/time format.
              if ( dateTime.IndexOf( '-' ) == 4 )
              {
                try
                {
                  return System.Xml.XmlConvert.ToDateTime( (string)source, XmlDateTimeSerializationMode.Utc );
                }
                catch
                {
                  // ignore errors - try the localized version next.
                }
              }
            }

            // use default conversion. 
            return System.Convert.ToDateTime( source );
          }

          if ( type == typeof( Boolean ) )
          {
            // check for conversions to boolean from string.
            if ( typeof( string ).IsInstanceOfType( source ) )
            {
              string text = ( (string)source ).Trim().ToLower();

              // check for XML schema defined true values.
              if ( text == "true" || text == "1" )
              {
                return true;
              }

              // check for XML schema defined false values.
              if ( text == "false" || text == "0" )
              {
                return true;
              }
            }

            // use default conversion. 
            return System.Convert.ToBoolean( source );
          }
        }
        else if ( source.GetType().IsArray && type == typeof( string ) )
        {
          int count = ( (Array)source ).Length;

          StringBuilder buffer = new StringBuilder();

          buffer.Append( "{" );

          foreach ( object element in (Array)source )
          {
            buffer.Append( (string)ChangeType( element, typeof( string ) ) );

            count--;

            if ( count > 0 )
            {
              buffer.Append( " | " );
            }
          }

          buffer.Append( "}" );

          return buffer.ToString();
        }

        // no conversions between scalar and array types allowed.
        throw new ResultIDException( ResultID.Da.E_BADTYPE );
      }
      catch ( ResultIDException e )
      {
        throw e;
      }
      catch ( Exception e )
      {
        throw new ResultIDException( ResultID.Da.E_BADTYPE, e.Message, e );
      }
    }

    /// <summary>
    /// Converts a value to the specified type using COM conversion rules.
    /// </summary>
    private static object ChangeTypeForCOM( object source, System.Type type )
    {
      // check for conversions to date time from string.
      if ( typeof( string ).IsInstanceOfType( source ) && type == typeof( DateTime ) )
      {
        try { return System.Convert.ToDateTime( (string)source ); }
        catch { }
      }

      // check for conversions from date time to boolean.
      if ( typeof( DateTime ).IsInstanceOfType( source ) && type == typeof( bool ) )
      {
        return !( new DateTime( 1899, 12, 30, 0, 0, 0 ).Equals( source ) );
      }

      // check for conversions from float to double.
      if ( typeof( float ).IsInstanceOfType( source ) && type == typeof( double ) )
      {
        return System.Convert.ToDouble( (Single)source );
      }

      // check for array conversion.
      if ( source.GetType().IsArray && type.IsArray )
      {
        ArrayList array = new ArrayList();

        foreach ( object element in (Array)source )
        {
          try
          {
            array.Add( ChangeTypeForCOM( element, type.GetElementType() ) );
          }
          catch
          {
            throw new ResultIDException( new ResultID( DISP_E_OVERFLOW ) );
          }
        }

        return array.ToArray( type.GetElementType() );
      }
      else if ( !source.GetType().IsArray && !type.IsArray )
      {
        IntPtr pvargDest = Marshal.AllocCoTaskMem( 16 );
        IntPtr pvarSrc = Marshal.AllocCoTaskMem( 16 );

        VariantInit( pvargDest );
        VariantInit( pvarSrc );

        Marshal.GetNativeVariantForObject( source, pvarSrc );

        try
        {
          // get vartype.
          short vt = (short)GetType( type );

          // change type.
          int error = VariantChangeTypeEx(
            pvargDest,
            pvarSrc,
            Thread.CurrentThread.CurrentCulture.LCID,
            VARIANT_NOVALUEPROP | VARIANT_ALPHABOOL,
            vt );

          // check error code.
          if ( error != 0 )
          {
            throw new ResultIDException( new ResultID( error ) );
          }

          // unmarshal result.
          object result = Marshal.GetObjectForNativeVariant( pvargDest );

          // check for invalid unsigned <=> signed conversions.
          switch ( (VarEnum)vt )
          {
            case VarEnum.VT_I1:
            case VarEnum.VT_I2:
            case VarEnum.VT_I4:
            case VarEnum.VT_I8:
            case VarEnum.VT_UI1:
            case VarEnum.VT_UI2:
            case VarEnum.VT_UI4:
            case VarEnum.VT_UI8:
              {
                // ignore issue for conversions from boolean.
                if ( typeof( bool ).IsInstanceOfType( source ) )
                {
                  break;
                }

                decimal sourceAsDecimal = 0;
                decimal resultAsDecimal = System.Convert.ToDecimal( result );

                try { sourceAsDecimal = System.Convert.ToDecimal( source ); }
                catch { sourceAsDecimal = 0; }

                if ( ( sourceAsDecimal < 0 && resultAsDecimal > 0 ) || ( sourceAsDecimal > 0 && resultAsDecimal < 0 ) )
                {
                  throw new ResultIDException( ResultID.Da.E_RANGE );
                }

                break;
              }

            case VarEnum.VT_R8:
              {
                // fix precision problem introduced with conversion from float to double.
                if ( typeof( float ).IsInstanceOfType( source ) )
                {
                  result = System.Convert.ToDouble( source.ToString() );
                }

                break;
              }
          }

          return result;
        }
        finally
        {
          VariantClear( pvargDest );
          VariantClear( pvarSrc );

          Marshal.FreeCoTaskMem( pvargDest );
          Marshal.FreeCoTaskMem( pvarSrc );
        }
      }
      else if ( source.GetType().IsArray && type == typeof( string ) )
      {
        int count = ( (Array)source ).Length;

        StringBuilder buffer = new StringBuilder();

        buffer.Append( "{" );

        foreach ( object element in (Array)source )
        {
          buffer.Append( (string)ChangeTypeForCOM( element, typeof( string ) ) );

          count--;

          if ( count > 0 )
          {
            buffer.Append( " | " );
          }
        }

        buffer.Append( "}" );

        return buffer.ToString();
      }

      // no conversions between scalar and array types allowed.
      throw new ResultIDException( ResultID.Da.E_BADTYPE );
    }

    #region oleout32.dll
    /// <summary>
    /// Converts a variant from one type to another, using a LCID
    /// </summary>
    /// <param name="pvargDest">Pointer to the VARIANTARG to receive the coerced type. If this is the same as pvarSrc, 
    /// the variant will be converted in place. </param>
    /// <param name="pvarSrc">Pointer to the source VARIANTARG to be coerced. </param>
    /// <param name="lcid">The LCID for the variant to coerce. The LCID is useful when the type of 
    /// the source or destination VARIANTARG is VT_BSTR, VT_DISPATCH, or VT_DATE. </param>
    /// <param name="wFlags">
    /// Flags that control the coercion. Acceptable values are: 
    ///    VARIANT_NOVALUEPROP. Prevents the function from attempting to coerce an object to a fundamental type by getting the Value property. 
    ///       Applications should set this flag only if necessary, because it makes their behavior inconsistent with other applications.
    ///    VARIANT_ALPHABOOL. Converts a VT_BOOL value to a string containing either "True" or "False". 
    ///    VARIANT_NOUSEROVERRIDE. For conversions to or from VT_BSTR, passes LOCALE_NOUSEROVERRIDE to the core coercion routines.
    ///    VARIANT_LOCALBOOL. For conversions from VT_BOOL to VT_BSTR and back, uses the language specified by the locale in use on 
    ///    the local computer. </param>
    /// <param name="vt">
    ///   The type to coerce to. If the return code is S_OK, the vt field of the *pvargDest is guaranteed to be equal 
    ///   to this value. 
    ///   </param>
    /// <returns></returns>
    [DllImport( "OleAut32.dll" )]
    private static extern int VariantChangeTypeEx(
      IntPtr pvargDest,
      IntPtr pvarSrc,
      int lcid,
      ushort wFlags,
      short vt );

    [DllImport( "oleaut32.dll" )]
    private static extern void VariantInit( IntPtr pVariant );

    [DllImport( "oleaut32.dll" )]
    private static extern void VariantClear( IntPtr pVariant );

    private const int DISP_E_TYPEMISMATCH = -0x7FFDFFFB; // 0x80020005
    private const int DISP_E_OVERFLOW = -0x7FFDFFF6; // 0x8002000A

    private const int VARIANT_NOVALUEPROP = 0x01;
    private const int VARIANT_ALPHABOOL = 0x02; // For VT_BOOL to VT_BSTR conversions convert to "True"/"False" instead of
    #endregion oleout32.dll

    /// <summary>
    /// Converts the system type to a VARTYPE.
    /// </summary>
    internal static VarEnum GetType( System.Type input )
    {
      if ( input == null )
        return VarEnum.VT_EMPTY;
      if ( input == typeof( sbyte ) )
        return VarEnum.VT_I1;
      if ( input == typeof( byte ) )
        return VarEnum.VT_UI1;
      if ( input == typeof( short ) )
        return VarEnum.VT_I2;
      if ( input == typeof( ushort ) )
        return VarEnum.VT_UI2;
      if ( input == typeof( int ) )
        return VarEnum.VT_I4;
      if ( input == typeof( uint ) )
        return VarEnum.VT_UI4;
      if ( input == typeof( long ) )
        return VarEnum.VT_I8;
      if ( input == typeof( ulong ) )
        return VarEnum.VT_UI8;
      if ( input == typeof( float ) )
        return VarEnum.VT_R4;
      if ( input == typeof( double ) )
        return VarEnum.VT_R8;
      if ( input == typeof( decimal ) )
        return VarEnum.VT_CY;
      if ( input == typeof( bool ) )
        return VarEnum.VT_BOOL;
      if ( input == typeof( DateTime ) )
        return VarEnum.VT_DATE;
      if ( input == typeof( string ) )
        return VarEnum.VT_BSTR;
      if ( input == typeof( object ) )
        return VarEnum.VT_EMPTY;
      if ( input == typeof( sbyte[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_I1;
      if ( input == typeof( byte[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_UI1;
      if ( input == typeof( short[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_I2;
      if ( input == typeof( ushort[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_UI2;
      if ( input == typeof( int[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_I4;
      if ( input == typeof( uint[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_UI4;
      if ( input == typeof( long[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_I8;
      if ( input == typeof( ulong[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_UI8;
      if ( input == typeof( float[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_R4;
      if ( input == typeof( double[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_R8;
      if ( input == typeof( decimal[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_CY;
      if ( input == typeof( bool[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_BOOL;
      if ( input == typeof( DateTime[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_DATE;
      if ( input == typeof( string[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_BSTR;
      if ( input == typeof( object[] ) )
        return VarEnum.VT_ARRAY | VarEnum.VT_VARIANT;

      return VarEnum.VT_EMPTY;
    }
    /// <summary>
    /// Reads the value from the cache and converts it to the rqeuested type.
    /// </summary>
    internal ItemValueResult Read( string locale, System.Type reqType, int maxAge, bool supportsCOM )
    {
      // read value from device.
      DateTime target = DateTimeProvider.GetCurrentTime().AddMilliseconds( ( maxAge < 0 ) ? 0 : -maxAge );



      ItemValueResult result = m_settings.ReadNewValue( maxAge, target );

      if ( result.ResultID.Succeeded() )
      {
        try
        {
          object currentvalue = result.Value;
          if ( m_settings.EuType != euType.enumerated || reqType != typeof( string ) )
          {
            result.Value = ChangeType( currentvalue, reqType, locale, supportsCOM );
          }
          else
          {
            result.Value = m_settings.EuInfo[ System.Convert.ToInt32( currentvalue ) ];
          }
        }
        catch ( OverflowException e )
        {
          result.Quality = Quality.Bad;
          result.ResultID = ResultID.Da.E_RANGE;
          result.DiagnosticInfo = e.Message;
        }
        catch ( InvalidCastException e )
        {
          result.Quality = Quality.Bad;
          result.ResultID = ResultID.Da.E_RANGE;
          result.DiagnosticInfo = e.Message;
        }
        catch ( ResultIDException e )
        {
          result.Quality = Quality.Bad;
          result.ResultID = e.Result;
          result.DiagnosticInfo = e.Message;
        }
        catch ( Exception e )
        {
          result.Quality = Quality.Bad;
          result.ResultID = ResultID.Da.E_BADTYPE;
          result.DiagnosticInfo = e.Message;
        }
      }
      return result;
    }
    /// <summary>
    /// Writes a value to the device.
    /// </summary>
    internal IdentifiedResult Write( string locale, ItemValue value, bool supportsCOM )
    {
      // check for invalid values.
      if ( value == null || value.Value == null )
      {
        return new IdentifiedResult( m_settings.ItemID, ResultID.Da.E_BADTYPE );
      }

      ItemValue canonicalValue = new ItemValue();

      // convert non-enumerated type to canonical type.
      if ( m_settings.EuType != euType.enumerated || !typeof( string ).IsInstanceOfType( value.Value ) )
      {
        try
        {
          canonicalValue.Value = ChangeType( value.Value, m_settings.DataType, locale, supportsCOM );
        }
        catch ( OverflowException e )
        {
          return new IdentifiedResult( m_settings.ItemID, ResultID.Da.E_RANGE, e.Message );
        }
        catch ( InvalidCastException e )
        {
          return new IdentifiedResult( m_settings.ItemID, ResultID.Da.E_RANGE, e.Message );
        }
        catch ( ResultIDException e )
        {
          return new IdentifiedResult( m_settings.ItemID, e.Result, e.Message );
        }
        catch ( Exception e )
        {
          return new IdentifiedResult( m_settings.ItemID, ResultID.Da.E_BADTYPE, e.Message );
        }
      }
      else
      {
        for ( int ii = 0; ii < m_settings.EuInfo.Length; ii++ )
        {
          if ( m_settings.EuInfo[ ii ] == (string)value.Value )
          {
            canonicalValue.Value = ii;
            break;
          }
        }

        if ( canonicalValue.Value == null )
        {
          return new IdentifiedResult( m_settings.ItemID, ResultID.Da.E_BADTYPE );
        }
      }

      canonicalValue.Quality = value.Quality;
      canonicalValue.QualitySpecified = value.QualitySpecified;
      canonicalValue.Timestamp = value.Timestamp;
      canonicalValue.TimestampSpecified = value.TimestampSpecified;

      return m_settings.Device.Write( m_settings.ItemIndex, Property.VALUE, canonicalValue );
    }
    /// <summary>
    /// Reads the value of the specified property.
    /// </summary>
    internal object ReadProperty( PropertyID propertyID )
    {
      return ReadProperty( propertyID, m_settings.Device, m_settings.ItemIndex );
    }
    internal static object ReadProperty( PropertyID propertyID, IDeviceIndexed device, ushort ItemIndex )
    {
      ItemValueResult result = device.Read( ItemIndex, propertyID );
      if ( result == null || result.ResultID.Failed() )
      {
        return null;
      }
      return result.Value;
    }
    #region Private Members
    private CacheItemSettings m_settings;
    #endregion
  }
}
