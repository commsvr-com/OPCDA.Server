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
			m_clsid          = clsid;
			m_progId         = ConfigUtils.ProgIDFromCLSID(clsid);
			m_codebase       = ConfigUtils.GetExecutablePath(clsid);
            m_specifications = GetSpecifications(clsid);
		}

		/// <summary>
		/// Sets private members to default values.
		/// </summary>
		private void Initialize()
		{
			m_clsid          = Guid.Empty;
			m_progId         = null;
			m_codebase       = null;
            m_specifications = Specifications.None;
		}
		#endregion

		#region Public Properties
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

		#region Public Members
		/// <summary>
		/// Returns the wrappers registered on the local machine.
		/// </summary>
		public static List<DotNetOpcServerWrapper> EnumWrappers()
		{
			// enumerate clsids.
			List<Guid> clsids = ConfigUtils.EnumClassesInCategory(ConfigUtils.CATID_DotNetOpcServerWrappers);

			// initialize objects.
			List<DotNetOpcServerWrapper> servers = new List<DotNetOpcServerWrapper>();

			for (int ii = 0; ii < clsids.Count; ii++)
			{
				servers.Add(new DotNetOpcServerWrapper(clsids[ii]));
			}

			return servers;
		}

        /// <summary>
        /// Finds the OPC specifications supported by the .NET server.
        /// </summary>
        private Specifications GetSpecifications(Guid clsid)
        {
            if (clsid == Guid.Empty)
            {
                return Specifications.None;
            }
            
            Specifications specifications = Specifications.None;

            foreach (Guid catid in ConfigUtils.GetImplementedCategories(clsid))
            {
                if (catid == typeof(OpcRcw.Da.CATID_OPCDAServer20).GUID)
                {
                    specifications |= Specifications.DA2;
                }

                if (catid == typeof(OpcRcw.Da.CATID_OPCDAServer30).GUID)
                {
                    specifications |= Specifications.DA3;
                }

                if (catid == typeof(OpcRcw.Ae.CATID_OPCAEServer10).GUID)
                {
                    specifications |= Specifications.AE;
                }
                
                if (catid == typeof(OpcRcw.Hda.CATID_OPCHDAServer10).GUID)
                {
                    specifications |= Specifications.HDA;
                }
            }

            return specifications;
        }
		#endregion

		#region Private Members
		#endregion

		#region Private Members
		private Guid m_clsid;
		private string m_progId;
		private string m_codebase;
        private Specifications m_specifications;
		#endregion
	}
}
