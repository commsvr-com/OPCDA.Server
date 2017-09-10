//_______________________________________________________________
//  Title   : DotNetOpcServerWrapper
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
using System.Collections.Generic;

namespace CAS.CommServer.DA.Server.ConfigTool.ServersModel
{
  /// <summary>
  /// A class that describes a wrapped object.
  /// </summary>
  public class DotNetOpcServerWrapper : DotNetOpcServerBase
  {

    #region Constructors
    /// <summary>
    /// The default constructor.
    /// </summary>
    public DotNetOpcServerWrapper() : base() { }
    /// <summary>
    /// Initializes the object with a url.
    /// </summary>
    public DotNetOpcServerWrapper(Guid clsid) : base(clsid)
    {
      m_codebase = clsid.GetExecutablePath();
      m_specifications = GetSpecifications(clsid);
    }
    #endregion

    #region Public
    /// <summary>
    /// The file path for the EXE for the wrapper.
    /// </summary>
    public string Codebase
    {
      get { return m_codebase; }
    }
    /// <summary>
    /// The specifications supported by the wrapper.
    /// </summary>
    public Specifications Specifications
    {
      get { return m_specifications; }
    }
    /// <summary>
    /// Returns the wrappers registered on the local machine.
    /// </summary>
    public static List<DotNetOpcServerWrapper> EnumWrappers()
    {
      // enumerate clsids.
      List<Guid> _CLSIDs = Utils.EnumClassesInCategories(CommonDefinitions.CATID_DotNetOpcServerWrappers);
      // initialize objects.
      List<DotNetOpcServerWrapper> _servers = new List<DotNetOpcServerWrapper>();
      for (int ii = 0; ii < _CLSIDs.Count; ii++)
        _servers.Add(new DotNetOpcServerWrapper(_CLSIDs[ii]));
      return _servers;
    }
    /// <summary>
    /// Finds the OPC specifications supported by the .NET server.
    /// </summary>
    private static Specifications GetSpecifications(Guid clsid)
    {
      if (clsid == Guid.Empty)
        return Specifications.None;
      Specifications specifications = Specifications.None;
      foreach (Guid _CATID in Utils.GetImplementedCategories(clsid))
      {
        if (_CATID == typeof(OpcRcw.Da.CATID_OPCDAServer20).GUID)
          specifications |= Specifications.DA2;
        if (_CATID == typeof(OpcRcw.Da.CATID_OPCDAServer30).GUID)
          specifications |= Specifications.DA3;
        if (_CATID == typeof(OpcRcw.Ae.CATID_OPCAEServer10).GUID)
          specifications |= Specifications.AE;
        if (_CATID == typeof(OpcRcw.Hda.CATID_OPCHDAServer10).GUID)
          specifications |= Specifications.HDA;
      }
      return specifications;
    }
    #endregion

    #region Overridden Methods
    /// <summary>
    /// Converts the object to a displayable string.
    /// </summary>
    public override string ToString()
    {
      if (!String.IsNullOrEmpty(ProgId))
        return ProgId;
      if (CLSID == Guid.Empty)
        return "(unknown)";
      return CLSID.ToString();
    }
    #endregion

    #region Private Members
    private string m_codebase;
    private Specifications m_specifications;
    protected override void Initialize()
    {
      m_codebase = null;
      m_specifications = Specifications.None;
      base.Initialize();
    }
    #endregion

  }
}
