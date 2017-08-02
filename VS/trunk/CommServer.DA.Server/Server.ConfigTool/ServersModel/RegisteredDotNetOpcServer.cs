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
using OpcRcw;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace CAS.CommServer.DA.Server.ConfigTool.ServersModel
{

  /// <summary>
  /// A class that describes a .NET implementation of an OPC server that has been registered with the category manager.
  /// </summary>
  public class RegisteredDotNetOpcServer
  {

    #region Constructors
    /// <summary>
    /// The default constructor.
    /// </summary>
    public RegisteredDotNetOpcServer()
    {
      Initialize();
    }
    /// <summary>
    /// Initializes the object from the registry.
    /// </summary>
    public RegisteredDotNetOpcServer(Guid clsid)
    {
      CLSID = clsid;
      ProgId = Utils.ProgIDFromCLSID(clsid);
      m_description = GetDescription(clsid);
      WrapperCLSID = GetWrapper(clsid);
      ServerCLSID = GetDotNetOpcServer(clsid);
      m_parameters = GetParameters(clsid);
      if (m_description == ProgId)
        m_description = null;
    }
    #endregion

    #region Public API
    /// <summary>
    /// The CLSID for the .NET OPC server.
    /// </summary>
    public Guid CLSID
    {
      get; set;
    }
    /// <summary>
    /// The prog id for the .NET OPC server.
    /// </summary>
    public string ProgId
    {
      get; set;
    }
    /// <summary>
    /// The description for the .NET OPC server.
    /// </summary>
    public string Description
    {
      get { return m_description; }
      set { m_description = value; }
    }
    /// <summary>
    /// The CLSID for the .NET OPC server wrapper.
    /// </summary>
    public Guid WrapperCLSID
    {
      get; set;
    }
    /// <summary>
    /// The CLSID for .NET OPC server implementation.
    /// </summary>
    public Guid ServerCLSID
    {
      get; set;
    }
    /// <summary>
    /// The configuration parameters to pass to the server when it is instantiated..
    /// </summary>
    public IDictionary<string, string> Parameters
    {
      get { return m_parameters; }
    }
    /// <summary>
    /// Imports a list of servers from a file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="register">if set to <c>true</c> the server is registered, otherwise unregistered.</param>
    public static void Import(string filePath, bool register)
    {
      Export.ListOfRegisteredServers list = new Export.ListOfRegisteredServers();
      // read from file.
      XmlTextReader reader = new XmlTextReader(filePath);
      XmlSerializer serializer = new XmlSerializer(typeof(Export.ListOfRegisteredServers), ConfigUtilities.ConfigToolSchemaUri);
      Export.ListOfRegisteredServers servers = (Export.ListOfRegisteredServers)serializer.Deserialize(reader);
      reader.Close();
      if (ConfigUtilities.IsEmpty(servers.Server))
        return;
      // registers the servers.
      for (int ii = 0; ii < servers.Server.Length; ii++)
      {
        RegisteredDotNetOpcServer server = Import(servers.Server[ii]);
        if (register)
          server.Register();
        else
          server.Unregister();
      }
    }
    /// <summary>
    /// Exports a list of servers to the file.
    /// </summary>
    /// <param name="filePath">The filename to write to. If the file exists, it truncates it and overwrites it with the new content..</param>
    public static void Export(string filePath)
    {
      // enumerate servers.
      List<RegisteredDotNetOpcServer> _servers = EnumRegisteredServers(false);
      // populate export structures.
      Export.ListOfRegisteredServers _serverList = new Export.ListOfRegisteredServers();
      _serverList.Server = new Export.RegisteredServer[_servers.Count];
      for (int ii = 0; ii < _servers.Count; ii++)
        _serverList.Server[ii] = Export(_servers[ii]);
      // write to file.
      XmlTextWriter writer = new XmlTextWriter(filePath, System.Text.Encoding.UTF8);
      writer.Formatting = Formatting.Indented;
      XmlSerializer serializer = new XmlSerializer(typeof(Export.ListOfRegisteredServers), ConfigUtilities.ConfigToolSchemaUri);
      serializer.Serialize(writer, _serverList);
      writer.Close();
    }
    /// <summary>
    /// Returns the UA COM servers registered on the local computer.
    /// </summary>
    public static List<RegisteredDotNetOpcServer> EnumRegisteredServers(bool updateWrapperPath)
    {
      // enumerate server clsids.
      List<Guid> _CLSIDs = Utils.EnumClassesInCategories(ConfigUtilities.CATID_RegisteredDotNetOpcServers);
      // initialize server objects.
      List<RegisteredDotNetOpcServer> _servers = new List<RegisteredDotNetOpcServer>();
      for (int ii = 0; ii < _CLSIDs.Count; ii++)
      {
        _servers.Add(new RegisteredDotNetOpcServer(_CLSIDs[ii]));
        if (updateWrapperPath)
        {
          string _wrapperPath = Utils.GetExecutablePath(_servers[ii].WrapperCLSID);
          if (String.IsNullOrEmpty(_wrapperPath))
            continue;
          RegistryKey key = Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\LocalServer32", _CLSIDs[ii]), true);
          if (key != null)
          {
            key.SetValue(null, _wrapperPath);
            key.Close();
          }
        }
      }
      return _servers;
    }
    /// <summary>
    /// Registers this instance as a COM server with the specified CLSID.
    /// </summary>
    public void Register()
    {
      DotNetOpcServer _server = new DotNetOpcServer(ServerCLSID);
      if (_server.Specifications == Specifications.None)
        throw new ApplicationException("The .NET server does not implement any OPC specifications.");
      DotNetOpcServerWrapper _wrapper = new DotNetOpcServerWrapper(WrapperCLSID);
      if (_wrapper.Specifications == Specifications.None)
        throw new ApplicationException("The .NET server wrapper does not implement any OPC interfaces.");
      // determine the intersection of between the specs supported by the wrapper and the specs supported by the server.
      Specifications specifications = _wrapper.Specifications & _server.Specifications;
      if (specifications == Specifications.None)
        throw new ApplicationException("The .NET server wrapper does not implement any OPC interfaces supported by the .NET server.");
      // verify url and prog id.
      string _progId = ProgId;
      if (WrapperCLSID == Guid.Empty || String.IsNullOrEmpty(_progId))
        throw new ApplicationException("Proxy does not have a valid wrapper clsid or prog id.");
      // verify wrapper path.
      string _wrapperPath = Utils.GetExecutablePath(WrapperCLSID);
      if (_wrapperPath == null)
        throw new ApplicationException("OPC server wrapper is not registered on this machine.");
      // remove existing CLSID.
      Guid _existingClsid = Utils.CLSIDFromProgID(_progId);
      if (_existingClsid != CLSID)
        Utils.UnregisterComServer(_existingClsid);
      string _CLSIDKey = String.Format(@"CLSID\{{{0}}}", CLSID.ToString().ToUpper());
      // create new entries.					
      RegistryKey _key = Registry.ClassesRoot.CreateSubKey(_CLSIDKey);
      if (_key == null)
        throw new ApplicationException("Could not create key: " + _CLSIDKey);
      // save description.
      if (String.IsNullOrEmpty(m_description))
        m_description = _progId;
      _key.SetValue(null, m_description);
      try
      {
        // create local server key.
        RegistryKey _subkey = _key.CreateSubKey("LocalServer32");
        if (_subkey == null)
          throw new ApplicationException("Could not create key: LocalServer32");
        _subkey.SetValue(null, _wrapperPath);
        _subkey.SetValue("WrapperClsid", String.Format("{{{0}}}", WrapperCLSID));
        _subkey.Close();
        // create prog id key.
        _subkey = _key.CreateSubKey("ProgId");
        if (_subkey == null)
          throw new ApplicationException("Could not create key: ProgId");
        _subkey.SetValue(null, _progId);
        _subkey.Close();
        // create endpoint key.
        _subkey = _key.CreateSubKey(WrappedServerSubKey);
        if (_subkey == null)
          throw new ApplicationException("Could not create key: " + WrappedServerSubKey);
        // add parameters.
        try
        {
          _subkey.SetValue(null, String.Format("{{{0}}}", ServerCLSID));
          // remove unused parameters.
          foreach (string name in _subkey.GetValueNames())
            if (!String.IsNullOrEmpty(name) && !m_parameters.ContainsKey(name))
              _subkey.DeleteValue(name, false);
          // add new parameters.
          foreach (KeyValuePair<string, string> entry in m_parameters)
            if (!String.IsNullOrEmpty(entry.Key))
              _subkey.SetValue(entry.Key, entry.Value);
        }
        finally
        {
          _subkey.Close();
        }
      }
      finally
      {
        _key.Close();
      }
      // create prog id key.
      _key = Registry.ClassesRoot.CreateSubKey(_progId);
      if (_key == null)
        throw new ApplicationException("Could not create key: " + _progId);
      try
      {
        // create clsid key.
        RegistryKey _subkey = _key.CreateSubKey("CLSID");
        if (_subkey == null)
          throw new ApplicationException("Could not create key: CLSID");
        _subkey.SetValue(null, String.Format("{{{0}}}", CLSID.ToString().ToUpper()));
        _subkey.Close();
        // create the OPC key use with DA 2.0 servers.
        if ((specifications & Specifications.DA2) != 0)
        {
          _subkey = _key.CreateSubKey("OPC");
          if (_subkey == null)
            throw new ApplicationException("Could not create key: OPC");
          _subkey.Close();
        }
      }
      finally
      {
        _key.Close();
      }
      // register as wrapper server.
      Utils.RegisterClassInCategory(CLSID, ConfigUtilities.CATID_RegisteredDotNetOpcServers, "OPC Wrapped COM Server Proxy");
      // register in OPC component categories.
      if ((specifications & Specifications.DA2) != 0)
        Utils.RegisterClassInCategory(CLSID, typeof(OpcRcw.Da.CATID_OPCDAServer20).GUID);
      if ((specifications & Specifications.DA3) != 0)
        Utils.RegisterClassInCategory(CLSID, typeof(OpcRcw.Da.CATID_OPCDAServer30).GUID);
      if ((specifications & Specifications.AE) != 0)
        Utils.RegisterClassInCategory(CLSID, typeof(OpcRcw.Ae.CATID_OPCAEServer10).GUID);
      if ((specifications & Specifications.HDA) != 0)
        Utils.RegisterClassInCategory(CLSID, typeof(OpcRcw.Hda.CATID_OPCHDAServer10).GUID);
    }
    /// <summary>
    /// Unregisters a COM server from the registry.
    /// </summary>
    public void Unregister()
    {
      Utils.UnregisterComServer(CLSID);
    }
    #endregion

    #region Private Members
    private const string WrappedServerSubKey = "WrappedServer";
    private string m_description;
    private Dictionary<string, string> m_parameters;
    /// <summary>
    /// Returns the server description 
    /// </summary>
    private static string GetDescription(Guid server)
    {
      string _CLSIDKey = String.Format(@"CLSID\{{{0}}}", server.ToString().ToUpper());
      RegistryKey _key = Registry.ClassesRoot.OpenSubKey(_CLSIDKey);
      if (_key != null)
      {
        try
        {
          return _key.GetValue(null) as string;
        }
        finally
        {
          _key.Close();
        }
      }
      return String.Empty;
    }
    /// <summary>
    /// Returns the wrapper 
    /// </summary>
    private static Guid GetWrapper(Guid server)
    {
      RegistryKey _key = Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\LocalServer32", server));
      if (_key != null)
      {
        try
        {
          string _CLSID = _key.GetValue("WrapperClsid") as string;
          if (_CLSID != null)
            return new Guid(_CLSID.Substring(1, _CLSID.Length - 2));
        }
        finally
        {
          _key.Close();
        }
      }
      return Guid.Empty;
    }
    /// <summary>
    /// Returns the url for the specified COM server.
    /// </summary>
    private static Guid GetDotNetOpcServer(Guid server)
    {
      RegistryKey _key = Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\{1}", server, WrappedServerSubKey));
      if (_key != null)
      {
        try
        {
          string _CLSID = _key.GetValue(null) as string;
          if (_CLSID != null)
            return new Guid(_CLSID.Substring(1, _CLSID.Length - 2));
        }
        finally
        {
          _key.Close();
        }
      }
      return Guid.Empty;
    }
    /// <summary>
    /// Returns the configuration parameters for the specified COM server.
    /// </summary>
    private static Dictionary<string, string> GetParameters(Guid clsid)
    {
      Dictionary<string, string> _parameters = new Dictionary<string, string>();
      RegistryKey key = Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\{1}", clsid, WrappedServerSubKey));
      if (key != null)
      {
        try
        {
          foreach (string name in key.GetValueNames())
            if (!String.IsNullOrEmpty(name))
              _parameters.Add(name, String.Format("{0}", key.GetValue(name)));
        }
        finally
        {
          key.Close();
        }
      }
      return _parameters;
    }
    /// <summary>
    /// Sets private members to default values.
    /// </summary>
    private void Initialize()
    {
      CLSID = Guid.Empty;
      ProgId = null;
      m_description = null;
      WrapperCLSID = Guid.Empty;
      ServerCLSID = Guid.Empty;
      m_parameters = new Dictionary<string, string>();
    }
    /// <summary>
    /// Imports a server from an export file.
    /// </summary>
    /// <param name="serverToImport">The server to import.</param>
    /// <returns>RegisteredDotNetOpcServer.</returns>
    private static RegisteredDotNetOpcServer Import(Export.RegisteredServer serverToImport)
    {
      RegisteredDotNetOpcServer server = new RegisteredDotNetOpcServer();
      // assign clsid if none specified.
      if (String.IsNullOrEmpty(serverToImport.Clsid))
      {
        server.CLSID = Utils.CLSIDFromProgID(serverToImport.ProgId);
        if (server.CLSID == Guid.Empty)
          server.CLSID = Guid.NewGuid();
      }
      else
        server.CLSID = new Guid(serverToImport.Clsid);
      // get prog id and description.
      server.ProgId = serverToImport.ProgId;
      server.Description = serverToImport.Description;
      // parse wrapper clsid/prog id.
      try
      {
        server.WrapperCLSID = new Guid(serverToImport.WrapperClsid);
      }
      catch
      {
        server.WrapperCLSID = Utils.CLSIDFromProgID(serverToImport.WrapperClsid);
      }
      // parse wrapped server clsid/prog id.
      try
      {
        server.ServerCLSID = new Guid(serverToImport.ServerClsid);
      }
      catch
      {
        server.ServerCLSID = Utils.CLSIDFromProgID(serverToImport.ServerClsid);
      }
      // read parameters.
      server.Parameters.Clear();
      if (!ConfigUtilities.IsEmpty(serverToImport.Parameter))
      {
        for (int ii = 0; ii < serverToImport.Parameter.Length; ii++)
        {
          Export.Parameter parameter = serverToImport.Parameter[ii];
          if (parameter != null && !String.IsNullOrEmpty(parameter.Name))
            server.Parameters.Add(parameter.Name, parameter.Value);
        }
      }
      // return new server.
      return server;
    }
    /// <summary>
    /// Exports a server to a export file.
    /// </summary>
    private static Export.RegisteredServer Export(RegisteredDotNetOpcServer server)
    {
      Export.RegisteredServer _serverToExport = new Export.RegisteredServer();
      _serverToExport.Clsid = server.CLSID.ToString();
      _serverToExport.ProgId = server.ProgId;
      if (server.Description != server.ProgId)
        _serverToExport.Description = server.Description;
      _serverToExport.WrapperClsid = Utils.ProgIDFromCLSID(server.WrapperCLSID);
      _serverToExport.ServerClsid = Utils.ProgIDFromCLSID(server.ServerCLSID);
      // export parameters.
      _serverToExport.Parameter = new Export.Parameter[server.Parameters.Count];
      int index = 0;
      foreach (KeyValuePair<string, string> entry in server.Parameters)
      {
        Export.Parameter parameter = new Export.Parameter();
        parameter.Name = entry.Key;
        parameter.Value = entry.Value;
        _serverToExport.Parameter[index++] = parameter;
      }
      // return exported server.
      return _serverToExport;
    }
    #endregion

  }

}
