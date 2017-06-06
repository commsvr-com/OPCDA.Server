//<summary>
//  Title   : Main installer for NetworkConfig and CommServer
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20080917: mzbrzezny: created
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System.ComponentModel;
using System.Configuration.Install;

namespace NetworkConfig
{
  /// <summary>
  /// Main installer for NetworkConfig and CommServer
  /// this class installs:
  /// - licence in CodeProtect installer
  /// - CommServer as eventlog source
  /// </summary>
  [RunInstaller( true )]
  public partial class NetworConfigCommServerInstaller: Installer
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NetworConfigCommServerInstaller"/> class.
    /// </summary>
    public NetworConfigCommServerInstaller()
    {
      InitializeComponent();
    }
  }
}
