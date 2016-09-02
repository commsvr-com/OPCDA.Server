
using CAS.CommServer.ProtocolHub.Communication;
using CAS.Lib.CodeProtect;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests
{
  [TestClass()]
  [DeploymentItem("CAS.License.lic")]
  [DeploymentItem("DefaultConfig.xml")]
  public class AssemblyInitializeClass
  {
    internal static CommServerComponent CommServerComponent { get; private set; }
    private static string m_InstallLicenseError = string.Empty;

    [AssemblyInitialize()]
    public static void InstallLicense(TestContext testContext)
    {
      try
      {
        LibInstaller.InstalLicense(false);
        CommServerComponent = new CommServerComponent();
        CommServerComponent.Initialize("DefaultConfig.xml");
      }
      catch (System.Exception _ex)
      {
        m_InstallLicenseError = _ex.ToString();
        Debug.WriteLine(m_InstallLicenseError);
        throw;
      }
    }
    [AssemblyCleanup()]
    public static void MyClassTestCleanup()
    {
      CommServerComponent.Dispose();
    }
    [TestMethod]
    public void LicenseExist()
    {
      FileInfo _licenseFile = new FileInfo("DefaultConfig.xml");
      Assert.IsTrue(_licenseFile.Exists, "DefaultConfig.xml doesn't exist in the working directory");
    }
    [TestMethod]
    public void AssemblyInitializeTestMethod()
    {
      Assert.IsTrue(string.IsNullOrEmpty(m_InstallLicenseError), m_InstallLicenseError);
    }

  }
}
