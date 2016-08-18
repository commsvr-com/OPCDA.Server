//<summary>
//  Title   : Facade impmlementation of CAS.Lib.CommonBus.ApplicationLayer.IBlockDescription
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    200709 mpostol - created
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
using System.Text;
namespace CAS.Lib.CommServer.Tests
{
  /// <summary>
  /// Facade impmlementation of CAS.Lib.CommonBus.ApplicationLayer.IBlockDescription
  /// </summary>
  class FacadeBlockDescription: CAS.Lib.CommonBus.ApplicationLayer.IBlockDescription
  {
    #region private
    private int myStartAddress;
    private int myLength;
    private short myDataType;
    #endregion
    #region IBlockDescription Members
    public int startAddress
    {
      get { return myStartAddress; }
    }
    public int length
    {
      get { return myLength; }
    }
    public short dataType
    {
      get { return myDataType; }
    }
    #endregion
    #region constructor
    public FacadeBlockDescription( int startAddress, int length, short dataType )
    {
      myStartAddress = startAddress;
      myLength = length;
      myDataType = dataType;
    }
    #endregion
  }
}
