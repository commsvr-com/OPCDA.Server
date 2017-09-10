
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using System;

namespace CAS.CommServer.DA.Server.ConfigTool.UnitTest
{
  /// <summary>
  /// Summary description for SoftwareClassesRegistryKeyUnitTest
  /// </summary>
  [TestClass]
  public class SoftwareClassesRegistryKeyUnitTest
  {
    public SoftwareClassesRegistryKeyUnitTest()
    {
      //
      // TODO: Add constructor logic here
      //
    }

    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    #region Additional test attributes
    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion

    [TestMethod]
    public void ConstructorTestMethod()
    {
      Guid _testGuid = new Guid("{BE77A3C7-D2B7-44E7-B943-B978C1C87E5A}");
      using (SoftwareClassesRegistryKey _softwareDescription = new SoftwareClassesRegistryKey(_testGuid))
      {
        Assert.AreEqual<string>(@"Software\Classes\CLSID\{be77a3c7-d2b7-44e7-b943-b978c1c87e5a}", _softwareDescription.KeyName);
        Assert.AreEqual<RegistryView>(RegistryView.Registry32, _softwareDescription.RegistryView);
        Tuple<string, SoftwareClassesRegistryKey.ServerType> _executablePath = _softwareDescription.GetExecutablePath();
        Assert.IsNotNull(_executablePath);
        Assert.AreEqual<string>(@"C:\Program Files (x86)\CAS\CAS.CommServer\CAS.CommServer.DA.Server.NETServer.dll", _executablePath.Item1);
        Assert.AreEqual<SoftwareClassesRegistryKey.ServerType>(SoftwareClassesRegistryKey.ServerType.InprocServer32, _executablePath.Item2);
      }
    }
  }
}


