//============================================================================
// (c) Copyright 2005 The OPC Foundation
// ALL RIGHTS RESERVED.
//
// DISCLAIMER:
//  This code is provided by the OPC Foundation solely to assist in 
//  understanding and use of the appropriate OPC Specification(s) and may be 
//  used as set forth in the License Grant section of the OPC Specification.
//  This code is provided as-is and without warranty or support of any sort
//  and is subject to the Warranty and Liability Disclaimers which appear
//  in the printed OPC Specification.

using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.Win32;

namespace Opc.ConfigTool
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
			m_clsid        = clsid;
			m_progId       = ConfigUtils.ProgIDFromCLSID(clsid);
			m_description  = GetDescription(clsid);
			m_wrapperClsid = GetWrapper(clsid);
			m_serverClsid  = GetDotNetOpcServer(clsid);
			m_parameters   = GetParameters(clsid);

			if (m_description == m_progId)
			{
				m_description = null;
			}
		}

		/// <summary>
		/// Sets private members to default values.
		/// </summary>
		private void Initialize()
		{
			m_clsid        = Guid.Empty;
			m_progId       = null;
			m_description  = null;
			m_wrapperClsid = Guid.Empty;
			m_serverClsid  = Guid.Empty;
			m_parameters   = new Dictionary<string,string>();
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// The CLSID for the .NET OPC server.
		/// </summary>
		public Guid Clsid
		{
			get { return m_clsid;  }
			set { m_clsid = value; }
		}

		/// <summary>
		/// The prog id for the .NET OPC server.
		/// </summary>
		public string ProgId
		{
			get { return m_progId;  }
			set { m_progId = value; }
		}

		/// <summary>
		/// The description for the .NET OPC server.
		/// </summary>
		public string Description
		{
			get { return m_description;  }			
			set { m_description = value; }
		}

		/// <summary>
		/// The CLSID for the .NET OPC server wrapper.
		/// </summary>
		public Guid WrapperClsid
		{
			get { return m_wrapperClsid;  }
			set { m_wrapperClsid = value; }
		}

		/// <summary>
		/// The CLSID for .NET OPC server implementation.
		/// </summary>
		public Guid ServerClsid
		{
			get { return m_serverClsid;  }
			set { m_serverClsid = value; }
		}

		/// <summary>
		/// The configuration parameters to pass to the server when it is instantiated..
		/// </summary>
		public IDictionary<string,string> Parameters
		{
			get { return m_parameters; }
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Imports a list of servers from a file.
		/// </summary>
		public static void Import(string filepath, bool register)
		{
            Export.ListOfRegisteredServers list = new Opc.ConfigTool.Export.ListOfRegisteredServers();

			// read from file.
			XmlTextReader reader = new XmlTextReader(filepath);
			XmlSerializer serializer = new XmlSerializer(typeof(Export.ListOfRegisteredServers), ConfigUtils.ConfigToolSchemaUri);
			Export.ListOfRegisteredServers servers = (Export.ListOfRegisteredServers)serializer.Deserialize(reader);
			reader.Close();

			if (ConfigUtils.IsEmpty(servers.Server))
			{
				return;
			}

			// registers the servers.
			for (int ii = 0; ii < servers.Server.Length; ii++)
			{
				RegisteredDotNetOpcServer server = Import(servers.Server[ii]);

                if (register)
                {
				    server.Register();
                }
                else
                {
				    server.Unregister();
                }
			}
		}

		/// <summary>
		/// Exports a list of servers.
		/// </summary>
		public static void Export(string filepath)
		{
			// enumerate servers.
			List<RegisteredDotNetOpcServer> servers = EnumRegisteredServers(false);

			// populate export structures.
			Export.ListOfRegisteredServers serverList = new Export.ListOfRegisteredServers();
			serverList.Server = new Export.RegisteredServer[servers.Count];

			for (int ii = 0; ii < servers.Count; ii++)
			{
				serverList.Server[ii] = Export(servers[ii]);
			}

			// write to file.
			XmlTextWriter writer = new XmlTextWriter(filepath, System.Text.Encoding.UTF8);
			writer.Formatting = Formatting.Indented;

			XmlSerializer serializer = new XmlSerializer(typeof(Export.ListOfRegisteredServers), ConfigUtils.ConfigToolSchemaUri);
			serializer.Serialize(writer, serverList);

			writer.Close();
		}

		/// <summary>
		/// Imports a server from a export file.
		/// </summary>
		private static RegisteredDotNetOpcServer Import(Export.RegisteredServer serverToImport)
		{
			RegisteredDotNetOpcServer server = new RegisteredDotNetOpcServer();

			// assign clsid if none specified.
			if (String.IsNullOrEmpty(serverToImport.Clsid))
			{
				server.Clsid = ConfigUtils.CLSIDFromProgID(serverToImport.ProgId);

				if (server.Clsid == Guid.Empty)
				{
					server.Clsid = Guid.NewGuid();
				}
			}
			else
			{
				server.Clsid = new Guid(serverToImport.Clsid);
			}

			// get prog id and description.
			server.ProgId      = serverToImport.ProgId;
			server.Description = serverToImport.Description;

			// parse wrapper clsid/prog id.
			try
			{
				server.WrapperClsid = new Guid(serverToImport.WrapperClsid);
			}
			catch
			{
				server.WrapperClsid = ConfigUtils.CLSIDFromProgID(serverToImport.WrapperClsid);
			}

			// parse wrapped server clsid/prog id.
			try
			{
				server.ServerClsid = new Guid(serverToImport.ServerClsid);
			}
			catch
			{
				server.ServerClsid = ConfigUtils.CLSIDFromProgID(serverToImport.ServerClsid);
			}

			// read parameters.
            server.Parameters.Clear();

			if (!ConfigUtils.IsEmpty(serverToImport.Parameter))
			{
				for (int ii = 0; ii < serverToImport.Parameter.Length; ii++)
				{
					Export.Parameter parameter = serverToImport.Parameter[ii];

					if (parameter != null && !String.IsNullOrEmpty(parameter.Name))
					{
						server.Parameters.Add(parameter.Name, parameter.Value);
					}
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
			Export.RegisteredServer serverToExport = new Export.RegisteredServer();

			serverToExport.Clsid  = server.Clsid.ToString();
			serverToExport.ProgId = server.ProgId;

			if (server.Description != server.ProgId)
			{
				serverToExport.Description = server.Description;
			}

			serverToExport.WrapperClsid = ConfigUtils.ProgIDFromCLSID(server.WrapperClsid);
			serverToExport.ServerClsid  = ConfigUtils.ProgIDFromCLSID(server.ServerClsid);

			// export parameters.
			serverToExport.Parameter = new Export.Parameter[server.Parameters.Count];

			int index = 0;

			foreach (KeyValuePair<string,string> entry in server.Parameters)
			{
				Export.Parameter parameter = new Export.Parameter();

				parameter.Name  = entry.Key;
				parameter.Value = entry.Value;
                
				serverToExport.Parameter[index++] = parameter;
			}

			// return exported server.
			return serverToExport;
		}        

		/// <summary>
		/// Returns the UA COM servers registered on the local computer.
		/// </summary>
		public static List<RegisteredDotNetOpcServer> EnumRegisteredServers(bool updateWrapperPath)
		{
			// enumerate server clsids.
			List<Guid> clsids = ConfigUtils.EnumClassesInCategory(ConfigUtils.CATID_RegisteredDotNetOpcServers);

			// initialize server objects.
			List<RegisteredDotNetOpcServer> servers = new List<RegisteredDotNetOpcServer>();

			for (int ii = 0; ii < clsids.Count; ii++)
			{
				servers.Add(new RegisteredDotNetOpcServer(clsids[ii]));

				if (updateWrapperPath)
				{
					string wrapperPath = ConfigUtils.GetExecutablePath(servers[ii].WrapperClsid);

					if (String.IsNullOrEmpty(wrapperPath))
					{
						continue;
					}
		
					RegistryKey key = Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\LocalServer32", clsids[ii]), true);
			
					if (key != null)
					{
						key.SetValue(null, wrapperPath);
						key.Close();
					}
				}
			}

			return servers;
		}

		/// <summary>
		/// Registers the endpoint as a COM server with the specified CLSID.
		/// </summary>
		public void Register()
		{
            DotNetOpcServer server = new DotNetOpcServer(m_serverClsid);

            if (server.Specifications == Specifications.None)
            {
				throw new ApplicationException("The .NET server does not implement any OPC specifications.");
            }

            DotNetOpcServerWrapper wrapper = new DotNetOpcServerWrapper(m_wrapperClsid);

            if (wrapper.Specifications == Specifications.None)
            {
				throw new ApplicationException("The .NET server wrapper does not implement any OPC interfaces.");
            }

            // determine the intersection of between the specs supported by the wrapper and the specs supported by the server.
            Specifications specifications = wrapper.Specifications & server.Specifications;
            
            if (specifications == Specifications.None)
            {
				throw new ApplicationException("The .NET server wrapper does not implement any OPC interfaces supported by the .NET server.");
            }

			// verify url and prog id.
			string progId = ProgId;

			if (m_wrapperClsid == Guid.Empty || String.IsNullOrEmpty(progId))
			{
				throw new ApplicationException("Proxy does not have a valid wrapper clsid or prog id.");
			}

			// verify wrapper path.
			string wrapperPath = ConfigUtils.GetExecutablePath(m_wrapperClsid);
		
			if (wrapperPath == null)
			{
				throw new ApplicationException("OPC server wrapper is not registered on this machine.");
			}

            // remove existing CLSID.
            Guid existingClsid = ConfigUtils.CLSIDFromProgID(progId);

            if (existingClsid != m_clsid)
            {
                ConfigUtils.UnregisterComServer(existingClsid);
            }

			string clsidKey = String.Format(@"CLSID\{{{0}}}", m_clsid.ToString().ToUpper());

			// create new entries.					
			RegistryKey key = Registry.ClassesRoot.CreateSubKey(clsidKey);
			
			if (key == null)
			{
				throw new ApplicationException("Could not create key: " + clsidKey);
			}

			// save description.
			if (String.IsNullOrEmpty(m_description))
			{
				m_description = progId;
			}

			key.SetValue(null, m_description);

			try
			{
				// create local server key.
				RegistryKey subkey = key.CreateSubKey("LocalServer32");

				if (subkey == null)
				{
					throw new ApplicationException("Could not create key: LocalServer32");
				}

				subkey.SetValue(null, wrapperPath);
				subkey.SetValue("WrapperClsid", String.Format("{{{0}}}", m_wrapperClsid));
				subkey.Close();

				// create prog id key.
				subkey = key.CreateSubKey("ProgId");

				if (subkey == null)
				{
					throw new ApplicationException("Could not create key: ProgId");
				}

				subkey.SetValue(null, progId);
				subkey.Close();

				// create endpoint key.
				subkey = key.CreateSubKey(WrappedServerSubKey);

				if (subkey == null)
				{
					throw new ApplicationException("Could not create key: " + WrappedServerSubKey);
				}
	
				// add parameters.
				try
				{					
					subkey.SetValue(null, String.Format("{{{0}}}", m_serverClsid));	
					
					// remove unused parameters.
					foreach (string name in subkey.GetValueNames())
					{
						if (!String.IsNullOrEmpty(name) && !m_parameters.ContainsKey(name))
						{
							subkey.DeleteValue(name, false);
						}
					}

					// add new parameters.
					foreach (KeyValuePair<string,string> entry in m_parameters)
					{
                        if (!String.IsNullOrEmpty(entry.Key))
						{
							subkey.SetValue(entry.Key, entry.Value);
						}
					}
				}
				finally
				{
					subkey.Close();
				}
			}
			finally
			{
				key.Close();
			} 
						
			// create prog id key.
			key = Registry.ClassesRoot.CreateSubKey(progId);
			
			if (key == null)
			{
				throw new ApplicationException("Could not create key: " + progId);
			}

			try
			{	
				// create clsid key.
				RegistryKey subkey = key.CreateSubKey("CLSID");

				if (subkey == null)
				{
					throw new ApplicationException("Could not create key: CLSID");
				}

				subkey.SetValue(null, String.Format("{{{0}}}", m_clsid.ToString().ToUpper()));
				subkey.Close();

				// create the OPC key use with DA 2.0 servers.
                if ((specifications & Specifications.DA2) != 0)
                {
				    subkey = key.CreateSubKey("OPC");

				    if (subkey == null)
				    {
					    throw new ApplicationException("Could not create key: OPC");
				    }

				    subkey.Close();
                }
			}
			finally
			{
				key.Close();
			} 

			// register as wrapper server.
			ConfigUtils.RegisterClassInCategory(m_clsid, ConfigUtils.CATID_RegisteredDotNetOpcServers, "OPC Wrapped COM Server Proxy");

            // register in OPC component categories.
            if ((specifications & Specifications.DA2) != 0)
            {
			    ConfigUtils.RegisterClassInCategory(m_clsid, typeof(OpcRcw.Da.CATID_OPCDAServer20).GUID);
            }
            
            if ((specifications & Specifications.DA3) != 0)
            {
			    ConfigUtils.RegisterClassInCategory(m_clsid, typeof(OpcRcw.Da.CATID_OPCDAServer30).GUID);
            }
            
            if ((specifications & Specifications.AE) != 0)
            {
			    ConfigUtils.RegisterClassInCategory(m_clsid, typeof(OpcRcw.Ae.CATID_OPCAEServer10).GUID);
            }
            
            if ((specifications & Specifications.HDA) != 0)
            {
			    ConfigUtils.RegisterClassInCategory(m_clsid, typeof(OpcRcw.Hda.CATID_OPCHDAServer10).GUID);
            }
		}

		/// <summary>
		/// Unregisters a COM server from the registry.
		/// </summary>
		public void Unregister()
		{
            ConfigUtils.UnregisterComServer(m_clsid);
		}
		#endregion

		#region Private Members
		/// <summary>
		/// Returns the description 
		/// </summary>
		private static string GetDescription(Guid server)
		{
			string clsidKey = String.Format(@"CLSID\{{{0}}}", server.ToString().ToUpper());
		
			RegistryKey key = Registry.ClassesRoot.OpenSubKey(clsidKey);
			
			if (key != null)
			{
				try
				{
					return key.GetValue(null) as string;
				}
				finally
				{
					key.Close();
				}
			}

			return String.Empty;
		}
		
		/// <summary>
		/// Returns the wrapper 
		/// </summary>
		private static Guid GetWrapper(Guid server)
		{

			RegistryKey key = Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\LocalServer32", server));

			if (key != null)
			{
				try
				{
					string clsid = key.GetValue("WrapperClsid") as string;

					if (clsid != null)
					{
						return new Guid(clsid.Substring(1, clsid.Length-2));
					}
				}
				finally
				{
					key.Close();
				}
			}

			return Guid.Empty;
		}

		/// <summary>
		/// Returns the url for the specified COM server.
		/// </summary>
		private static Guid GetDotNetOpcServer(Guid server)
		{
			RegistryKey key = Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\{1}", server, WrappedServerSubKey));

			if (key != null)
			{
				try
				{
					string clsid = key.GetValue(null) as string;

					if (clsid != null)
					{
						return new Guid(clsid.Substring(1, clsid.Length-2));
					}
				}
				finally
				{
					key.Close();
				}
			}

			return Guid.Empty;
		}

		/// <summary>
		/// Returns the configuration parameters for the specified COM server.
		/// </summary>
		private static Dictionary<string,string> GetParameters(Guid clsid)
		{
			Dictionary<string,string> parameters = new Dictionary<string,string>();

			RegistryKey key = Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\{1}", clsid, WrappedServerSubKey));

			if (key != null)
			{
				try
				{
					foreach (string name in key.GetValueNames())
					{
						if (!String.IsNullOrEmpty(name))
						{
							parameters.Add(name, String.Format("{0}", key.GetValue(name)));
						}
					}
				}
				finally
				{
					key.Close();
				}
			}

			return parameters;
		}
		#endregion

		#region Private Constants
		public const string WrappedServerSubKey = "WrappedServer";
		#endregion

		#region Private Members
		private Guid m_clsid;
		private string m_progId;
		private string m_description;
		private Guid m_wrapperClsid;
		private Guid m_serverClsid;
		private Dictionary<string,string> m_parameters;
		#endregion
	}
}
