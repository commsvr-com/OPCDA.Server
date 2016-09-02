//_______________________________________________________________
//  Title   : Custom installer for CommServer product.
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C) 2016, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;

namespace CAS.CommServer.ProtocolHub.Communication
{
  /// <summary>
  /// Custom installer for CommServer product.
  /// </summary>
  [RunInstaller( true )]
  public partial class CommServerInstaller: Installer
  {
    /// <summary>
    /// CommServer installer
    /// </summary>
    public CommServerInstaller()
    {
      InitializeComponent();
      EventLogInstaller eli = new System.Diagnostics.EventLogInstaller();
      eli.Source = CommServerComponent.Source;
      this.Installers.Add( eli );
    }
  }
}