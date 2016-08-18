//<summary>
//  Title   : DataSetHelpers
//  System  : Microsoft Visual C# .NET 
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//  20081105: mzbrzezny: WrappersHelpers: additional check in GetName
//  20081006: mzbrzezny: AddressSpaceDescriptor and Item Default Settings are implemented.
//  20081003: mzbrzezny: class is marked as internal
//  20081003: mzbrzezny: AddressSpaceDescriptor implementation
//  Tomasz Siwecki - February 2007 Add some comment and reformat code
//  Tomasz Siwecki - October 2006
//  Created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Collections;
using System.Collections.Generic;
using CAS.Lib.CommonBus;

namespace NetworkConfig.HMI
{
  /// <summary>
  /// Class containg methods responsible for converting some numeric parameters humen readable format 
  /// </summary>
  internal class WrappersHelpers
  {
    #region Methods
    /// <summary>
    /// Returns the name related to specifed id 
    /// </summary>
    /// <param name="table">has table with  enum </param>
    /// <param name="id">Id that will be changed to the name</param>
    /// <returns>String related to the id</returns>
    internal static string GetName( SortedList<short, IAddressSpaceDescriptor> table, short? id )
    {
      string name = "N/A";
      if ( id.HasValue && table != null )
      {
        try
        {
          return table[ (short)id ].Name;
        }
        catch ( Exception )
        {
          if ( id.HasValue )
          {
            return id.ToString();
          }
        }
      }
      else
      {
        if ( table == null && id.HasValue )
        {
          return id.ToString();
        }
      }
      return name;
    }
    /// <summary>
    /// Returns the id related to specified name
    /// </summary>
    /// <param name="table">has table with  enum </param>
    /// <param name="name">Name that will be changed to id</param>
    /// <returns>Integer related to the specified name</returns>
    internal static short? GetID( SortedList<short, IAddressSpaceDescriptor> table, string name )
    {
      if ( table != null )
      {
        // znaczy sie jest tablica zawierajaca dane
        foreach ( KeyValuePair<short, IAddressSpaceDescriptor> kvpIAddressSpaceDescriptor in table )
        {
          if ( kvpIAddressSpaceDescriptor.Value.Name == name )
            return kvpIAddressSpaceDescriptor.Value.Identifier;
        }
      }
      // probujemy dokonac konwersji name do int, jesli sie uda to konfiguracja jest dobra
      // jesli nie to znczy ze zostawiamy null
      try
      {
        return System.Convert.ToInt16( name );
      }
      catch ( Exception )
      {
        return null;
      }
    }
    /// <summary>
    /// Returns the id related to specified name
    /// </summary>
    /// <param name="table">has table with  enum </param>
    /// <param name="name">Name that will be changed to id</param>
    /// <returns>Integer related to the specified name</returns>
    internal static short? GetID( IAddressSpaceDescriptor[] table, string name )
    {
      if ( table != null )
      {
        // znaczy sie jest tablica zawierajaca dane
        foreach ( IAddressSpaceDescriptor myIAddressSpaceDescriptor in table )
        {
          if ( myIAddressSpaceDescriptor.Name == name )
            return myIAddressSpaceDescriptor.Identifier;
        }
      }
      // probujemy dokonac konwersji name do int, jesli sie uda to konfiguracja jest dobra
      // jesli nie to znczy ze zostawiamy null
      try
      {
        return System.Convert.ToInt16( name );
      }
      catch ( Exception )
      {
        return null;
      }
    }
    /// <summary>
    /// returns an array of string that are keys of the <see cref="Hashtable"/>
    /// </summary>
    /// <param name="array">The array of address space descriptors.</param>
    /// <returns>all keys</returns>
    internal static string[] GetNames( SortedList<short, IAddressSpaceDescriptor> array )
    {
      int count = 256;
      if ( array != null )
        count = array.Keys.Count;
      string[] return_array = new string[ count ];
      int idx = 0;
      if ( array != null )
      {
        foreach ( IAddressSpaceDescriptor AddressSpaceDescriptor in array.Values )
        {
          return_array[ idx++ ] = AddressSpaceDescriptor.Name;
        }
      }
      else
        for ( int i = 0; i < 256; i++ )
          return_array[ i ] = i.ToString();
      return return_array;
    }
    #endregion
  }
}
