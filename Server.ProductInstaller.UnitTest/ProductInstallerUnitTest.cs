
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Configuration.Install;
using System.Collections.Generic;

namespace CAS.CommServer.DA.Server.ProductInstaller.UnitTest
{
  [TestClass]
  public class ProductInstallerUnitTest
  {
    [TestMethod]
    public void InstallerCustomActionsTest()
    {
      using (InstallerCustomActions _customActions = new InstallerCustomActions())
      {
        Installer[] _embededInstallers = new Installer[2];
        _customActions.Installers.CopyTo(_embededInstallers, 0);
        CollectionAssert.AllItemsAreNotNull(_embededInstallers);
        List<string> names = _embededInstallers.Select<Installer, string>(x => x.ToString()).ToList<string>();
        CollectionAssert.AreEqual(names, new string[]{ "CAS.Lib.CodeProtect.LibInstaller", "CAS.CommServer.ProtocolHub.Communication.CommServerInstaller" });
      }
    }
  }
}
