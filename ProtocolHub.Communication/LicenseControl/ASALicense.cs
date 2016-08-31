//<summary>
//  Title   : Adaptive Scanning Algorithm license access helper
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using CAS.Lib.CodeProtect;

namespace CAS.Lib.CommServer.LicenseControl
{
  /// <summary>
  /// Adaptive Scanning Algorithm license access helper
  /// </summary>
  [LicenseProvider(typeof(CodeProtectLP))]
  [GuidAttribute("F3C086DE-30EC-426d-B507-8114074A9840")]
  public class ASALicense : IsLicensed<ASALicense>
  {
    internal ASALicense() : base(0, TimeSpan.Zero) { }
  }
}
