//<summary>
//  Title   : CommServer Assembly info
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    MPostol - 19-03-2007: Licensing added
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Resources;
using CAS;

[assembly: AssemblyTitle( "CAS.CommServer" )]
[assembly: AssemblyDescription( CommServerAssemblyVersionInfo.DescriptionAdd + "Redundant, multi-protocol, multi-channel OPC server for large scale distributed systems." )]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("CAS")]
[assembly: AssemblyProduct( "CommServer OPC DA Server" )]
[assembly: AssemblyCopyright( "Copyright (C) 2008, CAS LODZ POLAND." )]
[assembly: AssemblyTrademark( "CommServer" )]
[assembly: AssemblyCulture("")]
// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("11b3c9ce-9001-4aaa-bab1-407e3d74dc50")]
[assembly: AssemblyVersion( CommServerAssemblyVersionInfo.CurrentVersion )]
[assembly: AssemblyFileVersionAttribute( CommServerAssemblyVersionInfo.CurrentFileVersion )]
[assembly: CAS.Lib.CodeProtect.AssemblyKey( "2D0C30B3-ED45-4292-8CB3-ADB0E739E03E" )]
[assembly: NeutralResourcesLanguageAttribute( "en-US" )]
[assembly: CAS.Lib.CodeProtect.AssemblyHelper
  (
  Product = "CAS.CommServer",
  Company = "CAS",
  Url = "www.commsvr.com",
  Email = "techsupp@cas.eu",
  Phone = "+48(42)686 25 47"
  )
]
