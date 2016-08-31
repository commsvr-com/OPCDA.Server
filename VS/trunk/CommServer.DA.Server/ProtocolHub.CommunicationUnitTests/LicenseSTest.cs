//<summary>
//  Title   : This is a test class for OTALicenseTest and is intended to contain all OTALicenseTest Unit Tests
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using CAS.CommServer.ProtocolHub.CommunicationUnitTests.Instrumentation;
using CAS.Lib.CodeProtect;
using CAS.Lib.CommServer;
using CAS.Lib.CommServer.LicenseControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests
{
  /// <summary>
  ///This is a test class for OTALicenseTest and is intended
  ///to contain all OTALicenseTest Unit Tests
  ///</summary>
  [TestClass()]
  [DeploymentItem("CAS.License.lic")]
  [DeploymentItem("DefaultConfig.xml")]
  public class LicensesTest
  {
    private TestContext testContextInstance;
    private static CommServerComponent cs;
    private static string m_InstallLicenseError = string.Empty;
    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get { return testContextInstance; }
      set { testContextInstance = value; }
    }
    [AssemblyInitialize()]
    public static void InstallLicense(TestContext testContext)
    {
      try
      {
        LibInstaller.InstalLicense(false);
      }
      catch (System.Exception _ex)
      {
        m_InstallLicenseError = _ex.ToString();
        throw;
      }
    }
    [ClassInitialize()]
    public static void MyClassInitialize(TestContext testContext)
    {
      cs = new CommServerComponent();
      cs.Initialize("DefaultConfig.xml");
    }
    [ClassCleanup()]
    public static void MyClassTestCleanup()
    {
      cs.Dispose();
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
    /// <summary>
    ///A test for OTALicense Constructor
    ///</summary>
    [TestMethod()]
    public void OTALicenseConstructorTest()
    {
      OTALicense _ota = OTALicense.License;
      Assert.IsNotNull(_ota);
      Assert.IsTrue(OTALicense.License.Licensed, "License error - see log");
    }
    [TestMethod()]
    [ExpectedException(typeof(System.ComponentModel.LicenseException))]
    public void MultichannelTest()
    {
      Assert.AreEqual(Multichannel.License.Volumen, 5);
      Assert.AreEqual(Multichannel.License.RunTime.Value.TotalHours, 0);
      for (int i = 0; i < 5; i++)
        Multichannel.NextChannnel();
      Multichannel.NextChannnel();
    }
    [TestMethod()]
    [ExpectedException(typeof(System.ComponentModel.LicenseException))]
    public void RedundancyTest()
    {
      Assert.IsTrue(Redundancy.License.Licensed);
      Assert.AreEqual(Redundancy.License.Volumen, 2);
      Redundancy.CheckIfAllowed(2, "test name");
    }
    [TestMethod()]
    public void ASALicenseTest()
    {
      string _message = string.Empty;
      FacadeASALicense.m_Logger = (x) => _message = x;
      ASALicense _ASALicense = new FacadeASALicense();
      Assert.IsTrue(_ASALicense.Licensed, _message);
      Assert.IsTrue(_message.Contains("ASALicense"), _message);
      Assert.IsNull(_ASALicense.Warning, string.Join(", ", _ASALicense.Warning));
    }
  }
}
