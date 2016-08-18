//<summary>
//  Title   : Custom installer for CommServer product.
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Reflection;
namespace CAS.Lib.CommServer
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