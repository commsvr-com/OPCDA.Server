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

namespace CAS.CommServer.DA.Server.ConfigTool
{
  /// <summary>
  /// A class that describes a wrapped object.
  /// </summary>
  public class DotNetOpcServerWrapper
  {

    #region Constructors
    /// <summary>
    /// The default constructor.
    /// </summary>
    public DotNetOpcServerWrapper()
    {
      Initialize();
    }
    /// <summary>
    /// Initializes the object with a url.
    /// </summary>
    public DotNetOpcServerWrapper(Guid clsid)
    {
      m_clsid = clsid;
      m_progId = Utils.ProgIDFromCLSID(clsid);
      m_codebase = Utils.GetExecutablePath(clsid);
      m_specifications = GetSpecifications(clsid);
    }
    /// <summary>
    /// Sets private members to default values.
    /// </summary>
    #endregion

    #region Public
    /// <summary>
    /// The CLSID for the wrapper.
    /// </summary>
    public Guid Clsid
    {
      get { return m_clsid; }
    }
    /// <summary>
    /// The ProgId for the wrapper.
    /// </summary>
    public string ProgId
    {
      get { return m_progId; }
    }
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
      List<Guid> _CLSIDs = Utils.EnumClassesInCategories(ConfigUtilities.CATID_DotNetOpcServerWrappers);
      // initialize objects.
      List<DotNetOpcServerWrapper> _servers = new List<DotNetOpcServerWrapper>();
      for (int ii = 0; ii < _CLSIDs.Count; ii++)
        _servers.Add(new DotNetOpcServerWrapper(_CLSIDs[ii]));
      return _servers;
    }
    /// <summary>
    /// Finds the OPC specifications supported by the .NET server.
    /// </summary>
    private Specifications GetSpecifications(Guid clsid)
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
      if (!String.IsNullOrEmpty(m_progId))
        return m_progId;
      if (m_clsid == Guid.Empty)
        return "(unknown)";
      return m_clsid.ToString();
    }
    #endregion

    #region Private Members
    private Guid m_clsid;
    private string m_progId;
    private string m_codebase;
    private Specifications m_specifications;
    private void Initialize()
    {
      m_clsid = Guid.Empty;
      m_progId = null;
      m_codebase = null;
      m_specifications = Specifications.None;
    }
    #endregion

  }
}
