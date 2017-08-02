//_______________________________________________________________
//  Title   : IOPCWrappedServer
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
using System.Runtime.InteropServices;

namespace CAS.CommServer.DA.Server.ConfigTool
{
  /// <summary>
  /// An interface that is invoked when the wrapper loads/unloads the wrapped .net server.
  /// </summary>
  [ComImport]
  [GuidAttribute("50E8496C-FA60-46a4-AF72-512494C664C6")]
  [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
  //TODO it is defined in CAS.CommServer.OPCClassic.SDK.COMWrapper - consider to use NuGet package
  public interface IOPCWrappedServer
  {
    /// <summary>
    /// Called when the object is loaded by the COM wrapper process.
    /// </summary>
    void Load([MarshalAs(UnmanagedType.LPStruct)] Guid clsid);
    /// <summary>
    /// Called when the object is unloaded by the COM wrapper process.
    /// </summary>
    void Unload();
  }
}
