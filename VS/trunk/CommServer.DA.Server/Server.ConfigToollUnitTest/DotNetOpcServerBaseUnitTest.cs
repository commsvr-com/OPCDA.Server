using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAS.CommServer.DA.Server.ConfigTool.ServersModel;

namespace CAS.CommServer.DA.Server.ConfigTool.UnitTest
{
  [TestClass]
  public class DotNetOpcServerBaseUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      TestDotNetOpcServerBase _server = new TestDotNetOpcServerBase();
      Assert.AreEqual<Guid>(Guid.Empty, _server.CLSID);
      Assert.AreEqual<string>(String.Empty, _server.ProgId);
    }
    [TestMethod]
    public void ConstructorCLSIDCommServerTest()
    {
      Guid _clsid = new Guid("BE77A3C7-D2B7-44E7-B943-B978C1C87E5A");
      TestDotNetOpcServerBase _server = new TestDotNetOpcServerBase(_clsid);
      Assert.AreEqual<Guid>(_clsid, _server.CLSID);
      Assert.AreEqual<string>("CAS.CommServer.DA.Server.NETServer.DaServer", _server.ProgId);
      Assert.IsFalse(_server.Is64BitComponent);
    }
    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void ConstructorCLSIDWrapperTest()
    {
      Guid _clsid = new Guid("B41C9D1F-28AC-41cb-9DCD-CEBE1FC86210");
      TestDotNetOpcServerBase _server = new TestDotNetOpcServerBase(_clsid);
    }
    [TestMethod]
    public void ConstructorCLSIDServerTest()
    {
      Guid _clsid = new Guid("2032FE45-C774-46d7-9AA3-B844E8658919");
      TestDotNetOpcServerBase _server = new TestDotNetOpcServerBase(_clsid);
      Assert.AreEqual<Guid>(_clsid, _server.CLSID);
      Assert.AreEqual<string>("CAS.CommServer.OPC.Da.Server.1", _server.ProgId);
      Assert.IsFalse(_server.Is64BitComponent);
    }
    private class TestDotNetOpcServerBase : DotNetOpcServerBase
    {
      public TestDotNetOpcServerBase() { }
      public TestDotNetOpcServerBase(Guid clsid) : base(clsid) { }
    }
  }
}
