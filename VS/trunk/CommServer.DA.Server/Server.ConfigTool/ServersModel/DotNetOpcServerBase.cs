//_______________________________________________________________
//  Title   : Name of Application
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

using OpcRcw;
using System;

namespace CAS.CommServer.DA.Server.ConfigTool.ServersModel
{
  public abstract class DotNetOpcServerBase
  {
    public DotNetOpcServerBase()
    {
      Initialize();
    }
    public DotNetOpcServerBase(Guid clsid)
    {
      CLSID = clsid;
      ProgId = Utils.ProgIDFromCLSID(clsid);
    }
    protected virtual void Initialize(){ }

    /// <summary>
    /// The CLSID for the wrapped object.
    /// </summary>
    public Guid CLSID
    {
      get; protected set;
    } = Guid.Empty;
    /// <summary>
    /// The ProgId for the wrapped object.
    /// </summary>
    public string ProgId
    {
      get; protected set;
    } = null;

  }
}
