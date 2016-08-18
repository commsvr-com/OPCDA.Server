//<summary>
//  Title   : Interface of the Segment Statistics
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    MPostol creatyed 
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
using System;
namespace CAS.Lib.CommServerConsoleInterface
{
  /// <summary>
  /// Interface of the Segment Statistics
  /// </summary>
  public interface ISegmentStatistics: IInterface2SegmentLink
  {
    /// <summary>
    /// Marks the Segment connection failure.
    /// </summary>
    void MarkConnFail();
    /// <summary>
    /// Sets the new state.
    /// </summary>
    /// <value>The new state.</value>
    BaseStation.Management.Statistics.SegmentStatistics.States NewState { set; }
    /// <summary>
    /// Sets the overtime coefficients.
    /// </summary>
    /// <param name="min">The min. value</param>
    /// <param name="max">The max. value</param>
    /// <param name="avr">The average value</param>
    void SetOvertimeCoefficient( long min, long max, long avr );
  }
}
