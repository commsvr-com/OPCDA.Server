//<summary>
//  Title   : AssemblyInfo of CASCommServerOpcNETDa implementation 
//  System  : Microsoft Visual C# .NET
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

//============================================================================
// TITLE: AssemblyInfo.cs
//
// CONTENTS:
// 
// The assembly information file for the OPC .NET API sample client.
//
// (c) Copyright 2003 The OPC Foundation
// ALL RIGHTS RESERVED.
//
// DISCLAIMER:
//  This code is provided by the OPC Foundation solely to assist in 
//  understanding and use of the appropriate OPC Specification(s) and may be 
//  used as set forth in the License Grant section of the OPC Specification.
//  This code is provided as-is and without warranty or support of any sort
//  and is subject to the Warranty and Liability Disclaimers which appear
//  in the printed OPC Specification.
//
// MODIFICATION LOG:
//
// Date       By    Notes
// ---------- ---   -----
// 2004/03/24 RSA   Initial implementation.

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using CAS;

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
//#if COMMSERVER
[assembly: AssemblyTitle("CASCommServerOpcNETDa")]
[assembly: AssemblyDescription( CommServerAssemblyVersionInfo.DescriptionAdd + "CAS CommServer: redundant, multi-protocol, optimal OPC Da SERVER" )]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("CAS")]
[assembly: AssemblyProduct("CASCommServerOpcNETDa")]
[assembly: AssemblyCopyright("Copyright © 2008 CAS")]
[assembly: AssemblyTrademark("CAS")]
[assembly: AssemblyCulture("")]		

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion( CommServerAssemblyVersionInfo.CurrentVersion )]
[assembly: AssemblyFileVersionAttribute( CommServerAssemblyVersionInfo.CurrentFileVersion )]
//#endif
//#if SNIFFER
//[assembly: AssemblyTitle("CASBusSnifferOpcNETDa")]
//[assembly: AssemblyDescription("CAS BusSniffer: pasive OPC Da Server")]
//[assembly: AssemblyConfiguration("")]
//[assembly: AssemblyCompany("CAS")]
//[assembly: AssemblyProduct("CASBusSnifferOpcNETDa")]
//[assembly: AssemblyCopyright("Copyright © 2005 CAS")]
//[assembly: AssemblyTrademark("CAS")]
//[assembly: AssemblyCulture("")]		

////
//// Version information for an assembly consists of the following four values:
////
////      Major Version
////      Minor Version 
////      Build Number
////      Revision
////
//// You can specify all the values or you can default the Revision and Build Numbers 
//// by using the '*' as shown below:

//[assembly: AssemblyVersion("1.0.0.0")]
//#endif
//
// In order to sign your assembly you must specify a key to use. Refer to the 
// Microsoft .NET Framework documentation for more information on assembly signing.
//
// Use the attributes below to control which key is used for signing. 
//
// Notes: 
//   (*) If no key is specified, the assembly is not signed.
//   (*) KeyName refers to a key that has been installed in the Crypto Service
//       Provider (CSP) on your machine. KeyFile refers to a file which contains
//       a key.
//   (*) If the KeyFile and the KeyName values are both specified, the 
//       following processing occurs:
//       (1) If the KeyName can be found in the CSP, that key is used.
//       (2) If the KeyName does not exist and the KeyFile does exist, the key 
//           in the KeyFile is installed into the CSP and used.
//   (*) In order to create a KeyFile, you can use the sn.exe (Strong Name) utility.
//       When specifying the KeyFile, the location of the KeyFile should be
//       relative to the project output directory which is
//       %Project Directory%\obj\<configuration>. For example, if your KeyFile is
//       located in the project directory, you would specify the AssemblyKeyFile 
//       attribute as [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
//   (*) Delay Signing is an advanced option - see the Microsoft .NET Framework
//       documentation for more information on this.
//
[assembly: SecurityPermissionAttribute(SecurityAction.RequestMinimum, SerializationFormatter = true)]


[assembly: ComVisibleAttribute(true)]
[assembly: GuidAttribute( "41E9B462-5C59-455a-909D-7C38A54DAC9A" )]
