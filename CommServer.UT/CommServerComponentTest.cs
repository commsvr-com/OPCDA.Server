//<summary>
//  Title   : Unit tests for CommServerComponent constructor
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

using CAS.Lib.CommServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests
{
  /// <summary>
  ///This is a test class for <see cref="CommServerComponent"/>and is intended
  ///to contain all CommServerComponentTest Unit Tests
  ///</summary>
  [TestClass()]
  public class CommServerComponentTest
  {
    private TestContext testContextInstance;
    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get { return testContextInstance; }
      set { testContextInstance = value; }
    }
    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    [ClassInitialize()]
    public static void MyClassInitialize( TestContext testContext )
    {
      CAS.Lib.CodeProtect.LibInstaller.InstalLicense();
    }
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //
    /// <summary>
    /// Use TestInitialize to run code before running each test
    /// </summary>
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //  CAS.Lib.CodeProtect.LibInstaller.InstalLicense();
    //}
    //
    //Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion
    /// <summary>
    ///A test for <see cref="CommServerComponent"/> Constructor
    ///</summary>
    [TestMethod()]
    [
    DeploymentItem( "..\\PR21-CommServer\\CommServer.UT\\DefaultConfig.xml " ),
    DeploymentItem( "..\\PR21-CommServer\\CommServer.UT\\item_dsc.xml" ),
    DeploymentItem( "..\\PR21-CommServer\\CommServer.UT\\CAS.License.lic" ),
    DeploymentItem( "..\\PR21-CommServer\\bin\\Debug\\CAS.CommSvrPlugin_EC2-3SYM2.dll" ),
    DeploymentItem( "..\\PR21-CommServer\\bin\\Debug\\CAS.CommSvrPlugin_MBUS.dll" ),
    DeploymentItem( "..\\PR21-CommServer\\bin\\Debug\\CAS.CommSvrPlugin_MODBUS.dll" ),
    DeploymentItem( "..\\PR21-CommServer\\bin\\Debug\\CAS.CommSvrPlugin_MODBUSNet.dll" ),
    DeploymentItem( "..\\PR21-CommServer\\bin\\Debug\\CAS.CommSvrPlugin_NULLbus.dll" ),
    DeploymentItem( "..\\PR21-CommServer\\bin\\Debug\\CAS.CommSvrPlugin_SBUSNET.dll" ),
    DeploymentItem( "..\\PR21-CommServer\\bin\\Debug\\CAS.CommSvrPlugin_SBUSRS.dll" ),
    DeploymentItem( "..\\PR21-CommServer\\bin\\Debug\\CAS.CommSvrPlugin_SymSiec.dll" ),
    DeploymentItem( "..\\PR21-CommServer\\bin\\Debug\\CAS.CommSvrPlugin_DemoSimulator.dll" )
    ]
    public void CommServerComponentConstructorTest()
    {
      using ( CommServerComponent target = new CommServerComponent() )
      { target.Initialize("DefaultConfig.xml" );}
    }
  }
}
