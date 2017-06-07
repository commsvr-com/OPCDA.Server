using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAS.OpcSvr.Da.NETServer;

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

    }
  }
}
