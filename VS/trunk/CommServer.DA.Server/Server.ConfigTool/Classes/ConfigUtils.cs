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
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

using Microsoft.Win32;

namespace Opc.ConfigTool
{
	/// <summary>
	/// Exposes WIN32 and COM API functions.
	/// </summary>
	public class ConfigUtils
	{
		#region NetApi Function Declarations
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			private struct SERVER_INFO_100
		{
			public uint   sv100_platform_id;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string sv100_name;
		} 	

		private const uint LEVEL_SERVER_INFO_100 = 100;
		private const uint LEVEL_SERVER_INFO_101 = 101;

		private const int  MAX_PREFERRED_LENGTH  = -1;

		private const uint SV_TYPE_WORKSTATION   = 0x00000001;
		private const uint SV_TYPE_SERVER        = 0x00000002;

		[DllImport("Netapi32.dll")]
		private static extern int NetServerEnum(
			IntPtr     servername,
			uint       level,
			out IntPtr bufptr,
			int        prefmaxlen,
			out int    entriesread,
			out int    totalentries,
			uint       servertype,
			IntPtr     domain,
			IntPtr     resume_handle);

		[DllImport("Netapi32.dll")]	
		private static extern int NetApiBufferFree(IntPtr buffer);

		/// <summary>
		/// Enumerates computers on the local network.
		/// </summary>
		public static string[] EnumComputers()
		{
			IntPtr pInfo;

			int entriesRead = 0;
			int totalEntries = 0;

			int result = NetServerEnum(
				IntPtr.Zero,
				LEVEL_SERVER_INFO_100,
				out pInfo,
				MAX_PREFERRED_LENGTH,
				out entriesRead,
				out totalEntries,
				SV_TYPE_WORKSTATION | SV_TYPE_SERVER,
				IntPtr.Zero,
				IntPtr.Zero);		

			if (result != 0)
			{
				throw new ApplicationException("NetApi Error = " + String.Format("0x{0:X8}", result));
			}

			string[] computers = new string[entriesRead];

			IntPtr pos = pInfo;

			for (int ii = 0; ii < entriesRead; ii++)
			{
				SERVER_INFO_100 info = (SERVER_INFO_100)Marshal.PtrToStructure(pos, typeof(SERVER_INFO_100));
				
				computers[ii] = info.sv100_name;

				pos = (IntPtr)(pos.ToInt32() + Marshal.SizeOf(typeof(SERVER_INFO_100)));
			}

			NetApiBufferFree(pInfo);

			return computers;
		}
		#endregion

		#region OLE32 Function/Interface Declarations
		private const int MAX_MESSAGE_LENGTH = 1024;

		private const uint FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200;
		private const uint FORMAT_MESSAGE_FROM_SYSTEM    = 0x00001000;

