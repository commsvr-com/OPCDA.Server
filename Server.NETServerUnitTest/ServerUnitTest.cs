
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CAS.CommServer.DA.Server.NETServer.UnitTest
{
  [TestClass]
  public class ServerUnitTest
  {

    [TestMethod]
    public void ConstructorTestMethod()
    {
     int _serverShutdownCount = 0;
      using (CAS.OpcSvr.Da.NETServer.Server _serverInstance = new OpcSvr.Da.NETServer.Server(true))
      {
        _serverInstance.ServerShutdown += x => _serverShutdownCount++;
      };
      Assert.AreEqual<int>(0, _serverShutdownCount);
    }
  }
}
