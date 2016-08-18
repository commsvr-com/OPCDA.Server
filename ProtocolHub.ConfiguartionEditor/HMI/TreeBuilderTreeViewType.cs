//<summary>
//  Title   : TreeBuilderTreeViewType
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    <Author>Tomek Siwecki - February 2007 - Created <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
namespace NetworkConfig
{
  /// <summary>
  /// Represents TreeView type
  /// </summary>
  public enum TreeBuilderTreeViewType
  {
    /// <summary>
    /// From stations
    /// </summary>
    Stations = 0,

    /// <summary>
    /// From Channels
    /// </summary>
    Channels = 1,

    ///<summary>
    /// From stations and channels
    ///</summary>
    StationsAndChannels = 2,

  }
}