		[DllImport("Kernel32.dll")]
		private static extern int FormatMessageW(
			int    dwFlags,
			IntPtr lpSource,
			int    dwMessageId,
			int    dwLanguageId,
			IntPtr lpBuffer,
			int    nSize,
			IntPtr Arguments);
        
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		private struct COSERVERINFO
		{
			public uint         dwReserved1;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string       pwszName;
			public IntPtr       pAuthInfo;
			public uint         dwReserved2;
		};

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		private struct COAUTHINFO
		{
			public uint   dwAuthnSvc;
			public uint   dwAuthzSvc;
			public IntPtr pwszServerPrincName;
			public uint   dwAuthnLevel;
			public uint   dwImpersonationLevel;
			public IntPtr pAuthIdentityData;
			public uint   dwCapabilities;
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		private struct COAUTHIDENTITY
		{
			public IntPtr User;
			public uint   UserLength;
			public IntPtr Domain;
			public uint   DomainLength;
			public IntPtr Password;
			public uint   PasswordLength;
			public uint   Flags;
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		private struct MULTI_QI
		{
			public IntPtr iid;
			[MarshalAs(UnmanagedType.IUnknown)]
			public object pItf;
			public uint   hr;
		}
		
		private const uint CLSCTX_INPROC_SERVER	= 0x1;
		private const uint CLSCTX_INPROC_HANDLER	= 0x2;
		private const uint CLSCTX_LOCAL_SERVER	= 0x4;
		private const uint CLSCTX_REMOTE_SERVER	= 0x10;

		private static readonly Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");
		
		private const uint SEC_WINNT_AUTH_IDENTITY_ANSI    = 0x1;
		private const uint SEC_WINNT_AUTH_IDENTITY_UNICODE = 0x2;

		[DllImport("ole32.dll")]
		private static extern void CoCreateInstanceEx(
			ref Guid         clsid,
			[MarshalAs(UnmanagedType.IUnknown)]
			object           punkOuter,
			uint             dwClsCtx,
			[In]
			ref COSERVERINFO pServerInfo,
			uint             dwCount,
			[In, Out]
			MULTI_QI[]       pResults);

		/// <summary>
		/// Creates an instance of a COM server.
		/// </summary>
		public static object CreateInstance(Guid clsid)
		{
			COSERVERINFO coserverInfo = new COSERVERINFO();

			coserverInfo.pwszName     = null;
			coserverInfo.pAuthInfo    = IntPtr.Zero;
			coserverInfo.dwReserved1  = 0;
			coserverInfo.dwReserved2  = 0;

			GCHandle hIID = GCHandle.Alloc(IID_IUnknown, GCHandleType.Pinned);

			MULTI_QI[] results = new MULTI_QI[1];

			results[0].iid  = hIID.AddrOfPinnedObject();
			results[0].pItf = null;
			results[0].hr   = 0;

			try
			{
				// create an instance.
				CoCreateInstanceEx(
					ref clsid,
					null,
					CLSCTX_INPROC_SERVER | CLSCTX_LOCAL_SERVER,
					ref coserverInfo,
					1,
					results);
			}
			finally
			{
				hIID.Free();
			}

			if (results[0].hr != 0)
			{
				throw new ExternalException("CoCreateInstanceEx: " + GetSystemMessage((int)results[0].hr));
			}

			return results[0].pItf;
		}

		[ComImport]
		[GuidAttribute("0002E013-0000-0000-C000-000000000046")]
		[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)] 
		private interface ICatInformation
		{
			void EnumCategories( 
				int lcid,				
				[MarshalAs(UnmanagedType.Interface)]
				[Out] out object ppenumCategoryInfo);
        
			void GetCategoryDesc( 
				[MarshalAs(UnmanagedType.LPStruct)] 
				Guid rcatid,
				int lcid,
				[MarshalAs(UnmanagedType.LPWStr)]
				[Out] out string pszDesc);
        
			void EnumClassesOfCategories( 
				int cImplemented,
				[MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.LPStruct, SizeParamIndex=0)] 
				Guid[] rgcatidImpl,
				int cRequired,
				[MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.LPStruct, SizeParamIndex=2)] 
				Guid[] rgcatidReq,
				[MarshalAs(UnmanagedType.Interface)]
				[Out] out object ppenumClsid);
        
