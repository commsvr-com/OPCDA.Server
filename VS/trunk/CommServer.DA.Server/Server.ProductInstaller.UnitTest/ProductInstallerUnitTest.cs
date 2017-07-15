
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Configuration.Install;
using System.Collections.Generic;
using System.Diagnostics;

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
        Installer[] _embeddedInstallers = new Installer[2];
        _customActions.Installers.CopyTo(_embeddedInstallers, 0);
        CollectionAssert.AllItemsAreNotNull(_embeddedInstallers);
        List<string> _names = _embeddedInstallers.Select<Installer, string>(x => x.ToString()).ToList<string>();
        CollectionAssert.AreEqual(_names, new string[]{ "CAS.Lib.CodeProtect.LibInstaller", "System.Diagnostics.EventLogInstaller" });
        EventLogInstaller _EventLogInstaller = _embeddedInstallers[1] as EventLogInstaller;
        Assert.IsNotNull(_EventLogInstaller);
        Assert.AreEqual<string>("CAS.RealTime.Core", _EventLogInstaller.Source);
        Assert.AreEqual<UninstallAction>(UninstallAction.Remove, _EventLogInstaller.UninstallAction);
      }
    }
  }
}
