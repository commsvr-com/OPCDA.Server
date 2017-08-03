//_______________________________________________________________
//  Title   : Specifications
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate:  $
//  $Rev: $
//  $LastChangedBy: $
//  $URL: $
//  $Id:  $
//
//  Copyright (C) 2017, CAS LODZ POLAND.
//  TEL: +48 608 61 98 99 
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using System;

namespace CAS.CommServer.DA.Server.ConfigTool.ServersModel
{

  /// <summary>
  /// The OPC specifications supported by a .NET server.
  /// </summary>
  [Flags]
  public enum Specifications
  {
    /// <summary>
    /// Does not support any OPC specifications.
    /// </summary>
    None = 0x00,
    /// <summary>
    /// Supports Data Access 2.0
    /// </summary>
    DA2 = 0x01,
    /// <summary>
    /// Supports Data Access 3.0
    /// </summary>
    DA3 = 0x02,
    /// <summary>
    /// Supports Alarms and Events 1.1
    /// </summary>
    AE = 0x04,
    /// <summary>
    /// Supports Historical Data Access 1.2
    /// </summary>
    HDA = 0x08
  }

}
