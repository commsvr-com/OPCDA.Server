//_______________________________________________________________
//  Title   : ProductInstaller - common entry point for installer custom actions.
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C) 2017, CAS LODZ POLAND.
//  TEL: +48 608 61 98 99 
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using System.ComponentModel;

namespace CAS.CommServer.DA.Server.ProductInstaller
{
  /// <summary>
  /// Main installer for NetworkConfig and CommServer
  /// this class installs:
  /// - licence in CodeProtect installer
  /// - CommServer as eventlog source
  /// </summary>
  [RunInstaller( true )]
  public partial class InstallerCustomActions: System.Configuration.Install.Installer
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallerCustomActions"/> class.
    /// </summary>
    public InstallerCustomActions()
    {
      InitializeComponent();
    }
  }
}
