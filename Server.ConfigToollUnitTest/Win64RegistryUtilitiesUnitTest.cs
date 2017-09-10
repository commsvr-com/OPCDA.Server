
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using System;

namespace CAS.CommServer.DA.Server.ConfigTool.UnitTest
{
  [TestClass]
  public class Win64RegistryUtilitiesUnitTest
  {
    [TestMethod]
    public void NETServerDaServerProgIDFromCLSIDTest()
    {
      Guid _testGuid = new Guid("{BE77A3C7-D2B7-44E7-B943-B978C1C87E5A}");
      Tuple<string, RegistryView> _result = _testGuid.ProgIDFromCLSID();
      Assert.IsNotNull(_result);
      Assert.AreEqual<string>("CAS.CommServer.DA.Server.NETServer.DaServer", _result.Item1);
      Assert.AreEqual<RegistryView>(RegistryView.Registry32, _result.Item2);
    }
    [TestMethod]
    public void ProgIDFromCLSIDTest()
    {
      Guid _testGuid = new Guid("{054bd34d-c8d6-4577-84ce-a8e19812df6a}");
      Tuple<string, RegistryView> _result = _testGuid.ProgIDFromCLSID();
      Assert.IsNotNull(_result);
      Assert.AreEqual<string>("ProgId not set", _result.Item1);
      Assert.AreEqual<RegistryView>(RegistryView.Registry64, _result.Item2);
    }
    [TestMethod]
    public void NETServerDaServerGetExecutablePath()
    {
      Guid _testGuid = new Guid("{BE77A3C7-D2B7-44E7-B943-B978C1C87E5A}");
      string  _result = _testGuid.GetExecutablePath();
      Assert.IsNotNull(_result);
      Assert.AreEqual<string>(@"C:\Program Files (x86)\CAS\CAS.CommServer\CAS.CommServer.DA.Server.NETServer.dll", _result);
    }

  }
}