			void IsClassOfCategories( 
				[MarshalAs(UnmanagedType.LPStruct)] 
				Guid rclsid,
				int cImplemented,
				[MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.LPStruct, SizeParamIndex=1)] 
				Guid[] rgcatidImpl,
				int cRequired,
				[MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.LPStruct, SizeParamIndex=3)] 
				Guid[] rgcatidReq);
        
			void EnumImplCategoriesOfClass( 
				[MarshalAs(UnmanagedType.LPStruct)] 
				Guid rclsid,
				[MarshalAs(UnmanagedType.Interface)]
				[Out] out object ppenumCatid);
        
			void EnumReqCategoriesOfClass(
				[MarshalAs(UnmanagedType.LPStruct)] 
				Guid rclsid,
				[MarshalAs(UnmanagedType.Interface)]
				[Out] out object ppenumCatid);
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
		struct CATEGORYINFO 
		{
			public Guid catid;
			public int lcid;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=127)] 
			public string szDescription;
		}

		[ComImport]
		[GuidAttribute("0002E012-0000-0000-C000-000000000046")]
		[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)] 
		private interface ICatRegister
		{
			void RegisterCategories(
				int cCategories,
				[MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.LPStruct, SizeParamIndex=0)] 
				CATEGORYINFO[] rgCategoryInfo);

			void UnRegisterCategories(
				int cCategories,
				[MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.LPStruct, SizeParamIndex=0)] 
				Guid[] rgcatid);

			void RegisterClassImplCategories(
				[MarshalAs(UnmanagedType.LPStruct)] 
				Guid rclsid,
				int cCategories,
				[MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.LPStruct, SizeParamIndex=1)] 
				Guid[] rgcatid);

			void UnRegisterClassImplCategories(
				[MarshalAs(UnmanagedType.LPStruct)] 
				Guid rclsid,
				int cCategories,
				[MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.LPStruct, SizeParamIndex=1)] 
				Guid[] rgcatid);

			void RegisterClassReqCategories(
				[MarshalAs(UnmanagedType.LPStruct)] 
				Guid rclsid,
				int cCategories,
				[MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.LPStruct, SizeParamIndex=1)] 
				Guid[] rgcatid);

			void UnRegisterClassReqCategories(
				[MarshalAs(UnmanagedType.LPStruct)] 
				Guid rclsid,
				int cCategories,
				[MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.LPStruct, SizeParamIndex=1)] 
				Guid[] rgcatid);
		}

        public const string ConfigToolSchemaUri = "http://opcfoundation.org/ConfigTool";
        
		public static readonly Guid CATID_DotNetOpcServers = new Guid("E633C3F5-7692-476c-9A35-8BEE25E5BA9D");  //it is not used
		public static readonly Guid CATID_DotNetOpcServerWrappers = new Guid("132B3E2B-0E92-4816-972B-E42AA9532529");
		public static readonly Guid CATID_RegisteredDotNetOpcServers = new Guid("62C8FE65-4EBB-45e7-B440-6E39B2CDBF29"); //It is added by regasm and registered at HKEY_CLASSES_ROOT\Component Categories\

    public static readonly Guid CLSID_StdComponentCategoriesMgr = new Guid("0002E005-0000-0000-C000-000000000046");
		#endregion

		#region ServerInfo Class
		/*
		/// <summary>
		/// A class used to allocate and deallocate the elements of a COSERVERINFO structure.
		/// </summary>
		class ServerInfo
		{
			#region Public Interface
			/// <summary>
			/// Allocates a COSERVERINFO structure.
			/// </summary>
			public COSERVERINFO Allocate(string hostName, NetworkCredential credential)
			{
				// initialize server info structure.
				COSERVERINFO serverInfo = new COSERVERINFO();

				serverInfo.pwszName     = hostName;
				serverInfo.pAuthInfo    = IntPtr.Zero;
				serverInfo.dwReserved1  = 0;
				serverInfo.dwReserved2  = 0;

				string userName = null;
				string password = null;
				string domain   = null;

				if (credential != null)
				{
					userName = credential.UserName;
					password = credential.Password;
					domain   = credential.Domain; 
				}

				// do not specify any authentication information if no user provided.
				if (ConfigUtils.IsEmpty(userName))
				{
					return serverInfo;
				}

				m_hPrincipal = GCHandle.Alloc(@"NT AUTHORITY\SYSTEM", GCHandleType.Pinned);
				m_hUserName  = GCHandle.Alloc(userName, GCHandleType.Pinned);
				m_hPassword  = GCHandle.Alloc(password, GCHandleType.Pinned);
				m_hDomain    = GCHandle.Alloc(domain,   GCHandleType.Pinned);

				m_hIdentity = new GCHandle();

				// create identity structure.
				COAUTHIDENTITY identity = new COAUTHIDENTITY();

				identity.User           = m_hUserName.AddrOfPinnedObject();
				identity.UserLength     = (uint)((userName != null)?userName.Length:0);
				identity.Password       = m_hPassword.AddrOfPinnedObject();
				identity.PasswordLength = (uint)((password != null)?password.Length:0);
				identity.Domain         = m_hDomain.AddrOfPinnedObject();
				identity.DomainLength   = (uint)((domain != null)?domain.Length:0);
				identity.Flags          = SEC_WINNT_AUTH_IDENTITY_UNICODE;

				m_hIdentity = GCHandle.Alloc(identity, GCHandleType.Pinned);
					
				// create authorization info structure.
				COAUTHINFO authInfo = new COAUTHINFO();

				authInfo.dwAuthnSvc           = RPC_C_AUTHN_WINNT;
				authInfo.dwAuthzSvc           = RPC_C_AUTHZ_NONE;
				authInfo.pwszServerPrincName  = m_hPrincipal.AddrOfPinnedObject();
				authInfo.dwAuthnLevel         = RPC_C_AUTHN_LEVEL_CONNECT;
				authInfo.dwImpersonationLevel = RPC_C_IMP_LEVEL_IMPERSONATE;
				authInfo.pAuthIdentityData    = m_hIdentity.AddrOfPinnedObject();
				authInfo.dwCapabilities       = EOAC_NONE;

				m_hAuthInfo = GCHandle.Alloc(authInfo, GCHandleType.Pinned);
			
				// update server info structure.
				serverInfo.pAuthInfo = m_hAuthInfo.AddrOfPinnedObject();

				return serverInfo;
			}

			/// <summary>
			/// Deallocated memory allocated when the COSERVERINFO structure was created.
			/// </summary>
			public void Deallocate()
			{
				if (m_hPrincipal.IsAllocated) m_hPrincipal.Free();
				if (m_hUserName.IsAllocated) m_hUserName.Free();
				if (m_hPassword.IsAllocated) m_hPassword.Free();
				if (m_hDomain.IsAllocated)   m_hDomain.Free();
				if (m_hIdentity.IsAllocated) m_hIdentity.Free();
				if (m_hAuthInfo.IsAllocated) m_hAuthInfo.Free();
			}
			#endregion

			#region Private Members
			private GCHandle m_hPrincipal;
			private GCHandle m_hUserName;
			private GCHandle m_hPassword;
			private GCHandle m_hDomain;
			private GCHandle m_hIdentity;
			private GCHandle m_hAuthInfo;
			#endregion
		}
		*/
		#endregion
        
        #region COM Functions
        /// <summary>
		/// Releases the server if it is a true COM server.
		/// </summary>
		public static void ReleaseServer(object server)
		{
			if (server != null && server.GetType().IsCOMObject)
			{
				Marshal.ReleaseComObject(server);
			}
		}
		
		/// <summary>
		/// Retrieves the system message text for the specified error.
		/// </summary>
		public static string GetSystemMessage(int error)
		{
			IntPtr buffer = Marshal.AllocCoTaskMem(MAX_MESSAGE_LENGTH);

			int result = FormatMessageW(
				(int)(FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_FROM_SYSTEM),
				IntPtr.Zero,
				error,
				0,
				buffer,
				MAX_MESSAGE_LENGTH-1,
				IntPtr.Zero);

			string msg = Marshal.PtrToStringUni(buffer);
			Marshal.FreeCoTaskMem(buffer);

			if (msg != null && msg.Length > 0)
			{
				return msg.Trim();
			}

			return String.Format("0x{0,0:X}", error);
		}

		/// <summary>
		/// Registers the COM types in the specified assembly.
		/// </summary>
		public static List<System.Type> RegisterComTypes(string filePath)
		{
			// load assmebly.
			Assembly assembly = Assembly.LoadFrom(filePath);

			// check that the correct assembly is being registered.
			VerifyCodebase(assembly, filePath);

			RegistrationServices services = new RegistrationServices();

			// list types to register/unregister.
			List<System.Type> types = new List<System.Type>(services.GetRegistrableTypesInAssembly(assembly));

			// register types.
			if (types.Count > 0)
			{
				// unregister types first.	
				if (!services.UnregisterAssembly(assembly))
				{
					throw new ApplicationException("Unregister COM Types Failed.");
				}

				// register types.	
				if (!services.RegisterAssembly(assembly, AssemblyRegistrationFlags.SetCodeBase))
				{
					throw new ApplicationException("Register COM Types Failed.");
				}
			}

			return types;
		}

		/// <summary>
		/// Checks that the assembly loaded has the expected codebase.
		/// </summary>
		private static void VerifyCodebase(Assembly assembly, string filepath)
		{
			string codebase = assembly.CodeBase.ToLower();
			string normalizedPath = filepath.Replace('\\', '/').Replace("//", "/").ToLower();

			if (!normalizedPath.StartsWith("file:///"))
			{
				normalizedPath = "file:///" + normalizedPath;
			}

			if (codebase != normalizedPath)
			{
				throw new ApplicationException(String.Format("Duplicate assembly loaded. You need to restart the configuration tool.\r\n{0}\r\n{1}", codebase, normalizedPath));
			}
		}

		/// <summary>
		/// Unregisters the COM types in the specified assembly.
		/// </summary>
		public static List<System.Type> UnregisterComTypes(string filePath)
		{
			// load assmebly.
			Assembly assembly = Assembly.LoadFrom(filePath);

			// check that the correct assembly is being unregistered.
			VerifyCodebase(assembly, filePath);

			RegistrationServices services = new RegistrationServices();

			// list types to register/unregister.
			List<System.Type> types = new List<System.Type>(services.GetRegistrableTypesInAssembly(assembly));

			// unregister types.	
			if (!services.UnregisterAssembly(assembly))
			{
				throw new ApplicationException("Unregister COM Types Failed.");
			}

			return types;
		}

		/// <summary>
		/// Returns the location of the COM server executable.
		/// </summary>
		public static string GetExecutablePath(Guid clsid)
		{
			RegistryKey key = Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\LocalServer32", clsid));

			if (key == null)
			{
				key	= Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\InprocServer32", clsid));
			}

			if (key != null)
			{
				try
				{
					string codebase = key.GetValue("Codebase") as string;

					if (codebase == null)
					{
						return key.GetValue(null) as string;
					}

					return codebase;
				}
				finally
				{
					key.Close();
				}
			}

			return null;
		}

		/// <summary>
		/// Returns the prog id from the clsid.
		/// </summary>
		public static string ProgIDFromCLSID(Guid clsid)
		{
			RegistryKey key = Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\ProgId", clsid));
					
			if (key != null)
			{
				try
				{
					return key.GetValue("") as string;
				}
				finally
				{
					key.Close();
				}
			}

			return null;
		}

		/// <summary>
		/// Returns the prog id from the clsid.
		/// </summary>
		public static Guid CLSIDFromProgID(string progID)
		{
			if (String.IsNullOrEmpty(progID))
			{
				return Guid.Empty;
			}

			RegistryKey key = Registry.ClassesRoot.OpenSubKey(String.Format(@"{0}\CLSID", progID));
					
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

		///// <summary>
		///// Fetches the classes in the specified category.
		///// </summary>
		//public static List<Guid> EnumClassesInCategory(Guid category)
		//{
  //          return OpcRcw.Utils.EnumClassesInCategories(category);
		//}

		/// <summary>
		/// Registers the classes in the specified category.
		/// </summary>
		public static void RegisterClassInCategory(Guid clsid, Guid catid)
		{
			RegisterClassInCategory(clsid, catid, null);
		}

		/// <summary>
		/// Registers the classes in the specified category.
		/// </summary>
		public static void RegisterClassInCategory(Guid clsid, Guid catid, string description)
		{
			ICatRegister manager = (ICatRegister)CreateInstance(CLSID_StdComponentCategoriesMgr);
	
			try
			{
				string exsistingDescription = null;

				try
				{
					((ICatInformation)manager).GetCategoryDesc(catid, 0, out exsistingDescription);
				}
				catch (Exception e)
				{
					exsistingDescription = description;

					if (String.IsNullOrEmpty(exsistingDescription))
					{
						if (catid == typeof(OpcRcw.Da.CATID_OPCDAServer30).GUID)
						{
							exsistingDescription = OpcRcw.Da.Constants.OPC_CATEGORY_DESCRIPTION_DA30;
						}

						if (catid == typeof(OpcRcw.Da.CATID_OPCDAServer20).GUID)
						{
							exsistingDescription = OpcRcw.Da.Constants.OPC_CATEGORY_DESCRIPTION_DA20;
						}
						else
						{
							throw new ApplicationException("No description for category available", e);
						}
					}

					CATEGORYINFO info;

					info.catid         = catid;
					info.lcid          = 0;
					info.szDescription = exsistingDescription;

					// register category.
					manager.RegisterCategories(1, new CATEGORYINFO[] { info });
				}

				// register class in category.
				manager.RegisterClassImplCategories(clsid, 1, new Guid[] { catid });
			}
			finally
			{
				ReleaseServer(manager);
			}
		}

        /// <summary>
        /// Removes the registration for a COM server from the registry.
        /// </summary>
        public static void UnregisterComServer(Guid clsid)
        {
			// unregister class in categories.
			string categoriesKey = String.Format(@"CLSID\{{{0}}}\Implemented Categories", clsid);
			
			RegistryKey key = Registry.ClassesRoot.OpenSubKey(categoriesKey);

			if (key != null)
			{
				try	  
				{ 
					foreach (string catid in key.GetSubKeyNames())
					{
						try	  
						{ 
							ConfigUtils.UnregisterClassInCategory(clsid, new Guid(catid.Substring(1, catid.Length-2)));
						}
						catch (Exception)
						{
							// ignore errors.
						}
					}
				}
				finally
				{
					key.Close();
				}
			}

			string progidKey = String.Format(@"CLSID\{{{0}}}\ProgId", clsid);

			// delete prog id.
			key = Registry.ClassesRoot.OpenSubKey(progidKey);
					
			if (key != null)
			{
				string progId = key.GetValue(null) as string;
				key.Close();

				if (!String.IsNullOrEmpty(progId))
				{
					try	  
					{ 
						Registry.ClassesRoot.DeleteSubKeyTree(progId); 
					}
					catch (Exception)
					{
						// ignore errors.
					}
				}
			}

			// delete clsid.
			try	  
			{ 
				Registry.ClassesRoot.DeleteSubKeyTree(String.Format(@"CLSID\{{{0}}}", clsid)); 
			}
			catch (Exception)
			{
				// ignore errors.
			}
        }

		/// <summary>
		/// Unregisters the classes in the specified category.
		/// </summary>
		public static void UnregisterClassInCategory(Guid clsid, Guid catid)
		{
			ICatRegister manager = (ICatRegister)CreateInstance(CLSID_StdComponentCategoriesMgr);
	
			try
			{
				manager.UnRegisterClassImplCategories(clsid, 1, new Guid[] { catid });
			}
			finally
			{
				ReleaseServer(manager);
			}
		}

        /// <summary>
        /// Returns the implemented categories for the class.
        /// </summary>
        public static List<Guid> GetImplementedCategories(Guid clsid)
        {
            List<Guid> categories = new List<Guid>();

			string categoriesKey = String.Format(@"CLSID\{{{0}}}\Implemented Categories", clsid);
			
			RegistryKey key = Registry.ClassesRoot.OpenSubKey(categoriesKey);

			if (key != null)
			{
                try
                {
				    foreach (string catid in key.GetSubKeyNames())
				    {
                        try
                        {
                            Guid guid = new Guid(catid.Substring(1, catid.Length-2));
                            categories.Add(guid);
                        }
                        catch
                        {
                            // ignore invalid keys.
                        }
				    }
                }
                finally
                {
                    key.Close();
                }
			}

            return categories;
        }

		/// <summary>
		/// Displays the contents of an exception.
		/// </summary>
		public static void HandleException(string caption, MethodBase method, Exception e)
		{
			StringBuilder message = new StringBuilder();

			BuildMessage(message, e);

			MessageBox.Show(message.ToString(), caption);
		}

		/// <summary>
		/// Builds a message showing the exception trace.
		/// </summary>
		private static void BuildMessage(StringBuilder message, Exception e)
		{
			if (e.InnerException != null)
			{
				BuildMessage(message, e.InnerException);
			}

			if (message.Length > 0)
			{
				message.Append("\r\n\r\n");
			}

			message.Append(">>> ");
			message.Append(e.Message);
			
			if (e.StackTrace != null)
			{
				string[] trace = e.StackTrace.Split(new char[] {'\r','\n'});

				for (int ii = 0; ii < trace.Length; ii++)
				{
					if (trace[ii] != null && trace[ii].Length > 0)
					{
						message.AppendFormat("\r\n--- {0}", trace[ii]);
					}
				}
			}
		}
		#endregion

        #region General Utility Functions
        /// <summary>
        /// Return true if the array contains no elements.
        /// </summary>
        public static bool IsEmpty(Array array)
        {
            return (array == null || array.Length == 0);
        }
        #endregion
	}
}

