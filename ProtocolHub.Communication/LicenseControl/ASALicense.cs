//_______________________________________________________________
//  Title   : Name of Application
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

using CAS.Lib.CodeProtect;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CAS.CommServer.ProtocolHub.Communication.LicenseControl
{
  /// <summary>
  /// Adaptive Scanning Algorithm license access helper
  /// </summary>
  [LicenseProvider(typeof(CodeProtectLP))]
  [GuidAttribute("F3C086DE-30EC-426d-B507-8114074A9840")]
  public class ASALicense : IsLicensed<ASALicense>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ASALicense"/> class.
    /// </summary>
    internal ASALicense() : base(0, TimeSpan.Zero) { }
  }
}
