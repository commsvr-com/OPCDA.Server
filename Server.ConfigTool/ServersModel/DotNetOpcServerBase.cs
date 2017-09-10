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

using Microsoft.Win32;
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
      Tuple<string, RegistryView> _id = clsid.ProgIDFromCLSID();
      if (_id == null)
        throw new ApplicationException($"Component {clsid} is not registered");
      ProgId = _id.Item1;
      Is64BitComponent = _id.Item2 == RegistryView.Registry64;
    }
    /// <summary>
    /// The CLSID for the wrapped object.
    /// </summary>
    public Guid CLSID
    {
      get; protected set;
    } = Guid.Empty;
    /// <summary>
    /// Gets a value indicating whether it is 64 bit component.
    /// </summary>
    /// <value><c>true</c> if it is is64 bit component; otherwise, <c>false</c>.</value>
    /// TODO Edit XML Comment Template for Is64BitComponent
    public bool Is64BitComponent { get; private set; } = false;

    /// <summary>
    /// The ProgId for the wrapped object.
    /// </summary>
    public string ProgId
    {
      get; protected set;
    } = string.Empty;
    /// <summary>
    /// Initializes this instance - set private members to default values.
    /// </summary>
    protected virtual void Initialize() { }

  }
}
