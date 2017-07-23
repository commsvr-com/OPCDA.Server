using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;

namespace CAS.CommServer.DA.Server.NETServer.UnitTest
{
  [TestClass]
  public class DaServerUnitTest
  {

    [TestMethod]
    public void DaServerTest()
    {
      DaServer _daserver = new DaServer();
      Assert.IsNotNull(_daserver);
      Assert.IsTrue(File.Exists("CommServer_Main.log"), $"Cannot find the file in {Environment.CurrentDirectory}" );
    }
  }
}
