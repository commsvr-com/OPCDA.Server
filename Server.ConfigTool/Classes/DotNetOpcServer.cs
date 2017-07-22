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
using System.Text;
using System.Reflection;

using Microsoft.Win32;

namespace Opc.ConfigTool
{
	/// <summary>
	/// A class that describes a wrapped object.
	/// </summary>
	public class DotNetOpcServer
	{
		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		public DotNetOpcServer()
		{
			Initialize();
		}

		/// <summary>
		/// Initializes the object with a url.
		/// </summary>
		public DotNetOpcServer(Guid clsid)
		{
			m_clsid          = clsid;
			m_progId         = ConfigUtils.ProgIDFromCLSID(clsid);
			m_codebase       = ConfigUtils.GetExecutablePath(clsid);
            m_systemType     = GetSystemType(clsid, m_codebase);
            m_specifications = GetSpecifications(m_systemType);
		}

		/// <summary>
		/// Sets private members to default values.
		/// </summary>
		private void Initialize()
		{
			m_clsid          = Guid.Empty;
			m_progId         = null;
			m_codebase       = null;
            m_systemType     = null;
            m_specifications = Specifications.None;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// The CLSID for the wrapped object.
		/// </summary>
		public Guid Clsid
		{
			get { return m_clsid; }
		}

		/// <summary>
		/// The ProgId for the wrapped object.
		/// </summary>
		public string ProgId
		{
			get { return m_progId; }
		}

		/// <summary>
		/// The file path for the DLL containing the wrapped object.
		/// </summary>
		public string Codebase
		{
			get { return m_codebase; }
		}

		/// <summary>
		/// The system type of the server object.
		/// </summary>
		public Type SystemType
		{
			get { return m_systemType; }
		}

		/// <summary>
		/// The specifications supported by the .NET server.
		/// </summary>
		public Specifications Specifications
		{
			get { return m_specifications; }
		}
		#endregion

		#region Public Members
		/// <summary>
		/// Returns the wrapped objects registered on the local machine.
		/// </summary>
		public static List<DotNetOpcServer> EnumServers()
		{
			// enumerate clsids.
			List<Guid> clsids = ConfigUtils.EnumClassesInCategory(ConfigUtils.CATID_DotNetOpcServers);

			// initialize objects.
			List<DotNetOpcServer> servers = new List<DotNetOpcServer>();

			for (int ii = 0; ii < clsids.Count; ii++)
			{
				servers.Add(new DotNetOpcServer(clsids[ii]));
			}

			return servers;
		}

		/// <summary>
		/// Registers the types in the specified assembly.
		/// </summary>
		public static void RegisterAssembly(string filePath)
		{
			List<System.Type> types = ConfigUtils.RegisterComTypes(filePath);

			foreach (System.Type type in types)
			{	
				try
				{
					// verify that the object implements the wrapper object interface.
					System.Type[] interfaces = type.GetInterfaces();

					// must manually compare guids because unrelated assemblies that define the same
					// COM interface have different .NET interface types even if the underlying COM 
					// interface is the same.
					for (int ii = 0; ii < interfaces.Length; ii++)
					{					
						if (interfaces[ii].GUID == typeof(IOPCWrappedServer).GUID)
						{
							ConfigUtils.RegisterClassInCategory(type.GUID, ConfigUtils.CATID_DotNetOpcServers, "OPC Wrapped Server Objects");
						}
					}
				}
				catch 
				{
					// ignore types that don't have a default constructor.
					continue;
				}
			}
		}

		/// <summary>
		/// Registers the types in the specified assembly.
		/// </summary>
		public static void UnregisterAssembly(string filePath)
		{
			List<System.Type> types = ConfigUtils.UnregisterComTypes(filePath);

			foreach (System.Type type in types)
			{	
				try
				{
					ConfigUtils.UnregisterClassInCategory(type.GUID, ConfigUtils.CATID_DotNetOpcServers);
				}
				catch 
				{
					continue;
				}
			}
		}

        /// <summary>
        /// Finds the system type for the .NET implementation of an OPC server.
        /// </summary>
        private Type GetSystemType(Guid clsid, string codebase)
        {
            if (clsid == Guid.Empty || String.IsNullOrEmpty(codebase))
            {
                return null;
            }

            try
            {
                Assembly assembly = Assembly.LoadFrom(codebase);

                foreach (Type type in assembly.GetExportedTypes())
                {
                    if (type.GUID == clsid)
                    {
                        return type;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Finds the OPC specifications supported by the .NET server.
        /// </summary>
        private Specifications GetSpecifications(Type systemType)
        {
            if (systemType == null)
            {
                return Specifications.None;
            }

            Specifications specifications = Specifications.None;

            foreach (Type interfaces in systemType.GetInterfaces())
            {
                if (interfaces.GUID == typeof(OpcRcw.Da.IOPCItemProperties).GUID)
                {
                    specifications |= Specifications.DA2;
                }

                if (interfaces.GUID == typeof(OpcRcw.Da.IOPCBrowse).GUID)
                {
                    specifications |= Specifications.DA3;
                }

                if (interfaces.GUID == typeof(OpcRcw.Ae.IOPCEventServer).GUID)
                {
                    specifications |= Specifications.AE;
                }

                if (interfaces.GUID == typeof(OpcRcw.Hda.IOPCHDA_Server).GUID)
                {
                    specifications |= Specifications.HDA;
                }
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
            {
                return m_progId;
            }

            if (m_clsid == Guid.Empty)
            {
                return "(unknown)";
            }

            return m_clsid.ToString();
        }
		#endregion

		#region Private Members
		#endregion

		#region Private Members
		private Guid m_clsid;
		private string m_progId;
		private string m_codebase;
        private Type m_systemType;
        private Specifications m_specifications;
		#endregion
	}
    
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
        /// Supports Historial Data Access 1.2
        /// </summary>
        HDA = 0x08
    }
}
