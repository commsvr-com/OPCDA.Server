// <summary>
//  Title   : Configuration utility for ComServer application
//  Author  : Mariusz Postol
//  System  : Microsoft Visual C# .NET
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    Problems:
//      -- Zrobiæ sprawdzanie czy zosta³ zdefiniowany ptzynajmniej jeden serial, bo inaczej wywala CommServer w assercji
//         Moze generalnie przez proba zapisu nalezy sprawdziæ spojnoœæ danych konfiguracyjnnych i zablokowaæ klawis Save do chwili stwierdzenia poprawnoœci
//    Mariusz Postol 09-08-2004:
//      - Names w DataBloks zrobilem kluczem, bo w CommClient tworzone sa grupy OPC z wykorzystaniem tego pola i musza byc jednoznacze
//      - Dopasowalem do nowego ukladu biblioteki
//    Mariusz Postol 11-11-2003: Var 1.1
//     - import form WIZCON GLS and BLS was added
//    Mariusz Postol 10-11-2003: Ver 1.0. created and tested
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: 42' 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
// </summary>
//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using CAS;

[assembly: AssemblyTitle( "NetworkConfig" )]
[assembly: AssemblyDescription( CommServerAssemblyVersionInfo.DescriptionAdd + "Configuration tool for CommServer server." )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "CAS" )]
[assembly: AssemblyProduct( "CommServer" )]
[assembly: AssemblyCopyright( "Copyright (C)2008, CAS LODZ POLAND" )]
[assembly: AssemblyTrademark( "CommServer" )]
[assembly: AssemblyCulture( "" )]

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
[assembly: CAS.Lib.CodeProtect.AssemblyKey( "2D0C30B3-ED45-4292-8CB3-ADB0E739E03E" )]
[assembly: CAS.Lib.CodeProtect.AssemblyHelper
  (
  Product = "Configuration tool for CommServer.",
  Company = "CAS",
  Url = "www.cas.eu",
  Email = "techsupp@cas.eu",
  Phone = "+48(42)686 25 47"
  )
]
[assembly: ComVisibleAttribute( false )]
[assembly: NeutralResourcesLanguageAttribute( "en-US" )]
[assembly: GuidAttribute( "F919845E-627B-4b17-875A-09471880C266" )]
