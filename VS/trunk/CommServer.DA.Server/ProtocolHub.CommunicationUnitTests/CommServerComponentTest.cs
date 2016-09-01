//<summary>
//  Title   : Unit tests for CommServerComponent constructor
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

using CAS.CommServer.ProtocolHub.Communication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests
{
  [TestClass()]
  public class CommServerComponentTest
  {
    /// <summary>
    ///A test for <see cref="CommServerComponent"/> Constructor - because it was created in assembly initialize class must throw an exception.
    ///</summary>
    [TestMethod()]
    [ExpectedException(typeof(Exception))]
    public void CommServerComponentConstructorTest()
    {
      using (CommServerComponent target = new CommServerComponent())
      { target.Initialize("DefaultConfig.xml"); }
    }
  }
}
