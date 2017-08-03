
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.QualityTools.Testing.Fakes.Shims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Fakes;
using OpcRcw.Comn;
using CAS.CommServer.DA.Server.ConfigTool.ServersModel;

namespace CAS.CommServer.DA.Server.NETServer.UnitTest
{
  [TestClass]
  public class DaServerUnitTest
  {

    [TestMethod]
    public void DaServerTest()
    {
      DaServer _daServer = new DaServer();
      Assert.IsNotNull(_daServer);
      Assert.IsTrue(File.Exists("CommServer_Main.log"), $"Cannot find the file in {Environment.CurrentDirectory}");
      Assert.IsFalse(Environment.Is64BitProcess);
      RegistrationServices _registrationServices = new RegistrationServices();
      //GetProgIdForType
      string _progId = _registrationServices.GetProgIdForType(typeof(DaServer));
      Assert.AreEqual<string>("CAS.CommServer.DA.Server.NETServer.DaServer", _progId);
      //GetRegistrableTypesInAssembly
      Type[] _typesToRegister = _registrationServices.GetRegistrableTypesInAssembly(typeof(DaServer).Assembly);
      Assert.IsNotNull(_typesToRegister);
      Assert.AreEqual<int>(1, _typesToRegister.Length);
      Assert.AreEqual<string>("CAS.CommServer.DA.Server.NETServer.DaServer", _typesToRegister[0].FullName);
      //TypeRepresentsComType
      Assert.IsFalse(_registrationServices.TypeRepresentsComType(typeof(DaServer)));
      Assert.IsFalse(_registrationServices.TypeRepresentsComType(typeof(IOPCCommon)));
      Assert.IsFalse(_registrationServices.TypeRepresentsComType(typeof(IOPCCommon)));
      Assert.IsFalse(_registrationServices.TypeRepresentsComType(typeof(IOPCWrappedServer)));
      //TypeRequiresRegistration
      Assert.IsTrue(_registrationServices.TypeRequiresRegistration(typeof(DaServer)));
      object[] _attributes =  typeof(DaServer).GetCustomAttributes(typeof(GuidAttribute), false);
      Assert.AreEqual<int>(1, _attributes.Length);
      Assert.AreEqual<string>("BE77A3C7-D2B7-44E7-B943-B978C1C87E5A", ((GuidAttribute)_attributes[0]).Value.ToUpper());
    }
    //[TestMethod]
    //public void RegisterTypeTest()
    //{
    //  using (IDisposable context  =  ShimsContext.Create())
    //  {
    //    List<string> _keyNames = new List<string>();
    //    //Microsoft.Win32.Shim
    //    Microsoft.Win32.Fakes.ShimRegistryKey.AllInstances.CreateSubKeyString = (x, y) => { _keyNames.Add(y); return x; };
    //    RegistrationServices _registrationServices = new RegistrationServices();
    //    Guid _registrationGuid = Guid.NewGuid();
    //    _registrationServices.RegisterTypeForComClients(typeof(DaServer), ref _registrationGuid);
    //    ShimBehaviors.Current = ShimBehaviors.DefaultValue;
    //  } // clear all shims  
    //}

  }
}
