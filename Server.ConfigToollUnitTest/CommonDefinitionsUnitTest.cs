
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;

namespace CAS.CommServer.DA.Server.ConfigTool.UnitTest
{
  [TestClass]
  public class CommonDefinitionsUnitTest
  {

    [TestMethod]
    public void CATID_DotNetOpcServersTestMethod()
    {
      RegistrationServices _registrationServices = new RegistrationServices();
      Assert.AreEqual<Guid>(_registrationServices.GetManagedCategoryGuid(), CommonDefinitions.CATID_RegisteredDotNetOpcServers );
    }

  }
}

