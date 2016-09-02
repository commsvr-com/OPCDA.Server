//_______________________________________________________________
//  Title   : FacadeBlockDescription
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

using CAS.Lib.CommonBus.ApplicationLayer;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests.Instrumentation
{
  /// <summary>
  /// Facade implementation of CAS.Lib.CommonBus.ApplicationLayer.IBlockDescription
  /// </summary>
  internal class FacadeBlockDescription: IBlockDescription
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
