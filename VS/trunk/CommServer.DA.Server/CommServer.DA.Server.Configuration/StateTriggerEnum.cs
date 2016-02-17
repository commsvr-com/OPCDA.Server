//<summary>
//  Title   : State Trigger Enum
//  System  : Microsoft Visual C# .NET
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20081006: mzbrzezny: created
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>


namespace CAS.NetworkConfigLib
{
  /// <summary>
  /// State Triggers Enum
  /// </summary>
  public enum StateTrigger: sbyte
  {
    /// <summary>
    /// None trigger
    /// </summary>
    None = 0,
    /// <summary>
    /// State high trigger
    /// </summary>
    StateHigh = 1,
    /// <summary>
    /// State low trigger
    /// </summary>
    StateLow = 2
  }
}
